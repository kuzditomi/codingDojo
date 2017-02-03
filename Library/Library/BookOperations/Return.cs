using System;
using Library.Contracts;
using Library.Helpers;

namespace Library.BookOperations
{
    public class Return
    {
        private readonly IBookRepository _bookrepository;

        public Return(IBookRepository repo)
        {
            _bookrepository = repo;
        }

        public void ReturnBook()
        {
            ScreenHelper.Reset();
            var id = ScreenHelper.GetBookId();
            var book = _bookrepository.ReturnABook(id);
            if (book == null)
                Console.WriteLine(Texts.NoBook);
            MenuHelper.NavigateToMainMenu();
        }
    }
}
