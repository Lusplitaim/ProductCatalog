using ProductCatalog.Core.Data;
using ProductCatalog.Core.DTOs.RolePermission;

namespace ProductCatalog.Core.Storages
{
    internal class RolePermissionStorage : IRolePermissionStorage
    {
        private readonly IUnitOfWork m_UnitOfWork;
        public RolePermissionStorage(IUnitOfWork uow)
        {
            m_UnitOfWork = uow;
        }

        public async Task<ICollection<RolePermissionDto>> GetByUserIdAsync(int userId)
        {
            var userRoles = await m_UnitOfWork.UserRoleRepository.GetByUserIdAsync(userId);
            var roleIds = userRoles.Select(ur => ur.RoleId).ToHashSet();

            var rolePermissions = await m_UnitOfWork.RolePermissionRepository.GetAsync(roleIds);
            return rolePermissions.Select(RolePermissionDto.From).ToList();
        }
    }
}
