using System.Collections.Generic;
using Library.Contracts.Models;

namespace Library.Contracts
{
    public interface IBookRepository
    {
        void StoreABook(Book book);
        void StoreMultipleBooks(List<Book> books);
        Book BorrowABook(int id, Reader reader, int daysToBorrow);
        IEnumerable<Book> GetAllBooks();
        string GetBookReader(string title);
    }
}
