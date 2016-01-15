using GOINSP.Models.QuestionnaireModels;
using GOINSP.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GOINSP.ViewModel.QuestionnaireViewModels
{
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
            foreach (RadioAnswer answer in radioQuestion.Answers)
            {
                Answers.Add(new RadioAnswerVM(answer));
            }
        }

        public RadioQuestion Insert()
        {
            radioQuestion.Answers = Answers.Select(x => x.Insert()).ToList();

            return radioQuestion;
        }

        public override string GetAnswer()
        {
            if(AlternativeAnswer == "")
            {
                foreach(RadioAnswerVM answer in Answers)
                {
                    if(answer.Checked)
                    {
                        return answer.Text;
                    }
                }
            }
            return AlternativeAnswer;
        }
    }
}
