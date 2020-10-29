using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Book> Books { get; set; }

        public async Task OnGet()
        {
            Books = await _db.Books.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id) // the parameter name should be the same as the one passed in the asp-route handler
        {
            var selectedBook = await _db.Books.FindAsync(id);

            if (selectedBook == null)
            {
                return NotFound();
            }

            _db.Books.Remove(selectedBook);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}