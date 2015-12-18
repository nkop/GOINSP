using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GOINSP.Models;
using GOINSP.Models.Opendata;
using GOINSP.Models.Opendata.PostCodeData;
using GOINSP.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.SqlServer;
using System.Device.Location;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GOINSP.ViewModel
{
    public class LocationPickerVM : ViewModelBase
    {
        private Context context;

        public ICommand AddPushPinCommand { get; set; }

        private string longitude;
        public string Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                RaisePropertyChanged("longitude");
            }
        }

        private string latitude;
        public string Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                RaisePropertyChanged("latitude");
                GetClosestMapPoint();
            }
        }

        private PostCodeData postCode;
        public PostCodeData PostCode
        {
            get { return postCode; }
            set
            {
                postCode = value;
                RaisePropertyChanged("postCode");
            }
        }

        public LocationPickerVM()
        {
            context = new Context();
        }

        public void GetClosestMapPoint()
        {
            double LongDec = double.Parse(Longitude);
            double LatDec = double.Parse(Latitude);

            GmapAPIImpler gmap = new GmapAPIImpler();
            gmap.lat = LatDec.ToString().Replace(',', '.');
            gmap.lon = LongDec.ToString().Replace(',', '.');
            gmap.Import(null);

            List<GmapAPI> mapApiList = gmap.list;

            PostCodeData data = new PostCodeData();

            foreach (GmapAPI mapApi in mapApiList)
            {
                foreach(AddressComponent address_component in mapApi.address_components)
                {
                    foreach(string type in address_component.types)
                    {
                        if (type == "postal_code" && data.postcode == null)
                        {
                            data.postcode = address_component.long_name.Replace(" ", "");
                        }
                        if (type == "route" && data.street == null)
                        {
                            data.street = address_component.long_name;
                        }
                        if (type == "locality" && data.city == null)
                        {
                            data.city = address_component.short_name;
                        }
                    }
                }
            }

            string GMCode = "";

            if (data.postcode.Count() == 4)
            {
                GMCode = "GM"+String.Format("{0:0000}", context.PostCodeData.Where(entity => SqlFunctions.PatIndex(data.postcode + "%%", entity.postcode) > 0).First().municipality_id);
            }
            else
            {
                GMCode = "GM"+String.Format("{0:0000}", context.PostCodeData.Where(entity => entity.postcode == data.postcode).First().municipality_id);
            }


            data.municipality = context.HuishoudelijkAfvalRegioS.Where(x => x.Key == GMCode).First().Title;
            PostCode = data;

            //PostCode = context.PostCodeData.SqlQuery("SELECT TOP 1 [id], [postcode], [postcode_id], [pnum], [pchar],[minnumber],[maxnumber],[numbertype],[street],[city],[city_id],[municipality],[municipality_id],[province],[province_code],[lat] = (ABS(lat - " + LatDec.ToString().Replace(",", ".") + ") + ABS(lon - " + LongDec.ToString().Replace(",", ".") + ")) / 2,[lon],[rd_x],[rd_y],[location_detail],[changed_date] FROM PostCodeDatas WHERE (lat IS NOT NULL) AND (lon IS NOT NULL) ORDER BY lat").First();
        }
    }
}
