using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Models
{
    public class Account
    {
        public Account()
        {

        }

        [Key]
        public int AccountID { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
