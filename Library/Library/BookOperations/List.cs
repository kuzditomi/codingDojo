using System;
using System.Diagnostics;
using Library.Contracts;
using Library.Helpers;

namespace Library.BookOperations
{
    public class List
    {
        private readonly IBookRepository _bookrepository;

        public List(IBookRepository repo)
        {
            _bookrepository = repo;
        }

        public void AllBooks()
        {
            ScreenHelper.Reset();
            Console.WriteLine("====== List of books ======");
            
            var books = _bookrepository.GetAllBooks();
            foreach (var book in books)
            {
                ScreenHelper.PrintBookDetails(book, _bookrepository.GetBookReader(book.Title));
            }

            MenuHelper.NavigateToMainMenu();
        }

        public void ExpiringBooks()
        {
            ScreenHelper.Reset();
            Console.WriteLine("How many days should be the limit for the search:");
            var limit = NumberInputReader.Reader.Read();

            var books = _bookrepository.GetAllBooks();
            foreach (var book in books)
            {
                if (book.Reader != null && 
                    book.DueDate < DateTime.Now.AddDays(limit) &&
                    book.DueDate != new DateTime(1900, 01, 01))
                    ScreenHelper.PrintBookDetails(book, _bookrepository.GetBookReader(book.Title));
            }

            MenuHelper.NavigateToMainMenu();
        }
    }
}
