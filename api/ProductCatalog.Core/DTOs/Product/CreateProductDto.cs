namespace ProductCatalog.Core.DTOs.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string? Note { get; set; }
        public string? SpecialNote { get; set; }
        public int CategoryId { get; set; }
    }
}
