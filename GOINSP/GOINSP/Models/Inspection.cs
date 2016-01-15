using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using GOINSP.Models.QuestionnaireModels;

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
        public string description { get; set; }
        public Guid directory { get; set; }

        public virtual Account inspector { get; set; }
        public virtual Company company { get; set; }
        public virtual Questionnaire questionnaire { get; set; }
        public virtual InspectionType inspectiontype { get; set; }
    }
}
