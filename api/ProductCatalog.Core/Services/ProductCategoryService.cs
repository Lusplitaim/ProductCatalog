using ProductCatalog.Core.Data;
using ProductCatalog.Core.DTOs.ProductCategory;
using ProductCatalog.Core.Exceptions;
using ProductCatalog.Core.Models;
using ProductCatalog.Core.Storages;

namespace ProductCatalog.Core.Services
{
    internal class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryStorage m_ProductCategoryStorage;
        private readonly IUnitOfWork m_UnitOfWork;
        public ProductCategoryService(IProductCategoryStorage pcs, IUnitOfWork uow)
        {
            m_ProductCategoryStorage = pcs;
            m_UnitOfWork = uow;
        }

        public async Task<ExecResult<ProductCategoryDto>> CreateAsync(CreateProductCategoryDto model)
        {
            try
            {
                var result = await m_ProductCategoryStorage.CreateAsync(model);
                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                throw new Exception("Failed to create category", ex);
            }
        }

        public async Task<ExecResult<ProductCategoryDto>> UpdateAsync(int categoryId, UpdateProductCategoryDto model)
        {
            try
            {
                var result = await m_ProductCategoryStorage.UpdateAsync(categoryId, model);
                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                throw new Exception("Failed to update category", ex);
            }
        }

        public async Task<ExecResult> DeleteAsync(int categoryId)
        {
            try
            {
                var result = await m_ProductCategoryStorage.DeleteAsync(categoryId);
                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                throw new Exception("Failed to delete category", ex);
            }
        }

        public async Task<ICollection<ProductCategoryDto>> GetAsync()
        {
            try
            {
                var result = await m_ProductCategoryStorage.GetAsync();
                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                throw new Exception("Failed to get categories", ex);
            }
        }
    }
}
