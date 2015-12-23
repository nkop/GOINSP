using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Models.QuestionnaireModels
{
    public class SimpleDateQuestion : Question
    {
        public SimpleDateQuestion()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SimpleDateQuestionID { get; set; }
        public string Question { get; set; }
        public DateTime Answer { get; set; }
    }
}
