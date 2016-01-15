using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GOINSP.ViewModel
{
    public class ManagInfoVM : ViewModelBase
    {
        private Models.Context context;
        public ObservableCollection<AccountVM> Inspecteurs { get; set; }
        public ICommand ShowInspCommand { get; set; }
        public ICommand ShowBedrCommand { get; set; }
        public ICommand ShowGemInspCommand { get; set; }
        private AccountVM _selectedInspecteur { get; set; }
        public AccountVM SelectedInspecteur
        {
            get { return _selectedInspecteur; }
            set
            {
                _selectedInspecteur = value;
                RaisePropertyChanged("SelectedInspecteur");
            }
        }
        public ObservableCollection<InspecteurInspecties> GemInspInspecteur { get; set; }
        public ObservableCollection<InspecteurInspecties> ChartData { get; set; }
        public ObservableCollection<BedrijfInspecties> BedrInspData {get; set;}

        private string _gemInspectiesInspecteur;
        public string GemInspectiesInspecteur
        {
            get { return _gemInspectiesInspecteur; }
            set { 
                _gemInspectiesInspecteur = value;
                RaisePropertyChanged("GemInspectiesInspecteur");
                }
        }

        private Visibility _bedrInsp;
        public Visibility BedrInsp
        {
            get { return _bedrInsp; }
            set
            {
                _bedrInsp = value;
                RaisePropertyChanged("BedrInsp");
            }
        }

        private Visibility _inspInsp;
        public Visibility InspInsp
        {
            get { return _inspInsp; }
            set 
            {
            _inspInsp = value;
            RaisePropertyChanged("InspInsp");
            }
        }

        private Visibility _gemInspInsp;
        public Visibility GemInspInsp
        {
            get { return _gemInspInsp; }
            set
            {
                _gemInspInsp = value;
                RaisePropertyChanged("GemInspInsp");
            }
        }

        public ManagInfoVM()
        {
            context = new Models.Context();

            ShowBedrCommand = new RelayCommand(ShowBedrInsp);
            ShowInspCommand = new RelayCommand(ShowInspInsp);
            ShowGemInspCommand = new RelayCommand(ShowGemInsp);

            ShowInspInsp();
        }

        private void ShowGemInsp()
        {
            GemInspInsp = Visibility.Visible;
            BedrInsp = Visibility.Collapsed;
            InspInsp = Visibility.Collapsed;

            LoadGemInspData();
        }

        private void ShowInspInsp()
        {
            InspInsp = Visibility.Visible;
            BedrInsp = Visibility.Collapsed;
            GemInspInsp = Visibility.Collapsed;

            LoadInspInspData();
        }

        private void ShowBedrInsp()
        {
            BedrInsp = Visibility.Visible;
            InspInsp = Visibility.Collapsed;
            GemInspInsp = Visibility.Collapsed;

            LoadBedrInspData();
        }

        private void LoadGemInspData()
        {
            GemInspInspecteur = new ObservableCollection<InspecteurInspecties>();
            DateTime datum = DateTime.Now.AddYears(-1);
            var gemInspecties = context.Inspection.Where(i => i.date >= datum).GroupBy(i => i.inspector).Select(i => new { inspector = i.Key, count = i.Count() });
            if (gemInspecties != null)
            {
                foreach (var item in gemInspecties)
                {
                    GemInspInspecteur.Add(new InspecteurInspecties(new AccountVM(item.inspector), ((double)item.count / 12)));
                }
            }
            RaisePropertyChanged("GemInspInspecteur");
        }

        private void LoadInspInspData()
        {
            int sum = 0;
            ChartData = new ObservableCollection<InspecteurInspecties>();
            var tempInspecties = context.Inspection.GroupBy(p => p.inspector).Select(g => new { inspector = g.Key, count = g.Count() });
            if (tempInspecties != null)
            {
                foreach (var item in tempInspecties)
                {
                    ChartData.Add(new InspecteurInspecties(new AccountVM(item.inspector), item.count));
                    sum += item.count;
                }
            }
            RaisePropertyChanged("ChartData");

            GemInspectiesInspecteur = "Gemiddeld aantal inspecties per inspecteur: ";
            GemInspectiesInspecteur += (sum / ChartData.Count());
        }

        private void LoadBedrInspData()
        {
            BedrInspData = new ObservableCollection<BedrijfInspecties>();
            var tempInspecties = context.Inspection.GroupBy(p => p.company).Select(g => new { company = g.Key, count = g.Count() });
            if (tempInspecties != null)
            {
                foreach (var item in tempInspecties)
                {
                    BedrInspData.Add(new BedrijfInspecties(new NewCompanyVM(item.company), item.count));
                }
            }
            RaisePropertyChanged("BedrInspData");
        }


        private void ClearSelectedInspecteur()
        {
            SelectedInspecteur = new AccountVM();
        }
    }

    public class InspecteurInspecties
    {
        public AccountVM Inspecteur { get; set; }
        public double Inspecties { get; set; }

        public InspecteurInspecties(AccountVM inspecteur, double inspecties)
        {
            this.Inspecteur = inspecteur;
            this.Inspecties = inspecties;
        }
    }

    public class BedrijfInspecties
    {
        public NewCompanyVM Bedrijf { get; set; }
        public int Inspecties { get; set; }

        public BedrijfInspecties(NewCompanyVM bedrijf, int inspecties)
        {
            this.Bedrijf = bedrijf;
            this.Inspecties = inspecties;
        }
    }
}
