using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.ViewModel
{
    public class InspectionViewModel : ViewModelBase
    {
        public ObservableCollection<InspectionVM> Inspections { get; set; }
        public Models.Context context;

        public InspectionViewModel()
        {
            List<Models.Inspection> tempInspection = context.Inspection.ToList();
            Inspections = new ObservableCollection<AccountVM>(tempInspection.Select(a => new InspectionVM(a)).Distinct());
        }
    }
}
