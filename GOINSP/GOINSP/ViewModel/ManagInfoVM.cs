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

        public ObservableCollection<InspecteurInspecties> ChartData { get; set; }
        public ObservableCollection<BedrijfInspecties> BedrInspData {get; set;}

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

        public ManagInfoVM()
        {
            context = new Models.Context();


            List<Models.Account> tempUsers = context.Account.ToList();
            List<Models.Account> tempInspecteurs = new List<Models.Account>();
            foreach (Models.Account item in tempUsers)
            {
                if (item.AccountRights == Models.Account.Rights.ExterneInspecteur || item.AccountRights == Models.Account.Rights.InterneInspecteur)
                {
                    tempInspecteurs.Add(item);
                }
            }
            Inspecteurs = new ObservableCollection<AccountVM>(tempInspecteurs.Select(a => new AccountVM(a)).Distinct());
            SelectedInspecteur = new AccountVM();

            ShowBedrCommand = new RelayCommand(ShowBedrInsp);
            ShowInspCommand = new RelayCommand(ShowInspInsp);

            ShowInspInsp();
        }

        private void ShowInspInsp()
        {
            InspInsp = Visibility.Visible;
            BedrInsp = Visibility.Collapsed;

            LoadInspInspData();
        }

        private void ShowBedrInsp()
        {
            BedrInsp = Visibility.Visible;
            InspInsp = Visibility.Collapsed;

            LoadBedrInspData();
        }



        private void LoadInspInspData()
        {
            ChartData = new ObservableCollection<InspecteurInspecties>();
            var tempInspecties = context.Inspection.GroupBy(p => p.inspector).Select(g => new { inspector = g.Key, count = g.Count() });
            if (tempInspecties != null)
            {
                foreach (var item in tempInspecties)
                {
                    ChartData.Add(new InspecteurInspecties(new AccountVM(item.inspector), item.count));
                }
            }
            RaisePropertyChanged("ChartData");
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
        public int Inspecties { get; set; }

        public InspecteurInspecties(AccountVM inspecteur, int inspecties)
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
