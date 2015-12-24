using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GOINSP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using GOINSP.Models.QuestionnaireModels;
using GOINSP.ViewModel.QuestionnaireViewModels;

namespace GOINSP.ViewModel
{
    public class QuestionTemplateVM : ViewModelBase
    {
        public ICommand AddNewQuestionCommand { get; set; }

        private QuestionnaireVM questionnaire;
        public QuestionnaireVM Questionnaire
        {
            get
            {
                return questionnaire;
            }
            set
            {
                questionnaire = value;
                RaisePropertyChanged("Questionnaire");
            }
        }

        public Context Context { get; set; }

        public QuestionTemplateVM()
        {
            Context = new Context();

            /*List<Questionnaire> questionnaires = Context.Questionnaire.ToList();
            ObservableCollection<QuestionnaireVM> questionnaireVMs = new ObservableCollection<QuestionnaireVM>(questionnaires.Select(x => new QuestionnaireVM(x)));


            Questionnaire = questionnaireVMs.First();
            Questionnaire.QuestionnaireCollection = new ObservableCollection<QuestionVM>(Questionnaire.QuestionnaireCollection.OrderBy(x => x.ListNumber));*/


            // Project Questionnaire

            Questionnaire = new QuestionnaireVM();

            SimpleTextQuestionVM locationTextQuestion = new SimpleTextQuestionVM() { ListNumber = 1, Visible = Visibility.Visible, Question = "Locatie naam:" };
            SimpleTextQuestionVM addressTextQuestion = new SimpleTextQuestionVM() { ListNumber = 2, Visible = Visibility.Visible, Question = "Locatie adres:" };
            RadioQuestionVM locationTypeRadioQuestion = new RadioQuestionVM() { ListNumber = 3, Question = "Locatie type:", Visible = Visibility.Visible, AlternativeAnswerVisibility = Visibility.Collapsed };

            SimpleDateTimeQuestionVM datetimeQuestion = new SimpleDateTimeQuestionVM() { ListNumber = 4, Question = "Datum en tijd:", Visible = Visibility.Visible, Answer = DateTime.Now};
            SimpleTextQuestionVM inspectorTextQuestion = new SimpleTextQuestionVM() { ListNumber = 5, Visible = Visibility.Visible, Question = "Inspecteur:" };
            SimpleIntegerQuestionVM capacityIntegerQuestion = new SimpleIntegerQuestionVM() { ListNumber = 6, Question = "Capaciteit:", Visible = Visibility.Visible };
            SimpleIntegerQuestionVM capacityTakenIntegerQuestion = new SimpleIntegerQuestionVM() { ListNumber = 7, Question = "Aantal bezet tijdens inspectie:", Visible = Visibility.Visible };
            SimpleTextQuestionVM specialTextQuestion = new SimpleTextQuestionVM() { ListNumber = 8, Visible = Visibility.Visible, Question = "Bijzonderheden:" };
            RadioQuestionVM reasonRadioQuestion = new RadioQuestionVM() { ListNumber = 9, Question = "Reden van capaciteitvermindering:", Visible = Visibility.Visible, AlternativeAnswerVisibility = Visibility.Visible };

            DropDownQuestionVM carsOutsideDropDownQuestion = new DropDownQuestionVM() { ListNumber = 10, Visible = Visibility.Visible, Question = "Staan er auto's buiten de vakken?"};
            carsOutsideDropDownQuestion.Answers.Add("Ja");
            carsOutsideDropDownQuestion.Answers.Add("Nee");
            carsOutsideDropDownQuestion.Answers.Add("Nvt");

            SimpleIntegerQuestionVM carsOutsideCountIntegerQuestion = new SimpleIntegerQuestionVM() { ListNumber = 11, VisibleConditions = new List<string>(), Question = "Hoeveel auto's staan buiten de vakken?", Visible = Visibility.Collapsed };
            carsOutsideCountIntegerQuestion.VisibleConditions.Add("Ja");

            DropDownQuestionVM unsafeDropDownQuestion = new DropDownQuestionVM() { ListNumber = 12, VisibleConditions = new List<string>(), Visible = Visibility.Hidden, Question = "Is er hierdoor sprake van een onveilige situatie?" };
            unsafeDropDownQuestion.Answers.Add("Ja");
            unsafeDropDownQuestion.Answers.Add("Nee");
            unsafeDropDownQuestion.Answers.Add("Nvt");
            unsafeDropDownQuestion.VisibleConditions.Add("Ja");

            DropDownQuestionVM blockDropDownQuestion = new DropDownQuestionVM() { ListNumber = 13, Visible = Visibility.Visible, Question = "Blokkeren of gebruiken er autos meerdere vakken?" };
            blockDropDownQuestion.Answers.Add("Ja");
            blockDropDownQuestion.Answers.Add("Nee");
            blockDropDownQuestion.Answers.Add("Nvt");
            SimpleIntegerQuestionVM multipleCountIntegerQuestion = new SimpleIntegerQuestionVM() { ListNumber = 14, VisibleConditions = new List<string>(), Question = "Hoeveel auto's gebruiken er meerdere vakken?", Visible = Visibility.Collapsed };
            multipleCountIntegerQuestion.VisibleConditions.Add("Ja");
            SimpleIntegerQuestionVM CountIntegerQuestion = new SimpleIntegerQuestionVM() { ListNumber = 15, VisibleConditions = new List<string>(), Question = "Hoeveel vakken betreft dit?", Visible = Visibility.Collapsed };
            CountIntegerQuestion.VisibleConditions.Add("Ja");
            SimpleIntegerQuestionVM whiteCarsIntegerQuestion = new SimpleIntegerQuestionVM() { ListNumber = 16, Question = "Hoeveel witte auto's staan er?", Visible = Visibility.Visible };

            locationTypeRadioQuestion.Answers = new List<RadioAnswerVM>();
            locationTypeRadioQuestion.Answers.Add(new RadioAnswerVM() { Text = "buitenterrein publiek", GroupName = "group1", Checked = false });
            locationTypeRadioQuestion.Answers.Add(new RadioAnswerVM() { Text = "buitenterrein privaat", GroupName = "group1", Checked = false });
            locationTypeRadioQuestion.Answers.Add(new RadioAnswerVM() { Text = "garage bovengronds", GroupName = "group1", Checked = false });
            locationTypeRadioQuestion.Answers.Add(new RadioAnswerVM() { Text = "garage ondergronds", GroupName = "group1", Checked = false });

            reasonRadioQuestion.Answers = new List<RadioAnswerVM>();
            reasonRadioQuestion.Answers.Add(new RadioAnswerVM() { Text = "bouwwerkzaamheden", GroupName = "group2", Checked = false });
            reasonRadioQuestion.Answers.Add(new RadioAnswerVM() { Text = "schoonmaakwerkzaamheden", GroupName = "group2", Checked = false });
            reasonRadioQuestion.Answers.Add(new RadioAnswerVM() { Text = "onderhoudswerkzaamheden", GroupName = "group2", Checked = false });
            reasonRadioQuestion.Answers.Add(new RadioAnswerVM() { Text = "heggen snoeien / tuin onderhoud", GroupName = "group2", Checked = false });
            reasonRadioQuestion.Answers.Add(new RadioAnswerVM() { Text = "event (vb concert)", GroupName = "group2", Checked = false });
            reasonRadioQuestion.Answers.Add(new RadioAnswerVM() { Text = "verhuizing", GroupName = "group2", Checked = false });


            carsOutsideDropDownQuestion.ConditionBoundQuestions.Add(carsOutsideCountIntegerQuestion);
            carsOutsideDropDownQuestion.ConditionBoundQuestions.Add(unsafeDropDownQuestion);
            blockDropDownQuestion.ConditionBoundQuestions.Add(multipleCountIntegerQuestion);
            blockDropDownQuestion.ConditionBoundQuestions.Add(CountIntegerQuestion);

            questionnaire.QuestionnaireCollection.Add(locationTextQuestion);
            questionnaire.QuestionnaireCollection.Add(addressTextQuestion);
            questionnaire.QuestionnaireCollection.Add(locationTypeRadioQuestion);
            questionnaire.QuestionnaireCollection.Add(datetimeQuestion);
            questionnaire.QuestionnaireCollection.Add(inspectorTextQuestion);
            questionnaire.QuestionnaireCollection.Add(capacityIntegerQuestion);
            questionnaire.QuestionnaireCollection.Add(capacityTakenIntegerQuestion);
            questionnaire.QuestionnaireCollection.Add(specialTextQuestion);
            questionnaire.QuestionnaireCollection.Add(reasonRadioQuestion);
            questionnaire.QuestionnaireCollection.Add(carsOutsideDropDownQuestion);
            questionnaire.QuestionnaireCollection.Add(carsOutsideCountIntegerQuestion);
            questionnaire.QuestionnaireCollection.Add(unsafeDropDownQuestion);
            questionnaire.QuestionnaireCollection.Add(blockDropDownQuestion);
            questionnaire.QuestionnaireCollection.Add(multipleCountIntegerQuestion);
            questionnaire.QuestionnaireCollection.Add(CountIntegerQuestion);
            questionnaire.QuestionnaireCollection.Add(whiteCarsIntegerQuestion);

            Questionnaire.Context = Context;

            AddNewQuestionCommand = new RelayCommand(AddNewQuestion);
        }

        public void AddNewQuestion()
        {
            Questionnaire.Insert();
        }
    }
}
