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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }
        public string name { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime date { get; set; }
        public double longtitude { get; set; }
        public double latitude { get; set; }
        public string address { get; set; }
        public string zipcode { get; set; }
        [ForeignKey("inspector")]
        public Guid inspectorid { get; set; }
        [ForeignKey("company")]
        public Guid companyid { get; set; }
        public string description { get; set; }
        public string image { get; set; }

        public virtual Account inspector { get; set; }
        public virtual Company company { get; set; }
    }
}
