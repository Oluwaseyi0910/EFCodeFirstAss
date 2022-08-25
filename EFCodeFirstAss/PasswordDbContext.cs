using Microsoft.EntityFrameworkCore;
using System;

namespace EFCodeFirstAss.Models
{
    public class PasswordDbContext : DbContext
    {
        public PasswordDbContext()
        {

        }
        public PasswordDbContext(DbContextOptions<PasswordDbContext> options) : base(options)
        {

        }

        public DbSet<Passwords> Passwords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                object value = optionsBuilder.UseSqlServer
                    ("Server=.;Database=Passwordmanagerdb;Trusted_Connection=True; Encrypt=True;TrustServerCertificate=True;");
            }
        }

        internal int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}

