namespace ProductCatalog.Core.Data.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string? Note { get; set; }
        public string? SpecialNote { get; set; }

        public int CategoryId { get; set; }
        public ProductCategoryEntity Category { get; set; }
    }
}
