using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
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
    public class LocationPickerVM : ViewModelBase, INavigatableViewModel
    {
        private NewCompanyVM newCompanyVM;
        public NewCompanyVM NewCompanyVM
        {
            get
            {
                return newCompanyVM;
            }
            set
            {
                newCompanyVM = value;
                RaisePropertyChanged("NewCompanyVM");
            }
        }

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

        public ICommand AddPushPinCommand { get; set; }
        public ICommand SaveLocationCommand { get; set; }

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

        public LocationPickerVM()
        {
            ObservableRegios = new ObservableCollection<RegioS>(Config.Context.HuishoudelijkAfvalRegioS.OrderBy(xy => xy.Title));

            SaveLocationCommand = new RelayCommand(SaveLocation);
        }

        private void SaveLocation()
        {
            CloseView();
        }

        public void CleanVM()
        {
            NewCompanyVM.BedrijfsPostcode = null;
            NewCompanyVM.BedrijfsAdres = null;
            NewCompanyVM.BedrijfsWijk = null;
            NewCompanyVM.BedrijfsNummer = null;
            NewCompanyVM.BedrijfsGemeente = null;
        }

        public void GetClosestMapPoint()
        {
            CleanVM();

            double LongDec = double.Parse(Longitude);
            double LatDec = double.Parse(Latitude);

            GmapAPIImpler gmap = new GmapAPIImpler();
            gmap.lat = LatDec.ToString().Replace(',', '.');
            gmap.lon = LongDec.ToString().Replace(',', '.');
            gmap.Import(null);

            List<GmapAPI> mapApiList = gmap.list;

            foreach (GmapAPI mapApi in mapApiList)
            {
                foreach(AddressComponent address_component in mapApi.address_components)
                {
                    foreach(string type in address_component.types)
                    {
                        if (type == "postal_code" && NewCompanyVM.BedrijfsPostcode == null)
                        {
                            NewCompanyVM.BedrijfsPostcode = address_component.long_name.Replace(" ", "");
                        }
                        if (type == "route" && NewCompanyVM.BedrijfsAdres == null)
                        {
                            NewCompanyVM.BedrijfsAdres = address_component.long_name;
                        }
                        if (type == "locality" && NewCompanyVM.BedrijfsWijk == null)
                        {
                            NewCompanyVM.BedrijfsWijk = address_component.short_name;
                        }
                        if (type == "street_number" && NewCompanyVM.BedrijfsNummer == null)
                        {
                            NewCompanyVM.BedrijfsNummer = address_component.short_name;
                        }
                        if (type == "administrative_area_level_2" && NewCompanyVM.BedrijfsGemeente == null)
                        {
                            NewCompanyVM.BedrijfsGemeente = address_component.short_name;
                        }
                    }
                }
            }

            NewCompanyVM.BedrijfsLon = (decimal)LongDec;
            NewCompanyVM.BedrijfsLat = (decimal)LatDec;

            try
            {
                RegioS regio = Config.Context.HuishoudelijkAfvalRegioS.Where(x => x.Title == NewCompanyVM.BedrijfsGemeente).First();
                NewCompanyVM.BedrijfsGemeente = regio.Title;
                NewCompanyVM.BedrijfsGemeenteCode = regio.Key;
            }
            catch(Exception ex)
            {
            }
        }

        public void Show(INavigatableViewModel sender = null)
        {
            LocationPicker view = new LocationPicker();
            view.Show();
        }

        public void CloseView()
        {
            Messenger.Default.Send<NotificationMessage>(
                new NotificationMessage(this, "CloseView")
            );
        }
    }
}
