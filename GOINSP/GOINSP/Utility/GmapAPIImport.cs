using GOINSP.Models;
using GOINSP.Models.Opendata;
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
    public class GmapAPIImpler : IImport
    {
        public JSONImport jsonImporter { get; set; }
        public CSVImport csvImporter { get; set; }
        public List<GmapAPI> list = new List<GmapAPI>();
        public string lon;
        public string lat;

        public GmapAPIImpler()
        {
            jsonImporter = new JSONImport();
        }

        public void Import(IProgress<ImportProgressValues> progress)
        {
            jsonImporter.GetJsonByURL("https://maps.googleapis.com/maps/api/geocode/json?latlng="+lat+", "+lon+"&key=AIzaSyDDhiVuTU5eovr9crHR-cPjHZvQ8FACDJ0");

            JObject jo = JObject.Parse(jsonImporter.JsonString);
            list = jo.SelectToken("results", false).ToObject<List<GmapAPI>>();
        }
    }
}
