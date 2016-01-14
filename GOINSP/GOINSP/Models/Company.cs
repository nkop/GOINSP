using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace GOINSP.Models
{
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid companyid { get; set; }
        public string BedrijfsNaam { get; set; }
        public string BedrijfsEmail { get; set; }
        public string BedrijfsAdres { get; set; }
        public string BedrijfsPostcode { get; set; }
        public string BedrijfsNummer { get; set; }
        public string BedrijfsGemeente { get; set; }
        public string BedrijfsWijk { get; set; }
        public string BedrijfsGemeenteCode { get; set; }
        public decimal BedrijfsLat { get; set; }
        public decimal BedrijfsLon { get; set; }
    }
}
