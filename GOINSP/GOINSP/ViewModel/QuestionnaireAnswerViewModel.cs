using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GOINSP.Models;
using GOINSP.Utility;
using GOINSP.ViewModel.QuestionnaireViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.ViewModel
{
    public class QuestionnaireAnswerViewModel : ViewModelBase, INavigatableViewModel
    {
        public Context context { get; set; }
        private QuestionnaireVM questionnaireVM;
        public QuestionnaireVM QuestionnaireVM {
            get
            {
                return questionnaireVM;
            }
            set
            {
                questionnaireVM = value;
                RaisePropertyChanged("QuestionnaireVM");
            } 
        }

        public QuestionnaireAnswerViewModel()
        {
            context = new Context();
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
