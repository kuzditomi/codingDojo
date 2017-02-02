using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Library.Contracts.Models;
using Library.Data;
using Library.Helpers;

namespace Library.DatabaseOperations
{
    public static class Fetch
    {
        public static List<Book> GetAllBooks()
        {
            using (var context = new DataContext())
            {
                var books = context.Books.Include(n => n.Reader).ToList();
                return books;
            }
        }

        public static string GetReaderOfBook(string title)
        {
            using (var context = new DataContext())
            {
                var book = context.Books.Include(n => n.Reader).FirstOrDefault(n => n.Title == title);
                return book?.Reader != null ? book.Reader.Name : Texts.InLibrary;
            }
        }
    }
}
