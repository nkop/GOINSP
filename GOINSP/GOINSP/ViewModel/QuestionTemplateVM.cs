using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace GOINSP.ViewModel
{
    public class QuestionTemplateVM : ViewModelBase
    {
        private ObservableCollection<Question> questionnaireCollection;
        public ObservableCollection<Question> QuestionnaireCollection
        {
            get
            {
                return questionnaireCollection;
            }
            set
            {
                questionnaireCollection = value;
            }
        }

        public QuestionTemplateVM()
        {
            QuestionnaireCollection = new ObservableCollection<Question>();
            SimpleBoolQuestion simpleBool = new SimpleBoolQuestion() { ListNumber = 1, Visible = Visibility.Visible };
            SimpleTextQuestion simpleTextConditionBound = new SimpleTextQuestion() { ListNumber = 2, Visible = Visibility.Collapsed, VisibleCondition = true };
            RadioQuestion radioQuestion = new RadioQuestion() { ListNumber = 3, Question = "Reden van capaciteitvermindering:", Visible = Visibility.Visible, AlternativeAnswerVisibility = Visibility.Visible };

            radioQuestion.Answers = new List<RadioAnswer>();
            radioQuestion.Answers.Add(new RadioAnswer() { Text = "bouwwerkzaamheden", GroupName = "group1", Checked = false });
            radioQuestion.Answers.Add(new RadioAnswer() { Text = "schoonmaakwerkzaamheden", GroupName = "group1", Checked = false });
            radioQuestion.Answers.Add(new RadioAnswer() { Text = "onderhoudswerkzaamheden", GroupName = "group1", Checked = false });
            radioQuestion.Answers.Add(new RadioAnswer() { Text = "heggen snoeien / tuin onderhoud", GroupName = "group1", Checked = false });
            radioQuestion.Answers.Add(new RadioAnswer() { Text = "event (vb concert)", GroupName = "group1", Checked = false });
            radioQuestion.Answers.Add(new RadioAnswer() { Text = "verhuizing", GroupName = "group1", Checked = false });

            simpleBool.ConditionBoundQuestions = new List<Question>();
            simpleBool.ConditionBoundQuestions.Add(simpleTextConditionBound);

            questionnaireCollection.Add(simpleBool);
            questionnaireCollection.Add(simpleTextConditionBound);
            questionnaireCollection.Add(radioQuestion);
        }
    }

    public class SimpleTextQuestion : Question
    {
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
    }

    public class SimpleBoolQuestion : Question
    {
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
            foreach(Question question in ConditionBoundQuestions)
            {
                if (question.VisibleCondition == answer)
                    question.Visible = Visibility.Visible;
                else
                    question.Visible = Visibility.Collapsed;
            }
        }
    }

    public class RadioQuestion : Question
    {
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

        private List<RadioAnswer> answers;
        public List<RadioAnswer> Answers
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
    }

    public class RadioAnswer
    {
        public string Text { get; set; }
        public bool Checked { get; set; }
        public string GroupName { get; set; }
    }

    public class Question : ViewModelBase
    {
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

        public List<Question> ConditionBoundQuestions { get; set; }
        public bool VisibleCondition { get; set; }
    }
}
