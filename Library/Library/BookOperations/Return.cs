using System;
using System.Linq;
using Library.DatabaseOperations;
using Library.Helpers;

namespace Library.BookOperations
{
    public static class Return
    {
        public static void ReturnBook()
        {
            ScreenHelper.Reset();
            var id = ScreenHelper.GetBookId();

            var books = Fetch.GetAllBooks();
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
