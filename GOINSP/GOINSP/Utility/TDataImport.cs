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
    public class TDataImport : IImport
    {
        public JSONImport jsonImporter { get; set; }
        public CSVImport csvImporter { get; set; }
        public Context context { get; set; }

        public TDataImport()
        {
            jsonImporter = new JSONImport();
            context = new Context();
        }

        public void Import(IProgress<ImportProgressValues> progress)
        {
            progress.Report(new ImportProgressValues(0, 0, ImportProgressValues.ProgressStatus.downloading));
            jsonImporter.GetJsonByURL("http://opendata.cbs.nl/ODataApi/OData/80563ned/TypedDataSet");

            List<TDataVM> list = new List<TDataVM>();
            JObject jo = JObject.Parse(jsonImporter.JsonString);
            list = jo.SelectToken("value", false).ToObject<List<TDataVM>>();

            progress.Report(new ImportProgressValues(0, 0, ImportProgressValues.ProgressStatus.removing));
            context.HuishoudelijkAfvalTData.RemoveRange(context.HuishoudelijkAfvalTData);
            context.SaveChanges();

            int count = 0;

            foreach (TDataVM vm in list)
            {
                vm.Insert();
                count++;
                progress.Report(new ImportProgressValues(count, list.Count, ImportProgressValues.ProgressStatus.inserting));
            }
        }
    }
}
