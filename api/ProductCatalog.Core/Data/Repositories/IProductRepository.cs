using ProductCatalog.Core.Data.Entities;
using ProductCatalog.Core.Models;

namespace ProductCatalog.Core.Data.Repositories
{
    public interface IProductRepository
    {
        Task<ICollection<ProductEntity>> GetAsync(ProductFilters filters);
        Task<ProductEntity?> GetAsync(int productId);
        Task<ProductEntity> CreateAsync(ProductEntity entity);
        void Delete(ProductEntity entity);
    }
}
