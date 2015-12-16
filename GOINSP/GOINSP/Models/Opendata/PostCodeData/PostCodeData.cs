using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Models.Opendata.PostCodeData
{
    public class PostCodeData
    {
        public PostCodeData()
        {

        }

        [Key]
        public int id { get; set; }
        public string postcode { get; set; }
        public int postcode_id { get; set; }
        public int pnum { get; set; }
        public string pchar { get; set; }
        public int minnumber { get; set; }
        public int maxnumber { get; set; }
        public string numbertype { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public int city_id { get; set; }
        public string municipality { get; set; }
        public int municipality_id { get; set; }
        public string province { get; set; }
        public string province_code { get; set; }
        public decimal lat { get; set; }
        public decimal lon { get; set; }
        public decimal rd_x { get; set; }
        public decimal rd_y { get; set; }
        public string location_detail { get; set; }
        public DateTime changed_date { get; set; }
    }
}
