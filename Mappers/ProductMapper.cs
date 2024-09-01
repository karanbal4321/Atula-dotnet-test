using Atula_dotnet_test.Data;
using Atula_dotnet_test.DTOs;
using Atula_dotnet_test.Models;

namespace Atula_dotnet_test.Mappers
{
    public class ProductMapper
    {
        private readonly ApplicationDbContext _context;

        public ProductMapper(ApplicationDbContext context)
        {
            _context = context;
        }

        // Maps Product entity to ProductDTO
        public ProductDTO MapToDTO(Product product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                Sku = product.Sku,
                Name = product.Name,
                CategoryIds = product.Categories?.Select(c => c.Id).ToList() ?? new List<int>()
            };
        }

        // Maps ProductDTO to Product entity
        public Product MapFromDTO(ProductDTO productDto)
        {
            var product = new Product
            {
                Id = productDto.Id,
                Sku = productDto.Sku,
                Name = productDto.Name,
                Categories = _context.Categories
                                     .Where(c => productDto.CategoryIds.Contains(c.Id))
                                     .ToList()
            };

            return product;
        }

        // Updates an existing Product entity with data from ProductDTO
        public void UpdateEntity(Product product, ProductDTO productDto)
        {
            product.Sku = productDto.Sku;
            product.Name = productDto.Name;
            product.Categories = _context.Categories
                                        .Where(c => productDto.CategoryIds.Contains(c.Id))
                                        .ToList();
        }
    }
}
