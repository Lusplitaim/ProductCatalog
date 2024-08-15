using ProductCatalog.Core.DTOs.Product;
using ProductCatalog.Core.Models;

namespace ProductCatalog.Core.Storages
{
    public interface IProductStorage
    {
        Task<ICollection<ProductDto>> GetAsync(ProductFilters filters);
        Task<ProductDto> GetAsync(int productId);
        Task<ExecResult<ProductDto>> CreateAsync(CreateProductDto model);
        Task<ExecResult<ProductDto>> UpdateAsync(int productId, UpdateProductDto model);
        Task<ExecResult> DeleteAsync(int productId);
    }
}