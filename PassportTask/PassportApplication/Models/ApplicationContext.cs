using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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

        //public override int SaveChanges()
        //{
        //    Debug.WriteLine("In savechanges!");
        //    List<PassportChangesHistory> passportChanges = new List<PassportChangesHistory>();
        //    DateOnly date = DateOnly.FromDateTime(DateTime.Now);

        //    this.ChangeTracker.DetectChanges();

        //    var added = this.ChangeTracker.Entries()
        //        .Where(t => t.State == EntityState.Added)
        //        .Select(t => t.Entity as Passport)
        //        .ToList();

        //    Debug.WriteLine("Added:" + added.Count);

        //    foreach (var entity in added)
        //    {
        //        passportChanges.Add(new PassportChangesHistory { Series = entity.Series, Number = entity.Number, ChangeType = true, Date = date });
        //    }

        //    var deleted = this.ChangeTracker.Entries()
        //        .Where(t => t.State == EntityState.Deleted)
        //        .Select(t => t.Entity as Passport)
        //        .ToList();

        //    foreach (var entity in deleted)
        //    {
        //        passportChanges.Add(new PassportChangesHistory { Series = entity.Series, Number = entity.Number, ChangeType = false, Date = date });
        //    }

        //    this.BulkInsert(passportChanges);
        //    return base.SaveChanges();
        //}
    }
}
