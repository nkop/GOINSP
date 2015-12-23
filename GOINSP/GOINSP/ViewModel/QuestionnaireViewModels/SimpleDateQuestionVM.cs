using GOINSP.Models.QuestionnaireModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.ViewModel.QuestionnaireViewModels
{
    public class SimpleDateQuestionVM : QuestionVM
    {
        private SimpleDateQuestion simpleDateQuestion;

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

        public SimpleDateQuestionVM()
        {
            simpleDateQuestion = new SimpleDateQuestion();
            base.question = simpleDateQuestion;
        }

        public SimpleDateQuestionVM(SimpleDateQuestion simpleDateQuestion)
            : base(simpleDateQuestion)
        {
            this.simpleDateQuestion = simpleDateQuestion;

            this.Question = simpleDateQuestion.Question;
            this.Answer = simpleDateQuestion.Answer;
        }

        public SimpleDateQuestion Insert()
        {
            return simpleDateQuestion;
        }
    }
}
