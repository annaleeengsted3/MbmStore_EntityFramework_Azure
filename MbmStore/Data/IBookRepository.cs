using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MbmStore.Models;

namespace MbmStore.Data
{
   public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBookList();
        Book GetBookById(int id);
        void SaveBook(Book book);
        Book DeleteBook(int bookId);
        bool BookExists(int id);
    }
}
