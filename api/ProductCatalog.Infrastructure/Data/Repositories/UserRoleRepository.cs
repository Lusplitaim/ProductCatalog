using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Core.Data.Entities;
using ProductCatalog.Core.Data.Repositories;

namespace ProductCatalog.Infrastructure.Data.Repositories
{
    internal class UserRoleRepository : IUserRoleRepository
    {
        private DatabaseContext m_DbContext;
        public UserRoleRepository(DatabaseContext dbContext)
        {
            m_DbContext = dbContext;
        }

        public async Task<ICollection<IdentityUserRole<int>>> GetByUserIdAsync(int userId)
        {
            return await m_DbContext.UserRoles.Where(ur => ur.UserId == userId).ToListAsync();
        }

        public async Task AddAsync(IEnumerable<IdentityUserRole<int>> userRoles)
        {
            await m_DbContext.UserRoles.AddRangeAsync(userRoles);
        }

        public async Task<ICollection<UserRoleEntity>> GetAsync()
        {
            return await m_DbContext.Roles.AsNoTracking()
                .ToListAsync();
        }

        public void Delete(IEnumerable<IdentityUserRole<int>> userRoles)
        {
            m_DbContext.UserRoles.RemoveRange(userRoles);
        }
    }
}
