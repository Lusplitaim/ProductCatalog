using Microsoft.EntityFrameworkCore;
using ProductCatalog.Core.Data.Entities;
using ProductCatalog.Core.Data.Repositories;

namespace ProductCatalog.Infrastructure.Data.Repositories
{
    internal class RolePermissionRepository : IRolePermissionRepository
    {
        private DatabaseContext m_DbContext;
        public RolePermissionRepository(DatabaseContext dbContext)
        {
            m_DbContext = dbContext;
        }

        public async Task<ICollection<RolePermissionEntity>> GetAsync(IEnumerable<int> roles)
        {
            return await m_DbContext.RolePermissions.Where(rp => roles.Contains(rp.RoleId)).ToListAsync();
        }
    }
}
