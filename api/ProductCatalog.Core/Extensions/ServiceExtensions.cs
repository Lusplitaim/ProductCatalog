using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Extensions.Logging;
using ProductCatalog.Core.Managers;
using ProductCatalog.Core.Models.Options;
using ProductCatalog.Core.Services;
using ProductCatalog.Core.Storages;
using ProductCatalog.Core.Utils;
using System.Text;

namespace ProductCatalog.Core.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration config)
        {
            ConfigureJWT(services, config);
            ConfigureLogging(config);

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IUserStorage, UserStorage>();
            services.AddScoped<IProductCategoryStorage, ProductCategoryStorage>();
            services.AddScoped<IProductStorage, ProductStorage>();
            services.AddScoped<IRolePermissionStorage, RolePermissionStorage>();

            services.AddScoped<IAuthUtils, AuthUtils>();
            services.AddSingleton<ILoggerManager, LoggerManager>();

            services.Configure<JwtOptions>(config.GetSection(JwtOptions.JwtSettings));

            return services;
        }

        private static void ConfigureLogging(IConfiguration config)
        {
            LogManager.Configuration = new NLogLoggingConfiguration(config.GetSection("NLog"));
        }

        private static void ConfigureJWT(IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["ValidIssuer"],
                    ValidAudience = jwtSettings["ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
                };
            });
        }
    }
}
