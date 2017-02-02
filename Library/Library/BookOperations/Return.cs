using System;
using System.Linq;
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

            var books = _bookrepository.GetAllBooks();
            var book = books.FirstOrDefault(b => b.Id == Convert.ToInt32(id));

            if (book != null)
            {
                book.Available = true;
                book.Reader = null;
            }
            else
            {
                Console.WriteLine(Texts.NoBook);
            }
            MenuHelper.NavigateToMainMenu();
        }
    }
}
