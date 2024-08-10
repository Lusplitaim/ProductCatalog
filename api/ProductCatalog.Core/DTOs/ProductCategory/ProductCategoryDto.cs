using ProductCatalog.Core.Data.Entities;

namespace ProductCatalog.Core.DTOs.ProductCategory
{
    public class ProductCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static ProductCategoryDto From(ProductCategoryEntity entity)
        {
            return new()
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }
    }
}
