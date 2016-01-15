using GOINSP.Models.QuestionnaireModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.ViewModel.QuestionnaireViewModels
{
    public class SimpleTextBlockQuestionVM : QuestionVM
    {
        private SimpleTextBlockQuestion simpleTextBlockQuestion;

        public string Question
        {
            get
            {
                return simpleTextBlockQuestion.Question;
            }
            set
            {
                simpleTextBlockQuestion.Question = value;
                RaisePropertyChanged("Question");
            }
        }

        public string Answer
        {
            get
            {
                return simpleTextBlockQuestion.Answer;
            }
            set
            {
                simpleTextBlockQuestion.Answer = value;
                RaisePropertyChanged("Answer");
            }
        }

        public SimpleTextBlockQuestionVM()
        {
            simpleTextBlockQuestion = new SimpleTextBlockQuestion();
            base.question = simpleTextBlockQuestion;
        }

        public SimpleTextBlockQuestionVM(SimpleTextBlockQuestion simpleTextBlockQuestion)
            : base(simpleTextBlockQuestion)
        {
            this.simpleTextBlockQuestion = simpleTextBlockQuestion;

            this.Question = simpleTextBlockQuestion.Question;
            this.Answer = simpleTextBlockQuestion.Answer;
        }

        public SimpleTextBlockQuestion Insert()
        {
            return simpleTextBlockQuestion;
        }

        public override string GetAnswer()
        {
            return Answer;
        }
    }
}