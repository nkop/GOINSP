using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GOINSP.ViewModel
{
    public class InspectionManagementVM : ViewModelBase
    {
        Models.Context context;
        public ObservableCollection<CompanyVM> Bedrijven { get; set; }
        public ObservableCollection<InspectionVM> BedrijfInspecties { get; set; }
        private CompanyVM _selectedBedrijf { get; set; }
        public CompanyVM SelectedBedrijf 
        {
            get { return _selectedBedrijf; }
            set 
            { _selectedBedrijf = value;
            RaisePropertyChanged("SelectedBedrijf");
            }
        }

        private InspectionVM _selectedInspection { get; set; }
        public InspectionVM SelectedInspection
        {
            get { return _selectedInspection; }
            set
            {
                _selectedInspection = value;
                RaisePropertyChanged("SelectedInspection");
            }
        }

              
        public InspectionManagementVM()
        {
            context = new Models.Context();
//            List<Models.Inspection> TempInspecties = context.Inspection.ToList();
            List<Models.Company> companies = context.Company.ToList();
            Bedrijven = new ObservableCollection<CompanyVM>(companies.Select(c => new CompanyVM(c)).Distinct());

            SelectedBedrijf = new CompanyVM();
        }

        private void LoadInspections()
        {
            if (SelectedBedrijf.ID != null)
            {
                List<Models.Inspection> inspections = context.Inspection.ToList();
               // BedrijfInspecties = new ObservableCollection<InspectionVM>(inspections.Where(i.bedrijfsnaam = SelectedBedrijf.ID))
            }
        }

       
    }
}
