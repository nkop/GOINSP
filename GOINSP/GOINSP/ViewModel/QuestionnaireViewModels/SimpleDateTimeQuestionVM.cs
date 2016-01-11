using GOINSP.Models.QuestionnaireModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.ViewModel.QuestionnaireViewModels
{
    public class SimpleDateTimeQuestionVM : QuestionVM
    {
        private SimpleDateTimeQuestion simpleDateQuestion;

        public string Question
        {
            get
            {
                return simpleDateQuestion.Question;
            }
            set
            {
                simpleDateQuestion.Question = value;
                RaisePropertyChanged("Question");
            }
        }

        public DateTime Answer
        {
            get
            {
                return simpleDateQuestion.Answer;
            }
            set
            {
                simpleDateQuestion.Answer = value;
                RaisePropertyChanged("Answer");
            }
        }

        public SimpleDateTimeQuestionVM()
        {
            simpleDateQuestion = new SimpleDateTimeQuestion();
            base.question = simpleDateQuestion;
        }

        public SimpleDateTimeQuestionVM(SimpleDateTimeQuestion simpleDateQuestion)
            : base(simpleDateQuestion)
        {
            this.simpleDateQuestion = simpleDateQuestion;

            this.Question = simpleDateQuestion.Question;
            this.Answer = simpleDateQuestion.Answer;
        }

        public SimpleDateTimeQuestion Insert()
        {
            return simpleDateQuestion;
        }
    }
}
