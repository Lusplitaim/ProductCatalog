using Microsoft.EntityFrameworkCore;
using ProductCatalog.Core.Data.Entities;
using ProductCatalog.Core.Data.Repositories;

namespace ProductCatalog.Infrastructure.Data.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private DatabaseContext m_DbContext;
        public UserRepository(DatabaseContext dbContext)
        {
            m_DbContext = dbContext;
        }

        public async Task<ICollection<UserEntity>> GetAsync()
        {
            return await m_DbContext.Users.AsNoTracking()
                .Include(u => u.Roles)
                .ToListAsync();
        }

        public async Task<UserEntity?> GetAsync(int userId, bool track = true)
        {
            IQueryable<UserEntity> users = m_DbContext.Users;
            if (!track)
            {
                users = users.AsNoTracking();
            }

            return await users.Include(u => u.Roles).SingleOrDefaultAsync(u => u.Id == userId);
        }
    }
}
