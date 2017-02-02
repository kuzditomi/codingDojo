using System;
using System.Linq;
using Library.Contracts.Models;
using Library.Data;
using Library.Helpers;

namespace Library.BookOperations
{
    public static class Borrow
    {
        public static void SingleBook()
        {
            Book book;
            ScreenHelper.Reset();
            var id = ScreenHelper.GetBookId();
            var reader = ScreenHelper.GetNewReader();
            var daysToBorrow = ScreenHelper.GetDueDate();

            using (var context = new DataContext())
            {
                book = context.Books.FirstOrDefault(n => n.Id == id);
                book.Reader = reader;
                book.Available = false;
                book.DueDate = DateTime.Now.AddDays(daysToBorrow);
                context.SaveChanges();
            }

            Console.WriteLine("{0} is borrowed by {1} until {2}.", book.Title, reader.Name, book.DueDate);

            MenuHelper.NavigateToMainMenu();
        }
    }
}
