using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GOINSP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GOINSP.ViewModel
{
    public class InspectionViewModel : ViewModelBase
    {
        public ObservableCollection<InspectionVM> Inspections { get; set; }
        public Context context;

        public ICommand AddInspection { get; set; }

        public InspectionViewModel()
        {
            context = new Context();

            IEnumerable<Inspection> inspectie = context.Inspection;
            IEnumerable<InspectionVM> inspectionVM = inspectie.Select(a => new InspectionVM(a));
            Inspections = new ObservableCollection<InspectionVM>(inspectionVM);
            RaisePropertyChanged("Inspections");

            AddInspection = new RelayCommand(Add);
        }

        private void Add()
        {
            AddInspection window = new AddInspection();
            window.Show();
        }
    }
}
