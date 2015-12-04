using GOINSP.Models;
using GOINSP.Models.Opendata.GemeenteGrenzen;
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
    public class GrenzenImport : IImport
    {
        public JSONImport jsonImporter { get; set; }
        public Context context { get; set; }

        public GrenzenImport()
        {
            jsonImporter = new JSONImport();
            context = new Context();
        }

        public void Import(IProgress<ImportProgressValues> progress)
        {
            //jsonImporter.GetJsonByURL("http://opendata.cbs.nl/ODataApi/OData/80563ned/RegioS");
            jsonImporter.LoadFromFile();

            JObject jo = JObject.Parse(jsonImporter.JsonString);
            List<JToken> list = jo.SelectToken("features", false).ToList();

            List<Grenzen> grenzen = new List<Grenzen>();

            foreach (JToken token in list)
            {
                JObject o = (JObject)token;
                List<JToken> list1 = o.SelectToken("geometry", false).SelectToken("coordinates", false).ToList();
                string GMCode = o.SelectToken("properties", false).SelectToken("GMCODE", false).ToString();
                string GMNaam = o.SelectToken("properties", false).SelectToken("GMNAAM", false).ToString();

                int itt = 0;

                foreach (JToken coord in list1)
                {
                    JArray oc = (JArray)coord[0];
                    if(oc[0].Count() != 0)
                    {
                        foreach (var test in oc)
                        {
                            Grenzen grens = new Grenzen();
                            grens.Latitude = test[0].ToObject<double>();
                            grens.Longitude = test[1].ToObject<double>();
                            grens.GMCode = GMCode;
                            grens.GMNaam = GMNaam;
                            grens.ID = itt;
                            grenzen.Add(grens);
                        }
                    }
                    else
                    {
                        oc = (JArray)coord;
                        foreach (var test in oc)
                        {
                            Grenzen grens = new Grenzen();
                            grens.Latitude = test[0].ToObject<double>();
                            grens.Longitude = test[1].ToObject<double>();
                            grens.GMCode = GMCode;
                            grens.GMNaam = GMNaam;
                            grens.ID = itt;
                            grenzen.Add(grens);
                        }
                    }
                    itt++;
                }
            }

            int count = 0;
            foreach (Grenzen grens in grenzen)
            {
                context.Grenzen.Add(grens);
                count++;
                progress.Report(new ImportProgressValues(count, grenzen.Count, ImportProgressValues.ProgressStatus.inserting));
            }
            context.SaveChanges();
        }
    }
}
