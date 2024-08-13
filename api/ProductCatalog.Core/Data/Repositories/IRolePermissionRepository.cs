using ProductCatalog.Core.Data.Entities;

namespace ProductCatalog.Core.Data.Repositories
{
    public interface IRolePermissionRepository
    {
        Task<ICollection<RolePermissionEntity>> GetAsync(IEnumerable<int> roles);
    }
}
