﻿using GalaSoft.MvvmLight;
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
        public ICommand AddBindableQuestionCommand { get; set; }

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
            }
        }

        public DropDownQuestionAssemblerVM()
        {
            Visibility = Visibility.Collapsed;
            AssemblerName = "Meerkeuze Vraag";
            Answers = new ObservableCollection<ObservableString>();

            AnswerCount = 0;
            question = "";

            AddBindableQuestionCommand = new RelayCommand(AddBindableQuestion);
        }

        public void ChangeRadioAnswerCount()
        {
            if (Answers == null)
                return;

            if (AnswerCount < Answers.Count && AnswerCount >= 0)
            {
                Answers.Remove(Answers.Last());
            }
            else if (AnswerCount > Answers.Count)
            {
                Answers.Add(new ObservableString("", ObservableStringCallback));
            }
        }

        public int ObservableStringCallback()
        {
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

        public void Attach(DropDownQuestionVM question, QuestionnaireVM questionnaire)
        {
            attachedQuestion = question;
            Question = attachedQuestion.Question;
            Answers = new ObservableCollection<ObservableString>(attachedQuestion.Answers.Select(x => new ObservableString(x, ObservableStringCallback)).ToList());
            AnswerCount = attachedQuestion.Answers.Count;
            PossibleQuestions = new ObservableCollection<QuestionVM>(questionnaire.QuestionnaireCollection.Where(x => x.GetType() != typeof(DropDownQuestionVM)));
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
            }
        }

        public DropDownQuestionVM Create()
        {
            List<string> stringList = new List<string>();
            foreach(ObservableString obsString in Answers)
            {
                stringList.Add(obsString.ToString());
            }
            DropDownQuestionVM tempDropDownQuestion = new DropDownQuestionVM() { Question = Question, Answers = stringList, Visible = Visibility.Visible };

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
}