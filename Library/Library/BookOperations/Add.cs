using Library.Contracts;
using Library.Contracts.Models;
using Library.Helpers;

namespace Library.BookOperations
{
    public interface IAdd
    {
        void AddNewBook();
        Book GetNewBookDetails();
        void AddBook(Book book);
    }

    public class Add : IAdd
    {
        private readonly IBookRepository _bookrepository;
        private readonly IScreenHelper _screenHelper;
        private readonly IMenuHelper _menuHelper;

        public Add(IBookRepository repo, IScreenHelper screenHelper, IMenuHelper menuHelper)
        {
            _bookrepository = repo;
            _screenHelper = screenHelper;
            _menuHelper = menuHelper;
        }

        public void AddNewBook()
        {
            _screenHelper.Reset();
            var book = GetNewBookDetails();
            AddBook(book);
            _menuHelper.NavigateToMainMenu();
        }

        public Book GetNewBookDetails()
        {
            return _screenHelper.GetNewBookDetails();
        }

        public void AddBook(Book book)
        {
            _bookrepository.StoreABook(book);
            _screenHelper.PrintBookAddedMessage(book);
        }
    }
}
