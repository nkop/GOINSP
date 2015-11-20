using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Models
{
    class Context : DbContext
    {
        public DbSet<Inspection> Inspection { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Location> Location { get; set; }
        
        public Context()
            : base()
        {

        }
    }
}
