using System;
using Library.Contracts;
using Library.Helpers;

namespace Library.BookOperations
{
    public class Return
    {
        private readonly IBookRepository _bookrepository;
        readonly ScreenHelper _screenHelper = new ScreenHelper();
        readonly MenuHelper _menuHelper = new MenuHelper();

        public Return(IBookRepository repo)
        {
            _bookrepository = repo;
        }

        public void ReturnBook()
        {
            _screenHelper.Reset();
            var id = _screenHelper.GetBookId();
            var book = _bookrepository.ReturnABook(id);
            if (book == null)
                Console.WriteLine(Texts.NoBook);
            _menuHelper.NavigateToMainMenu();
        }
    }
}
