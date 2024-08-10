using Microsoft.EntityFrameworkCore;
using ProductCatalog.Core.Data.Entities;
using ProductCatalog.Core.Data.Repositories;

namespace ProductCatalog.Infrastructure.Data.Repositories
{
    internal class ProductCategoryRepository : IProductCategoryRepository
    {
        private DatabaseContext m_DbContext;
        public ProductCategoryRepository(DatabaseContext dbContext)
        {
            m_DbContext = dbContext;
        }

        public async Task<ICollection<ProductCategoryEntity>> GetAsync()
        {
            return await m_DbContext.ProductCategories.ToListAsync();
        }

        public async Task<ProductCategoryEntity?> GetAsync(int categoryId)
        {
            return await m_DbContext.ProductCategories.SingleOrDefaultAsync(pc => pc.Id == categoryId);
        }

        public async Task<ProductCategoryEntity> CreateAsync(ProductCategoryEntity entity)
        {
            return (await m_DbContext.ProductCategories.AddAsync(entity)).Entity;
        }

        public void Delete(ProductCategoryEntity entity)
        {
            m_DbContext.ProductCategories.Remove(entity);
        }
    }
}
