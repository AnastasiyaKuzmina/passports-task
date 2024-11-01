using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Npgsql;
using PassportApplication.Models;

using System.Diagnostics;

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
        public ApplicationContext(DbContextOptions<ApplicationContext> options, IConfiguration configuration) : base(options)
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
            builder.Property(p => p.Series).HasColumnType("smallint");
            builder.Property(p => p.Number).HasColumnType("integer");
            builder.HasKey(p => new { p.Series, p.Number });
            builder.Property(p => p.Active).HasDefaultValue(true);

        }

        public void PassportChangesHistoryConfigure(EntityTypeBuilder<PassportChangesHistory> builder)
        {
            builder.Property(x => x.Series).HasColumnType("smallint");
            builder.Property(x => x.Number).HasColumnType("integer");
            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.Date);
        }
    }
}
