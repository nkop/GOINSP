using GalaSoft.MvvmLight;
using GOINSP.Models.QuestionnaireModels;
using GOINSP.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GOINSP.ViewModel.QuestionnaireViewModels
{
    public class QuestionVM : ViewModelBase
    {
        public QuestionBase question { get; set; }

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

        public Guid QuestionID
        {
            get
            {
                return question.QuestionID;
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

        public bool HasFileAttached()
        {
            if(QuestionID.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string specificFolder = Path.Combine(folder, "GoInspGroepB\\QuestionImages");

                if (File.Exists(specificFolder + "\\" + QuestionID.ToString() + ".jpg")){
                    return true;
                }
            }

            return false;
        }

        private Visibility imageViewable;
        public Visibility ImageViewable
        {
            get
            {
                if (HasFileAttached())
                    return Visibility.Visible;
                return Visibility.Collapsed;
            }
            set
            {
                imageViewable = value;
                RaisePropertyChanged("ImageViewable");
            }
        }

        private Visibility imageAddable;
        public Visibility ImageAddable
        {
            get
            {
                if (!HasFileAttached())
                    return Visibility.Visible;
                return Visibility.Collapsed;
            }
            set
            {
                imageAddable = value;
                RaisePropertyChanged("ImageAddable");
            }
        }
    }
}
