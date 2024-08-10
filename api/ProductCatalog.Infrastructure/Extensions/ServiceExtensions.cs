using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Core.Data;
using ProductCatalog.Core.Data.Entities;
using ProductCatalog.Infrastructure.Data;

namespace ProductCatalog.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabaseContext(configuration);
            services.AddUnitOfWork();

            ConfigureIdentity(services);

            return services;
        }

        private static void ConfigureIdentity(IServiceCollection services)
        {
            var builder = services.AddIdentityCore<UserEntity>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 8;
                o.User.RequireUniqueEmail = true;
            })
            .AddSignInManager()
            .AddRoles<UserRoleEntity>()
            .AddRoleManager<RoleManager<UserRoleEntity>>()
            .AddEntityFrameworkStores<DatabaseContext>();
        }

        private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<DatabaseContext>(opts =>
            {
                opts.UseSqlServer(configuration["DatabaseConnections:SqlServer"]);
            });
        }
    }
}
