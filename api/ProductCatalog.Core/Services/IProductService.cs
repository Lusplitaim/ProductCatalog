using ProductCatalog.Core.DTOs.Product;
using ProductCatalog.Core.Models;
using ProductCatalog.Core.Services.Products;

namespace ProductCatalog.Core.Services
{
    public interface IProductService
    {
        Task<ICollection<ProductDto>> GetAsync(ProductFilters filters);
        Task<ProductDto> GetAsync(int productId);
        Task<ProductEditContext> GetContextForEditAsync(int? productId);
        Task<ProductFiltersContext> GetFiltersContextAsync();
        Task<ExecResult<ProductDto>> CreateAsync(CreateProductDto model);
        Task<ExecResult<ProductDto>> UpdateAsync(int productId, UpdateProductDto model);
        Task<ExecResult> DeleteAsync(int productId);
    }
}