using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Models.QuestionnaireModels
{
    public class DropDownQuestion : Question
    {
        public DropDownQuestion()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DropDownQuestionID { get; set; }
        public string Question { get; set; }
        public string Answers { get; set; }
        public string SelectedAnswer { get; set; }
        public virtual List<Question> ConditionBoundQuestions { get; set; }
    }
}
