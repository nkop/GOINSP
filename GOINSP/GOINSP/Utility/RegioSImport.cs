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
    class RegioSImport : IImport
    {
        public JSONImport jsonImporter { get; set; }
        public Context context { get; set; }

        public RegioSImport()
        {
            jsonImporter = new JSONImport();
            context = new Context();
        }

        public void Import(IProgress<int> progress)
        {
            jsonImporter.LoadFromFile();

            List<RegioSVM> list = new List<RegioSVM>();
            JObject jo = JObject.Parse(jsonImporter.JsonString);
            list = jo.SelectToken("value", false).ToObject<List<RegioSVM>>();

            context.HuishoudelijkAfvalRegioS.RemoveRange(context.HuishoudelijkAfvalRegioS);
            context.SaveChanges();

            int count = 0;

            foreach (RegioSVM vm in list)
            {
                vm.Insert();
                count++;
                progress.Report((int)(((float)count/(float)list.Count)*100));
            }
        }
    }
}
