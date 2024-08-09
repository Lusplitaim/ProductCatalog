using Microsoft.EntityFrameworkCore.Storage;

namespace ProductCatalog.Core.Data
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
        IDbContextTransaction BeginTransaction();
    }
}
