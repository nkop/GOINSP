using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GOINSP.ViewModel.QuestionnaireViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GOINSP.ViewModel.QuestionnaireAssemblerViewModels
{
    public class DropDownQuestionAssemblerVM : ViewModelBase, IAssemblerVM
    {
        DropDownQuestionVM attachedQuestion;
        QuestionnaireVM questionnaire;
        public ICommand AddBindableQuestionCommand { get; set; }
        public ICommand RemoveBoundDropDownQuestionCommand { get; set; }

        private Visibility visibility;
        public Visibility Visibility
        {
            get
            {
                return visibility;
            }
            set
            {
                visibility = value;
                RaisePropertyChanged("Visibility");
            }
        }

        public string AssemblerName { get; set; }

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

        private ObservableCollection<ObservableString> answers;
        public ObservableCollection<ObservableString> Answers
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

        private ObservableCollection<QuestionVM> conditionBoundQuestions;
        public ObservableCollection<QuestionVM> ConditionBoundQuestions
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

        private ObservableCollection<QuestionVM> possibleQuestions;
        public ObservableCollection<QuestionVM> PossibleQuestions
        {
            get
            {
                return possibleQuestions;
            }
            set
            {
                possibleQuestions = value;
                RaisePropertyChanged("PossibleQuestions");
            }
        }

        private ObservableCollection<BoolAndString> checkBoxAnswers;
        public ObservableCollection<BoolAndString> CheckBoxAnswers
        {
            get
            {
                return checkBoxAnswers;
            }
            set
            {
                checkBoxAnswers = value;
                RaisePropertyChanged("CheckBoxAnswers");
            }
        }
        

        private QuestionVM selectedBindableQuestion;
        public QuestionVM SelectedBindableQuestion
        {
            get
            {
                return selectedBindableQuestion;
            }
            set
            {
                selectedBindableQuestion = value;
                RaisePropertyChanged("SelectedBindableQuestion");
            }
        }

        private QuestionVM selectedBoundQuestion;
        public QuestionVM SelectedBoundQuestion
        {
            get
            {
                return selectedBoundQuestion;
            }
            set
            {
                selectedBoundQuestion = value;
                RaisePropertyChanged("SelectedBoundQuestion");
                CreateBoundQuestionBoxes();
            }
        }

        public DropDownQuestionAssemblerVM(QuestionnaireVM questionnaire)
        {
            AssemblerName = "Meerkeuze Vraag";
            Clean();

            AddBindableQuestionCommand = new RelayCommand(AddBindableQuestion);
            RemoveBoundDropDownQuestionCommand = new RelayCommand(RemoveBoundDropDownQuestion);
            
            this.questionnaire = questionnaire;
        }

        public void ChangeRadioAnswerCount()
        {
            if (Answers == null)
                return;

            if (AnswerCount < Answers.Count && AnswerCount >= 0)
            {
                Answers.Remove(Answers.Last());
                CreateBoundQuestionBoxes();
            }
            else if (AnswerCount > Answers.Count)
            {
                Answers.Add(new ObservableString("", ObservableStringCallback));
            }
        }

        public int ObservableStringCallback()
        {
            CreateBoundQuestionBoxes();
            return 0;
        }

        public void AddBindableQuestion()
        {
            if(SelectedBindableQuestion != null)
            {
                if(!ConditionBoundQuestions.Contains(SelectedBindableQuestion))
                    ConditionBoundQuestions.Add(SelectedBindableQuestion);
            }
        }

        public void RemoveBoundDropDownQuestion()
        {
            if (SelectedBoundQuestion != null)
            {
                ConditionBoundQuestions.Remove(SelectedBoundQuestion);
            }
        }

        public void CreateBoundQuestionBoxes()
        {
            if (SelectedBoundQuestion != null)
            {
                CheckBoxAnswers = new ObservableCollection<BoolAndString>();

                foreach (ObservableString answer in Answers)
                {
                    BoolAndString boolAndString = new BoolAndString(answer.ToString(), false, CheckBoxAnswerCallback);
                    if (SelectedBoundQuestion.VisibleConditions != null && SelectedBoundQuestion.VisibleConditions.Contains(answer.ToString()))
                    {
                        boolAndString.BoolObservable = true;
                    }
                    boolAndString.currentQuestion = SelectedBoundQuestion;
                    CheckBoxAnswers.Add(boolAndString);
                }
            }
        }

        public int CheckBoxAnswerCallback()
        {
            bool go = false;
            foreach(BoolAndString boolAndString in CheckBoxAnswers)
            {
                if (boolAndString.currentQuestion == SelectedBoundQuestion)
                    go = true;
            }

            if (SelectedBoundQuestion != null && CheckBoxAnswers.Count > 0 && go)
            {
                List<string> conditionList = new List<string>();
                foreach(BoolAndString boolAndString in CheckBoxAnswers)
                {
                    if (boolAndString.BoolObservable)
                        conditionList.Add(boolAndString.StringObservable);
                }
                SelectedBoundQuestion.VisibleConditions = conditionList;
            }
            return 0;
        }

        public void Attach(DropDownQuestionVM question)
        {
            attachedQuestion = question;
            Question = attachedQuestion.Question;
            Answers = new ObservableCollection<ObservableString>(attachedQuestion.Answers.Select(x => new ObservableString(x, ObservableStringCallback)).ToList());
            AnswerCount = attachedQuestion.Answers.Count;
            ConditionBoundQuestions = null;
            ConditionBoundQuestions = new ObservableCollection<QuestionVM>();
            ConditionBoundQuestions = new ObservableCollection<QuestionVM>(attachedQuestion.ConditionBoundQuestions);
        }

        public void Update()
        {
            if (attachedQuestion != null)
            {
                attachedQuestion.Question = Question;
                List<string> stringList = new List<string>();
                foreach (ObservableString obsString in Answers)
                {
                    stringList.Add(obsString.ToString());
                }
                attachedQuestion.Answers = stringList;
                attachedQuestion.ConditionBoundQuestions = null;
                attachedQuestion.ConditionBoundQuestions = new List<QuestionVM>(ConditionBoundQuestions.ToList());
                Clean();
            }
        }

        public void Clean()
        {
            Visibility = Visibility.Collapsed;
            Answers = new ObservableCollection<ObservableString>();
            AnswerCount = 0;
            Question = "";
            ConditionBoundQuestions = null;
            ConditionBoundQuestions = new ObservableCollection<QuestionVM>();
            attachedQuestion = null;
        }

        public void OnFocus()
        {
            Clean();
            PossibleQuestions = new ObservableCollection<QuestionVM>(questionnaire.QuestionnaireCollection.Where(x => x.GetType() != typeof(DropDownQuestionVM)));
            Visibility = Visibility.Visible;
        }

        public DropDownQuestionVM Create()
        {
            List<string> stringList = new List<string>();
            foreach(ObservableString obsString in Answers)
            {
                stringList.Add(obsString.ToString());
            }
            DropDownQuestionVM tempDropDownQuestion = new DropDownQuestionVM() { Question = Question, Answers = stringList, Visible = Visibility.Visible };
            tempDropDownQuestion.ConditionBoundQuestions = new List<QuestionVM>(ConditionBoundQuestions.ToList());
            Clean();

            return tempDropDownQuestion;
        }
    }

    public class ObservableString : ViewModelBase
    {
        Func<int> ObservableStringCallback;

        private string stringObservable;
        public string StringObservable
        {
            get
            {
                return stringObservable;
            }
            set
            {
                stringObservable = value;
                RaisePropertyChanged("StringObservable");
                if (ObservableStringCallback != null)
                    ObservableStringCallback();
            }
        }

        public ObservableString(string observableString, Func<int> ObservableStringCallback)
        {
            this.StringObservable = observableString;
            this.ObservableStringCallback = ObservableStringCallback;
        }

        public override string ToString()
        {
            return StringObservable;
        }
    }

    public class BoolAndString : ViewModelBase
    {
        Func<int> ObservableBoolStringCallback;
        public QuestionVM currentQuestion { get; set; }


        private string stringObservable;
        public string StringObservable
        {
            get
            {
                return stringObservable;
            }
            set
            {
                stringObservable = value;
                RaisePropertyChanged("StringObservable");
            }
        }

        private bool boolObservable;
        private bool initialized;

        public bool BoolObservable
        {
            get
            {
                return boolObservable;
            }
            set
            {
                boolObservable = value;
                RaisePropertyChanged("BoolObservable");
                if (ObservableBoolStringCallback != null && currentQuestion != null)
                    ObservableBoolStringCallback();
            }
        }

        public BoolAndString(string observableString, bool observableBool, Func<int> ObservableStringCallback)
        {
            initialized = false;
            this.StringObservable = observableString;
            this.BoolObservable = observableBool;
            this.ObservableBoolStringCallback = ObservableStringCallback;
            initialized = true;
        }

        public override string ToString()
        {
            return StringObservable;
        }
    }
}
