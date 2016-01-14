using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Models.QuestionnaireModels
{
    public class SimpleTextQuestion : QuestionBase
    {
        public SimpleTextQuestion()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SimpleTextQuestionID { get; set; }
        //public string Question { get; set; }
        public string Answer { get; set; }
    }
}
