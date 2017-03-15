using Library.Contracts;
using Library.Contracts.PublicAPI.BookOperations;
using Library.Contracts.PublicAPI.Helpers;

namespace Library.BookOperations
{
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
            var book = _screenHelper.GetNewBookDetails();
            _bookrepository.StoreABook(book);
            _screenHelper.PrintBookAddedMessage(book);
            _menuHelper.NavigateToMainMenu();
        }
    }
}
