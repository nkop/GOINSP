using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Models.QuestionnaireModels
{
    public class RadioAnswer : QuestionBase
    {
        public RadioAnswer()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RadioAnswerID { get; set; }
        public string Text { get; set; }
        public bool Checked { get; set; }
        public string GroupName { get; set; }
    }
}
