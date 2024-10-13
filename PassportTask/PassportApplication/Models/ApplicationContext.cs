using Microsoft.EntityFrameworkCore;

namespace PassportApplication.Models
{
    /// <summary>
    /// Application context class
    /// </summary>
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// Passports table
        /// </summary>
        public DbSet<Passport> Passports { get; set; } = null!;

        /// <summary>
        /// Passports changes history table
        /// </summary>
        public DbSet<PassportChangesHistory> PassportsChangesHistory { get; set; } = null!;

        /// <summary>
        /// Constructor of ApplicationContext
        /// </summary>
        /// <param name="options">Application context options</param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Passport>().HasKey(p => new { p.Series, p.Number });
            modelBuilder.Entity<PassportChangesHistory>().HasKey(p => p.Id);
            modelBuilder.Entity<PassportChangesHistory>().HasIndex(p => p.Date);
        }
    }
}
