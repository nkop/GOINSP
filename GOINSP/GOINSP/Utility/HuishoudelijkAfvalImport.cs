using GOINSP.ViewModel.Opendata;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Utility
{
    class HuishoudelijkAfvalImport : IImport
    {
        public JSONImport jsonImporter { get; set; }

        public HuishoudelijkAfvalImport()
        {
            jsonImporter = new JSONImport();
        }

        public void Import()
        {
            jsonImporter.LoadFromFile();

            List<HuishoudelijkAfvalVM> list = new List<HuishoudelijkAfvalVM>();

            JToken objects = JObject.Parse(jsonImporter.JsonString).SelectToken("value");
            foreach (JProperty property in objects)
            {
                JToken token = property.First;
                list.Add(JsonConvert.DeserializeObject<HuishoudelijkAfvalVM>(token.ToString()));
            }

            foreach (HuishoudelijkAfvalVM vm in list)
            {
                vm.Insert();
            }
        }
    }
}
