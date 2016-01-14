using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Models.QuestionnaireModels
{
    public class SimpleTextBlockQuestion : QuestionBase
    {
        public SimpleTextBlockQuestion()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SimpleTextBlockQuestionID { get; set; }
        //public string Question { get; set; }
        public string Answer { get; set; }
    }
}

