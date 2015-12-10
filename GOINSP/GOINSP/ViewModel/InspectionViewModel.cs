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
        public Context context;

        public ICommand AddInspection { get; set; }
        public ICommand SaveInspection { get; set; }

        private InspectionVM _newInspection;

        public InspectionViewModel()
        {
            context = new Context();

            IEnumerable<Inspection> inspectie = context.Inspection;
            IEnumerable<InspectionVM> inspectionVM = inspectie.Select(a => new InspectionVM(a));
            Inspections = new ObservableCollection<InspectionVM>(inspectionVM);
            RaisePropertyChanged("Inspections");

            AddInspection = new RelayCommand(Add);
            SaveInspection = new RelayCommand(Save);

            _newInspection = new InspectionVM();
        }

        public InspectionVM newInspection
        {
            get { return _newInspection; }
            set { _newInspection = value; }
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
                // Set foreign key
                _newInspection.inspectorid = new Guid("C4A2D055-2722-4C8C-80BE-8C332B84842F");

                // Add to database
                context.Inspection.Add(_newInspection.toInspection());
                context.SaveChanges();

                // Add to view
                Inspections.Add(_newInspection);
                newInspection = new InspectionVM();
                RaisePropertyChanged("Inspections");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Er is iets fout gegaan, probeer het nogmaals. " + ex);
            }
        }
    }
}
