using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PassportApplication.Models;

namespace PassportApplication.Database
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
            modelBuilder.Entity<Passport>(PassportConfigure);
            modelBuilder.Entity<PassportChangesHistory>(PassportChangesHistoryConfigure);
        }

        public void PassportConfigure(EntityTypeBuilder<Passport> builder)
        {
            builder.Property(x => x.Series).HasColumnType("varchar(4)");
            builder.Property(x => x.Number).HasColumnType("varchar(6)");
        }

        public void PassportChangesHistoryConfigure(EntityTypeBuilder<PassportChangesHistory> builder)
        {
            builder.Property(x => x.Series).HasColumnType("varchar(4)");
            builder.Property(x => x.Number).HasColumnType("varchar(6)");
            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.Date);
        }
    }
}
