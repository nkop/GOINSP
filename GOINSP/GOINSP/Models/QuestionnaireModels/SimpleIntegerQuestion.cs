using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Models.QuestionnaireModels
{
    public class SimpleIntegerQuestion : QuestionBase
    {
        public SimpleIntegerQuestion()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SimpleIntegerQuestionID { get; set; }
        //public string Question { get; set; }
        public int Answer { get; set; }
    }
}
