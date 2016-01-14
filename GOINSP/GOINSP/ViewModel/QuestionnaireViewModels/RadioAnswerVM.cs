using GOINSP.Models.QuestionnaireModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.ViewModel.QuestionnaireViewModels
{
    public class RadioAnswerVM : QuestionVM
    {
        public RadioAnswer radioAnswer { get; set; }

        public string Text
        {
            get
            {
                return radioAnswer.Text;
            }
            set
            {
                radioAnswer.Text = value;
                RaisePropertyChanged("Text");
            }
        }

        public bool Checked
        {
            get
            {
                return radioAnswer.Checked;
            }
            set
            {
                radioAnswer.Checked = value;
                RaisePropertyChanged("Checked");
            }
        }

        public string GroupName
        {
            get
            {
                return radioAnswer.GroupName;
            }
            set
            {
                radioAnswer.GroupName = value;
                RaisePropertyChanged("GroupName");
            }
        }

        public RadioAnswerVM()
        {
            radioAnswer = new RadioAnswer();
        }

        public RadioAnswerVM(RadioAnswer radioAnswer)
            : base(radioAnswer)
        {
            this.radioAnswer = radioAnswer;
            Text = radioAnswer.Text;
            Checked = radioAnswer.Checked;
            GroupName = radioAnswer.GroupName;
        }

        public RadioAnswer Insert()
        {
            return radioAnswer;
        }
    }
}
