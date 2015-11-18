using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Models
{
    public class Inspection
    {
        public Inspection()
        {

        }

        [Key]
        public int InspectionID { get; set; }
    }
}
