using GalaSoft.MvvmLight;
using GOINSP.Utility;
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
    public class RadioQuestionAssemblerVM : ViewModelBase, IAssemblerVM
    {
        RadioQuestionVM attachedQuestion;

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

        private ObservableCollection<RadioAnswerVM> radioAnswers;
        public ObservableCollection<RadioAnswerVM> RadioAnswers
        {
            get
            {
                return radioAnswers;
            }
            set
            {
                radioAnswers = value;
                RaisePropertyChanged("RadioAnswers");
            }
        }

        private bool emptyField;
        public bool EmptyField
        {
            get
            {
                return emptyField;
            }
            set
            {
                emptyField = value;
                RaisePropertyChanged("EmptyField");
            }
        }

        private string guidString; 

        public RadioQuestionAssemblerVM()
        {
            AssemblerName = "Radio Lijst Vraag";
            Clean();
        }

        public void ChangeRadioAnswerCount()
        {
            if (RadioAnswers == null)
                return;

            if(AnswerCount < RadioAnswers.Count && AnswerCount >= 0)
            {
                RadioAnswers.Remove(RadioAnswers.Last());
            } 
            else if(AnswerCount > RadioAnswers.Count)
            {
                RadioAnswers.Add(new RadioAnswerVM() { Text = "", GroupName = guidString, Checked = false });
            }
        }

        public void Attach(RadioQuestionVM question)
        {
            attachedQuestion = question;
            Question = attachedQuestion.Question;
            EmptyField = ConversionHelper.VisibilityToBool(attachedQuestion.AlternativeAnswerVisibility);
            AnswerCount = attachedQuestion.Answers.Count;

            RadioAnswers = new ObservableCollection<RadioAnswerVM>();

            foreach(RadioAnswerVM answer in attachedQuestion.Answers)
            {
                guidString = answer.GroupName;
                RadioAnswers.Add(new RadioAnswerVM() { Text = answer.Text, GroupName = guidString, Checked = false });
            }
        }

        public void Update()
        {
            if (attachedQuestion != null)
            {
                attachedQuestion.Question = Question;
                attachedQuestion.Answers = RadioAnswers.ToList();
                attachedQuestion.AlternativeAnswerVisibility = ConversionHelper.BoolToVisibility(EmptyField);
                Clean();
            }
        }

        public RadioQuestionVM Create()
        {
            RadioQuestionVM tempRadioQuestion = new RadioQuestionVM() { Question = Question, Visible = Visibility.Visible, AlternativeAnswerVisibility = ConversionHelper.BoolToVisibility(EmptyField) };
            tempRadioQuestion.Answers = RadioAnswers.ToList();
            Clean();

            return tempRadioQuestion;
        }

        public void Clean()
        {
            Visibility = Visibility.Collapsed;

            AnswerCount = 0;
            RadioAnswers = new ObservableCollection<RadioAnswerVM>();
            EmptyField = false;
            guidString = Guid.NewGuid().ToString();
            Question = "";

            attachedQuestion = null;
        }

        public void OnFocus()
        {
            Clean();
            Visibility = Visibility.Visible;
        }
    }
}
