﻿using GOINSP.Models.Opendata.HuishoudelijkAfval;
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
        public DbSet<Company> Company { get; set; }

        public DbSet<GOINSP.ViewModel.Questionnaire> Questionnaire { get; set; }
        public DbSet<GOINSP.ViewModel.Question> Question { get; set; }
        public DbSet<GOINSP.ViewModel.RadioAnswer> RadioAnswer { get; set; }
        public DbSet<GOINSP.ViewModel.SimpleBoolQuestion> SimpleBoolQuestion { get; set; }
        public DbSet<GOINSP.ViewModel.SimpleTextQuestion> SimpleTextQuestion { get; set; }
        public DbSet<GOINSP.ViewModel.RadioQuestion> RadioQuestion { get; set; }

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
