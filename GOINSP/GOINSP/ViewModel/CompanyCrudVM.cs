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

namespace GOINSP.ViewModel
{
    public class CompanyCrudVM : ViewModelBase
    {
        private Context context;
        private ObservableCollection<NewCompanyVM> companyList { get; set; }
        private NewCompanyVM selectedCompany { get; set; }
        private NewCompanyVM newCompany { get; set; }
        public ICommand btnSave { get; set; }
        public ICommand btnNew { get; set; }
        public ICommand btnSaveNew { get; set; }
        public ICommand btnRefresh { get; set; }

        public CompanyCrudVM()
        {
            context = new Context();
            LoadList();

            NewCompany = new NewCompanyVM();
            btnSave = new RelayCommand(save);
            btnNew = new RelayCommand(createNew);
            btnSaveNew = new RelayCommand<Window>(saveNew);
            btnRefresh = new RelayCommand(LoadList);
        }

        private void createNew()
        {
            NewCompany = new NewCompanyVM();
            AddCompanyWindow w = new AddCompanyWindow();
            w.Show();
        }

        private void LoadList()
        {
            List<Company> tempList = context.Company.ToList();
            CompanyList = null;
            CompanyList = new ObservableCollection<NewCompanyVM>(tempList.Select(c => new NewCompanyVM(c)).Distinct());
           
            if(CompanyList.Count > 0)
                SelectedCompany = tempList.Select(c => new NewCompanyVM(c)).Distinct().First();
            RaisePropertyChanged();
        }

        private void save()
        {
            context.SaveChanges();
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
                    context.Company.Add(newCompany.toCompany());
                    context.SaveChanges();
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
