using GOINSP.Models.QuestionnaireModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.ViewModel.QuestionnaireViewModels
{
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

        public override string GetAnswer()
        {
            return Answer;
        }
    }
}
