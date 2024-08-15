using ProductCatalog.Core.DTOs.Product;
using ProductCatalog.Core.DTOs.ProductCategory;

namespace ProductCatalog.Core.Services.Products
{
    public class ProductEditContext
    {
        public ProductDto? Product { get; set; }
        public ICollection<ProductCategoryDto> Categories { get; set; }
    }
}
