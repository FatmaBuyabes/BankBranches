using Microsoft.EntityFrameworkCore;

namespace BankBranches.Models
{
    public class BankContext : DbContext
    {
        public DbSet<BankBranch> BankBranches { get; set; }
        public DbSet<Employee> employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=Bank.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.CivilId)
                .IsUnique();
        }
    }
}

