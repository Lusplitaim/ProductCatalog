using ProductCatalog.Core.Data;
using ProductCatalog.Core.DTOs.ProductCategory;
using ProductCatalog.Core.Exceptions;
using ProductCatalog.Core.Managers;
using ProductCatalog.Core.Models;
using ProductCatalog.Core.Models.Enums;
using ProductCatalog.Core.Services.Authorization;
using ProductCatalog.Core.Services.Authorization.Requirements;
using ProductCatalog.Core.Storages;

namespace ProductCatalog.Core.Services
{
    internal class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryStorage m_ProductCategoryStorage;
        private readonly IUnitOfWork m_UnitOfWork;
        private readonly ILoggerManager m_Logger;
        private readonly IAuthService m_AuthService;
        public ProductCategoryService(IProductCategoryStorage pcs, IUnitOfWork uow, ILoggerManager logger, IAuthService authService)
        {
            m_ProductCategoryStorage = pcs;
            m_UnitOfWork = uow;
            m_Logger = logger;
            m_AuthService = authService;
        }

        public async Task<ExecResult<ProductCategoryDto>> CreateAsync(CreateProductCategoryDto model)
        {
            try
            {
                await m_AuthService.AuthorizeAsync(AreaActionRequirements.CreateRequirement, Area.ProductCategories);

                using var transaction = m_UnitOfWork.BeginTransaction();

                var result = await m_ProductCategoryStorage.CreateAsync(model);
                
                transaction.Commit();

                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                m_Logger.LogError(ex, ex.Message);
                throw new Exception("Failed to create category", ex);
            }
        }

        public async Task<ExecResult<ProductCategoryDto>> UpdateAsync(int categoryId, UpdateProductCategoryDto model)
        {
            try
            {
                await m_AuthService.AuthorizeAsync(AreaActionRequirements.UpdateRequirement, Area.ProductCategories);

                using var transaction = m_UnitOfWork.BeginTransaction();

                var result = await m_ProductCategoryStorage.UpdateAsync(categoryId, model);
                
                transaction.Commit();
                
                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                m_Logger.LogError(ex, ex.Message);
                throw new Exception("Failed to update category", ex);
            }
        }

        public async Task<ExecResult> DeleteAsync(int categoryId)
        {
            try
            {
                await m_AuthService.AuthorizeAsync(AreaActionRequirements.DeleteRequirement, Area.ProductCategories);

                using var transaction = m_UnitOfWork.BeginTransaction();

                var result = await m_ProductCategoryStorage.DeleteAsync(categoryId);
                
                transaction.Commit();
                
                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                m_Logger.LogError(ex, ex.Message);
                throw new Exception("Failed to delete category", ex);
            }
        }

        public async Task<ICollection<ProductCategoryDto>> GetAsync()
        {
            try
            {
                await m_AuthService.AuthorizeAsync(AreaActionRequirements.ReadRequirement, Area.ProductCategories);
                var result = await m_ProductCategoryStorage.GetAsync();
                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                m_Logger.LogError(ex, ex.Message);
                throw new Exception("Failed to get categories", ex);
            }
        }
    }
}
