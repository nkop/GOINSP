using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Models
{
    public class OpenData
    {
        public OpenData()
        {

        }

        [Key]
        public int OpenDataID { get; set; }
    }
}
