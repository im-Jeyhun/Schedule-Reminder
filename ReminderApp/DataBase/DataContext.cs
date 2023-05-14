using Microsoft.EntityFrameworkCore;
using ReminderApp.DataBase.Models;
using ReminderApp.DataBase.Models.Common;
using ReminderApp.Extensions;

namespace ReminderApp.DataBase
{
    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
            :base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserActivation> UserActivations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Reminder> Reminders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly<Program>();
        }
    }


    #region Auditing

    public partial class DataContext
    {
        public override int SaveChanges()
        {
            AutoAudit();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            AutoAudit();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AutoAudit();

            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            AutoAudit();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        private void AutoAudit()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is not IAuditable auditableEntry) //Short version
                {
                    continue;
                }

                //IAuditable auditableEntry = (IAuditable)entry;

                DateTime currentDate = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    auditableEntry.CreatedAt = currentDate;
                    auditableEntry.UpdatedAt = currentDate;
                }
                else if (entry.State == EntityState.Modified)
                {
                    auditableEntry.UpdatedAt = currentDate;
                }
            }
        }
    }

    #endregion
}
