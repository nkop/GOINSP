using GalaSoft.MvvmLight;
using GOINSP.Models;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GOINSP.ViewModel
{
    public class InspectionSpecsViewModel : ViewModelBase
    {
        public Context context;
        public ObservableCollection<InspectionVM> InspectionSpecs { get; set; }

        InspectionViewModel InspectionVMInstance = ServiceLocator.Current.GetInstance<InspectionViewModel>();

        public InspectionSpecsViewModel()
        {
            context = new Context();

            IEnumerable<Inspection> inspectie = context.Inspection;
            IEnumerable<InspectionVM> inspectionVM = inspectie.Select(a => new InspectionVM(a)).Where(p => p.id == InspectionVMInstance.InspectionID);
            InspectionSpecs = new ObservableCollection<InspectionVM>(inspectionVM);
            RaisePropertyChanged("InspectionSpecs");
        }
    }
}
