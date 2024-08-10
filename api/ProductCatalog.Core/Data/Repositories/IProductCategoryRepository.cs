using ProductCatalog.Core.Data.Entities;

namespace ProductCatalog.Core.Data.Repositories
{
    public interface IProductCategoryRepository
    {
        Task<ICollection<ProductCategoryEntity>> GetAsync();
        Task<ProductCategoryEntity?> GetAsync(int categoryId);
        Task<ProductCategoryEntity> CreateAsync(ProductCategoryEntity entity);
        void Delete(ProductCategoryEntity entity);
    }
}
