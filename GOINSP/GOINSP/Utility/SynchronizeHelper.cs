using System;
using Microsoft.Synchronization;
using System.Data.SqlClient;
using Microsoft.Synchronization.Data.SqlServer;
using Microsoft.Synchronization.Data;
using System.Configuration;
using System.Data;

namespace GOINSP.Utility
{
    class SynchronizeHelper
    {
        private SqlConnection clientConn;
        private SqlConnection serverConn;
        private DbSyncScopeDescription scopeDesc;

        public void setConnection()
        {
            clientConn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalContext"].ConnectionString);

            // create a connection to the server database
            serverConn = new SqlConnection(ConfigurationManager.ConnectionStrings["RemoteContext"].ConnectionString);
        }

        public void SetClient()
        {
            Console.WriteLine("Setting client");

            // get the description of SyncScope from the server database
            DbSyncScopeDescription scopeDesc = SqlSyncDescriptionBuilder.GetDescriptionForScope("GOINSPSyncScope", serverConn);

            // create server provisioning object based on the SyncScope
            SqlSyncScopeProvisioning clientProvision = new SqlSyncScopeProvisioning(clientConn, scopeDesc);


            // starts the provisioning process
            clientProvision.Apply();

            Console.WriteLine("Client Successfully Provisioned.");
            Console.ReadLine();
        }

        private void setScopes() {
            Console.WriteLine("Setting scopes");
            // define a new scope named MySyncScope
            scopeDesc = new DbSyncScopeDescription("GOINSPSyncScope");

            // get the description of the CUSTOMER & PRODUCT table from SERVER database
            DbSyncTableDescription accountsTableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("Accounts", serverConn);
            DbSyncTableDescription companiesTableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("Companies", serverConn);
            DbSyncTableDescription inspectionTableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("Inspection", serverConn);
            DbSyncTableDescription postCodeDatasTableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("PostCodeDatas", serverConn);
            DbSyncTableDescription questionBasesTableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("QuestionBases", serverConn);
            DbSyncTableDescription questionairesTableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("Questionnaires", serverConn);
            DbSyncTableDescription regiosTableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("RegioS", serverConn);
            DbSyncTableDescription tdatasTableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("TDatas", serverConn);

            // add the table description to the sync scope definition
            scopeDesc.Tables.Add(accountsTableDesc);
            scopeDesc.Tables.Add(companiesTableDesc);
            scopeDesc.Tables.Add(inspectionTableDesc);
            scopeDesc.Tables.Add(postCodeDatasTableDesc);
            scopeDesc.Tables.Add(questionBasesTableDesc);
            scopeDesc.Tables.Add(questionairesTableDesc);
            scopeDesc.Tables.Add(regiosTableDesc);
            scopeDesc.Tables.Add(tdatasTableDesc);

        }

        public void setServer()
        {
            Console.WriteLine("Setting server");
            // create a server scope provisioning object based on the MySyncScope
            SqlSyncScopeProvisioning serverProvision = new SqlSyncScopeProvisioning(serverConn, scopeDesc);

            // skipping the creation of table since table already exists on server
            serverProvision.SetCreateTableDefault(DbSyncCreationOption.CreateOrUseExisting);

            // start the provisioning process
            serverProvision.Apply();

            Console.WriteLine("Server Successfully Provisioned.");
            Console.ReadLine();
        }

        private void deprovisionRemote()
        {
            // Remove the retail customer scope from the Sql Server client database.
            SqlSyncScopeDeprovisioning serverDepro = new SqlSyncScopeDeprovisioning(serverConn);

            // Remove the scope.
            try
            {
                Console.WriteLine("Deprovision server");
                serverDepro.DeprovisionScope("GOINSPSyncScope");
                Console.WriteLine("Server deprovisioned");
            }
            catch (Exception e) {
                Console.WriteLine("Something went wrong");
            }
        }

        private void deprovisionLocal()
        {
            SqlSyncScopeDeprovisioning clientDepro = new SqlSyncScopeDeprovisioning(clientConn);

            try
            {
                Console.WriteLine("Deprovision client");
                clientDepro.DeprovisionScope("GOINSPSyncScope");
                Console.WriteLine("client deprovisioned");
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong");
            }
        }

        public void work()
        {
            Console.WriteLine("Setting connection");
            this.setConnection();
            Console.WriteLine("Starting the sync method");
            this.sync();

        }

        public void reprovision()
        {
            this.setConnection();
            this.setScopes();
            this.deprovisionLocal();
            this.deprovisionRemote();
            this.setServer();
            this.SetClient();
        }

        public void reprovisionLocal()
        {
            this.setConnection();
            this.setScopes();
            this.deprovisionLocal();
            this.SetClient();
        }

        private void sync()
        {
            try
            {
                serverConn.Open();
            }
            catch (Exception)
            {
                Console.WriteLine("Can't connect");
            }
            if (serverConn.State == ConnectionState.Open)
            {
                serverConn.Close();
            // subscribe for errors that occur when applying changes to the client
            try {
                Console.WriteLine("Set new orchestrator");
                // create the sync orhcestrator
                SyncOrchestrator syncOrchestrator = new SyncOrchestrator();

                // set local provider of orchestrator to a sync provider associated with the 
                // GOINSPSyncScope in the client database
                Console.WriteLine("Set localprovider");
                syncOrchestrator.LocalProvider = new SqlSyncProvider("GOINSPSyncScope", clientConn);

                // set the remote provider of orchestrator to a server sync provider associated with
                // the GOINSPSyncScope in the server database
                Console.WriteLine("Set remoteprovider");
                syncOrchestrator.RemoteProvider = new SqlSyncProvider("GOINSPSyncScope", serverConn);



                // set the direction of sync session to Upload and Download
                Console.WriteLine("Set direction");
                syncOrchestrator.Direction = SyncDirectionOrder.UploadAndDownload;
                Console.WriteLine("Set eventhandler");
                ((SqlSyncProvider)syncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
                // execute the synchronization process
                Console.WriteLine("Sync action");
                SyncOperationStatistics syncStats = syncOrchestrator.Synchronize();
                // print statistics
                Console.WriteLine("Start Time: " + syncStats.SyncStartTime);
                Console.WriteLine("Total Changes Uploaded: " + syncStats.UploadChangesTotal);
                Console.WriteLine("Total Changes Downloaded: " + syncStats.DownloadChangesTotal);
                Console.WriteLine("Complete Time: " + syncStats.SyncEndTime);
                Console.WriteLine(String.Empty);
            }
            catch(Exception e)
            {
                // print statistics
                Console.WriteLine("Error occured, trying to reprovision locally");
                this.reprovisionLocal();
                Console.WriteLine("Reprovision done");
                Console.WriteLine("Retry syncing");
                this.sync();

            }
            }
            else
            {
                Console.WriteLine("No Connection");
            } 

            

            
        }
        static void Program_ApplyChangeFailed(object sender, DbApplyChangeFailedEventArgs e)
        {
            // display conflict type
            Console.WriteLine(e.Conflict.Type);

            // display error message 
            Console.WriteLine(e.Error);
        }
    }
}
