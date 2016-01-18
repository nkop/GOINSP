using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace GOINSP.Models
{
    public class Account
    {
        public enum Rights
        {
            ExterneInspecteur,
            Manager,
            Administrator,
            InterneInspecteur            
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }

        public Rights AccountRights {get; set;}

        public string Email { get; set; }
    }
}
