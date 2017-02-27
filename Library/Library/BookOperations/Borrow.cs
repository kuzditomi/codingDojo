using Library.Contracts;
using Library.Contracts.Models;
using Library.Helpers;

namespace Library.BookOperations
{
    public class Borrow
    {
        private readonly IBookRepository _bookrepository;
        private readonly IScreenHelper _screenHelper;
        private readonly IMenuHelper _menuHelper;

        public Borrow(IBookRepository repo, IScreenHelper screenHelper, IMenuHelper menuHelper)
        {
            _bookrepository = repo;
            _screenHelper = screenHelper;
            _menuHelper = menuHelper;
        }

        public void SingleBook()
        {
            _screenHelper.Reset();
            var id = _screenHelper.GetBookId();
            var reader = _screenHelper.GetNewReader();
            var daysToBorrow = _screenHelper.GetDueDate();

            var book = _bookrepository.BorrowABook(id, reader,daysToBorrow);

            _screenHelper.PrintBookBorrowedMessage(book, reader);
            _menuHelper.NavigateToMainMenu();
        }

        public void SingleBook(int id, Reader reader, int daysToBorrow)
        {
            _bookrepository.BorrowABook(id, reader, daysToBorrow);
        }
    }
}
