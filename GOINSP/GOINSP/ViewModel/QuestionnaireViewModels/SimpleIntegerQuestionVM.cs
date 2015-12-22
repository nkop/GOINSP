using GOINSP.Models.QuestionnaireModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.ViewModel.QuestionnaireViewModels
{
    class SimpleIntegerQuestionVM : QuestionVM
    {
        private SimpleIntegerQuestion simpleIntegerQuestion;

        public string Question
        {
            get
            {
                return simpleIntegerQuestion.Question;
            }
            set
            {
                simpleIntegerQuestion.Question = value;
                RaisePropertyChanged("Question");
            }
        }

        public int Answer
        {
            get
            {
                return simpleIntegerQuestion.Answer;
            }
            set
            {
                simpleIntegerQuestion.Answer = value;
                RaisePropertyChanged("Answer");
            }
        }

        public SimpleIntegerQuestionVM()
        {
            simpleIntegerQuestion = new SimpleIntegerQuestion();
            base.question = simpleIntegerQuestion;
        }

        public SimpleIntegerQuestionVM(SimpleIntegerQuestion simpleIntegerQuestion)
            : base(simpleIntegerQuestion)
        {
            this.simpleIntegerQuestion = simpleIntegerQuestion;

            this.Question = simpleIntegerQuestion.Question;
            this.Answer = simpleIntegerQuestion.Answer;
        }

        public SimpleIntegerQuestion Insert()
        {
            return simpleIntegerQuestion;
        }
    }
}
