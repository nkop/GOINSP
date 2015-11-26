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
    class TDataImport : IImport
    {
        public JSONImport jsonImporter { get; set; }
        public Context context { get; set; }

        public TDataImport()
        {
            jsonImporter = new JSONImport();
            context = new Context();
        }

        public void Import(IProgress<int> progress)
        {
            jsonImporter.LoadFromFile();

            List<TDataVM> list = new List<TDataVM>();
            JObject jo = JObject.Parse(jsonImporter.JsonString);
            list = jo.SelectToken("value", false).ToObject<List<TDataVM>>();

            context.HuishoudelijkAfvalTData.RemoveRange(context.HuishoudelijkAfvalTData);
            context.SaveChanges();

            int count = 0;

            foreach (TDataVM vm in list)
            {
                vm.Insert();
                count++;
                progress.Report((int)(((float)count / (float)list.Count) * 100));
            }
        }
    }
}
