using ProductCatalog.Core.Attributes;
using ProductCatalog.Core.Data.Entities;
using ProductCatalog.Core.DTOs.ProductCategory;
using ProductCatalog.Core.Models.Enums;

namespace ProductCatalog.Core.DTOs.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string? Note { get; set; }
        [RequiredRoles(UserRoleType.AdvancedUser)]
        public string? SpecialNote { get; set; }
        public ProductCategoryDto Category { get; set; }

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
                Category = ProductCategoryDto.From(entity.Category),
            };
        }
    }
}
