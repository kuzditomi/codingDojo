using System.Collections.Generic;
using Library.Contracts.Models;

namespace Library.Contracts
{
    public interface IBookRepository
    {
        void StoreABook(Book book);
        void StoreMultipleBooks(List<Book> books);
        Book BorrowABook(int id, Reader reader, int daysToBorrow);
        Book ReturnABook(int id);
        IEnumerable<Book> GetAllBooks();
        Book GetBookByTitle(string title);
        Book GetBookById(int id);
        string GetBookReader(string title);
    }
}
