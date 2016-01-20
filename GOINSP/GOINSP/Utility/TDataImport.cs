using GOINSP.Models;
using GOINSP.ViewModel.Opendata;
using GOINSP.ViewModel.Opendata.HuishoudelijkAfval;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GOINSP.Utility
{
    public class TDataImport : IImport
    {
        public JSONImport jsonImporter { get; set; }
        public CSVImport csvImporter { get; set; }

        public TDataImport()
        {
            jsonImporter = new JSONImport();
        }

        public void Import(IProgress<ImportProgressValues> progress)
        {
            try
            {
                progress.Report(new ImportProgressValues(0, 0, ImportProgressValues.ProgressStatus.downloading));
                jsonImporter.GetJsonByURL("http://opendata.cbs.nl/ODataApi/OData/80563ned/TypedDataSet");

                List<TDataVM> list = new List<TDataVM>();
                JObject jo = JObject.Parse(jsonImporter.JsonString);
                list = jo.SelectToken("value", false).ToObject<List<TDataVM>>();

                progress.Report(new ImportProgressValues(0, 0, ImportProgressValues.ProgressStatus.removing));
                Config.Context.HuishoudelijkAfvalTData.RemoveRange(Config.Context.HuishoudelijkAfvalTData);
                Config.Context.SaveChanges();

                int count = 0;

                foreach (TDataVM vm in list)
                {
                    vm.Insert();
                    count++;
                    progress.Report(new ImportProgressValues(count, list.Count, ImportProgressValues.ProgressStatus.inserting));
                }
                progress.Report(new ImportProgressValues(0, 0, ImportProgressValues.ProgressStatus.saving));
                Config.Context.SaveChanges();
                progress.Report(new ImportProgressValues(0, 0, ImportProgressValues.ProgressStatus.done));
            }
            catch(Exception ex)
            {
                MessageBox.Show("Sorry, er is iets mis gegaan.");
                progress.Report(new ImportProgressValues(0, 0, ImportProgressValues.ProgressStatus.error));
            }
        }
    }
}
