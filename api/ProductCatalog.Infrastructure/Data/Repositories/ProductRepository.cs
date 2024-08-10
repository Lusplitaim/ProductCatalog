using Microsoft.EntityFrameworkCore;
using ProductCatalog.Core.Data.Entities;
using ProductCatalog.Core.Data.Repositories;

namespace ProductCatalog.Infrastructure.Data.Repositories
{
    internal class ProductRepository : IProductRepository
    {
        private DatabaseContext m_DbContext;
        public ProductRepository(DatabaseContext dbContext)
        {
            m_DbContext = dbContext;
        }

        public async Task<ICollection<ProductEntity>> GetAsync()
        {
            return await m_DbContext.Products.ToListAsync();
        }

        public async Task<ProductEntity?> GetAsync(int productId)
        {
            return await m_DbContext.Products.SingleOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<ProductEntity> CreateAsync(ProductEntity entity)
        {
            return (await m_DbContext.Products.AddAsync(entity)).Entity;
        }

        public void Delete(ProductEntity entity)
        {
            m_DbContext.Products.Remove(entity);
        }
    }
}
