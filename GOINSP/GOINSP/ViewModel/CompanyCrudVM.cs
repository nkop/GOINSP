using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GOINSP.Models;
using Microsoft.Practices.ServiceLocation;
using GOINSP.Utility;

namespace GOINSP.ViewModel
{
    public class CompanyCrudVM : ViewModelBase
    {
        private ObservableCollection<NewCompanyVM> companyList { get; set; }
        private NewCompanyVM selectedCompany { get; set; }
        private NewCompanyVM newCompany { get; set; }
        public ICommand btnSave { get; set; }
        public ICommand btnNew { get; set; }
        public ICommand btnSaveNew { get; set; }
        public ICommand btnRefresh { get; set; }
        public ICommand btnDelete { get; set; }
        public ICommand btnPickLocation { get; set; }
        public ICommand btnPickLocationEdit { get; set; }

        public CompanyCrudVM()
        {
            LoadList();

            NewCompany = new NewCompanyVM();
            btnSave = new RelayCommand(save);
            btnNew = new RelayCommand(createNew);
            btnSaveNew = new RelayCommand<Window>(saveNew);
            btnRefresh = new RelayCommand(LoadList);
            btnDelete = new RelayCommand(deleteCompany);
            btnPickLocation = new RelayCommand(pickLocation);
            btnPickLocationEdit = new RelayCommand(pickLocationEdit);
        }

        public void pickLocation()
        {
            LocationPickerVM picker = ServiceLocator.Current.GetInstance<LocationPickerVM>();
            picker.NewCompanyVM = newCompany;
            picker.Show();
        }
        public void pickLocationEdit()
        {
            LocationPickerVM picker = ServiceLocator.Current.GetInstance<LocationPickerVM>();
            picker.NewCompanyVM = SelectedCompany;
            picker.Show();
        }
        
        private void deleteCompany()
        {
            try
            {
                Config.Context.Company.Remove(selectedCompany.toCompany());
                Config.Context.SaveChanges();
                CompanyList.Remove(selectedCompany);
                SelectedCompany = CompanyList.First();
                RaisePropertyChanged();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kan dit bedrijf niet verwijderen. Het bedrijf zit gekoppeld aan een inspectie.");
            }
           
        }

        private void createNew()
        {
            NewCompany = new NewCompanyVM();
            AddCompanyWindow w = new AddCompanyWindow();
            w.Show();
        }

        private void LoadList()
        {
            List<Company> tempList = Config.Context.Company.ToList();
            CompanyList = null;
            CompanyList = new ObservableCollection<NewCompanyVM>(tempList.Select(c => new NewCompanyVM(c)).Distinct());
           
            if(CompanyList.Count > 0)
                SelectedCompany = tempList.Select(c => new NewCompanyVM(c)).Distinct().First();
            RaisePropertyChanged();
        }

        private void save()
        {
            Config.Context.SaveChanges();
        }

        public ObservableCollection<NewCompanyVM> CompanyList
        {
            get { return companyList; }
            set { 
                companyList = value;
                RaisePropertyChanged();
            }
        }

        public NewCompanyVM SelectedCompany
        {
            get { return selectedCompany; }
            set { 
                selectedCompany = value;
                RaisePropertyChanged();
            }
        }

        public NewCompanyVM NewCompany
        {
            get { return newCompany; }
            set { newCompany = value; }
        }

        private void saveNew(Window currentWindow)
        {
            if (newCompany.Bedrijfsnaam != null && newCompany.BedrijfsEmail != null)
            {
                try
                {
                    Config.Context.Company.Add(newCompany.toCompany());
                    Config.Context.SaveChanges();
                    LoadList();
                    RaisePropertyChanged("CompanyList");
                    currentWindow.Close();
                }
                catch
                {
                    MessageBox.Show("Er is iets fout gegaan. Probeer het opnieuw.");
                }
            }
            else
            {
                MessageBox.Show("Er moet iets ingevuld zijn.");
            }
        }

    }
}
