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
            SimpleTextQuestion simpleTextConditionBound = new SimpleTextQuestion() { ListNumber = 2, Visible = Visibility.Hidden, VisibleCondition = true };
            SimpleTextQuestion simpleTextConditionBound2 = new SimpleTextQuestion() { ListNumber = 3, Visible = Visibility.Hidden, VisibleCondition = true };
            SimpleTextQuestion simpleTextConditionBound3 = new SimpleTextQuestion() { ListNumber = 4, Visible = Visibility.Hidden, VisibleCondition = true };
            SimpleTextQuestion simpleTextConditionBound4 = new SimpleTextQuestion() { ListNumber = 5, Visible = Visibility.Hidden, VisibleCondition = true };
            SimpleTextQuestion simpleTextConditionBound5 = new SimpleTextQuestion() { ListNumber = 6, Visible = Visibility.Hidden, VisibleCondition = true };

            simpleBool.ConditionBoundQuestions = new List<Question>();
            simpleBool.ConditionBoundQuestions.Add(simpleTextConditionBound);
            simpleBool.ConditionBoundQuestions.Add(simpleTextConditionBound2);
            simpleBool.ConditionBoundQuestions.Add(simpleTextConditionBound3);
            simpleBool.ConditionBoundQuestions.Add(simpleTextConditionBound4);
            simpleBool.ConditionBoundQuestions.Add(simpleTextConditionBound5);

            questionnaireCollection.Add(simpleBool);
            questionnaireCollection.Add(simpleTextConditionBound);
            questionnaireCollection.Add(simpleTextConditionBound2);
            questionnaireCollection.Add(simpleTextConditionBound3);
            questionnaireCollection.Add(simpleTextConditionBound4);
            questionnaireCollection.Add(simpleTextConditionBound5);
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
                    question.Visible = Visibility.Hidden;
            }
        }
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
