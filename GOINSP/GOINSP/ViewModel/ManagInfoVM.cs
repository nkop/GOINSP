using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GOINSP.Utility;
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
        public ObservableCollection<AccountVM> Inspecteurs { get; set; }
        public ICommand ShowInspCommand { get; set; }
        public ICommand ShowBedrCommand { get; set; }
        public ICommand ShowGemInspCommand { get; set; }
        public ICommand ShowTypeInspCommand { get; set; }
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
        public ObservableCollection<InspectieTypeInspecties> TypeInspData { get; set; } 

        private string _gemInspectiesInspecteur;
        public string GemInspectiesInspecteur
        {
            get { return _gemInspectiesInspecteur; }
            set { 
                _gemInspectiesInspecteur = value;
                RaisePropertyChanged("GemInspectiesInspecteur");
                }
        }

        private Visibility _typeInsp;
        public Visibility TypeInsp
        {
            get { return _typeInsp; }
            set
            {
                _typeInsp = value;
                RaisePropertyChanged("TypeInsp");
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
            ShowBedrCommand = new RelayCommand(ShowBedrInsp);
            ShowInspCommand = new RelayCommand(ShowInspInsp);
            ShowGemInspCommand = new RelayCommand(ShowGemInsp);
            ShowTypeInspCommand = new RelayCommand(ShowTypeInsp);

            ShowInspInsp();
        }

        private void ShowTypeInsp()
        {
            TypeInsp = Visibility.Visible;
            GemInspInsp = Visibility.Collapsed;
            BedrInsp = Visibility.Collapsed;
            InspInsp = Visibility.Collapsed;

            LoadTypeInspData();
        }

        private void ShowGemInsp()
        {
            TypeInsp = Visibility.Collapsed;
            GemInspInsp = Visibility.Visible;
            BedrInsp = Visibility.Collapsed;
            InspInsp = Visibility.Collapsed;

            LoadGemInspData();
        }

        private void ShowInspInsp()
        {
            TypeInsp = Visibility.Collapsed;
            InspInsp = Visibility.Visible;
            BedrInsp = Visibility.Collapsed;
            GemInspInsp = Visibility.Collapsed;

            LoadInspInspData();
        }

        private void ShowBedrInsp()
        {
            TypeInsp = Visibility.Collapsed;
            BedrInsp = Visibility.Visible;
            InspInsp = Visibility.Collapsed;
            GemInspInsp = Visibility.Collapsed;

            LoadBedrInspData();
        }

        private void LoadGemInspData()
        {
            GemInspInspecteur = new ObservableCollection<InspecteurInspecties>();
            DateTime datum = DateTime.Now.AddYears(-1);
            var gemInspecties = Config.Context.Inspection.Where(i => i.date >= datum).GroupBy(i => i.inspector).Select(i => new { inspector = i.Key, count = i.Count() });
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
            var tempInspecties = Config.Context.Inspection.GroupBy(p => p.inspector).Select(g => new { inspector = g.Key, count = g.Count() });
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
            int amount = ChartData.Count();
            if (amount == 0) amount = 1;
            GemInspectiesInspecteur += (sum / amount);
        }

        private void LoadBedrInspData()
        {
            BedrInspData = new ObservableCollection<BedrijfInspecties>();
            var tempInspecties = Config.Context.Inspection.GroupBy(p => p.company).Select(g => new { company = g.Key, count = g.Count() });
            if (tempInspecties != null)
            {
                foreach (var item in tempInspecties)
                {
                    BedrInspData.Add(new BedrijfInspecties(new NewCompanyVM(item.company), item.count));
                }
            }
            RaisePropertyChanged("BedrInspData");
        }

        private void LoadTypeInspData()
        {
            TypeInspData = new ObservableCollection<InspectieTypeInspecties>();
            var tempInspecties = Config.Context.Inspection.GroupBy(i => i.inspectiontype).Select(i => new { type = i.Key, count = i.Count() });
            if (tempInspecties != null)
            {
                foreach (var item in tempInspecties)        
                {
                    TypeInspData.Add(new InspectieTypeInspecties(new InspectionTypeVM(item.type), item.count));
                }
            }
            RaisePropertyChanged("TypeInspData");
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

    public class InspectieTypeInspecties
    {
        public InspectionTypeVM Type{ get; set; }

        public int Inspecties { get; set; }

        public InspectieTypeInspecties(InspectionTypeVM Type, int Inspecties)
        {
            this.Type = Type;
            this.Inspecties = Inspecties;
        }
    }
}
