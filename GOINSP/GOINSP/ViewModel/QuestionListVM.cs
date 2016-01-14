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
using System.Windows.Input;
using System.Windows.Navigation;

namespace GOINSP.ViewModel
{
    public class QuestionListVM : ViewModelBase, INavigatableViewModel
    {
        public Context context { get; set; }
        public ICommand EditQuestionnaireCommand { get; set; }
        public ICommand AddNewTemplateQuestionnaireCommand { get; set; }
        public ICommand AddNewQuestionnaireCommand { get; set; }
        public ICommand DuplicateCurrentTemplateCommand { get; set; }
        

        public InspectionVM BoundInspection { get; set; }

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
            EditQuestionnaireCommand = new RelayCommand(EditQuestionnaire);
            AddNewTemplateQuestionnaireCommand = new RelayCommand(AddNewTemplateQuestionnaire);
            AddNewQuestionnaireCommand = new RelayCommand(AddNewQuestionnaire);
            DuplicateCurrentTemplateCommand = new RelayCommand(DuplicateCurrentTemplate);
            
        }

        public void AddNewTemplateQuestionnaire()
        {
            QuestionnaireVM tempQuestionnaire = new QuestionnaireVM() { Name = "Nieuwe Vragenlijst", Description = "Een compleet nieuwe vragenlijst.", IsTemplate = true};
            tempQuestionnaire.Context = context;
            tempQuestionnaire.Insert();
            CreateQuestionnaireList();
        }

        public void AddNewQuestionnaire()
        {
            if (BoundInspection != null)
            {
                QuestionnaireVM tempQuestionnaire = new QuestionnaireVM() { Name = "Nieuwe Vragenlijst", Description = "Een compleet nieuwe vragenlijst.", IsTemplate = false };
                tempQuestionnaire.Context = context;
                tempQuestionnaire.Insert();
                SelectedQuestionnaire = tempQuestionnaire;
                BoundInspection.questionnaire = tempQuestionnaire;
                context.Entry(BoundInspection.toInspection()).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                EditQuestionnaire();
                BoundInspection = null;
            }
        }

        public void DuplicateCurrentTemplate()
        {
            if(SelectedQuestionnaire != null)
            {
                var cb = from c in context.Questionnaire.AsNoTracking().Include("QuestionnaireCollection") where c.QuestionnaireID == SelectedQuestionnaire.QuestionnaireID select c;
                var q = cb.First();
                var s = new QuestionnaireVM(q);
                s.Context = context;
                s.IsTemplate = false;
                s.Insert();
                BoundInspection.questionnaire = s;
                context.Entry(BoundInspection.toInspection()).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                BoundInspection = null;
                CloseView();
            }
        }

        public void CreateQuestionnaireList()
        {
            ObservableQuestionnaireCollection = new ObservableCollection<QuestionnaireVM>(context.Questionnaire.Where(y => y.IsTemplate == true).ToList().Select(x => new QuestionnaireVM(x)));
        }

        public void Show(INavigatableViewModel sender = null)
        {
            QuestionListWindow questionListWindow = new QuestionListWindow();
            questionListWindow.Show();
        }

        public void EditQuestionnaire()
        {
            if(SelectedQuestionnaire != null)
            {
                QuestionTemplateVM questionTemplateVM = ServiceLocator.Current.GetInstance<QuestionTemplateVM>();
                questionTemplateVM.Context = context;
                questionTemplateVM.SetQuestionnaire(SelectedQuestionnaire);
                questionTemplateVM.Show(this);
                CloseView();
            }
        }

        public void CloseView()
        {
            Messenger.Default.Send<NotificationMessage>(
                new NotificationMessage(this, "CloseView")
            );
        }
    }
}
