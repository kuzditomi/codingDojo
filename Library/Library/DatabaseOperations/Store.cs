using System.Collections.Generic;
using Library.Contracts.Models;
using Library.Data;

namespace Library.DatabaseOperations
{
    public static class Store
    {
        public static void SingleBook(Book book)
        {
            using (var context = new DataContext())
            {
                context.Books.Add(book);
                context.SaveChanges();
            }
        }

        public static void MultipleBooks(List<Book> books)
        {
            using (var context = new DataContext())
            {
                context.Books.AddRange(books);
                context.SaveChanges();
            }
        }
    }
}
