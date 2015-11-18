using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace GOINSP.Models
{
    [Table("Inspection")]
    public class Inspection
    {
        public Inspection()
        {

        }

        [Key]
        public int InspectionID { get; set; }
    }
}
