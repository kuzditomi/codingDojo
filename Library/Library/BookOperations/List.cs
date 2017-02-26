using System;
using Library.Contracts;
using Library.Helpers;

namespace Library.BookOperations
{
    public class List
    {
        private readonly IBookRepository _bookrepository;
        private readonly IScreenHelper _screenHelper;
        private readonly IMenuHelper _menuHelper;

        public List(IBookRepository repo, IScreenHelper screenHelper, IMenuHelper menuHelper)
        {
            _bookrepository = repo;
            _screenHelper = screenHelper;
            _menuHelper = menuHelper;
        }

        public void AllBooks()
        {
            _screenHelper.Reset();
            Console.WriteLine("====== List of books ======");
            
            var books = _bookrepository.GetAllBooks();
            foreach (var book in books)
            {
                _screenHelper.PrintBookDetails(book, _bookrepository.GetBookReader(book.Title));
            }

            _menuHelper.NavigateToMainMenu();
        }

        public void ExpiringBooks()
        {
            _screenHelper.Reset();
            Console.WriteLine("How many days should be the limit for the search:");
            var limit = NumberInputReader.Reader.Read();

            var books = _bookrepository.GetAllBooks();
            foreach (var book in books)
            {
                if (book.Reader != null && 
                    book.DueDate < DateTime.Now.AddDays(limit) &&
                    book.DueDate != new DateTime(1900, 01, 01))
                    _screenHelper.PrintBookDetails(book, _bookrepository.GetBookReader(book.Title));
            }

            _menuHelper.NavigateToMainMenu();
        }
    }
}
