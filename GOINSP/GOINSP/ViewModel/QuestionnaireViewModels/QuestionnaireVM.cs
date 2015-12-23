using GalaSoft.MvvmLight;
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
        public Context Context { get; set; }
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
            foreach (SimpleIntegerQuestion simpleIntegerQuestion in questionnaire.QuestionnaireCollection.OfType<SimpleIntegerQuestion>())
            {
                this.QuestionnaireCollection.Add(new SimpleIntegerQuestionVM(simpleIntegerQuestion));
            }
            foreach (SimpleDateQuestion simpleDateQuestion in questionnaire.QuestionnaireCollection.OfType<SimpleDateQuestion>())
            {
                this.QuestionnaireCollection.Add(new SimpleDateQuestionVM(simpleDateQuestion));
            }

            foreach (SimpleBoolQuestionVM boolQuestion in QuestionnaireCollection.OfType<SimpleBoolQuestionVM>())
            {
                boolQuestion.CompileConditionBoundQuestions(QuestionnaireCollection.ToList());
            }
        }

        public void PrepareForCRUD()
        {
            questionnaire.QuestionnaireCollection = new List<Question>();
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
            foreach (SimpleIntegerQuestionVM simpleIntegerQuestion in QuestionnaireCollection.OfType<SimpleIntegerQuestionVM>())
            {
                questionnaire.QuestionnaireCollection.Add(simpleIntegerQuestion.Insert());
            }
            foreach (SimpleDateQuestionVM simpleDateQuestion in QuestionnaireCollection.OfType<SimpleDateQuestionVM>())
            {
                questionnaire.QuestionnaireCollection.Add(simpleDateQuestion.Insert());
            }
        }

        public void Update()
        {
            PrepareForCRUD();
            Context.Entry(questionnaire).State = System.Data.Entity.EntityState.Modified;
            Context.SaveChanges();
        }

        public void Insert()
        {
            PrepareForCRUD();
            Context.Questionnaire.Add(questionnaire);
            Context.SaveChanges();
        }
    }
}
