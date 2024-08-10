using Microsoft.EntityFrameworkCore.Storage;
using ProductCatalog.Core.Data;
using ProductCatalog.Core.Data.Repositories;
using ProductCatalog.Infrastructure.Data.Repositories;

namespace ProductCatalog.Infrastructure.Data
{
    internal class UnitOfWork : IUnitOfWork
    {
        private DatabaseContext m_DbContext;
        public UnitOfWork(DatabaseContext dbContext)
        {
            m_DbContext = dbContext;
        }

        public IUserRepository UserRepository => new UserRepository(m_DbContext);
        public IUserRoleRepository UserRoleRepository => new UserRoleRepository(m_DbContext);

        public async Task SaveAsync()
        {
            await m_DbContext.SaveChangesAsync();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return m_DbContext.Database.BeginTransaction();
        }
    }
}
