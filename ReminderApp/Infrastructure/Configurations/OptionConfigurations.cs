
using AspNetCoreRateLimit;
using ReminderApp.Options;

namespace ReminderApp.Infrastructure.Configurations
{
    public static class OptionConfigurations
    {
        public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<EmailConfigOptions>(configuration.GetSection(nameof(EmailConfigOptions)));
            services.Configure<TelegramConfigOptions>(configuration.GetSection(nameof(TelegramConfigOptions)));
            services.Configure<IpRateLimitOptions>(options => configuration.GetSection("IpRateLimitingSettings").Bind(options));

        }
    }
}
