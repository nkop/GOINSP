﻿using GalaSoft.MvvmLight;
using GOINSP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOINSP.Models.QuestionnaireModels;

namespace GOINSP.ViewModel.QuestionnaireViewModels
{
    public class QuestionnaireVM : ViewModelBase
    {
        private Context context;
        private Questionnaire questionnaire;

        private ObservableCollection<QuestionVM> questionnaireCollection;
        public ObservableCollection<QuestionVM> QuestionnaireCollection
        {
            get
            {
                return questionnaireCollection;
            }
            set
            {
                questionnaireCollection = value;
                RaisePropertyChanged("QuestionnaireCollection");
            }
        }

        public QuestionnaireVM()
        {
            questionnaire = new Questionnaire();
            questionnaire.QuestionnaireCollection = new List<Question>();
        }

        public QuestionnaireVM(Questionnaire questionnaire)
        {
            this.questionnaire = questionnaire;

            this.QuestionnaireCollection = new ObservableCollection<QuestionVM>();

            foreach (SimpleTextQuestion simpleTextQuestion in questionnaire.QuestionnaireCollection.OfType<SimpleTextQuestion>())
            {
                this.QuestionnaireCollection.Add(new SimpleTextQuestionVM(simpleTextQuestion));
            }
            foreach (SimpleBoolQuestion simpleBoolQuestion in questionnaire.QuestionnaireCollection.OfType<SimpleBoolQuestion>())
            {
                this.QuestionnaireCollection.Add(new SimpleBoolQuestionVM(simpleBoolQuestion));
            }
            foreach (RadioQuestion radioQuestion in questionnaire.QuestionnaireCollection.OfType<RadioQuestion>())
            {
                this.QuestionnaireCollection.Add(new RadioQuestionVM(radioQuestion));
            }

            foreach (SimpleBoolQuestionVM boolQuestion in QuestionnaireCollection.OfType<SimpleBoolQuestionVM>())
            {
                boolQuestion.CompileConditionBoundQuestions(QuestionnaireCollection.ToList());
            }
        }

        public void Insert()
        {
            context = new Context();

            foreach (SimpleTextQuestionVM simpleTextQuestion in QuestionnaireCollection.OfType<SimpleTextQuestionVM>())
            {
                questionnaire.QuestionnaireCollection.Add(simpleTextQuestion.Insert());
            }
            foreach (SimpleBoolQuestionVM simpleBoolQuestion in QuestionnaireCollection.OfType<SimpleBoolQuestionVM>())
            {
                questionnaire.QuestionnaireCollection.Add(simpleBoolQuestion.Insert());
            }
            foreach (RadioQuestionVM radioQuestion in QuestionnaireCollection.OfType<RadioQuestionVM>())
            {
                questionnaire.QuestionnaireCollection.Add(radioQuestion.Insert());
            }

            context.Questionnaire.Add(questionnaire);
            context.SaveChanges();
        }
    }
}
