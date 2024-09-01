using Atula_dotnet_test.Data;
using Atula_dotnet_test.Models;

namespace Atula_dotnet_test.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public List<int> CategoryIds { get; set; } // To manage the many-to-many relationship

        public static ProductDTO FromEntity(Product product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                Sku = product.Sku,
                Name = product.Name,
                CategoryIds = product.Categories?.Select(c => c.Id).ToList()
            };
        }

        public static Product ToEntity(ProductDTO productDto, ApplicationDbContext context)
        {
            return new Product
            {
                Id = productDto.Id,
                Sku = productDto.Sku,
                Name = productDto.Name,
                Categories = context.Categories
                                    .Where(c => productDto.CategoryIds.Contains(c.Id))
                                    .ToList()
            };
        }
    }
}
