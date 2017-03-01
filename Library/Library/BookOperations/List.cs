using System;
using System.Collections.Generic;
using Library.Contracts;
using Library.Contracts.Models;
using Library.Helpers;

namespace Library.BookOperations
{
    public class Fetch
    {
        private readonly IBookRepository _bookrepository;
        private readonly IScreenHelper _screenHelper;
        private readonly IMenuHelper _menuHelper;
        private readonly IInputReader<int> _numberReader;

        public Fetch(IBookRepository repo, IScreenHelper screenHelper, IMenuHelper menuHelper, IInputReader<int> numberReader)
        {
            _bookrepository = repo;
            _screenHelper = screenHelper;
            _menuHelper = menuHelper;
            _numberReader = numberReader;
        }

        public void ListAllBooks()
        {
            _screenHelper.Reset();
            _screenHelper.PrintBookListingText();
            
            var books = _bookrepository.GetAllBooks();
            ListBooks(books);

            _menuHelper.NavigateToMainMenu();
        }

        public void ListExpiringBooks()
        {
            _screenHelper.Reset();

            _screenHelper.GetLimit();
            var books = GetExpiringBooks();
            ListBooks(books);

            _menuHelper.NavigateToMainMenu();
        }

        public void ListBooks(IEnumerable<Book> books)
        {
            foreach (var book in books)
            {
                _screenHelper.PrintBookDetails(book, _bookrepository.GetBookReader(book.Title));
            }
        }

        public IEnumerable<Book> GetExpiringBooks()
        {
            var expiringBooks = new List<Book>();
            var limit = _numberReader.Read();
            var books = _bookrepository.GetAllBooks();
            foreach (var book in books)
            {
                if (book.Reader != null &&
                    book.DueDate < DateTime.Now.AddDays(limit) &&
                    book.DueDate != new DateTime(1900, 01, 01))
                {
                    expiringBooks.Add(book);
                }
            }
            return expiringBooks;
        } 
    }
}
