using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Models
{
    public class QuestionTemplate
    {
        public QuestionTemplate()
        {

        }

        [Key]
        public int QuestionTemplateID { get; set; }
    }
}
