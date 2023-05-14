using AspNetCoreRateLimit;
using ReminderApp.Client.Services.Concretes;
using ReminderApp.Client.Services.Services;
using ReminderApp.Services.Concretes;
using ReminderApp.Services.Services;

namespace ReminderApp.Infrastructure.Configurations
{
    public static class RegisterCustomServicesConfigurations
    {
        public static void RegisterCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmailService, SMTPService>();
            services.AddScoped<ITelegramService, TelegramService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserActivationService, UserActivationService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        }
    }
}
