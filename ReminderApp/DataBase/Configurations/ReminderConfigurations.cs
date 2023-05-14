using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReminderApp.DataBase.Models;

namespace ReminderApp.DataBase.Configurations
{
    public class ReminderConfigurations : IEntityTypeConfiguration<Reminder>
    {
        public void Configure(EntityTypeBuilder<Reminder> builder)
        {
            builder
               .ToTable("Reminders");

        }
    }
}
