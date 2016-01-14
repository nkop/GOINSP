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
        public ICommand OpenEditInspection { get; set; }

        public InspectionSpecsViewModel()
        {
            OpenQuestionnaireCommand = new RelayCommand(OpenQuestionnaire);
            OpenEditInspection = new RelayCommand(OpenEditInspectionWindow);
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
            if(inspectionSpecs.questionnaire == null)
            {
                QuestionListVM questionListVM = ServiceLocator.Current.GetInstance<QuestionListVM>();
                questionListVM.context = context;
                questionListVM.CreateQuestionnaireList();
                questionListVM.BoundInspection = InspectionSpecs;
                questionListVM.Show(this);
            }
            else
            {
                QuestionnaireAnswerViewModel questionnaireAnswerViewModel = ServiceLocator.Current.GetInstance<QuestionnaireAnswerViewModel>();
                questionnaireAnswerViewModel.context = context;
                questionnaireAnswerViewModel.QuestionnaireVM = inspectionSpecs.questionnaire;
                questionnaireAnswerViewModel.QuestionnaireVM.Context = context;
                questionnaireAnswerViewModel.QuestionnaireVM.CheckConditionBoundQuestions();
                questionnaireAnswerViewModel.Show(this);
            }
        }

        public void OpenEditInspectionWindow()
        {
            EditInspection window = new EditInspection();
            window.Show();
        }

        public void CloseView()
        {
            Messenger.Default.Send<NotificationMessage>(
                new NotificationMessage(this, "CloseView")
            );
        }
    }
}
