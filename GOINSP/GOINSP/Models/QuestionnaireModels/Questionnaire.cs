using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Models.QuestionnaireModels
{
    public class Questionnaire
    {
        public Questionnaire()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid QuestionnaireID { get; set; }

        public virtual List<Question> QuestionnaireCollection { get; set; }
    }
}
