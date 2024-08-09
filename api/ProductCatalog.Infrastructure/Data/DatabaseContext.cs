using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProductCatalog.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalog.Infrastructure.Data
{
    internal class DatabaseContext : IdentityDbContext<UserEntity, UserRole, int>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


        }
    }
}
