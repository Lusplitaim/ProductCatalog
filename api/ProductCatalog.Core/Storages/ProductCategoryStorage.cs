using ProductCatalog.Core.Data;
using ProductCatalog.Core.Data.Entities;
using ProductCatalog.Core.DTOs.ProductCategory;
using ProductCatalog.Core.Exceptions;
using ProductCatalog.Core.Models;

namespace ProductCatalog.Core.Storages
{
    internal class ProductCategoryStorage : IProductCategoryStorage
    {
        private readonly IUnitOfWork m_UnitOfWork;
        public ProductCategoryStorage(IUnitOfWork uow)
        {
            m_UnitOfWork = uow;
        }

        public async Task<ICollection<ProductCategoryDto>> GetAsync()
        {
            var entities = await m_UnitOfWork.ProductCategoryRepository.GetAsync();
            return entities.Select(ProductCategoryDto.From).ToList();
        }

        public async Task<ExecResult<ProductCategoryDto>> CreateAsync(CreateProductCategoryDto model)
        {
            var result = new ExecResult<ProductCategoryDto>();

            ProductCategoryEntity entity = new()
            {
                Name = model.Name.ToLower(),
            };

            await m_UnitOfWork.ProductCategoryRepository.CreateAsync(entity);

            await m_UnitOfWork.SaveAsync();

            result.Result = ProductCategoryDto.From(entity);

            return result;
        }

        public async Task<ExecResult<ProductCategoryDto>> UpdateAsync(int categoryId, UpdateProductCategoryDto model)
        {
            var result = new ExecResult<ProductCategoryDto>();

            var entity = await m_UnitOfWork.ProductCategoryRepository.GetAsync(categoryId);
            if (entity is null)
            {
                throw new NotFoundCoreException();
            }

            entity.Name = model.Name.ToLower();

            await m_UnitOfWork.SaveAsync();

            result.Result = ProductCategoryDto.From(entity);

            return result;
        }

        public async Task<ExecResult> DeleteAsync(int categoryId)
        {
            var result = new ExecResult();

            var entity = await m_UnitOfWork.ProductCategoryRepository.GetAsync(categoryId);
            if (entity is null)
            {
                throw new NotFoundCoreException();
            }

            m_UnitOfWork.ProductCategoryRepository.Delete(entity);

            await m_UnitOfWork.SaveAsync();

            return result;
        }
    }
}
