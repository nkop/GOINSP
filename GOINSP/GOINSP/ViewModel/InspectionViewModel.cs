using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
        public ObservableCollection<CompanyVM> Bedrijven { get; set; }
        public ObservableCollection<InspectionVM> BedrijfInspecties { get; set; }
        public ObservableCollection<AccountVM> Inspecteurs { get; set; }

        public Context context;

        public ICommand AddInspection { get; set; }
        public ICommand SaveInspection { get; set; }
        public ICommand UpdateInspection { get; set; }
        public ICommand WeergeefBedrijfCommand { get; set; }

        private InspectionVM _newInspection;

        private InspectionVM _selectedInspection;

        private string _searchQuota { get; set; }
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

        private CompanyVM _selectedBedrijf;
        private AccountVM _selectedUser;

        public Guid InspectionID;

        public InspectionViewModel()
        {
            context = new Context();

            IEnumerable<Inspection> inspectie = context.Inspection;
            IEnumerable<InspectionVM> inspectionVM = inspectie.Select(a => new InspectionVM(a));
            Inspections = new ObservableCollection<InspectionVM>(inspectionVM);
            RaisePropertyChanged("Inspections");

            List<Models.Company> companies = context.Company.ToList();
            Bedrijven = new ObservableCollection<CompanyVM>(companies.Select(c => new CompanyVM(c)).Distinct());

            IEnumerable<Account> inspecteurs = context.Account;
            IEnumerable<AccountVM> accountVM = inspecteurs.Select(c => new AccountVM(c)).Distinct();
            Inspecteurs = new ObservableCollection<AccountVM>(accountVM);
            RaisePropertyChanged("Inspecteurs");

            AddInspection = new RelayCommand(Add);
            SaveInspection = new RelayCommand(Save);
            UpdateInspection = new RelayCommand(Update);
            WeergeefBedrijfCommand = new RelayCommand(ShowBedrijf);

            _newInspection = new InspectionVM();
            _selectedInspection = new InspectionVM();

            _selectedBedrijf = new CompanyVM();
            _selectedUser = new AccountVM();
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

        public AccountVM selectedUser
        {
            get { return _selectedUser; }
            set { _selectedUser = value; }
        }

        public CompanyVM SelectedBedrijf
        {
            get { return _selectedBedrijf; }
            set
            {
                _selectedBedrijf = value;
                newInspection.address = SelectedBedrijf.BedrijfsAdres;
                newInspection.zipcode = SelectedBedrijf.BedrijfsPostcode;
                RaisePropertyChanged("newInspection");
                RaisePropertyChanged("SelectedBedrijf");
            }
        }

        private void Add()
        {
            AddInspection window = new AddInspection();
            window.Show();
        }

        private void Save()
        {
            try
            {
                // Set foreign keys
                _newInspection.inspectorid = selectedUser.id;
                _newInspection.companyid = SelectedBedrijf.ID;

                // Add to database
                context.Inspection.Add(_newInspection.toInspection());
                context.SaveChanges();

                // Add to view
                Inspections.Add(_newInspection);
                newInspection = new InspectionVM();
                RaisePropertyChanged("Inspections");

                MessageBox.Show("Toevoegen is geslaagd");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Er is iets fout gegaan, probeer het nogmaals. " + ex);
            }
        }

        private void Update()
        {
            try
            {
                context.Entry(selectedInspection.toInspection()).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                MessageBox.Show("Opslaan is geslaagd");
            }
            catch (Exception)
            {
                MessageBox.Show("Er is iets fout gegaan, probeer het nogmaals.");
            }
        }

        private void LoadInspections()
        {
            if (SelectedBedrijf.ID != null)
            {
                List<Models.Inspection> inspections = context.Inspection.ToList();

                foreach (Models.Inspection item in inspections)
                {
                    if (item.companyid == SelectedBedrijf.ID)
                    {
                        BedrijfInspecties.Add(new InspectionVM(item));
                    }
                }
            }
        }

        private void Search()
        {
            if (SearchQuota.Length >= 0)
            {
                List<Models.Company> tempBedrijven = context.Company.ToList();
                List<CompanyVM> tempCompanyVM = new List<CompanyVM>();
                foreach (Models.Company item in tempBedrijven)
                {
                    tempCompanyVM.Add(new CompanyVM(item));
                }
                Bedrijven.Clear();
                foreach (CompanyVM item in tempCompanyVM)
                {
                    if (item.Bedrijfsnaam.Contains(SearchQuota))
                    {
                        Bedrijven.Add(item);
                    }
                }
                RaisePropertyChanged("Bedrijven");
            }
        }

        public void OpenInspection()
        {
            InspectionSpecsViewModel InspectionVMInstance = ServiceLocator.Current.GetInstance<InspectionSpecsViewModel>();
            InspectionVMInstance.context = context;
            InspectionVMInstance.SetInspection(_selectedInspection.id);
            InspectionVMInstance.Show(this);
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
            throw new NotImplementedException();
        }
    }
}
