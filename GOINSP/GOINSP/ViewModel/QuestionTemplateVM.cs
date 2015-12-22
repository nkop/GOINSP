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

namespace GOINSP.ViewModel
{
    public class QuestionTemplateVM : ViewModelBase
    {
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
            SimpleBoolQuestionVM simpleBool = new SimpleBoolQuestionVM() { ListNumber = 1, Visible = Visibility.Visible };
            SimpleTextQuestionVM simpleTextConditionBound = new SimpleTextQuestionVM() { ListNumber = 2, Visible = Visibility.Collapsed, VisibleCondition = true };
            RadioQuestionVM radioQuestion = new RadioQuestionVM() { ListNumber = 3, Question = "Reden van capaciteitvermindering:", Visible = Visibility.Visible, AlternativeAnswerVisibility = Visibility.Visible };

            radioQuestion.Answers = new List<RadioAnswerVM>();
            radioQuestion.Answers.Add(new RadioAnswerVM() { Text = "bouwwerkzaamheden", GroupName = "group1", Checked = false });
            radioQuestion.Answers.Add(new RadioAnswerVM() { Text = "schoonmaakwerkzaamheden", GroupName = "group1", Checked = false });
            radioQuestion.Answers.Add(new RadioAnswerVM() { Text = "onderhoudswerkzaamheden", GroupName = "group1", Checked = false });
            radioQuestion.Answers.Add(new RadioAnswerVM() { Text = "heggen snoeien / tuin onderhoud", GroupName = "group1", Checked = false });
            radioQuestion.Answers.Add(new RadioAnswerVM() { Text = "event (vb concert)", GroupName = "group1", Checked = false });
            radioQuestion.Answers.Add(new RadioAnswerVM() { Text = "verhuizing", GroupName = "group1", Checked = false });

            simpleBool.ConditionBoundQuestions = new List<QuestionVM>();
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
        }
    }


    public class Questionnaire
    {
        public Questionnaire()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid QuestionnaireID { get; set; }

        public virtual List<Question> QuestionnaireCollection { get; set; }
    }

    public class QuestionnaireVM : ViewModelBase
    {
        private Questionnaire questionnaire;

        private ObservableCollection<QuestionVM> questionnaireCollection;
        public ObservableCollection<QuestionVM> QuestionnaireCollection
        {
            get
            {
                return questionnaireCollection;
            }
            set
            {
                questionnaireCollection = value;
                RaisePropertyChanged("QuestionnaireCollection");
            }
        }

        public QuestionnaireVM()
        {

        }

        public QuestionnaireVM(Questionnaire questionnaire)
        {
            this.questionnaire = questionnaire;

            this.QuestionnaireCollection = new ObservableCollection<QuestionVM>();

            foreach (SimpleTextQuestion simpleTextQuestion in questionnaire.QuestionnaireCollection.OfType<SimpleTextQuestion>())
            {
                this.QuestionnaireCollection.Add(new SimpleTextQuestionVM(simpleTextQuestion));
            }
            foreach (SimpleBoolQuestion simpleBoolQuestion in questionnaire.QuestionnaireCollection.OfType<SimpleBoolQuestion>())
            {
                this.QuestionnaireCollection.Add(new SimpleBoolQuestionVM(simpleBoolQuestion));
            }
            foreach (RadioQuestion radioQuestion in questionnaire.QuestionnaireCollection.OfType<RadioQuestion>())
            {
                this.QuestionnaireCollection.Add(new RadioQuestionVM(radioQuestion));
            }
        }
    }

    public class SimpleTextQuestion : Question
    {
        public SimpleTextQuestion()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SimpleTextQuestionID { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }

    public class SimpleTextQuestionVM : QuestionVM
    {
        private SimpleTextQuestion simpleTextQuestion;

        private string question;
        public string Question
        {
            get
            {
                return question;
            }
            set
            {
                question = value;
            }
        }

        private string answer;
        public string Answer
        {
            get
            {
                return answer;
            }
            set
            {
                answer = value;
            }
        }

        public SimpleTextQuestionVM()
        {

        }

        public SimpleTextQuestionVM(SimpleTextQuestion simpleTextQuestion)
            : base(simpleTextQuestion)
        {
            this.simpleTextQuestion = simpleTextQuestion;

            this.Question = simpleTextQuestion.Question;
            this.Answer = simpleTextQuestion.Answer;

        }
    }

    public class SimpleBoolQuestion : Question
    {
        public SimpleBoolQuestion()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SimpleBoolQuestionID { get; set; }
        public string Question { get; set; }
        public bool Answer { get; set; }
    }

    public class SimpleBoolQuestionVM : QuestionVM
    {
        private SimpleBoolQuestion simpleBoolQuestion;

        private string question;
        public string Question
        {
            get
            {
                return question;
            }
            set
            {
                question = value;
            }
        }

        private bool answer;
        public bool Answer
        {
            get
            {
                return answer;
            }
            set
            {
                answer = value;
                CheckConditionBoundQuestions();
            }
        }

        public void CheckConditionBoundQuestions()
        {
            if (ConditionBoundQuestions == null)
                return;

            foreach (QuestionVM question in ConditionBoundQuestions)
            {
                if (question.VisibleCondition == answer)
                    question.Visible = Visibility.Visible;
                else
                    question.Visible = Visibility.Collapsed;
            }
        }

        public SimpleBoolQuestionVM()
        {

        }

        public SimpleBoolQuestionVM(SimpleBoolQuestion simpleBoolQuestion)
            : base(simpleBoolQuestion)
        {
            this.simpleBoolQuestion = simpleBoolQuestion;

            this.Question = simpleBoolQuestion.Question;
            this.Answer = simpleBoolQuestion.Answer;

        }
    }

    public class RadioQuestion : Question
    {
        public RadioQuestion()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RadioQuestionID { get; set; }
        public string Question { get; set; }
        public virtual List<RadioAnswer> Answers { get; set; }
        public string SelectedAnswer { get; set; }
        public string AlternativeAnswer { get; set; }
        public bool AlternativeAnswerVisibility { get; set; }
    }

    public class RadioQuestionVM : QuestionVM
    {
        private RadioQuestion radioQuestion;

        private string question;
        public string Question
        {
            get
            {
                return question;
            }
            set
            {
                question = value;
            }
        }

        private List<RadioAnswerVM> answers;
        public List<RadioAnswerVM> Answers
        {
            get
            {
                return answers;
            }
            set
            {
                answers = value;
            }
        }

        private string selectedAnswer;
        public string SelectedAnswer
        {
            get
            {
                return selectedAnswer;
            }
            set
            {
                selectedAnswer = value;
            }
        }

        private string alternativeAnswer;
        public string AlternativeAnswer
        {
            get
            {
                return alternativeAnswer;
            }
            set
            {
                alternativeAnswer = value;
            }
        }

        private Visibility alternativeAnswerVisibility;
        public Visibility AlternativeAnswerVisibility
        {
            get
            {
                return alternativeAnswerVisibility;
            }
            set
            {
                alternativeAnswerVisibility = value;
                RaisePropertyChanged("AlternativeAnswerVisibility");
            }
        }

        public RadioQuestionVM()
        {

        }

        public RadioQuestionVM(RadioQuestion radioQuestion)
            : base(radioQuestion)
        {
            this.radioQuestion = radioQuestion;

            Question = radioQuestion.Question;
            SelectedAnswer = radioQuestion.SelectedAnswer;
            AlternativeAnswer = radioQuestion.AlternativeAnswer;
            AlternativeAnswerVisibility = ConversionHelper.BoolToVisibility(radioQuestion.AlternativeAnswerVisibility);

            Answers = new List<RadioAnswerVM>();
            foreach(RadioAnswer answer in radioQuestion.Answers)
            {
                Answers.Add(new RadioAnswerVM(answer));
            }
        }
    }

    public class RadioAnswer : Question
    {
        public RadioAnswer()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RadioAnswerID { get; set; }
        public string Text { get; set; }
        public bool Checked { get; set; }
        public string GroupName { get; set; }
    }


    public class RadioAnswerVM : QuestionVM
    {
        private RadioAnswer radioAnswer;

        public string Text { get; set; }
        public bool Checked { get; set; }
        public string GroupName { get; set; }

        public RadioAnswerVM()
        {

        }

        public RadioAnswerVM(RadioAnswer radioAnswer)
            : base(radioAnswer)
        {
            this.radioAnswer = radioAnswer;
            Text = radioAnswer.Text;
            Checked = radioAnswer.Checked;
            GroupName = radioAnswer.GroupName;
        }
    }

    //---------------------------------------

    public class Question
    {
        public Question()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid QuestionID { get; set; }
        public int ListNumber { get; set; }
        public bool Visible { get; set; }
        public List<Question> ConditionBoundQuestions { get; set; }
        public bool VisibleCondition { get; set; }
    }

    public class QuestionVM : ViewModelBase
    {
        private Question question;

        public int ListNumber { get; set; }

        public Visibility visible;
        public Visibility Visible {
            get
            {
                return visible;
            }
            set
            {
                visible = value;
                RaisePropertyChanged("Visible");
            } 
        }

        private List<QuestionVM> conditionBoundQuestions;
        public List<QuestionVM> ConditionBoundQuestions
        {
            get
            {
                return conditionBoundQuestions;
            }
            set
            {
                conditionBoundQuestions = value;
                RaisePropertyChanged("ConditionBoundQuestions");
            }
        }

        public bool VisibleCondition { get; set; }

        public QuestionVM()
        {

        }

        public QuestionVM(Question question)
        {
            this.question = question;

            this.ListNumber = question.ListNumber;
            this.Visible = ConversionHelper.BoolToVisibility(question.Visible);

            this.VisibleCondition = question.VisibleCondition;

            this.ConditionBoundQuestions = new List<QuestionVM>();

            if (question.ConditionBoundQuestions != null)
            {
                foreach (SimpleTextQuestion simpleTextQuestion in question.ConditionBoundQuestions.OfType<SimpleTextQuestion>())
                {
                    this.ConditionBoundQuestions.Add(new SimpleTextQuestionVM(simpleTextQuestion));
                }
                foreach (SimpleBoolQuestion simpleBoolQuestion in question.ConditionBoundQuestions.OfType<SimpleBoolQuestion>())
                {
                    this.ConditionBoundQuestions.Add(new SimpleBoolQuestionVM(simpleBoolQuestion));
                }
                foreach (RadioQuestion radioQuestion in question.ConditionBoundQuestions.OfType<RadioQuestion>())
                {
                    this.ConditionBoundQuestions.Add(new RadioQuestionVM(radioQuestion));
                }
            }
        }
    }

    public class ConversionHelper
    {
        public static bool VisibilityToBool(Visibility visibility)
        {
            if (visibility == Visibility.Visible)
                return true;
            return false;
        }

        public static Visibility BoolToVisibility(bool visibility)
        {
            if (visibility)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }
    }
}
