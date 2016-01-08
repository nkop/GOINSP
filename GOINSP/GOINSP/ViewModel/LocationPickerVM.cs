using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GOINSP.Models;
using GOINSP.Models.Opendata;
using GOINSP.Models.Opendata.HuishoudelijkAfval;
using GOINSP.Models.Opendata.PostCodeData;
using GOINSP.Utility;
using GOINSP.ViewModel.Opendata.PostCodeDatas;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.SqlServer;
using System.Device.Location;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GOINSP.ViewModel
{
    public class LocationPickerVM : ViewModelBase
    {
        private Context context;

        private ObservableCollection<RegioS> observableRegios;
        public ObservableCollection<RegioS> ObservableRegios
        {
            get
            {
                return observableRegios;
            }
            set
            {
                observableRegios = value;
                RaisePropertyChanged("ObservableRegios");
            }
        }

        private string test;
        public string Test
        {
            get
            {   
                return test;
            }
            set
            {
                test = value;
                RaisePropertyChanged("Test");
            }
        }

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

        private PostCodeDataVM postCode;
        public PostCodeDataVM PostCode
        {
            get { return postCode; }
            set
            {
                postCode = value;
                RaisePropertyChanged("PostCode");
            }
        }

        public LocationPickerVM()
        {
            context = new Context();
            ObservableRegios = new ObservableCollection<RegioS>(context.HuishoudelijkAfvalRegioS.OrderBy(xy => xy.Title));
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

            PostCodeDataVM data = new PostCodeDataVM();

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
                        if (type == "street_number" && data.street_number == null)
                        {
                            try
                            {
                                data.street_number = Convert.ToInt32(address_component.short_name);
                            }
                            catch(Exception exc)
                            {

                            }
                        }
                        if (type == "administrative_area_level_2" && data.municipality == null)
                        {
                            data.municipality = address_component.short_name;
                        }
                    }
                }
            }

            try
            {
                RegioS regio = context.HuishoudelijkAfvalRegioS.Where(x => x.Title == data.municipality).First();
                data.municipality = regio.Title;
                data.municipality_id = regio.Key;
                PostCode = data;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Hier is geen adres gevonden");
            }
        }
    }
}
