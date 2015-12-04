using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Models.Opendata.GemeenteGrenzen
{
    public class Grenzen
    {
        public Grenzen()
        {

        }

        [Key]
        public int ID { get; set; }
        public string GMCode { get; set; }
        public string GMNaam { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
