using GalaSoft.MvvmLight;
using GOINSP.Models.QuestionnaireModels;
using GOINSP.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GOINSP.ViewModel.QuestionnaireViewModels
{
    public class QuestionVM : ViewModelBase
    {
        public QuestionBase question { get; set; }

        public Guid QuestionID
        {
            get
            {
                return question.QuestionID;
            }
        }

        public int ListNumber
        {
            get
            {
                return question.ListNumber;
            }
            set
            {
                question.ListNumber = value;
                RaisePropertyChanged("ListNumber");
            }
        }

        public Visibility Visible
        {
            get
            {
                return ConversionHelper.BoolToVisibility(question.Visible);
            }
            set
            {
                question.Visible = ConversionHelper.VisibilityToBool(value);
                RaisePropertyChanged("Visible");
            }
        }

        public List<string> visibleConditions;
        public List<string> VisibleConditions
        {
            get
            {
                return visibleConditions;
            }
            set
            {
                visibleConditions = value;
                RaisePropertyChanged("VisibleCondition");
            }
        }

        public QuestionVM()
        {
            VisibleConditions = new List<string>();
        }

        public QuestionVM(QuestionBase question)
        {
            this.question = question;

            this.ListNumber = question.ListNumber;
            this.Visible = ConversionHelper.BoolToVisibility(question.Visible);

            if (question.VisibleConditions != null)
                this.VisibleConditions = question.VisibleConditions.Split(',').ToList();
        }

        public virtual string GetAnswer()
        {
            throw new NotImplementedException();
        }
    }
}
