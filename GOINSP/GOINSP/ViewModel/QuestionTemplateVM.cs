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

            AddNewQuestionCommand = new RelayCommand(AddNewQuestion);
        }

        public void AddNewQuestion() 
        {
            //Questionnaire.Insert();
            ((SimpleBoolQuestionVM)Questionnaire.QuestionnaireCollection[0]).ConditionBoundQuestions[0].Visible = Visibility.Collapsed;
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
        private Context context;
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
            questionnaire = new Questionnaire();
            questionnaire.QuestionnaireCollection = new List<Question>();
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

            foreach (SimpleBoolQuestionVM boolQuestion in QuestionnaireCollection.OfType<SimpleBoolQuestionVM>())
            {
                boolQuestion.CompileConditionBoundQuestions(QuestionnaireCollection.ToList());
            }
        }

        public void Insert()
        {
            context = new Context();

            foreach (SimpleTextQuestionVM simpleTextQuestion in QuestionnaireCollection.OfType<SimpleTextQuestionVM>())
            {
                questionnaire.QuestionnaireCollection.Add(simpleTextQuestion.Insert());
            }
            foreach (SimpleBoolQuestionVM simpleBoolQuestion in QuestionnaireCollection.OfType<SimpleBoolQuestionVM>())
            {
                questionnaire.QuestionnaireCollection.Add(simpleBoolQuestion.Insert());
            }
            foreach (RadioQuestionVM radioQuestion in QuestionnaireCollection.OfType<RadioQuestionVM>())
            {
                questionnaire.QuestionnaireCollection.Add(radioQuestion.Insert());
            }

            context.Questionnaire.Add(questionnaire);
            context.SaveChanges();
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

        public string Question
        {
            get
            {
                return simpleTextQuestion.Question;
            }
            set
            {
                simpleTextQuestion.Question = value;
                RaisePropertyChanged("Question");
            }
        }

        public string Answer
        {
            get
            {
                return simpleTextQuestion.Answer;
            }
            set
            {
                simpleTextQuestion.Answer = value;
                RaisePropertyChanged("Answer");
            }
        }

        public SimpleTextQuestionVM()
        {
            simpleTextQuestion = new SimpleTextQuestion();
            base.question = simpleTextQuestion;
        }

        public SimpleTextQuestionVM(SimpleTextQuestion simpleTextQuestion)
            : base(simpleTextQuestion)
        {
            this.simpleTextQuestion = simpleTextQuestion;

            this.Question = simpleTextQuestion.Question;
            this.Answer = simpleTextQuestion.Answer;
        }

        public SimpleTextQuestion Insert() 
        {
            return simpleTextQuestion;
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
        public List<Question> ConditionBoundQuestions { get; set; }
        public string Question { get; set; }
        public bool Answer { get; set; }
    }

    public class SimpleBoolQuestionVM : QuestionVM
    {
        private SimpleBoolQuestion simpleBoolQuestion;

        public string Question
        {
            get
            {
                return simpleBoolQuestion.Question;
            }
            set
            {
                simpleBoolQuestion.Question = value;
                RaisePropertyChanged("Question");
            }
        }

        public bool Answer
        {
            get
            {
                return simpleBoolQuestion.Answer;
            }
            set
            {
                simpleBoolQuestion.Answer = value;
                RaisePropertyChanged("Answer");
                CheckConditionBoundQuestions();
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

        public void CheckConditionBoundQuestions()
        {
            if (ConditionBoundQuestions == null)
                return;

            foreach (QuestionVM question in ConditionBoundQuestions)
            {
                if (question.VisibleCondition == Answer)
                    question.Visible = Visibility.Visible;
                else
                    question.Visible = Visibility.Collapsed;
            }
        }

        public SimpleBoolQuestionVM()
        {
            simpleBoolQuestion = new SimpleBoolQuestion();
            simpleBoolQuestion.ConditionBoundQuestions = new List<Question>();

            ConditionBoundQuestions = new List<QuestionVM>();
            base.question = simpleBoolQuestion;
        }

        public SimpleBoolQuestionVM(SimpleBoolQuestion simpleBoolQuestion)
            : base(simpleBoolQuestion)
        {
            this.simpleBoolQuestion = simpleBoolQuestion;

            Question = simpleBoolQuestion.Question;
            Answer = simpleBoolQuestion.Answer;
        }

        public void CompileConditionBoundQuestions(List<QuestionVM> originalQuestionList)
        {
            if (simpleBoolQuestion.ConditionBoundQuestions != null)
            {
                ConditionBoundQuestions = new List<QuestionVM>();

                foreach (Question question in simpleBoolQuestion.ConditionBoundQuestions)
                {
                    ConditionBoundQuestions.Add(originalQuestionList.Where(x => x.question == question).First());
                }
            }
        }

        public SimpleBoolQuestion Insert()
        {
            foreach (SimpleTextQuestionVM simpleTextQuestion in ConditionBoundQuestions.OfType<SimpleTextQuestionVM>())
            {
                simpleBoolQuestion.ConditionBoundQuestions.Add(simpleTextQuestion.Insert());
            }
            foreach (SimpleBoolQuestionVM _simpleBoolQuestion in ConditionBoundQuestions.OfType<SimpleBoolQuestionVM>())
            {
                simpleBoolQuestion.ConditionBoundQuestions.Add(_simpleBoolQuestion.Insert());
            }
            foreach (RadioQuestionVM radioQuestion in ConditionBoundQuestions.OfType<RadioQuestionVM>())
            {
                simpleBoolQuestion.ConditionBoundQuestions.Add(radioQuestion.Insert());
            }

            return simpleBoolQuestion;
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

        public string Question
        {
            get
            {
                return radioQuestion.Question;
            }
            set
            {
                radioQuestion.Question = value;
                RaisePropertyChanged("Question");
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
                RaisePropertyChanged("Answers");
            }
        }

        public string SelectedAnswer
        {
            get
            {
                return radioQuestion.SelectedAnswer;
            }
            set
            {
                radioQuestion.SelectedAnswer = value;
                RaisePropertyChanged("SelectedAnswer");
            }
        }

        public string AlternativeAnswer
        {
            get
            {
                return radioQuestion.AlternativeAnswer;
            }
            set
            {
                radioQuestion.AlternativeAnswer = value;
                RaisePropertyChanged("AlternativeAnswer");
            }
        }

        public Visibility AlternativeAnswerVisibility
        {
            get
            {
                return ConversionHelper.BoolToVisibility(radioQuestion.AlternativeAnswerVisibility);
            }
            set
            {
                radioQuestion.AlternativeAnswerVisibility = ConversionHelper.VisibilityToBool(value);
                RaisePropertyChanged("AlternativeAnswerVisibility");
            }
        }

        public RadioQuestionVM()
        {
            radioQuestion = new RadioQuestion();
            base.question = radioQuestion;
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

        public RadioQuestion Insert()
        {
            radioQuestion.Answers = Answers.Select(x => x.Insert()).ToList();

            return radioQuestion;
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
        public RadioAnswer radioAnswer { get; set; }

        public string Text
        {
            get
            {
                return radioAnswer.Text;
            }
            set
            {
                radioAnswer.Text = value;
                RaisePropertyChanged("Text");
            }
        }

        public bool Checked
        {
            get
            {
                return radioAnswer.Checked;
            }
            set
            {
                radioAnswer.Checked = value;
                RaisePropertyChanged("Checked");
            }
        }

        public string GroupName
        {
            get
            {
                return radioAnswer.GroupName;
            }
            set
            {
                radioAnswer.GroupName = value;
                RaisePropertyChanged("GroupName");
            }
        }

        public RadioAnswerVM()
        {
            radioAnswer = new RadioAnswer();
        }

        public RadioAnswerVM(RadioAnswer radioAnswer)
            : base(radioAnswer)
        {
            this.radioAnswer = radioAnswer;
            Text = radioAnswer.Text;
            Checked = radioAnswer.Checked;
            GroupName = radioAnswer.GroupName;
        }

        public RadioAnswer Insert()
        {
            return radioAnswer;
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
        public bool VisibleCondition { get; set; }
    }

    public class QuestionVM : ViewModelBase
    {
        public Question question { get; set; }

        public int ListNumber
        {
            get
            {
                return question.ListNumber;
            }
            set
            {
                question.ListNumber = value;
                RaisePropertyChanged("ListNumber");
            }
        }

        public Visibility Visible 
        {
            get
            {
                return ConversionHelper.BoolToVisibility(question.Visible);
            }
            set
            {
                question.Visible = ConversionHelper.VisibilityToBool(value);
                RaisePropertyChanged("Visible");
            } 
        }

        public bool VisibleCondition
        {
            get
            {
                return question.VisibleCondition;
            }
            set
            {
                question.VisibleCondition = value;
                RaisePropertyChanged("VisibleCondition");
            }
        }

        public QuestionVM()
        {
        }

        public QuestionVM(Question question)
        {
            this.question = question;

            this.ListNumber = question.ListNumber;
            this.Visible = ConversionHelper.BoolToVisibility(question.Visible);

            this.VisibleCondition = question.VisibleCondition;
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
