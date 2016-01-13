using GOINSP.Models.QuestionnaireModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GOINSP.ViewModel.QuestionnaireViewModels
{
    public class DropDownQuestionVM : QuestionVM
    {
        private DropDownQuestion dropDownQuestion;

        public string Question
        {
            get
            {
                return dropDownQuestion.Question;
            }
            set
            {
                dropDownQuestion.Question = value;
                RaisePropertyChanged("Question");
            }
        }

        private List<string> answers;
        public List<string> Answers
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
                return dropDownQuestion.SelectedAnswer;
            }
            set
            {
                dropDownQuestion.SelectedAnswer = value;
                CheckConditionBoundQuestions();
                RaisePropertyChanged("SelectedAnswer");
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
                if (question.VisibleConditions != null)
                {
                    if (question.VisibleConditions.FirstOrDefault(x => x == SelectedAnswer) != null)
                        question.Visible = Visibility.Visible;
                    else
                        question.Visible = Visibility.Collapsed;
                }
            }
        }
        
        public DropDownQuestionVM()
        {
            dropDownQuestion = new DropDownQuestion();
            dropDownQuestion.ConditionBoundQuestions = new List<Question>();

            ConditionBoundQuestions = new List<QuestionVM>();
            Answers = new List<string>();
            base.question = dropDownQuestion;
        }

        public DropDownQuestionVM(DropDownQuestion dropDownQuestion)
            : base(dropDownQuestion)
        {
            this.dropDownQuestion = dropDownQuestion;
            Answers = dropDownQuestion.Answers.Split(',').ToList();
        }

        public void CompileConditionBoundQuestions(List<QuestionVM> originalQuestionList)
        {
            if (dropDownQuestion.ConditionBoundQuestions != null)
            {
                ConditionBoundQuestions = new List<QuestionVM>();

                foreach (Question question in dropDownQuestion.ConditionBoundQuestions)
                {
                    List<QuestionVM> tempList = originalQuestionList.Where(x => x.question.QuestionID == question.QuestionID).ToList();
                    if(tempList.Count > 0)
                        ConditionBoundQuestions.Add(tempList.First());
                }
            }
        }

        public DropDownQuestion Insert()
        {
            if (ConditionBoundQuestions.Count != 0)
            {
                dropDownQuestion.ConditionBoundQuestions = new List<Models.QuestionnaireModels.Question>();

                foreach (SimpleTextQuestionVM simpleTextQuestion in ConditionBoundQuestions.OfType<SimpleTextQuestionVM>())
                {
                    dropDownQuestion.ConditionBoundQuestions.Add(simpleTextQuestion.Insert());
                }
                foreach (SimpleTextBlockQuestionVM simpleTextBlockQuestion in ConditionBoundQuestions.OfType<SimpleTextBlockQuestionVM>())
                {
                    dropDownQuestion.ConditionBoundQuestions.Add(simpleTextBlockQuestion.Insert());
                }
                foreach (DropDownQuestionVM _dropDownQuestion in ConditionBoundQuestions.OfType<DropDownQuestionVM>())
                {
                    dropDownQuestion.ConditionBoundQuestions.Add(_dropDownQuestion.Insert());
                }
                foreach (RadioQuestionVM radioQuestion in ConditionBoundQuestions.OfType<RadioQuestionVM>())
                {
                    dropDownQuestion.ConditionBoundQuestions.Add(radioQuestion.Insert());
                }
                foreach (SimpleIntegerQuestionVM simpleIntegerQuestionVM in ConditionBoundQuestions.OfType<SimpleIntegerQuestionVM>())
                {
                    dropDownQuestion.ConditionBoundQuestions.Add(simpleIntegerQuestionVM.Insert());
                }
                foreach (SimpleDateTimeQuestionVM simpleDateQuestionVM in ConditionBoundQuestions.OfType<SimpleDateTimeQuestionVM>())
                {
                    dropDownQuestion.ConditionBoundQuestions.Add(simpleDateQuestionVM.Insert());
                }

                foreach (QuestionVM question in ConditionBoundQuestions)
                {
                    question.question.VisibleConditions = question.VisibleConditions.Aggregate((c, n) => c + "," + n);
                }
            }

            if(Answers.Count != 0)
                dropDownQuestion.Answers = Answers.Aggregate((c, n) => c + "," + n);

            return dropDownQuestion;
        }
    }
}
