﻿using System;
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

        public Context()
            : base()
        {

        }
    }
}
