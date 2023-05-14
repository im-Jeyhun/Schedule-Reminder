using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using ReminderApp.Extensions;
using ReminderApp.Infrastructure.Configurations;
using System.Text;
using AspNetCore.IServiceCollection.AddIUrlHelper;
using ReminderApp.CustomExceptionHandler.Concretes;
using ReminderApp.CustomExceptionHandler;
using ReminderApp.BackgroundService;
using AspNetCoreRateLimit;

namespace ReminderApp.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(o =>
               {
                   o.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidIssuer = configuration["JwtOptinos:Issuer"],
                       ValidAudience = configuration["JwtOptinos:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey
                                      (Encoding.UTF8.GetBytes(configuration["JwtOptinos:Key"]))
                   };
               });

            services.AddHttpContextAccessor();
            services.AddHostedService<ReminderService>();


            services.AddScoped<BadRequestExceptionHandler>();
            services.AddScoped<NotFoundExceptionHandler>();
            services.AddScoped<ExceptionHandlerCoordinator>();

            services.AddAutoMapper(typeof(Program));

            services.AddLocalization();

            services.AddMemoryCache();


            services.AddInMemoryRateLimiting();

            services.AddUrlHelper();
            services.AddSignalR();
            services.Configure<ApiBehaviorOptions>(o =>
            {
                o.InvalidModelStateResponseFactory =
                (ac) => new BadRequestObjectResult(new { Errors = ac.ModelState.GenerateCustomErrorModel() });
            });
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(o =>
            {
                o.AddSecurityDefinition("Auth", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please Enter Token",
                    Name = HeaderNames.Authorization,
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                o.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                            Name=HeaderNames.Authorization,
                            Reference=new OpenApiReference{ Type=ReferenceType.SecurityScheme,Id="Auth"}
                    },
                    new string[]{ }
                }
            });
            });

            services.ConfigureDatabase(configuration);

            services.ConfigureOptions(configuration);

            services.ConfigureFluentValidatios(configuration);

            services.RegisterCustomServices(configuration);
        }
    }
}
