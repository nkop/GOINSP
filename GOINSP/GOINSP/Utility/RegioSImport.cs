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

namespace GOINSP.Utility
{
    public class RegioSImport : IImport
    {
        public JSONImport jsonImporter { get; set; }
        public CSVImport csvImporter { get; set; }

        public RegioSImport()
        {
            jsonImporter = new JSONImport();
        }

        public void Import(IProgress<ImportProgressValues> progress)
        {
            progress.Report(new ImportProgressValues(0, 0, ImportProgressValues.ProgressStatus.downloading));
            jsonImporter.GetJsonByURL("http://opendata.cbs.nl/ODataApi/OData/80563ned/RegioS");

            List<RegioSVM> list = new List<RegioSVM>();
            JObject jo = JObject.Parse(jsonImporter.JsonString);
            list = jo.SelectToken("value", false).ToObject<List<RegioSVM>>();

            progress.Report(new ImportProgressValues(0, 0, ImportProgressValues.ProgressStatus.removing));
            Config.Context.HuishoudelijkAfvalRegioS.RemoveRange(Config.Context.HuishoudelijkAfvalRegioS);
            Config.Context.SaveChanges();

            int count = 0;

            foreach (RegioSVM vm in list)
            {
                vm.Insert();
                count++;
                progress.Report(new ImportProgressValues(count, list.Count, ImportProgressValues.ProgressStatus.inserting));
            }
            progress.Report(new ImportProgressValues(0, 0, ImportProgressValues.ProgressStatus.saving));
            Config.Context.SaveChanges();
        }
    }
}
