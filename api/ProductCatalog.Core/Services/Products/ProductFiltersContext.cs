using ProductCatalog.Core.DTOs.ProductCategory;

namespace ProductCatalog.Core.Services.Products
{
    public class ProductFiltersContext
    {
        public ICollection<ProductCategoryDto> Categories { get; set; } = [];
    }
}
