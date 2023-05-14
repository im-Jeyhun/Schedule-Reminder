using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReminderApp.DataBase.Models;

namespace ReminderApp.DataBase.Configurations
{
    public class UserConigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
               .ToTable("Users");

            builder
               .HasOne(u => u.Role)
               .WithMany(r => r.Users)
               .HasForeignKey(u => u.RoleId);

            builder
                .HasOne(u => u.UserActivation)
                .WithOne(ua => ua.User)
                .HasForeignKey<UserActivation>(ua => ua.UserId);
        }
    }
}
