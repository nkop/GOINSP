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
    public class RadioQuestionAssemblerVM : ViewModelBase
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
            AnswerCount = 0;
            RadioAnswers = new ObservableCollection<RadioAnswerVM>();
            EmptyField = false;
            guidString = Guid.NewGuid().ToString();
            question = "";
        }

        public void ChangeRadioAnswerCount()
        {
            if (RadioAnswers == null)
                return;

            if(AnswerCount < RadioAnswers.Count)
            {
                RadioAnswers.Remove(RadioAnswers.Last());
            } 
            else if(AnswerCount > RadioAnswers.Count)
            {
                RadioAnswers.Add(new RadioAnswerVM() { Text = "", GroupName = guidString, Checked = false });
            }
        }

        public RadioQuestionVM Create()
        {
            RadioQuestionVM tempRadioQuestion = new RadioQuestionVM() { ListNumber = 1, Question = Question, Visible = Visibility.Visible, AlternativeAnswerVisibility = ConversionHelper.BoolToVisibility(EmptyField) };
            tempRadioQuestion.Answers = RadioAnswers.ToList();

            return tempRadioQuestion;
        }
    }
}
