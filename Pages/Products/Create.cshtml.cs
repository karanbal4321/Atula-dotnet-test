using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Atula_dotnet_test.Data;
using Atula_dotnet_test.DTOs;
using Atula_dotnet_test.Mappers;
using Atula_dotnet_test.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Atula_dotnet_test.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductMapper _mapper;

        public CreateModel(ApplicationDbContext context, ProductMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [BindProperty]
        public ProductDTO ProductDTO { get; set; }

        public List<SelectListItem> CategorySelectList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Fetch categories for the dropdown
            var categories = await Task.FromResult(_context.Categories.ToList());
            CategorySelectList = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Re-populate categories if validation fails
                var categories = await Task.FromResult(_context.Categories.ToList());
                CategorySelectList = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

                return Page();
            }

            // Map DTO to entity
            var product = _mapper.MapFromDTO(ProductDTO);

            // Add to database
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}

