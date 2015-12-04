﻿using System;
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
        public DateTime date { get; set; }
        public double longtitude { get; set; }
        public double latitude { get; set; }
        public string address { get; set; }
        [ForeignKey("inspector")]
        public Guid inspectorid { get; set; }
        public string description { get; set; }
        public string image { get; set; }

        public virtual Account inspector { get; set; }
    }
}
