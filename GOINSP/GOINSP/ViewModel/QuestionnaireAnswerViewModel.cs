using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GOINSP.Models;
using GOINSP.Utility;
using GOINSP.ViewModel.QuestionnaireViewModels;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace GOINSP.ViewModel
{
    public class QuestionnaireAnswerViewModel : ViewModelBase, INavigatableViewModel
    {
        public Context context { get; set; }

        public ICommand SaveQuestionnaireCommand { get; set; }
        public ICommand EditQuestionnaireCommand { get; set; }
        public ICommand DeleteQuestionnaireCommand { get; set; }

        private QuestionnaireVM questionnaireVM;
        public QuestionnaireVM QuestionnaireVM {
            get
            {
                return questionnaireVM;
            }
            set
            {
                questionnaireVM = value;
                questionnaireVM.QuestionnaireCollection = new ObservableCollection<QuestionVM>(questionnaireVM.QuestionnaireCollection.OrderBy(xy => xy.ListNumber));
                RaisePropertyChanged("QuestionnaireVM");
            } 
        }

        public QuestionnaireAnswerViewModel()
        {
            SaveQuestionnaireCommand = new RelayCommand(SaveQuestionnaire);
            EditQuestionnaireCommand = new RelayCommand(EditQuestionnaire);
            DeleteQuestionnaireCommand = new RelayCommand(DeleteQuestionnaire);
        }

        private void DeleteQuestionnaire()
        {
            DialogResult result = MessageBox.Show("Weet u zeker dat de vragenlijst verwijdert moet worden? Dit kan niet ongedaan gemaakt worden.", "Let op!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (QuestionnaireVM != null)
                {
                    context.Questionnaire.Remove(questionnaireVM.GetQuestionnaire());
                    context.SaveChanges();
                    CloseView();
                }
            }
        }

        private void SaveQuestionnaire()
        {
            if (QuestionnaireVM != null)
            {
                QuestionnaireVM.Update();
                CloseView();
            }
        }

        private void EditQuestionnaire()
        {
            if(QuestionnaireVM != null)
            {
                QuestionTemplateVM questionTemplateVM = ServiceLocator.Current.GetInstance<QuestionTemplateVM>();
                questionTemplateVM.Context = context;
                questionTemplateVM.SetQuestionnaire(QuestionnaireVM);
                questionTemplateVM.Show(this);
                CloseView();
            }
        }

        public void Show(INavigatableViewModel sender = null)
        {
            QuestionnaireAnswerView questionnaireAnswerView = new QuestionnaireAnswerView();
            questionnaireAnswerView.Show();
        }

        public void CloseView()
        {
            Messenger.Default.Send<NotificationMessage>(
                new NotificationMessage(this, "CloseView")
            );
        }
    }
}
