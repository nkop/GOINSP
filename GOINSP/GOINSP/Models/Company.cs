using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Models
{
    public class Company
    {
        [Key]
        public int ID { get; set; }
        public string BedrijfsNaam { get; set; }
        public string BedrijfsEmail { get; set; }
    }
}
