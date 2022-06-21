using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MbmStore.Data;
using MbmStore.Models;

namespace MbmStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private readonly IBookRepository bookRepo;

        public BookController(IBookRepository bookRepository) { 
            bookRepo = bookRepository; 
        }

        // GET: Admin/Books
        public async Task<IActionResult> Index()
        {
            return View(await bookRepo.GetBookList());
        }

        // GET: Admin/Books/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = bookRepo.GetBookById(id.Value);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Admin/Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Author,Publisher,Published,ISBN,ProductId,Title,Price,ImageUrl,Category")] Book book)
        {
            if (ModelState.IsValid)
            {
                bookRepo.SaveBook(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Admin/Books/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = bookRepo.GetBookById(id.Value);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Admin/Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Author,Publisher,Published,ISBN,ProductId,Title,Price,ImageUrl,Category")] Book book)
        {
            if (id != book.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bookRepo.SaveBook(book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!bookRepo.BookExists(book.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Admin/Books/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = bookRepo.GetBookById(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Admin/Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            bookRepo.DeleteBook(id);
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return bookRepo.BookExists(id);
        }
    }
}
