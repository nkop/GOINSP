using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Models.QuestionnaireModels
{
    public class InspectorDropDownQuestion : QuestionBase
    {
        public InspectorDropDownQuestion()
        {

        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid InspectorDropDownQuestionID { get; set; }
        
        //public string Question { get; set; }

        public string Answer { get; set; }
    }
}
