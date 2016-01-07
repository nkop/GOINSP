using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GOINSP.Models;
using GOINSP.ViewModel.QuestionnaireViewModels;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace GOINSP.ViewModel
{
    public class QuestionListVM : ViewModelBase
    {
        private Context context;
        public ICommand EditQuestionnaireCommand { get; set; }
        public ICommand AddNewQuestionnaireCommand { get; set; }
        

        private ObservableCollection<QuestionnaireVM> observableQuestionnaireCollection;
        public ObservableCollection<QuestionnaireVM> ObservableQuestionnaireCollection
        {
            get
            {
                return observableQuestionnaireCollection;
            }
            set
            {
                observableQuestionnaireCollection = value;
                RaisePropertyChanged("ObservableQuestionnaireCollection");
            }
        }

        private QuestionnaireVM selectedQuestionnaire;
        public QuestionnaireVM SelectedQuestionnaire
        {
            get
            {
                return selectedQuestionnaire;
            }
            set
            {
                selectedQuestionnaire = value;
                RaisePropertyChanged("SelectedQuestionnaire");
            }
        }

        public QuestionListVM()
        {
            context = new Context();

            ObservableQuestionnaireCollection = new ObservableCollection<QuestionnaireVM>(context.Questionnaire.ToList().Select(x => new QuestionnaireVM(x)));

            EditQuestionnaireCommand = new RelayCommand(EditQuestionnaire);
            AddNewQuestionnaireCommand = new RelayCommand(AddNewQuestionnaire);
        }

        public void AddNewQuestionnaire()
        {
            QuestionnaireVM tempQuestionnaire = new QuestionnaireVM() { Name = "Nieuwe Vragenlijst", Description = "Een compleet nieuwe vragenlijst.", IsTemplate = false};
            tempQuestionnaire.Context = context;
            tempQuestionnaire.Insert();
            ObservableQuestionnaireCollection = new ObservableCollection<QuestionnaireVM>(context.Questionnaire.ToList().Select(x => new QuestionnaireVM(x)));
        }

        public void EditQuestionnaire()
        {
            if(SelectedQuestionnaire != null)
            {
                QuestionTemplateVM questionTemplateVM = ServiceLocator.Current.GetInstance<QuestionTemplateVM>();
                questionTemplateVM.Context = context;
                questionTemplateVM.SetQuestionnaire(SelectedQuestionnaire);
                questionTemplateVM.Show();
            }
        }
    }
}
