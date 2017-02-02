using System;
using Library.DatabaseOperations;
using Library.Helpers;

namespace Library.BookOperations
{
    public static class Add
    {
        public static void NewBook()
        {
            ScreenHelper.Reset();

            var newBook = ScreenHelper.GetNewBookDetails();
            Store.SingleBook(newBook);

            Console.WriteLine("\r\nBook added: {0}, by {1} from year {2}",
                newBook.Title, newBook.Author, newBook.Year);
            MenuHelper.NavigateToMainMenu();
        }
    }
}
