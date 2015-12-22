using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GOINSP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using GOINSP.Models.QuestionnaireModels;
using GOINSP.ViewModel.QuestionnaireViewModels;

namespace GOINSP.ViewModel
{
    public class QuestionTemplateVM : ViewModelBase
    {
        public ICommand AddNewQuestionCommand { get; set; }

        private QuestionnaireVM questionnaire;
        public QuestionnaireVM Questionnaire
        {
            get
            {
                return questionnaire;
            }
            set
            {
                questionnaire = value;
                RaisePropertyChanged("Questionnaire");
            }
        }

        public Context Context { get; set; }

        public QuestionTemplateVM()
        {
            Context = new Context();
            /*Questionnaire = new QuestionnaireVM();

            Questionnaire.QuestionnaireCollection = new ObservableCollection<QuestionVM>();
            SimpleBoolQuestionVM simpleBool = new SimpleBoolQuestionVM() { ListNumber = 1, Visible = Visibility.Visible, Question = "Ga jij vaak naar de bios?" };
            SimpleTextQuestionVM simpleTextConditionBound = new SimpleTextQuestionVM() { ListNumber = 2, Visible = Visibility.Visible, VisibleCondition = false, Question = "Waarom niet?" };
            RadioQuestionVM radioQuestion = new RadioQuestionVM() { ListNumber = 3, Question = "Reden van capaciteitvermindering:", Visible = Visibility.Visible, AlternativeAnswerVisibility = Visibility.Visible };

            radioQuestion.Answers = new List<RadioAnswerVM>();
            radioQuestion.Answers.Add(new RadioAnswerVM() { Text = "bouwwerkzaamheden", GroupName = "group1", Checked = false });
            radioQuestion.Answers.Add(new RadioAnswerVM() { Text = "schoonmaakwerkzaamheden", GroupName = "group1", Checked = false });
            radioQuestion.Answers.Add(new RadioAnswerVM() { Text = "onderhoudswerkzaamheden", GroupName = "group1", Checked = false });
            radioQuestion.Answers.Add(new RadioAnswerVM() { Text = "heggen snoeien / tuin onderhoud", GroupName = "group1", Checked = false });
            radioQuestion.Answers.Add(new RadioAnswerVM() { Text = "event (vb concert)", GroupName = "group1", Checked = false });
            radioQuestion.Answers.Add(new RadioAnswerVM() { Text = "verhuizing", GroupName = "group1", Checked = false });

            simpleBool.ConditionBoundQuestions.Add(simpleTextConditionBound);

            Questionnaire.QuestionnaireCollection.Add(simpleBool);
            Questionnaire.QuestionnaireCollection.Add(simpleTextConditionBound);
            Questionnaire.QuestionnaireCollection.Add(radioQuestion);*/


            /*Questionnaire questionnaire = new Questionnaire();

            questionnaire.QuestionnaireCollection = new List<Question>();
            SimpleBoolQuestion simpleBoolQuestion = new SimpleBoolQuestion() { ListNumber = 1, Visible = true, Question = "Is 1 meer als 2?", Answer = true};
            SimpleTextQuestion simpleTextQuestion = new SimpleTextQuestion() { ListNumber = 2, Visible = false, VisibleCondition = true };
            RadioQuestion radioQuestion = new RadioQuestion() { ListNumber = 3, Question = "Reden van capaciteitvermindering:", Visible = true, AlternativeAnswerVisibility = true };

            radioQuestion.Answers = new List<RadioAnswer>();
            radioQuestion.Answers.Add(new RadioAnswer() { Text = "bouwwerkzaamheden", GroupName = "Groep1", Checked = false });
            radioQuestion.Answers.Add(new RadioAnswer() { Text = "schoonmaakwerkzaamheden", GroupName = "Groep1", Checked = true });
            radioQuestion.Answers.Add(new RadioAnswer() { Text = "onderhoudswerkzaamheden", GroupName = "Groep1", Checked = false });
            radioQuestion.Answers.Add(new RadioAnswer() { Text = "heggen snoeien / tuin onderhoud", GroupName = "Groep1", Checked = false });
            radioQuestion.Answers.Add(new RadioAnswer() { Text = "event (vb concert)", GroupName = "Groep1", Checked = false });
            radioQuestion.Answers.Add(new RadioAnswer() { Text = "verhuizing", GroupName = "Groep1", Checked = false });

            simpleBoolQuestion.ConditionBoundQuestions = new List<Question>();
            simpleBoolQuestion.ConditionBoundQuestions.Add(simpleTextQuestion);

            questionnaire.QuestionnaireCollection.Add(simpleBoolQuestion);
            questionnaire.QuestionnaireCollection.Add(simpleTextQuestion);
            questionnaire.QuestionnaireCollection.Add(radioQuestion);

            Context.Questionnaire.Add(questionnaire);
            Context.SaveChanges();*/

            List<Questionnaire> questionnaires = Context.Questionnaire.ToList();
            ObservableCollection<QuestionnaireVM> questionnaireVMs = new ObservableCollection<QuestionnaireVM>(questionnaires.Select(x => new QuestionnaireVM(x)));


            Questionnaire = questionnaireVMs.First();
            Questionnaire.QuestionnaireCollection = new ObservableCollection<QuestionVM>(Questionnaire.QuestionnaireCollection.OrderBy(x => x.ListNumber));
            Questionnaire.Context = Context;

            AddNewQuestionCommand = new RelayCommand(AddNewQuestion);
        }

        public void AddNewQuestion()
        {
            Questionnaire.Update();
        }
    }
}
