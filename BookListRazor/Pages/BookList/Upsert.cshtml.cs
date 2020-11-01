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
    public class UpsertModel : PageModel
    {
        private ApplicationDbContext _db;

        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            Book = new Book();
            if (id == null)
            {
                // CREATE
                return Page();
            }

            // UPDATE
            Book = await _db.Books.FirstOrDefaultAsync(book => book.Id == id); // you can also use FindAsync()
            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Book.Id == 0)
                {
                    _db.Books.Add(Book);
                }
                else
                {
                    _db.Books.Update(Book); // use the update method if all properties are going to be update, otherwise use:
                    /* THIS:
                    var BookFromDb = await _db.Books.FindAsync(Book.Id);
                    BookFromDb.Name = Book.Name;
                    BookFromDb.Author = Book.Author;
                    BookFromDb.ISBN = Book.ISBN; */
                }

                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}