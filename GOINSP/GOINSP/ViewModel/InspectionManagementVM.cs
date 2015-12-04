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
    public class InspectionManagementVM
    {
        Models.Context context;
        public ObservableCollection<CompanyVM> Bedrijven { get; set; }
        public CompanyVM SelectedBedrijf { get; set; }
        public InspectionManagementVM()
        {
            context = new Models.Context();
//            List<Models.Inspection> TempInspecties = context.Inspection.ToList();
            List<Models.Company> companies = context.Company.ToList();
            Bedrijven = new ObservableCollection<CompanyVM>(companies.Select(c => new CompanyVM(c)).Distinct());

            SelectedBedrijf = new CompanyVM();
        }
    }
}
