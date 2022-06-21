using MbmStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MbmStore.Data
{
    public class EFBookRepository : IBookRepository
    {
        private readonly MbmStoreContext dataContext;
        public EFBookRepository(MbmStoreContext context)
        {
            dataContext = context;
        }
        public async Task<IEnumerable<Book>> GetBookList()
        {
            return await dataContext.Books.ToListAsync();
        }

        public Book GetBookById(int id)
        {
            return dataContext.Books.Find(id);
        }
        public void SaveBook(Book book)
        {
            if (book.ProductId == 0)
            {
                book.CreatedDate = DateTime.Now;
                dataContext.Books.Add(book);
                dataContext.SaveChanges();
            }
            else
            {
                dataContext.Entry(book).State = EntityState.Modified;
                dataContext.Entry(book).Property(c => c.CreatedDate).IsModified = false;
                dataContext.SaveChanges();
            }
        }
        public Book DeleteBook(int bookId)
        {
            Book book = dataContext.Books.Find(bookId);
            dataContext.Books.Remove(book);
            dataContext.SaveChanges();
            return book;
        }
        public bool BookExists(int id)
        {
            return dataContext.Books.Any(e => e.ProductId == id);
        }
    }
}
