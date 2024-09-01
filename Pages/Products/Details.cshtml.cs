using Atula_dotnet_test.Data;
using Atula_dotnet_test.DTOs;
using Atula_dotnet_test.Models; // Ensure this is the correct namespace for your models
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Atula_dotnet_test.Pages.Products 
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public ProductDTO ProductDTO { get; set; }
        public SelectList CategorySelectList { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Categories) // Include Categories directly
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            ProductDTO = new ProductDTO
            {
                Id = product.Id,
                Sku = product.Sku,
                Name = product.Name,
                CategoryIds = product.Categories.Select(c => c.Id).ToList() 
            };

            var categories = await _context.Categories.ToListAsync();
            CategorySelectList = new SelectList(categories, "Id", "Name");

            return Page();
        }
    }
}
