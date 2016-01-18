using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GOINSP.Models;
using GOINSP.Utility;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

        private InspectionVM _selectedInspection;
        public InspectionVM selectedInspection
        {
            get { return _selectedInspection; }
            set
            {
                _selectedInspection = value;
                OpenInspection();
            }
        }

        public ObservableCollection<InspectionTypeVM> TypeInspectie { get; set; }
        public ObservableCollection<InspectionVM> BedrijfInspecties { get; set; }
        

        private Visibility addInspectionVisibility;
        public Visibility AddInspectionVisibility
        {
            get
            {
                return addInspectionVisibility;
            }
            set
            {
                addInspectionVisibility = value;
                RaisePropertyChanged("AddInspectionVisibility");
            }
        }

        public ICommand AddInspection { get; set; }




        private string _searchQuota { get; set; }


        public Guid InspectionID;

        public InspectionViewModel()
        {
            IEnumerable<Inspection> inspectie = Config.Context.Inspection;

            IEnumerable<InspectionVM> inspectionVM  = null; 
            
            if (Config.Rechten == Models.Account.Rights.ExterneInspecteur)
            {
                List<Inspection> AllInspections = inspectie.ToList();
                List<Inspection> InspectionsForUser = inspectie.Where(x => x.inspector.id == Config.GebruikerID).ToList();
                List<Inspection> inspections = new List<Inspection>();
                List<Company> Companies = InspectionsForUser.Select(x => x.company).ToList();
                foreach(Company company in Companies)
                {
                    InspectionsForUser.AddRange(AllInspections.Where(x => x.company == company).ToList().Distinct());
                }

                InspectionsForUser = InspectionsForUser.Distinct().ToList();

                inspectionVM = InspectionsForUser.Select(a => new InspectionVM(a));
            }
            else if (Config.Rechten == Models.Account.Rights.InterneInspecteur)
            {
                List<Inspection> AllInspections = inspectie.ToList();
                List<Inspection> InspectionsForUser = inspectie.Where(x => x.inspector.id == Config.GebruikerID).ToList();
                inspectionVM = InspectionsForUser.Select(a => new InspectionVM(a));
            }
            else
            {
                inspectionVM = inspectie.Select(a => new InspectionVM(a));
            }

            AddInspectionVisibility = Visibility.Collapsed;
            if (Config.Rechten == Account.Rights.Manager || Config.Rechten == Account.Rights.Administrator)
            {
                AddInspectionVisibility = Visibility.Visible;
            }
            
            Inspections = new ObservableCollection<InspectionVM>(inspectionVM);
            RaisePropertyChanged("Inspections");

            IEnumerable<InspectionType> inspectiontype = Config.Context.Inspectiontype;
            IEnumerable<InspectionTypeVM> inspectiontypeVM = inspectiontype.Select(a => new InspectionTypeVM(a));
            TypeInspectie = new ObservableCollection<InspectionTypeVM>(inspectiontypeVM);
            RaisePropertyChanged("TypeInspectie");

            AddInspection = new RelayCommand(Add);

            _selectedInspection = new InspectionVM();
        }

        private void Add()
        {
            InspectieEditViewModel inspectieEditViewModel = ServiceLocator.Current.GetInstance<InspectieEditViewModel>();
            inspectieEditViewModel.LoadAddInspection();
            inspectieEditViewModel.Show(this);

            CloseView();
        }

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

        private void Search()
        {
            if (SearchQuota.Length >= 0)
            {
                List<Models.Inspection> tempInspection = Config.Context.Inspection.ToList();
                List<InspectionVM> tempInspectionVM = new List<InspectionVM>();
                foreach (Models.Inspection item in tempInspection)
                {
                    tempInspectionVM.Add(new InspectionVM(item));
                }
                Inspections.Clear();
                foreach (InspectionVM item in tempInspectionVM)
                {
                    if (item.name.ToLower().Contains(SearchQuota.ToLower()) || item.company.BedrijfsAdres.ToLower().Contains(SearchQuota.ToLower()) || item.company.BedrijfsPostcode.ToLower().Contains(SearchQuota.ToLower()))
                    {
                        Inspections.Add(item);
                    }
                }
                RaisePropertyChanged("Inspections");
            }
        }


        public void OpenInspection(bool show = true)
        {
            if (_selectedInspection != null)
            {
                InspectionSpecsViewModel InspectionVMInstance = ServiceLocator.Current.GetInstance<InspectionSpecsViewModel>();
                InspectionVMInstance.SetInspection(_selectedInspection.id);

                if (show)
                    InspectionVMInstance.Show(this);
            }
        }


        public void Show(INavigatableViewModel sender = null)
        {
            InspectionViewModel view = new InspectionViewModel();
            view.Show();
        }

        public void CloseView()
        {
            Messenger.Default.Send<NotificationMessage>(
                new NotificationMessage(this, "CloseView")
            );
        }


    }
}
