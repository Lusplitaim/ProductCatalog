using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProductCatalog.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalog.Infrastructure.Data
{
    internal class DatabaseContext : IdentityDbContext<UserEntity, UserRoleEntity, int>
    {
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ProductCategoryEntity> ProductCategories { get; set; }
        public DbSet<RolePermissionEntity> RolePermissions { get; set; }

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

            builder.Entity<UserEntity>(b =>
            {
                b.HasMany(e => e.Roles)
                    .WithOne()
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<RolePermissionEntity>(b =>
            {
                b.ToTable("RolePermission");
                b.HasKey(p => new { p.RoleId, p.Area, p.Action });
                b.HasOne(e => e.Role)
                    .WithMany()
                    .HasForeignKey(e => e.RoleId);
                b.HasOne(e => e.AreaEntity)
                    .WithMany()
                    .HasForeignKey(e => e.Area);
                b.HasOne(e => e.AreaActionEntity)
                    .WithMany()
                    .HasForeignKey(e => e.Action);
            });

            builder.Entity<AreaActionEntity>(b =>
            {
                b.ToTable("AreaAction");
                b.HasKey(p => p.Id);
                b.Property(p => p.Name).HasMaxLength(200);
            });

            builder.Entity<AreaEntity>(b =>
            {
                b.ToTable("Area");
                b.HasKey(p => p.Id);
                b.Property(p => p.Name).HasMaxLength(200);
            });
        }
    }
}
