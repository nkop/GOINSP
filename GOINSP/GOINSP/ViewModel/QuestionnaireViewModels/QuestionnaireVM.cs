using GalaSoft.MvvmLight;
using GOINSP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOINSP.Models.QuestionnaireModels;
using System.Data.Entity.Infrastructure;

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

        public Guid QuestionnaireID
        {
            get
            {
                return questionnaire.QuestionnaireID;
            }
            set
            {
            }
        } 

        public bool IsTemplate
        {
            get
            {
                return questionnaire.IsTemplate;
            }
            set
            {
                questionnaire.IsTemplate = value;
                RaisePropertyChanged("IsTemplate");
            }
        } 

        public string Description
        {
            get
            {
                return questionnaire.Description;
            }
            set
            {
                questionnaire.Description = value;
                RaisePropertyChanged("Description");
            }
        }

        public string Name
        {
            get
            {
                return questionnaire.Name;
            }
            set
            {
                questionnaire.Name = value;
                RaisePropertyChanged("Name");
            }
        }

        public int QuestionCount
        {
            get
            {
                return questionnaire.QuestionnaireCollection.Count;
            }
            set
            {
            }
        }

        public Questionnaire GetQuestionnaire()
        {
            return questionnaire;
        }

        public QuestionnaireVM()
        {
            questionnaire = new Questionnaire();
            questionnaire.QuestionnaireCollection = new List<Question>();
            questionnaire.IsTemplate = false;
            QuestionnaireCollection = new ObservableCollection<QuestionVM>();
        }

        public QuestionnaireVM(Questionnaire questionnaire)
        { 
            this.questionnaire = questionnaire;
            this.IsTemplate = questionnaire.IsTemplate;
            this.QuestionnaireCollection = new ObservableCollection<QuestionVM>();

            foreach (SimpleTextQuestion simpleTextQuestion in questionnaire.QuestionnaireCollection.OfType<SimpleTextQuestion>())
            {
                this.QuestionnaireCollection.Add(new SimpleTextQuestionVM(simpleTextQuestion));
            }
            foreach (SimpleTextBlockQuestion simpleTextBlockQuestion in questionnaire.QuestionnaireCollection.OfType<SimpleTextBlockQuestion>())
            {
                this.QuestionnaireCollection.Add(new SimpleTextBlockQuestionVM(simpleTextBlockQuestion));
            }
            foreach (DropDownQuestion dropDownQuestion in questionnaire.QuestionnaireCollection.OfType<DropDownQuestion>())
            {
                this.QuestionnaireCollection.Add(new DropDownQuestionVM(dropDownQuestion));
            }
            foreach (RadioQuestion radioQuestion in questionnaire.QuestionnaireCollection.OfType<RadioQuestion>())
            {
                this.QuestionnaireCollection.Add(new RadioQuestionVM(radioQuestion));
            }
            foreach (SimpleIntegerQuestion simpleIntegerQuestion in questionnaire.QuestionnaireCollection.OfType<SimpleIntegerQuestion>())
            {
                this.QuestionnaireCollection.Add(new SimpleIntegerQuestionVM(simpleIntegerQuestion));
            }
            foreach (SimpleDateTimeQuestion simpleDateQuestion in questionnaire.QuestionnaireCollection.OfType<SimpleDateTimeQuestion>())
            {
                this.QuestionnaireCollection.Add(new SimpleDateTimeQuestionVM(simpleDateQuestion));
            }

            foreach (DropDownQuestionVM dropDownQuestion in QuestionnaireCollection.OfType<DropDownQuestionVM>())
            {
                dropDownQuestion.CompileConditionBoundQuestions(QuestionnaireCollection.ToList());
                dropDownQuestion.CheckConditionBoundQuestions();
            }
        }

        public void CheckConditionBoundQuestions()
        {
            foreach (DropDownQuestionVM dropDownQuestion in QuestionnaireCollection.OfType<DropDownQuestionVM>())
            {
                dropDownQuestion.CheckConditionBoundQuestions();
            }
        }

        public void PrepareForCRUD()
        {
            questionnaire.QuestionnaireCollection = new List<Question>();
            foreach (SimpleTextQuestionVM simpleTextQuestion in QuestionnaireCollection.OfType<SimpleTextQuestionVM>())
            {
                questionnaire.QuestionnaireCollection.Add(simpleTextQuestion.Insert());
            }
            foreach (SimpleTextBlockQuestionVM simpleTextBlockQuestion in QuestionnaireCollection.OfType<SimpleTextBlockQuestionVM>())
            {
                questionnaire.QuestionnaireCollection.Add(simpleTextBlockQuestion.Insert());
            }
            foreach (DropDownQuestionVM dropDownQuestion in QuestionnaireCollection.OfType<DropDownQuestionVM>())
            {
                questionnaire.QuestionnaireCollection.Add(dropDownQuestion.Insert());
            }
            foreach (RadioQuestionVM radioQuestion in QuestionnaireCollection.OfType<RadioQuestionVM>())
            {
                questionnaire.QuestionnaireCollection.Add(radioQuestion.Insert());
            }
            foreach (SimpleIntegerQuestionVM simpleIntegerQuestion in QuestionnaireCollection.OfType<SimpleIntegerQuestionVM>())
            {
                questionnaire.QuestionnaireCollection.Add(simpleIntegerQuestion.Insert());
            }
            foreach (SimpleDateTimeQuestionVM simpleDateQuestion in QuestionnaireCollection.OfType<SimpleDateTimeQuestionVM>())
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

        public void Duplicate()
        {
            PrepareForCRUD();
            Context.Entry(questionnaire).State = System.Data.Entity.EntityState.Detached;
            Context.Questionnaire.Add(questionnaire);
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
