using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProductCatalog.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalog.Infrastructure.Data
{
    internal class DatabaseContext : IdentityDbContext<UserEntity, UserRoleEntity, int>
    {
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ProductCategoryEntity> ProductCategories { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ProductEntity>(b =>
            {
                b.HasKey(p => p.Id);
                b.Property(p => p.Name).HasMaxLength(200);
                b.Property(p => p.Description).HasMaxLength(2000);
                b.Property(p => p.Note).HasMaxLength(200);
                b.Property(p => p.SpecialNote).HasMaxLength(200);
                b.HasOne(p => p.Category).WithMany(pc => pc.Products)
                    .HasForeignKey(p => p.CategoryId);
            });

            builder.Entity<ProductCategoryEntity>(b =>
            {
                b.HasKey(p => p.Id);
                b.Property(p => p.Name).HasMaxLength(200);
                b.HasIndex(p => p.Name).IsUnique();
            });
        }
    }
}
