using System.Collections.Generic;
using Library.Contracts.Models;

namespace Library.Contracts.PublicAPI.BookOperations
{
    public interface IFetch
    {
        void ListAllBooks();
        void ListExpiringBooks();
        void ListBooks(IEnumerable<Book> books);
        IEnumerable<Book> GetExpiringBooks();
    }
}