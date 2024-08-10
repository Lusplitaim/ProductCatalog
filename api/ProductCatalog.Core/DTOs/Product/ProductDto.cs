using ProductCatalog.Core.Data.Entities;
using ProductCatalog.Core.DTOs.ProductCategory;

namespace ProductCatalog.Core.DTOs.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string? Note { get; set; }
        public string? SpecialNote { get; set; }
        public int CategoryId { get; set; }

        public static ProductDto From(ProductEntity entity)
        {
            return new()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                Note = entity.Note,
                SpecialNote = entity.SpecialNote,
                CategoryId = entity.CategoryId,
            };
        }
    }
}
