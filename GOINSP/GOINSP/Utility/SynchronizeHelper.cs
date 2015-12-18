﻿using System;
using Microsoft.Synchronization;
using System.Data.SqlClient;
using Microsoft.Synchronization.Data.SqlServer;
using Microsoft.Synchronization.Data;
using System.Configuration;

namespace GOINSP.Utility
{
    class SynchronizeHelper
    {
        private SqlConnection clientConn;
        private SqlConnection serverConn;

        public void setConnection()
        {
            clientConn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalContext"].ConnectionString);

            // create a connection to the server database
            serverConn = new SqlConnection(ConfigurationManager.ConnectionStrings["RemoteContext"].ConnectionString);
        }

        public void SetClient()
        {

            // get the description of SyncScope from the server database
            DbSyncScopeDescription scopeDesc = SqlSyncDescriptionBuilder.GetDescriptionForScope("GOINSPSyncScope", serverConn);

            // create server provisioning object based on the SyncScope
            SqlSyncScopeProvisioning clientProvision = new SqlSyncScopeProvisioning(clientConn, scopeDesc);


            // starts the provisioning process
            clientProvision.Apply();

            Console.WriteLine("Client Successfully Provisioned.");
            Console.ReadLine();
        }

        public void setServer()
        {

            // define a new scope named MySyncScope
            DbSyncScopeDescription scopeDesc = new DbSyncScopeDescription("GOINSPSyncScope");

            // get the description of the CUSTOMER & PRODUCT table from SERVER database
            DbSyncTableDescription accountsTableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("Accounts", serverConn);
            DbSyncTableDescription companiesTableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("Companies", serverConn);
            DbSyncTableDescription inspectionTableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("Inspection", serverConn);
            DbSyncTableDescription locationsTableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("Locations", serverConn);
            DbSyncTableDescription regiosTableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("RegioS", serverConn);
            DbSyncTableDescription tdatasTableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("TDatas", serverConn);

            // add the table description to the sync scope definition
            scopeDesc.Tables.Add(accountsTableDesc);
            scopeDesc.Tables.Add(companiesTableDesc);
            scopeDesc.Tables.Add(inspectionTableDesc);
            scopeDesc.Tables.Add(locationsTableDesc);
            scopeDesc.Tables.Add(regiosTableDesc);
            scopeDesc.Tables.Add(tdatasTableDesc);

            // create a server scope provisioning object based on the MySyncScope
            SqlSyncScopeProvisioning serverProvision = new SqlSyncScopeProvisioning(serverConn, scopeDesc);

            // skipping the creation of table since table already exists on server
            serverProvision.SetCreateTableDefault(DbSyncCreationOption.CreateOrUseExisting);

            // start the provisioning process
            serverProvision.Apply();

            Console.WriteLine("Server Successfully Provisioned.");
            Console.ReadLine();
        }

        private void deprovision()
        {
            // Remove the retail customer scope from the Sql Server client database.
            SqlSyncScopeDeprovisioning serverDepro = new SqlSyncScopeDeprovisioning(serverConn);
            SqlSyncScopeDeprovisioning clientDepro = new SqlSyncScopeDeprovisioning(clientConn);

            // Remove the scope.
            try
            {
                serverDepro.DeprovisionScope("GOINSPSyncScope");
                Console.WriteLine("Server deprovisioned");
                clientDepro.DeprovisionScope("GOINSPSyncScope");
                Console.WriteLine("client deprovisioned");
            }
            catch (Exception e) {
                Console.WriteLine("Something went wrong");
            }
        }

        public void work()
        {
            this.setConnection();
            this.Sync();

        }

        public void reprovision()
        {
            this.setConnection();
            this.deprovision();
            this.setServer();
            this.SetClient();
        }


        private void Sync()
        {

            // create the sync orhcestrator
            SyncOrchestrator syncOrchestrator = new SyncOrchestrator();

            // set local provider of orchestrator to a sync provider associated with the 
            // GOINSPSyncScope in the client database
            syncOrchestrator.LocalProvider = new SqlSyncProvider("GOINSPSyncScope", clientConn);

            // set the remote provider of orchestrator to a server sync provider associated with
            // the GOINSPSyncScope in the server database
            syncOrchestrator.RemoteProvider = new SqlSyncProvider("GOINSPSyncScope", serverConn);



            // set the direction of sync session to Upload and Download
            syncOrchestrator.Direction = SyncDirectionOrder.UploadAndDownload;

            // subscribe for errors that occur when applying changes to the client
            ((SqlSyncProvider)syncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);

            // execute the synchronization process
            SyncOperationStatistics syncStats = syncOrchestrator.Synchronize();

            // print statistics
            Console.WriteLine("Start Time: " + syncStats.SyncStartTime);
            Console.WriteLine("Total Changes Uploaded: " + syncStats.UploadChangesTotal);
            Console.WriteLine("Total Changes Downloaded: " + syncStats.DownloadChangesTotal);
            Console.WriteLine("Complete Time: " + syncStats.SyncEndTime);
            Console.WriteLine(String.Empty);
            Console.ReadLine();
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
