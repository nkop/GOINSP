using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Models.QuestionnaireModels
{
    public class SimpleBoolQuestion : Question
    {
        public SimpleBoolQuestion()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SimpleBoolQuestionID { get; set; }
        public List<Question> ConditionBoundQuestions { get; set; }
        public string Question { get; set; }
        public bool Answer { get; set; }
    }
}
