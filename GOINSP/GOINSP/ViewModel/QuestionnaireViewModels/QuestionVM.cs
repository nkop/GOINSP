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
        public Question question { get; set; }

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

        public bool VisibleCondition
        {
            get
            {
                return question.VisibleCondition;
            }
            set
            {
                question.VisibleCondition = value;
                RaisePropertyChanged("VisibleCondition");
            }
        }

        public QuestionVM()
        {
        }

        public QuestionVM(Question question)
        {
            this.question = question;

            this.ListNumber = question.ListNumber;
            this.Visible = ConversionHelper.BoolToVisibility(question.Visible);

            this.VisibleCondition = question.VisibleCondition;
        }
    }
}
