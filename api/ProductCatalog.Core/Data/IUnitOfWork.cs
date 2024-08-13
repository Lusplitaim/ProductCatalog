using Microsoft.EntityFrameworkCore.Storage;
using ProductCatalog.Core.Data.Repositories;

namespace ProductCatalog.Core.Data
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        IProductCategoryRepository ProductCategoryRepository { get; }
        IRolePermissionRepository RolePermissionRepository { get; }
        IProductRepository ProductRepository { get; }
        Task SaveAsync();
        IDbContextTransaction BeginTransaction();
    }
}
