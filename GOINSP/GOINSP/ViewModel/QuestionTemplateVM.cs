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
using GOINSP.ViewModel.QuestionnaireAssemblerViewModels;
using GalaSoft.MvvmLight.Messaging;
using GOINSP.Utility;

namespace GOINSP.ViewModel
{
    public class QuestionTemplateVM : ViewModelBase, INavigatableViewModel
    {
        public ICommand MoveSelectedQuestionDownCommand { get; set; }
        public ICommand MoveSelectedQuestionUpCommand { get; set; }
        public ICommand DeleteSelectedQuestionCommand { get; set; }

        public ICommand AddDropDownQuestionCommand { get; set; }
        public ICommand AddRadioQuestionCommand { get; set; }
        public ICommand AddSimpleTextBlockQuestionCommand { get; set; }
        public ICommand AddSimpleTextQuestionCommand { get; set; }
        public ICommand AddSimpleIntegerQuestionCommand { get; set; }
        public ICommand AddSimpleDateTimeQuestionCommand { get; set; }

        public ICommand UpdateDropDownQuestionCommand { get; set; }
        public ICommand UpdateRadioQuestionCommand { get; set; }
        public ICommand UpdateSimpleTextBlockQuestionCommand { get; set; }
        public ICommand UpdateSimpleTextQuestionCommand { get; set; }
        public ICommand UpdateSimpleIntegerQuestionCommand { get; set; }
        public ICommand UpdateSimpleDateTimeQuestionCommand { get; set; }

        public ICommand UpdateQuestionnaireCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }

        public INavigatableViewModel LastSender { get; set; }

        private IAssemblerVM selectedAssembler;
        public IAssemblerVM SelectedAssembler
        {
            get
            {
                return selectedAssembler;
            }
            set
            {
                selectedAssembler = value;
                RaisePropertyChanged("SelectedAssembler");
                ChangeAssemblerVisibility();
            }
        }

        private QuestionVM selectedQuestion;
        public QuestionVM SelectedQuestion
        {
            get
            {
                return selectedQuestion;
            }
            set
            {
                selectedQuestion = value;
                RaisePropertyChanged("SelectedQuestion");
                ParseQuestionToAssembler();
            }
        }

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

        private RadioQuestionAssemblerVM radioQuestionAssemblerVM;
        public RadioQuestionAssemblerVM RadioQuestionAssemblerVM
        {
            get
            {
                return radioQuestionAssemblerVM;
            }
            set
            {
                radioQuestionAssemblerVM = value;
                RaisePropertyChanged("RadioQuestionAssemblerVM");
            }
        }

        private SimpleTextBlockQuestionAssemblerVM simpleTextBlockQuestionAssemblerVM;
        public SimpleTextBlockQuestionAssemblerVM SimpleTextBlockQuestionAssemblerVM
        {
            get
            {
                return simpleTextBlockQuestionAssemblerVM;
            }
            set
            {
                simpleTextBlockQuestionAssemblerVM = value;
                RaisePropertyChanged("SimpleTextBlockQuestionAssemblerVM");
            }
        }

        private SimpleTextQuestionAssemblerVM simpleTextQuestionAssemblerVM;
        public SimpleTextQuestionAssemblerVM SimpleTextQuestionAssemblerVM
        {
            get
            {
                return simpleTextQuestionAssemblerVM;
            }
            set
            {
                simpleTextQuestionAssemblerVM = value;
                RaisePropertyChanged("SimpleTextQuestionAssemblerVM");
            }
        }

        private SimpleIntegerQuestionAssemblerVM simpleIntegerQuestionAssemblerVM;
        public SimpleIntegerQuestionAssemblerVM SimpleIntegerQuestionAssemblerVM
        {
            get
            {
                return simpleIntegerQuestionAssemblerVM;
            }
            set
            {
                simpleIntegerQuestionAssemblerVM = value;
                RaisePropertyChanged("SimpleIntegerQuestionAssemblerVM");
            }
        }

        private SimpleDateTimeQuestionAssemblerVM simpleDateTimeQuestionAssemblerVM;
        public SimpleDateTimeQuestionAssemblerVM SimpleDateTimeQuestionAssemblerVM
        {
            get
            {
                return simpleDateTimeQuestionAssemblerVM;
            }
            set
            {
                simpleDateTimeQuestionAssemblerVM = value;
                RaisePropertyChanged("SimpleDateTimeQuestionAssemblerVM");
            }
        }

        private DropDownQuestionAssemblerVM dropDownQuestionAssemblerVM;
        public DropDownQuestionAssemblerVM DropDownQuestionAssemblerVM
        {
            get
            {
                return dropDownQuestionAssemblerVM;
            }
            set
            {
                dropDownQuestionAssemblerVM = value;
                RaisePropertyChanged("DropDownQuestionAssemblerVM");
            }
        }

        private ObservableCollection<IAssemblerVM> assemblerVMList;
        public ObservableCollection<IAssemblerVM> AssemblerVMList
        {
            get
            {
                return assemblerVMList;
            }
            set
            {
                assemblerVMList = value;
                RaisePropertyChanged("AssemblerVMList");
            }
        }

        public Context Context { get; set; }

        public void SetQuestionnaire(QuestionnaireVM questionnaireVM)
        {
            this.Questionnaire = questionnaireVM;
            this.Questionnaire.Context = Context;
            this.Questionnaire.QuestionnaireCollection = new ObservableCollection<QuestionVM>(Questionnaire.QuestionnaireCollection.OrderBy(xy => xy.ListNumber));
            SetUpQuestionnaireAssembler();
        }

        public void SetUpQuestionnaireAssembler()
        {
            DropDownQuestionAssemblerVM = new DropDownQuestionAssemblerVM(Questionnaire);
            RadioQuestionAssemblerVM = new RadioQuestionAssemblerVM();
            SimpleTextQuestionAssemblerVM = new SimpleTextQuestionAssemblerVM();
            SimpleTextBlockQuestionAssemblerVM = new SimpleTextBlockQuestionAssemblerVM();
            SimpleIntegerQuestionAssemblerVM = new SimpleIntegerQuestionAssemblerVM();
            SimpleDateTimeQuestionAssemblerVM = new SimpleDateTimeQuestionAssemblerVM();

            CreateInterfaceList();
        }

        public void Show(INavigatableViewModel sender = null)
        {
            LastSender = sender;
            QuestionnaireAssembler questionnaireAssembler = new QuestionnaireAssembler();
            questionnaireAssembler.Show();
        }

        public QuestionTemplateVM()
        {
            /*List<Questionnaire> questionnaires = Context.Questionnaire.ToList();
            ObservableCollection<QuestionnaireVM> questionnaireVMs = new ObservableCollection<QuestionnaireVM>(questionnaires.Select(x => new QuestionnaireVM(x)));


            Questionnaire = questionnaireVMs.First();
            Questionnaire.QuestionnaireCollection = new ObservableCollection<QuestionVM>(Questionnaire.QuestionnaireCollection.OrderBy(x => x.ListNumber));*/


            // Project Questionnaire


            /*SimpleTextQuestionVM locationTextQuestion = new SimpleTextQuestionVM() { ListNumber = 1, Visible = Visibility.Visible, Question = "Locatie naam:" };
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
            questionnaire.QuestionnaireCollection.Add(whiteCarsIntegerQuestion);*/

            /*var test = new Guid("8F8E96EE-E9B2-E511-9BE4-AC293A96C9CA");
            var cb = from c in Context.Questionnaire.AsNoTracking().Include("QuestionnaireCollection") where c.QuestionnaireID == test select c;
            var q = cb.First();
            var s = new QuestionnaireVM(q);
            s.Context = Context;
            s.Insert();

            Questionnaire = s;*/
            
            //Questionnaire.QuestionnaireCollection = new ObservableCollection<QuestionVM>(Questionnaire.QuestionnaireCollection.OrderBy(xy => xy.ListNumber));

            MoveSelectedQuestionDownCommand = new RelayCommand(MoveSelectedQuestionDown);
            MoveSelectedQuestionUpCommand = new RelayCommand(MoveSelectedQuestionUp);
            DeleteSelectedQuestionCommand = new RelayCommand(DeleteSelectedQuestion);

            AddDropDownQuestionCommand = new RelayCommand(AddDropDownQuestion);
            AddRadioQuestionCommand = new RelayCommand(AddRadioQuestion);
            AddSimpleTextBlockQuestionCommand = new RelayCommand(AddSimpleTextBlockQuestion);
            AddSimpleTextQuestionCommand = new RelayCommand(AddSimpleTextQuestion);
            AddSimpleIntegerQuestionCommand = new RelayCommand(AddSimpleIntegerQuestion);
            AddSimpleDateTimeQuestionCommand = new RelayCommand(AddSimpleDateTimeQuestion);

            UpdateDropDownQuestionCommand = new RelayCommand(UpdateDropDownQuestion);
            UpdateRadioQuestionCommand = new RelayCommand(UpdateRadioQuestion);
            UpdateSimpleTextBlockQuestionCommand = new RelayCommand(UpdateSimpleTextBlockQuestion);
            UpdateSimpleTextQuestionCommand = new RelayCommand(UpdateSimpleTextQuestion);
            UpdateSimpleIntegerQuestionCommand = new RelayCommand(UpdateSimpleIntegerQuestion);
            UpdateSimpleDateTimeQuestionCommand = new RelayCommand(UpdateSimpleDateTimeQuestion);

            UpdateQuestionnaireCommand = new RelayCommand(UpdateQuestionnaire);
            CloseWindowCommand = new RelayCommand(CloseWindow);
        }

        public void UpdateQuestionnaire()
        {
            Questionnaire.Update();
            CloseView();
        }

        public void CreateInterfaceList()
        {
            AssemblerVMList = new ObservableCollection<IAssemblerVM>();

            AssemblerVMList.Add(RadioQuestionAssemblerVM);
            AssemblerVMList.Add(SimpleTextQuestionAssemblerVM);
            AssemblerVMList.Add(SimpleTextBlockQuestionAssemblerVM);
            AssemblerVMList.Add(SimpleIntegerQuestionAssemblerVM);
            AssemblerVMList.Add(SimpleDateTimeQuestionAssemblerVM);
            AssemblerVMList.Add(DropDownQuestionAssemblerVM);
        }

        public void MoveSelectedQuestionDown()
        {
            if (SelectedQuestion.ListNumber < Questionnaire.QuestionnaireCollection.Count-1)
            {
                QuestionVM downQuestion = questionnaire.QuestionnaireCollection.Where(x => x.ListNumber == SelectedQuestion.ListNumber + 1).First();
                downQuestion.ListNumber -= 1;
                SelectedQuestion.ListNumber += 1;
                Questionnaire.QuestionnaireCollection = new ObservableCollection<QuestionVM>(Questionnaire.QuestionnaireCollection.OrderBy(x => x.ListNumber));
            }
        }

        public void MoveSelectedQuestionUp()
        {
            if (SelectedQuestion.ListNumber > 0)
            {
                QuestionVM upQuestion = questionnaire.QuestionnaireCollection.Where(x => x.ListNumber == SelectedQuestion.ListNumber - 1).First();
                upQuestion.ListNumber += 1;
                SelectedQuestion.ListNumber -= 1;
                Questionnaire.QuestionnaireCollection = new ObservableCollection<QuestionVM>(Questionnaire.QuestionnaireCollection.OrderBy(x => x.ListNumber));
            }
        }

        public void DeleteSelectedQuestion()
        {
            if (SelectedQuestion != null)
            {
                List<QuestionVM> list = Questionnaire.QuestionnaireCollection.Where(x => x.ListNumber > SelectedQuestion.ListNumber).ToList();
                foreach (QuestionVM question in list)
                {
                    question.ListNumber -= 1;
                }
                Questionnaire.QuestionnaireCollection.Remove(SelectedQuestion);
                SelectedQuestion = null;
            }
        }

        // Add

        public void AddRadioQuestion()
        {
            AddNewQuestionToQuestionnaire(RadioQuestionAssemblerVM.Create());
            RadioQuestionAssemblerVM = new RadioQuestionAssemblerVM();
            CreateInterfaceList();
        }

        public void AddSimpleTextBlockQuestion()
        {
            AddNewQuestionToQuestionnaire(SimpleTextBlockQuestionAssemblerVM.Create());
            SimpleTextBlockQuestionAssemblerVM = new SimpleTextBlockQuestionAssemblerVM();
            CreateInterfaceList();
        }

        public void AddSimpleTextQuestion()
        {
            AddNewQuestionToQuestionnaire(SimpleTextQuestionAssemblerVM.Create());
            SimpleTextQuestionAssemblerVM = new SimpleTextQuestionAssemblerVM();
            CreateInterfaceList();
        }

        public void AddSimpleIntegerQuestion()
        {
            AddNewQuestionToQuestionnaire(SimpleIntegerQuestionAssemblerVM.Create());
            SimpleIntegerQuestionAssemblerVM = new SimpleIntegerQuestionAssemblerVM();
            CreateInterfaceList();
        }

        public void AddSimpleDateTimeQuestion()
        {
            AddNewQuestionToQuestionnaire(SimpleDateTimeQuestionAssemblerVM.Create());
            SimpleDateTimeQuestionAssemblerVM = new SimpleDateTimeQuestionAssemblerVM();
            CreateInterfaceList();
        }

        public void AddDropDownQuestion()
        {
            AddNewQuestionToQuestionnaire(DropDownQuestionAssemblerVM.Create());
            DropDownQuestionAssemblerVM = new DropDownQuestionAssemblerVM(Questionnaire);
            CreateInterfaceList();
        }

        //Update

        public void UpdateDropDownQuestion()
        {
            DropDownQuestionAssemblerVM.Update();
            DropDownQuestionAssemblerVM = new DropDownQuestionAssemblerVM(Questionnaire);
            CreateInterfaceList();
        }

        public void UpdateRadioQuestion()
        {
            RadioQuestionAssemblerVM.Update();
            RadioQuestionAssemblerVM = new RadioQuestionAssemblerVM();
            CreateInterfaceList();
        }

        public void UpdateSimpleTextBlockQuestion()
        {
            SimpleTextBlockQuestionAssemblerVM.Update();
            SimpleTextBlockQuestionAssemblerVM = new SimpleTextBlockQuestionAssemblerVM();
            CreateInterfaceList();
        }

        public void UpdateSimpleTextQuestion()
        {
            SimpleTextQuestionAssemblerVM.Update();
            SimpleTextQuestionAssemblerVM = new SimpleTextQuestionAssemblerVM();
            CreateInterfaceList();
        }

        public void UpdateSimpleIntegerQuestion()
        {
            SimpleIntegerQuestionAssemblerVM.Update();
            SimpleIntegerQuestionAssemblerVM = new SimpleIntegerQuestionAssemblerVM();
            CreateInterfaceList();
        }

        public void UpdateSimpleDateTimeQuestion()
        {
            SimpleDateTimeQuestionAssemblerVM.Update();
            SimpleDateTimeQuestionAssemblerVM = new SimpleDateTimeQuestionAssemblerVM();
            CreateInterfaceList();
        }

        public void AddNewQuestionToQuestionnaire(QuestionVM question)
        {
            int count = questionnaire.QuestionnaireCollection.Count;
            question.ListNumber = count;
            questionnaire.QuestionnaireCollection.Add(question);
            SelectedAssembler = null;
        }

        public void ParseQuestionToAssembler()
        {
            if (SelectedQuestion != null)
            {
                if (SelectedQuestion.GetType() == typeof(SimpleDateTimeQuestionVM))
                {
                    SelectedAssembler = SimpleDateTimeQuestionAssemblerVM;
                    SimpleDateTimeQuestionAssemblerVM.Attach((SimpleDateTimeQuestionVM)SelectedQuestion);
                }
                else if (SelectedQuestion.GetType() == typeof(SimpleIntegerQuestionVM))
                {
                    SelectedAssembler = SimpleIntegerQuestionAssemblerVM;
                    SimpleIntegerQuestionAssemblerVM.Attach((SimpleIntegerQuestionVM)SelectedQuestion);
                }
                else if (SelectedQuestion.GetType() == typeof(SimpleTextBlockQuestionVM))
                {

                    SelectedAssembler = SimpleTextBlockQuestionAssemblerVM;
                    SimpleTextBlockQuestionAssemblerVM.Attach((SimpleTextBlockQuestionVM)SelectedQuestion);
                }
                else if (SelectedQuestion.GetType() == typeof(SimpleTextQuestionVM))
                {
                    SelectedAssembler = SimpleTextQuestionAssemblerVM;
                    SimpleTextQuestionAssemblerVM.Attach((SimpleTextQuestionVM)SelectedQuestion);
                }
                else if (SelectedQuestion.GetType() == typeof(DropDownQuestionVM))
                {
                    SelectedAssembler = DropDownQuestionAssemblerVM;
                    DropDownQuestionAssemblerVM.Attach((DropDownQuestionVM)SelectedQuestion);
                }
                else if (SelectedQuestion.GetType() == typeof(RadioQuestionVM))
                {
                    SelectedAssembler = RadioQuestionAssemblerVM;
                    RadioQuestionAssemblerVM.Attach((RadioQuestionVM)SelectedQuestion);
                }
            }
        }

        public void ChangeAssemblerVisibility()
        {
            foreach(IAssemblerVM assembler in AssemblerVMList)
            {
                assembler.Visibility = Visibility.Collapsed;
            }

            if(SelectedAssembler != null)
                SelectedAssembler.OnFocus();
        }

        public void CloseWindow()
        {
            if(Questionnaire.IsTemplate == true)
                LastSender.Show();
        }

        public void CloseView()
        {
            Messenger.Default.Send<NotificationMessage>(
                new NotificationMessage(this, "CloseView")
            );
        }
    }
}
