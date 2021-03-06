﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CpStnPRS.Models;

namespace CpStnPRS.Data
{
    public class CpStnPRSContext : DbContext
    {
        public CpStnPRSContext (DbContextOptions<CpStnPRSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CpStnPRS.Models.User> Users { get; set; }

        public virtual DbSet<CpStnPRS.Models.Vendor> Vendors { get; set; }

        public virtual DbSet<CpStnPRS.Models.Product> Products { get; set; }
        public DbSet<CpStnPRS.Models.Request> Requests { get; set; }

        public DbSet<CpStnPRS.Models.RequestLine> RequestLines { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(e =>
            {
                e.HasIndex(p => p.Username).IsUnique();
            });
            builder.Entity<Vendor>(e =>
            {
                e.HasIndex(p => p.Code).IsUnique();
            });
            builder.Entity<Product>(e =>
            {
                e.HasIndex(p => p.PartNbr).IsUnique();
            });
        }



       
    }
}
