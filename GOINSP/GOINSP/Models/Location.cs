using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Models
{
    public class Location
    {
        public Location()
        {

        }

        [Key]
        public int LocationID { get; set; }
    }
}
