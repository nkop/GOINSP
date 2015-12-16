using GOINSP.Models;
using GOINSP.Models.Opendata.PostCodeData;
using GOINSP.ViewModel.Opendata.PostCodeDatas;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Utility
{
    public class PostCodeImport : IImport
    {
        public JSONImport jsonImporter { get; set; }
        public CSVImport csvImporter { get; set; }
        public Context context { get; set; }

        public PostCodeImport()
        {
            csvImporter = new CSVImport();
            context = new Context();
            context.Configuration.AutoDetectChangesEnabled = false;
            context.Configuration.ValidateOnSaveEnabled = false;
        }

        public void Import(IProgress<ImportProgressValues> progress)
        {
            csvImporter.LoadFromFile();

            List<PostCodeDataVM> postcodes = new List<PostCodeDataVM>();

            foreach (string line in csvImporter.CSVStrings)
            {
                string[] data = line.Split(';');
                PostCodeDataVM tempData = new PostCodeDataVM();
                tempData.id = Convert.ToInt32(data[0]);
                tempData.postcode = data[1].Replace("\"", "");
                tempData.postcode_id = Convert.ToInt32(data[2]);
                tempData.pnum = Convert.ToInt32(data[3]);
                tempData.pchar = data[4].Replace("\"", "");
                tempData.minnumber = Convert.ToInt32(data[5]);
                tempData.maxnumber = Convert.ToInt32(data[6]);
                tempData.numbertype = data[7].Replace("\"", "");
                tempData.street = data[8].Replace("\"", "");
                tempData.city = data[9].Replace("\"", "");
                tempData.city_id = Convert.ToInt32(data[10]);
                tempData.municipality = data[11].Replace("\"", "");
                tempData.municipality_id = Convert.ToInt32(data[12]);
                tempData.province = data[13].Replace("\"", "");
                tempData.province_code = data[14].Replace("\"", "");
                tempData.lat = decimal.Parse(data[15].Replace("\"", ""), CultureInfo.InvariantCulture);
                tempData.lon = decimal.Parse(data[16].Replace("\"", ""), CultureInfo.InvariantCulture);
                tempData.rd_x = decimal.Parse(data[17].Replace("\"", ""), CultureInfo.InvariantCulture);
                tempData.rd_y = decimal.Parse(data[18].Replace("\"", ""), CultureInfo.InvariantCulture);
                tempData.location_detail = data[19].Replace("\"", "");
                tempData.changed_date = Convert.ToDateTime(data[20].Replace("\"", ""));
                postcodes.Add(tempData);
            }

            progress.Report(new ImportProgressValues(0, 0, ImportProgressValues.ProgressStatus.removing));
            context.HuishoudelijkAfvalRegioS.RemoveRange(context.HuishoudelijkAfvalRegioS);
            context.SaveChanges();

            /*int count = 0;

            foreach (PostCodeDataVM vm in postcodes)
            {
                vm.Insert();
                count++;
                progress.Report(new ImportProgressValues(count, postcodes.Count, ImportProgressValues.ProgressStatus.inserting));
            }

            int listCount = 0;

            int realCount = 0;
            for (int i = 0; i < postcodes.Count; i += 10000)
            {
                List<PostCodeDataVM> partialList = postcodes.GetRange(i, 10000);
                foreach (PostCodeDataVM vm in partialList)
                {
                    realCount++;
                    context.PostCodeData.Add(vm.getObject());
                    progress.Report(new ImportProgressValues(realCount, postcodes.Count, ImportProgressValues.ProgressStatus.inserting));
                }
                context.SaveChanges();
            }*/

            int count = 0;
            foreach (var entityToInsert in postcodes)
            {
                ++count;
                context = AddToContext(context, entityToInsert.getObject(), count, 10000, true);
                progress.Report(new ImportProgressValues(count, postcodes.Count, ImportProgressValues.ProgressStatus.inserting));
            }

            context.SaveChanges();
        }

        private Context AddToContext(Context context, PostCodeData entity, int count, int commitCount, bool recreateContext)
        {
            context.Set<PostCodeData>().Add(entity);

            if (count % commitCount == 0)
            {
                context.SaveChanges();
                if (recreateContext)
                {
                    context.Dispose();
                    context = new Context();
                    context.Configuration.AutoDetectChangesEnabled = false;
                }
            }

            return context;
        }
    }
}
