using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GOINSP.Models;
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
    public class InspectionViewModel : ViewModelBase
    {
        public ObservableCollection<InspectionVM> Inspections { get; set; }
        public ObservableCollection<CompanyVM> Bedrijven { get; set; }
        public ObservableCollection<InspectionVM> BedrijfInspecties { get; set; }

        public Context context;

        public ICommand AddInspection { get; set; }
        public ICommand SaveInspection { get; set; }

        private InspectionVM _newInspection;
        private InspectionVM _selectedInspection;
        private CompanyVM _selectedBedrijf;

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

            AddInspection = new RelayCommand(Add);
            SaveInspection = new RelayCommand(Save);

            _newInspection = new InspectionVM();
            _selectedInspection = new InspectionVM();
            _selectedBedrijf = new CompanyVM();
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

                InspectionID = selectedInspection.id;

                InspectionSpecs window = new InspectionSpecs();
                window.Show();
            }
        }

        public CompanyVM SelectedBedrijf
        {
            get { return _selectedBedrijf; }
            set
            {
                _selectedBedrijf = value;
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
                // Set foreign key (NEED TO RETHINK THIS)
                _newInspection.inspectorid = new Guid("EC63BB1D-70A5-E511-9BD7-001C4205EA00");
                _newInspection.companyid = _selectedBedrijf.ID;

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

        private void LoadInspections()
        {
            if (SelectedBedrijf.ID != null)
            {
                List<Models.Inspection> inspections = context.Inspection.ToList();
                //BedrijfInspecties = new ObservableCollection<InspectionVM>(inspections.Where(i.bedrijfsnaam = SelectedBedrijf.ID))
            }
        }
    }
}
