using GOINSP.Models.QuestionnaireModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GOINSP.ViewModel.QuestionnaireViewModels
{
    public class SimpleBoolQuestionVM : QuestionVM
    {
        private SimpleBoolQuestion simpleBoolQuestion;

        public string Question
        {
            get
            {
                return simpleBoolQuestion.Question;
            }
            set
            {
                simpleBoolQuestion.Question = value;
                RaisePropertyChanged("Question");
            }
        }

        public bool Answer
        {
            get
            {
                return simpleBoolQuestion.Answer;
            }
            set
            {
                simpleBoolQuestion.Answer = value;
                RaisePropertyChanged("Answer");
                CheckConditionBoundQuestions();
            }
        }

        private List<QuestionVM> conditionBoundQuestions;
        public List<QuestionVM> ConditionBoundQuestions
        {
            get
            {
                return conditionBoundQuestions;
            }
            set
            {
                conditionBoundQuestions = value;
                RaisePropertyChanged("ConditionBoundQuestions");
            }
        }

        public void CheckConditionBoundQuestions()
        {
            if (ConditionBoundQuestions == null)
                return;

            foreach (QuestionVM question in ConditionBoundQuestions)
            {
                if (question.VisibleCondition == Answer)
                    question.Visible = Visibility.Visible;
                else
                    question.Visible = Visibility.Collapsed;
            }
        }

        public SimpleBoolQuestionVM()
        {
            simpleBoolQuestion = new SimpleBoolQuestion();
            simpleBoolQuestion.ConditionBoundQuestions = new List<Question>();

            ConditionBoundQuestions = new List<QuestionVM>();
            base.question = simpleBoolQuestion;
        }

        public SimpleBoolQuestionVM(SimpleBoolQuestion simpleBoolQuestion)
            : base(simpleBoolQuestion)
        {
            this.simpleBoolQuestion = simpleBoolQuestion;

            Question = simpleBoolQuestion.Question;
            Answer = simpleBoolQuestion.Answer;
        }

        public void CompileConditionBoundQuestions(List<QuestionVM> originalQuestionList)
        {
            if (simpleBoolQuestion.ConditionBoundQuestions != null)
            {
                ConditionBoundQuestions = new List<QuestionVM>();

                foreach (Question question in simpleBoolQuestion.ConditionBoundQuestions)
                {
                    ConditionBoundQuestions.Add(originalQuestionList.Where(x => x.question == question).First());
                }
            }
        }

        public SimpleBoolQuestion Insert()
        {
            foreach (SimpleTextQuestionVM simpleTextQuestion in ConditionBoundQuestions.OfType<SimpleTextQuestionVM>())
            {
                simpleBoolQuestion.ConditionBoundQuestions.Add(simpleTextQuestion.Insert());
            }
            foreach (SimpleBoolQuestionVM _simpleBoolQuestion in ConditionBoundQuestions.OfType<SimpleBoolQuestionVM>())
            {
                simpleBoolQuestion.ConditionBoundQuestions.Add(_simpleBoolQuestion.Insert());
            }
            foreach (RadioQuestionVM radioQuestion in ConditionBoundQuestions.OfType<RadioQuestionVM>())
            {
                simpleBoolQuestion.ConditionBoundQuestions.Add(radioQuestion.Insert());
            }

            return simpleBoolQuestion;
        }
    }
}
