using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GOINSP.Models;
using GOINSP.Utility;
using Microsoft.Practices.ServiceLocation;
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
    public class InspectionViewModel : ViewModelBase, INavigatableViewModel
    {
        public ObservableCollection<InspectionVM> Inspections { get; set; }
        public ObservableCollection<InspectionTypeVM> TypeInspectie { get; set; }
        private ObservableCollection<NewCompanyVM> bedrijven; 
        public ObservableCollection<NewCompanyVM> Bedrijven { 
            get
            {
                return bedrijven;
            }
            set
            {
                bedrijven = value;
                RaisePropertyChanged("Bedrijven");
            }
        }
        public ObservableCollection<InspectionVM> BedrijfInspecties { get; set; }
        public ObservableCollection<AccountVM> Inspecteurs { get; set; }

        public ICommand AddInspection { get; set; }
        public ICommand SaveInspection { get; set; }
        public ICommand UpdateInspection { get; set; }
        public ICommand WeergeefBedrijfCommand { get; set; }

        private InspectionVM _newInspection;

        private InspectionVM _selectedInspection;

        private string _searchQuota { get; set; }

        private NewCompanyVM _selectedBedrijf;
        private AccountVM _selectedUser;
        private InspectionTypeVM _selectedtype;

        public Guid InspectionID;

        public InspectionViewModel()
        {
            IEnumerable<Inspection> inspectie = Config.Context.Inspection;

            IEnumerable<InspectionVM> inspectionVM  = null; 
            
            if (Config.Rechten == Models.Account.Rights.ExterneInspecteur)
            {
                List<Inspection> allInsp = inspectie.ToList();
                List<Inspection> sad = inspectie.Where(x => x.inspector.id == Config.GebruikerID).ToList();
                List<Inspection> inspections = new List<Inspection>();
                List<Company> asd = sad.Select(x => x.company).ToList();
                foreach(Company company in asd)
                {
                    sad.AddRange(allInsp.Where(x => x.company == company).ToList().Distinct());
                }

                sad = sad.Distinct().ToList();

                inspectionVM = sad.Select(a => new InspectionVM(a));
            }
            else
            {
                inspectionVM = inspectie.Select(a => new InspectionVM(a));
            }

            
            Inspections = new ObservableCollection<InspectionVM>(inspectionVM);
            RaisePropertyChanged("Inspections");

            IEnumerable<InspectionType> inspectiontype = Config.Context.Inspectiontype;
            IEnumerable<InspectionTypeVM> inspectiontypeVM = inspectiontype.Select(a => new InspectionTypeVM(a));
            TypeInspectie = new ObservableCollection<InspectionTypeVM>(inspectiontypeVM);
            RaisePropertyChanged("TypeInspectie");

            AddInspection = new RelayCommand(Add);
            SaveInspection = new RelayCommand(Save);
            UpdateInspection = new RelayCommand(Update);
            WeergeefBedrijfCommand = new RelayCommand(ShowBedrijf);

            _newInspection = new InspectionVM();
            _selectedInspection = new InspectionVM();
            _selectedtype = new InspectionTypeVM();
            _selectedBedrijf = new NewCompanyVM();
            _selectedUser = new AccountVM();

            newInspection.date = DateTime.Now;
        }

        public string SearchQuota
        {
            get { return _searchQuota; }
            set
            {
                _searchQuota = value;
                RaisePropertyChanged("SearchQuota");
                Search();
            }
        }

        public void LoadAddInspection()
        {
            List<Company> companies = Config.Context.Company.ToList();
            Bedrijven = new ObservableCollection<NewCompanyVM>(companies.Select(c => new NewCompanyVM(c)));

            IEnumerable<Account> inspecteurs = Config.Context.Account;
            IEnumerable<AccountVM> accountVM = inspecteurs.Select(c => new AccountVM(c)).Where(x => x.AccountRights == Models.Account.Rights.ExterneInspecteur || x.AccountRights == Models.Account.Rights.InterneInspecteur);
            Inspecteurs = new ObservableCollection<AccountVM>(accountVM);
            RaisePropertyChanged("Inspecteurs");
        }

        public InspectionVM newInspection
        {
            get { return _newInspection; }
            set { _newInspection = value; }
        }

        public InspectionVM selectedInspection
        {
            get { return _selectedInspection; }
            set
            {
                _selectedInspection = value;
                OpenInspection();
            }
        }

        public InspectionVM UpdateSelectedInspection
        {
            get { return _selectedInspection; }
            set
            {
                _selectedInspection = value;
                OpenInspection(false);
            }
        }

        public AccountVM selectedUser
        {
            get { return _selectedUser; }
            set { _selectedUser = value; }
        }

        public NewCompanyVM SelectedBedrijf
        {
            get { return _selectedBedrijf; }
            set
            {
                _selectedBedrijf = value;
                RaisePropertyChanged("SelectedBedrijf");
            }
        }

        public InspectionTypeVM SelectedType
        {
            get { return _selectedtype; }
            set
            {
                _selectedtype = value;
                RaisePropertyChanged("SelectedType");
            }
        }

        private void Add()
        {
            AddInspection window = new AddInspection();
            window.Show();
            LoadAddInspection();
        }

        private void Save()
        {
            try
            {
                // Set foreign keys
                _newInspection.accountVM = selectedUser;
                _newInspection.company = SelectedBedrijf;
                _newInspection.InspectiontypeVM = SelectedType;

                // Add to database
                Config.Context.Inspection.Add(_newInspection.toInspection());
                Config.Context.SaveChanges();

                // Add to view
                Inspections.Add(_newInspection);
                newInspection = new InspectionVM();
                RaisePropertyChanged("Inspections");

                CloseView();
            }
            catch (Exception)
            {
                MessageBox.Show("Er is iets fout gegaan, probeer het nogmaals.");
            }
        }

        private void Update()
        {
            try
            {
                Config.Context.Entry(selectedInspection.toInspection()).State = System.Data.Entity.EntityState.Modified;
                Config.Context.SaveChanges();

                UpdateSelectedInspection = selectedInspection;
                RaisePropertyChanged("UpdateSelectedInspection");
                RaisePropertyChanged("Inspections");

                CloseView();
            }
            catch (Exception)
            {
                MessageBox.Show("Er is iets fout gegaan, probeer het nogmaals.");
            }
        }

        private void Search()
        {
            if (SearchQuota.Length >= 0)
            {
                List<Models.Inspection> tempInspection = Config.Context.Inspection.ToList();
                List<InspectionVM> tempInspectionVM = new List<InspectionVM>();
                foreach (Models.Inspection item in tempInspection)
                {
                    tempInspectionVM.Add(new InspectionVM(item));
                }
                Inspections.Clear();
                foreach (InspectionVM item in tempInspectionVM)
                {
                    if (item.name.ToLower().Contains(SearchQuota.ToLower()) || item.company.BedrijfsAdres.ToLower().Contains(SearchQuota.ToLower()) || item.company.BedrijfsPostcode.ToLower().Contains(SearchQuota.ToLower()))
                    {
                        Inspections.Add(item);
                    }
                }
                RaisePropertyChanged("Inspections");
            }
        }

        public void OpenInspection(bool show = true)
        {
            if (_selectedInspection != null)
            {
                InspectionSpecsViewModel InspectionVMInstance = ServiceLocator.Current.GetInstance<InspectionSpecsViewModel>();
                InspectionVMInstance.SetInspection(_selectedInspection.id);

                if (show)
                    InspectionVMInstance.Show(this);
            }
        }

        private void ShowBedrijf()
        {
            BedrijfInfo window = new BedrijfInfo(SelectedBedrijf.ID);
        }

        public void Show(INavigatableViewModel sender = null)
        {
            throw new NotImplementedException();
        }

        public void CloseView()
        {
            Messenger.Default.Send<NotificationMessage>(
                new NotificationMessage(this, "CloseView2")
            );
        }
    }
}
