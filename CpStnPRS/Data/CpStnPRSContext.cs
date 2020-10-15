using System;
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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(e =>
            {
                e.HasIndex(p => p.Username).IsUnique();
            });
        }
    }
}
