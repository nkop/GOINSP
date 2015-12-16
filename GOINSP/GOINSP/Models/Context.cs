using GOINSP.Models.Opendata.HuishoudelijkAfval;
using GOINSP.Models.Opendata.PostCodeData;
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
        public DbSet<PostCodeData> PostCodeData { get; set; }
        
        public Context()
            : base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostCodeData>().Property(x => x.lat).HasPrecision(15, 13);
            modelBuilder.Entity<PostCodeData>().Property(x => x.lon).HasPrecision(15, 13);
            modelBuilder.Entity<PostCodeData>().Property(x => x.rd_x).HasPrecision(31, 20);
            modelBuilder.Entity<PostCodeData>().Property(x => x.rd_y).HasPrecision(31, 20);
        }
    }
}
