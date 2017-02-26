using System;
using Library.Contracts;
using Library.Contracts.Models;
using Library.Helpers;

namespace Library.BookOperations
{
    public class Borrow
    {
        private readonly IBookRepository _bookrepository;
        readonly ScreenHelper _screenHelper = new ScreenHelper();
        readonly MenuHelper _menuHelper = new MenuHelper();

        public Borrow(IBookRepository repo)
        {
            _bookrepository = repo;
        }

        public void SingleBook()
        {
            _screenHelper.Reset();
            var id = _screenHelper.GetBookId();
            var reader = _screenHelper.GetNewReader();
            var daysToBorrow = _screenHelper.GetDueDate();

            var book = _bookrepository.BorrowABook(id, reader,daysToBorrow);

            Console.WriteLine("{0} is borrowed by {1} until {2}.", book.Title, reader.Name, book.DueDate);

            _menuHelper.NavigateToMainMenu();
        }

        public void SingleBook(int id, Reader reader, int daysToBorrow)
        {
            _bookrepository.BorrowABook(id, reader, daysToBorrow);
        }
    }
}
