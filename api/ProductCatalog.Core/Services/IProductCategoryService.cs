using ProductCatalog.Core.DTOs.ProductCategory;
using ProductCatalog.Core.Models;

namespace ProductCatalog.Core.Services
{
    public interface IProductCategoryService
    {
        Task<ICollection<ProductCategoryDto>> GetAsync();
        Task<ExecResult<ProductCategoryDto>> CreateAsync(CreateProductCategoryDto model);
        Task<ExecResult<ProductCategoryDto>> UpdateAsync(int categoryId, UpdateProductCategoryDto model);
        Task<ExecResult> DeleteAsync(int categoryId);
    }
}
