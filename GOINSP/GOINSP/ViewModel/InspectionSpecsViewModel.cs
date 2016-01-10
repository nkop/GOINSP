using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GOINSP.Models;
using GOINSP.Utility;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GOINSP.ViewModel
{
    public class InspectionSpecsViewModel : ViewModelBase, INavigatableViewModel
    {
        public Context context;

        private InspectionVM inspectionSpecs;
        public InspectionVM InspectionSpecs
        {
            get
            {
                return inspectionSpecs;
            }
            set
            {
                inspectionSpecs = value;
                RaisePropertyChanged("InspectionSpecs");
            }
        }

        public ICommand OpenQuestionnaireCommand { get; set; }

        public InspectionSpecsViewModel()
        {
            context = new Context();

            OpenQuestionnaireCommand = new RelayCommand(OpenQuestionnaire);
        }

        public void SetInspection(Guid id)
        {
            IEnumerable<Inspection> inspectie = context.Inspection;
            InspectionSpecs = inspectie.Select(a => new InspectionVM(a)).Where(p => p.id == id).First();
        }
          
        public void Show(INavigatableViewModel sender = null)
        {
            InspectionSpecs window = new InspectionSpecs();
            window.Show();
        }

        public void OpenQuestionnaire()
        {
            QuestionListVM questionListVM = ServiceLocator.Current.GetInstance<QuestionListVM>();
            questionListVM.Show(this);
        }

        public void CloseView()
        {
            Messenger.Default.Send<NotificationMessage>(
                new NotificationMessage(this, "CloseView")
            );
        }
    }
}
