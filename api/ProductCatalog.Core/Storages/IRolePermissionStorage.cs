using ProductCatalog.Core.DTOs.RolePermission;

namespace ProductCatalog.Core.Storages
{
    public interface IRolePermissionStorage
    {
        Task<ICollection<RolePermissionDto>> GetByUserIdAsync(int userId);
    }
}