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
        List<Book> GetBookByTitle(string title);
        List<Book> GetBookByAuthor(string author);
        List<Book> GetBookByYear(int year);
        void DeleteBook(Book id);
        Book GetBookById(int id);
        string GetBookReader(string title);
    }
}
