using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Models.Opendata.HuishoudelijkAfval
{
    public class RegioS
    {
        public RegioS()
        {

        }

        [Key]
        public string Key { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
