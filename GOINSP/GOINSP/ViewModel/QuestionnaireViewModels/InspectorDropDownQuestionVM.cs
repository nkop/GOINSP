using GOINSP.Models;
using GOINSP.Models.QuestionnaireModels;
using GOINSP.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.ViewModel.QuestionnaireViewModels
{
    public class InspectorDropDownQuestionVM : QuestionVM
    {
        private InspectorDropDownQuestion inspectorDropDownQuestion;

        public string Question
        {
            get
            {
                return inspectorDropDownQuestion.Question;
            }
            set
            {
                inspectorDropDownQuestion.Question = value;
                RaisePropertyChanged("Question");
            }
        }

        public string Answer
        {
            get
            {
                return inspectorDropDownQuestion.Answer;
            }
            set
            {
                inspectorDropDownQuestion.Answer = value;
                RaisePropertyChanged("Answer");
            }
        }

        public ObservableCollection<string> answers;
        public ObservableCollection<string> Answers
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

        public InspectorDropDownQuestionVM()
        {
            List<Account> tempList = Config.Context.Account.ToList(); ;
            Answers = new ObservableCollection<string>(tempList.Select(x => x.UserName));

            inspectorDropDownQuestion = new InspectorDropDownQuestion();
            base.question = inspectorDropDownQuestion;
        }

        public InspectorDropDownQuestionVM(InspectorDropDownQuestion inspectorDropDownQuestion)
            : base(inspectorDropDownQuestion)
        {
            List<Account> tempList = Config.Context.Account.ToList(); ;
            Answers = new ObservableCollection<string>(tempList.Select(x => x.UserName));
            this.inspectorDropDownQuestion = inspectorDropDownQuestion;

            this.Question = inspectorDropDownQuestion.Question;
            this.Answer = inspectorDropDownQuestion.Answer;
        }

        public InspectorDropDownQuestion Insert()
        {
            return inspectorDropDownQuestion;
        }

        public override string GetAnswer()
        {
            return Answer;
        }
    }
}