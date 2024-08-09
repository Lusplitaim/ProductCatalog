using Microsoft.EntityFrameworkCore.Storage;
using ProductCatalog.Core.Data;

namespace ProductCatalog.Infrastructure.Data
{
    internal class UnitOfWork : IUnitOfWork
    {
        private DatabaseContext m_DbContext;
        public UnitOfWork(DatabaseContext dbContext)
        {
            m_DbContext = dbContext;
        }

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
