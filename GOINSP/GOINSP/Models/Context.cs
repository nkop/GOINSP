using GOINSP.Models.Opendata.HuishoudelijkAfval;
using GOINSP.Models.Opendata.PostCodeData;
using GOINSP.Models.QuestionnaireModels;
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
        public DbSet<Company> Company { get; set; }

        public DbSet<Questionnaire> Questionnaire { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<RadioAnswer> RadioAnswer { get; set; }
        public DbSet<DropDownQuestion> DropDownQuestion { get; set; }
        public DbSet<SimpleTextBlockQuestion> SimpleTextBlockQuestion { get; set; }
        public DbSet<SimpleTextQuestion> SimpleTextQuestion { get; set; }
        public DbSet<RadioQuestion> RadioQuestion { get; set; }
        public DbSet<SimpleDateTimeQuestion> SimpleDateQuestion { get; set; }

        public Context()
            : base("LocalContext")
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
