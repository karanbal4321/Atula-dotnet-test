﻿using Atula_dotnet_test.Data;
using Atula_dotnet_test.DTOs;
using Atula_dotnet_test.Models; // Adjust based on your namespace
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Atula_dotnet_test.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProductDTO ProductDTO { get; set; }
        public SelectList CategorySelectList { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Categories)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var categories = await _context.Categories.ToListAsync();
                CategorySelectList = new SelectList(categories, "Id", "Name");
                return Page();
            }

            var product = await _context.Products.FindAsync(ProductDTO.Id);

            if (product == null)
            {
                return NotFound();
            }

            product.Sku = ProductDTO.Sku;
            product.Name = ProductDTO.Name;

            // Update categories
            product.Categories.Clear();
            var categoriesToAdd = await _context.Categories.Where(c => ProductDTO.CategoryIds.Contains(c.Id)).ToListAsync();
            foreach (var category in categoriesToAdd)
            {
                product.Categories.Add(category);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
