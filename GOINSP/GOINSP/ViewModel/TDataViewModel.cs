using GalaSoft.MvvmLight;
using GOINSP.Utility;
using GOINSP.ViewModel.Opendata.HuishoudelijkAfval;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GOINSP.Models.Opendata.HuishoudelijkAfval;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GalaSoft.MvvmLight.Messaging;

namespace GOINSP.ViewModel
{
    public class TDataViewModel : ViewModelBase, INavigatableViewModel
    {
        public string GMCode { get; set; }


        private List<DoubleString> gridData;
        public List<DoubleString> GridData
        {
            get
            {
                return gridData;
            }
            set
            {
                gridData = value;
            }
        }

        private ObservableCollection<TDataVM> tDataCollection;
        public ObservableCollection<TDataVM> TDataCollection
        {
            get
            {
                return tDataCollection;
            }
            set
            {
                tDataCollection = value;
                RaisePropertyChanged("TDataCollection");
            }
        }

        private TDataVM selectedTData;
        public TDataVM SelectedTData
        {
            get
            {
                return selectedTData;
            }
            set
            {
                selectedTData = value;
                SetGrid();
                RaisePropertyChanged("selectedTData");
            }
        }

        public void SetGrid()
        {
            if (SelectedTData != null)
            {
                GridData = new List<DoubleString>();

                GridData.Add(new DoubleString("Inwoners per 1 januari", SelectedTData.InwonersPer1Januari_32 != null ? SelectedTData.InwonersPer1Januari_32.ToString() : "Onbekend"));
                GridData.Add(new DoubleString("Totaal huishoudelijk afval", SelectedTData.TotaalHuishoudelijkAfval_1 != null ? SelectedTData.TotaalHuishoudelijkAfval_1.ToString() + "kg per inwoner" : "Onbekend"));
                GridData.Add(new DoubleString("Huishoudelijk restafval", SelectedTData.HuishoudelijkRestafval_2 != null ? SelectedTData.HuishoudelijkRestafval_2.ToString() + "kg per inwoner" : "Onbekend"));
                GridData.Add(new DoubleString("Grof huishoudelijk restafval", SelectedTData.GrofHuishoudelijkRestafval_3 != null ? SelectedTData.GrofHuishoudelijkRestafval_3.ToString() + "kg per inwoner" : "Onbekend"));
                GridData.Add(new DoubleString("Verbouwingsrestafval", SelectedTData.Verbouwingsrestafval_4 != null ? SelectedTData.Verbouwingsrestafval_4.ToString() + "kg per inwoner" : "Onbekend"));
                GridData.Add(new DoubleString("GFT-afval", SelectedTData.GFTAfval_5 != null ? SelectedTData.GFTAfval_5.ToString() + "kg per inwoner" : "Onbekend"));
                GridData.Add(new DoubleString("Oud papier en karton", SelectedTData.OudPapierEnKarton_6 != null ? SelectedTData.OudPapierEnKarton_6.ToString() + "kg per inwoner" : "Onbekend"));
                GridData.Add(new DoubleString("Verpakkingsglas", SelectedTData.Verpakkingsglas_7 != null ? SelectedTData.Verpakkingsglas_7.ToString() + "kg per inwoner" : "Onbekend"));
                GridData.Add(new DoubleString("Textiel", SelectedTData.Textiel_8 != null ? SelectedTData.Textiel_8.ToString() + "kg per inwoner" : "Onbekend"));
                GridData.Add(new DoubleString("Klein chemisch afval (KCA)", SelectedTData.KleinChemischAfvalKCA_9 != null ? SelectedTData.KleinChemischAfvalKCA_9.ToString() + "kg per inwoner" : "Onbekend"));
                GridData.Add(new DoubleString("Metalen verpakkingen (blik)", SelectedTData.MetalenVerpakkingenBlik_10 != null ? SelectedTData.MetalenVerpakkingenBlik_10.ToString() + "kg per inwoner" : "Onbekend"));
                GridData.Add(new DoubleString("Drankenkartons", SelectedTData.Drankenkartons_11 != null ? SelectedTData.Drankenkartons_11.ToString() + "kg per inwoner" : "Onbekend"));
                GridData.Add(new DoubleString("Kunststof verpakkingen", SelectedTData.KunststofVerpakkingen_12 != null ? SelectedTData.KunststofVerpakkingen_12.ToString() + "kg per inwoner" : "Onbekend"));
                GridData.Add(new DoubleString("Overige kunststoffen", SelectedTData.OverigeKunststoffen_13 != null ? SelectedTData.OverigeKunststoffen_13.ToString() + "kg per inwoner" : "Onbekend"));
                GridData.Add(new DoubleString("Luiers", SelectedTData.Luiers_14 != null ? SelectedTData.Luiers_14.ToString() + "kg per inwoner" : "Onbekend"));
                GridData.Add(new DoubleString("Wit- en bruingoed", SelectedTData.WitEnBruingoed_15 != null ? SelectedTData.WitEnBruingoed_15.ToString() + "kg per inwoner" : "Onbekend"));
                GridData.Add(new DoubleString("Grof tuinafval", SelectedTData.GrofTuinafval_16 != null ? SelectedTData.GrofTuinafval_16.ToString() + "kg per inwoner" : "Onbekend"));
                GridData.Add(new DoubleString("Bruikbaar huisraad", SelectedTData.BruikbaarHuisraad_17 != null ? SelectedTData.BruikbaarHuisraad_17.ToString() + "kg per inwoner" : "Onbekend"));
                GridData.Add(new DoubleString("Vloerbedekking", SelectedTData.Vloerbedekking_18 != null ? SelectedTData.Vloerbedekking_18.ToString() + "kg per inwoner" : "Onbekend"));
                GridData.Add(new DoubleString("Vlakglas", SelectedTData.Vlakglas_19 != null ? SelectedTData.Vlakglas_19.ToString() + "kg per inwoner" : "Onbekend"));
                GridData.Add(new DoubleString("Metalen", SelectedTData.Metalen_20 != null ? SelectedTData.Metalen_20.ToString() + "kg per inwoner" : "Onbekend"));
                GridData.Add(new DoubleString("Houtafval (A- en B-hout)", SelectedTData.HoutafvalAEnBHout_21 != null ? SelectedTData.HoutafvalAEnBHout_21.ToString() + "kg per inwoner" : "Onbekend"));
                GridData.Add(new DoubleString("Houtafval (C-hout)", SelectedTData.HoutafvalCHout_22 != null ? SelectedTData.HoutafvalCHout_22.ToString() + "kg per inwoner" : "Onbekend"));
                GridData.Add(new DoubleString("Schoon puin", SelectedTData.SchoonPuin_23 != null ? SelectedTData.SchoonPuin_23.ToString() + "kg per inwoner" : "Onbekend"));
                GridData.Add(new DoubleString("Bitumenhoudende dakbedekking", SelectedTData.BitumenhoudendeDakbedekking_24 != null ? SelectedTData.BitumenhoudendeDakbedekking_24.ToString() + "kg per inwoner" : "Onbekend"));
                GridData.Add(new DoubleString("Asbesthoudend afval", SelectedTData.AsbesthoudendAfval_25 != null ? SelectedTData.AsbesthoudendAfval_25.ToString() + "kg per inwoner" : "Onbekend"));
                GridData.Add(new DoubleString("Autobanden", SelectedTData.Autobanden_26 != null ? SelectedTData.Autobanden_26.ToString() + "kg per inwoner" : "Onbekend"));
                GridData.Add(new DoubleString("Schone grond", SelectedTData.SchoneGrond_27 != null ? SelectedTData.SchoneGrond_27.ToString() + "kg per inwoner" : "Onbekend"));
                GridData.Add(new DoubleString("Overige afvalstoffen", SelectedTData.OverigeAfvalstoffen_28 != null ? SelectedTData.OverigeAfvalstoffen_28.ToString() + "kg per inwoner" : "Onbekend"));
                RaisePropertyChanged("GridData");
            }
        }

        public TDataViewModel()
        {

        }

        public void CreateView()
        {
            SelectedTData = null;
            GridData = new List<DoubleString>();
            RaisePropertyChanged("GridData");

            List<TData> TDataList = Config.Context.HuishoudelijkAfvalTData.Where(x => x.RegioS == GMCode).OrderBy(xy => xy.Perioden).ToList();
            if (TDataList.Count != 0)
            {
                TDataCollection = new ObservableCollection<TDataVM>(TDataList.Select(x => new TDataVM(x)));
            }
            else
            {
                MessageBox.Show("Geen afval data is beschikbaar.");
                CloseView();
            }
        }

        public void CloseView()
        {
            Messenger.Default.Send<NotificationMessage>(
                new NotificationMessage(this, "CloseView")
            );
        }

        public void Show(INavigatableViewModel sender = null)
        {
            TDataWindow view = new TDataWindow();
            view.Show();
            CreateView();
        }
    }

    public class DoubleString
    {
        public string StringOne { get; set; }
        public string StringTwo { get; set; }

        public DoubleString(string StringOne, string StringTwo)
        {
            this.StringOne = StringOne;
            this.StringTwo = StringTwo;
        }
    }
}
