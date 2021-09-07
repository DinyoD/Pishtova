using Microsoft.EntityFrameworkCore;
using System;

namespace Pishtova.Data
{
    public class PishtovaDbContext : DbContext
    {
        public PishtovaDbContext(DbContextOptions<PishtovaDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

    }
}
