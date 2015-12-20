using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GOINSP.ViewModel
{
    public class QuestionTemplateVM : ViewModelBase
    {
        private ObservableCollection<Question> questionnaireCollection;
        public ObservableCollection<Question> QuestionnaireCollection
        {
            get
            {
                return questionnaireCollection;
            }
            set
            {
                questionnaireCollection = value;
            }
        }

        public QuestionTemplateVM()
        {
            QuestionnaireCollection = new ObservableCollection<Question>();
            QuestionnaireCollection.Add(new SimpleTextQuestion() { ListNumber = 1 });
            QuestionnaireCollection.Add(new SimpleBoolQuestion() { ListNumber = 2 });
            QuestionnaireCollection.Add(new SimpleBoolQuestion() { ListNumber = 3 });
            QuestionnaireCollection.Add(new SimpleTextQuestion() { ListNumber = 4 });
            QuestionnaireCollection.Add(new SimpleBoolQuestion() { ListNumber = 5 });
            QuestionnaireCollection.Add(new SimpleTextQuestion() { ListNumber = 6 });
        }
    }

    public class SimpleTextQuestion : Question
    {
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
            }
        }

        private string answer;
        public string Answer
        {
            get
            {
                return answer;
            }
            set
            {
                answer = value;
            }
        }
    }

    public class SimpleBoolQuestion : Question
    {
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
            }
        }

        private bool answer;
        public bool Answer
        {
            get
            {
                return answer;
            }
            set
            {
                answer = value;
            }
        }
    }

    public class Question
    {
        public int ListNumber { get; set; }
    }
}
