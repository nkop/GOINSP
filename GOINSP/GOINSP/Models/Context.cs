using GOINSP.Models.Opendata.HuishoudelijkAfval;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Models
{
    public class Context : DbContext
    {
        public DbSet<TData> HuishoudelijkAfvalTData { get; set; }
        public DbSet<RegioS> HuishoudelijkAfvalRegioS { get; set; }
        public DbSet<Inspection> Inspection { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Location> Location { get; set; }
        
        public Context()
            : base()
        {

        }
    }
}
