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

        public TDataImport()
        {
            jsonImporter = new JSONImport();
        }

        public void Import()
        {
            jsonImporter.LoadFromFile();

            List<TDataVM> list = new List<TDataVM>();

            JObject jo = JObject.Parse(jsonImporter.JsonString);
            list = jo.SelectToken("value", false).ToObject<List<TDataVM>>();

            foreach (TDataVM vm in list)
            {
                vm.Insert();
            }
        }
    }
}
