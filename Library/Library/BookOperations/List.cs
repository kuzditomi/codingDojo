using System;
using Library.DatabaseOperations;
using Library.Helpers;

namespace Library.BookOperations
{
    public static class List
    {
        public static void AllBooks()
        {
            ScreenHelper.Reset();
            Console.WriteLine("====== List of books ======");

            var books = Fetch.GetAllBooks();
            foreach (var book in books)
            {
                ScreenHelper.PrintBookDetails(book);
            }

            MenuHelper.NavigateToMainMenu();
        }

        public static void ExpiringBooks()
        {
            ScreenHelper.Reset();
            Console.WriteLine("How many days should be the limit for the search:");
            var limit = NumberInputReader.Reader.Read();

            var books = Fetch.GetAllBooks();
            foreach (var book in books)
            {
                if (book.Reader != null && book.DueDate < DateTime.Now.AddDays(limit))
                    ScreenHelper.PrintBookDetails(book);
            }

            MenuHelper.NavigateToMainMenu();
        }
    }
}
