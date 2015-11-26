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

        public enum Rights
        {
            Default,
            ExterneInspecteur,
            Manager,
            Administrator,
            InterneInspecteur            
        }

        [Key]
        public string UserName { get; set; }

        public string Password { get; set; }

        public Rights AccountRights {get; set;}

        public string Email { get; set; }
    }
}
