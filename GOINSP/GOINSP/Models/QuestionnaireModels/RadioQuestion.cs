using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Models.QuestionnaireModels
{
    public class RadioQuestion : Question
    {
        public RadioQuestion()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RadioQuestionID { get; set; }
        public string Question { get; set; }
        public virtual List<RadioAnswer> Answers { get; set; }
        public string SelectedAnswer { get; set; }
        public string AlternativeAnswer { get; set; }
        public bool AlternativeAnswerVisibility { get; set; }
    }
}
