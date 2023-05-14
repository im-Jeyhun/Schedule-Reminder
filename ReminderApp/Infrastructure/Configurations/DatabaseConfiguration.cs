using Microsoft.EntityFrameworkCore;
using ReminderApp.DataBase;

namespace ReminderApp.Infrastructure.Configurations
{
    public static class DatabaseConfiguration
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("ReminderApp"));
            });
        }
    }
}
