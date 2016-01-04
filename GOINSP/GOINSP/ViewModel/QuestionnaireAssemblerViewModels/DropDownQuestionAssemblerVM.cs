using GalaSoft.MvvmLight;
using GOINSP.ViewModel.QuestionnaireViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GOINSP.ViewModel.QuestionnaireAssemblerViewModels
{
    public class DropDownQuestionAssemblerVM : ViewModelBase, IAssemblerVM
    {
        private Visibility visibility;
        public Visibility Visibility
        {
            get
            {
                return visibility;
            }
            set
            {
                visibility = value;
                RaisePropertyChanged("Visibility");
            }
        }

        public string AssemblerName { get; set; }

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
                RaisePropertyChanged("Question");
            }
        }

        private int answerCount;
        public int AnswerCount
        {
            get
            {
                return answerCount;
            }
            set
            {
                answerCount = value;
                RaisePropertyChanged("AnswerCount");
                ChangeRadioAnswerCount();
            }
        }

        private ObservableCollection<ObservableString> answers;
        public ObservableCollection<ObservableString> Answers
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

        public DropDownQuestionAssemblerVM()
        {
            Visibility = Visibility.Collapsed;
            AssemblerName = "Meerkeuze Vraag";
            Answers = new ObservableCollection<ObservableString>();

            AnswerCount = 0;
            question = "";
        }

        public void ChangeRadioAnswerCount()
        {
            if (Answers == null)
                return;

            if (AnswerCount < Answers.Count && AnswerCount >= 0)
            {
                Answers.Remove(Answers.Last());
            }
            else if (AnswerCount > Answers.Count)
            {
                Answers.Add(new ObservableString(""));
            }
        }

        public DropDownQuestionVM Create()
        {
            List<string> stringList = new List<string>();
            foreach(ObservableString obsString in Answers)
            {
                stringList.Add(obsString.ToString());
            }
            DropDownQuestionVM tempDropDownQuestion = new DropDownQuestionVM() { Question = Question, Answers = stringList, Visible = Visibility.Visible };

            return tempDropDownQuestion;
        }
    }

    public class ObservableString
    {
        public string StringObservable { get; set; }

        public ObservableString(string observableString)
        {
            this.StringObservable = observableString;
        }

        public override string ToString()
        {
            return StringObservable;
        }
    }
}
