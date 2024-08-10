using ProductCatalog.Core.Data.Entities;

namespace ProductCatalog.Core.Data.Repositories
{
    public interface IProductRepository
    {
        Task<ICollection<ProductEntity>> GetAsync();
        Task<ProductEntity?> GetAsync(int productId);
        Task<ProductEntity> CreateAsync(ProductEntity entity);
        void Delete(ProductEntity entity);
    }
}
