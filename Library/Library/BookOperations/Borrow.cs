using System;
using Library.Contracts;
using Library.Helpers;

namespace Library.BookOperations
{
    public class Borrow
    {
        private readonly IBookRepository _bookrepository;

        public Borrow(IBookRepository repo)
        {
            _bookrepository = repo;
        }

        public void SingleBook()
        {
            ScreenHelper.Reset();
            var id = ScreenHelper.GetBookId();
            var reader = ScreenHelper.GetNewReader();
            var daysToBorrow = ScreenHelper.GetDueDate();

            var book = _bookrepository.BorrowABook(id, reader,daysToBorrow);

            Console.WriteLine("{0} is borrowed by {1} until {2}.", book.Title, reader.Name, book.DueDate);

            MenuHelper.NavigateToMainMenu();
        }
    }
}
