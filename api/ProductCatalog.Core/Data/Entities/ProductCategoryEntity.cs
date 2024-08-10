namespace ProductCatalog.Core.Data.Entities
{
    public class ProductCategoryEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ProductEntity> Products { get; set; } = [];
    }
}
