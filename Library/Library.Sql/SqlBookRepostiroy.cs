using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Library.Contracts;
using Library.Contracts.Models;

namespace Library.Sql
{
    public class SqlBookRepostiroy : IBookRepository
    {
        public void StoreABook(Book book)
        {
            using (var context = new DataContext())
            {
                context.Books.Add(book);
                context.SaveChanges();
            }
        }

        public void StoreMultipleBooks(List<Book> books)
        {
            using (var context = new DataContext())
            {
                context.Books.AddRange(books);
                context.SaveChanges();
            }
        }

        public Book BorrowABook(int id, Reader reader, int daysToBorrow)
        {
            using (var context = new DataContext())
            {
                var book = context.Books.FirstOrDefault(n => n.Id == id);
                if (book != null)
                {
                    book.Reader = reader;
                    book.Available = false;
                    book.DueDate = DateTime.Now.AddDays(daysToBorrow);
                    context.SaveChanges();
                }
                return book;
            }
        }

        public Book ReturnABook(int bookId)
        {
            using (var context = new DataContext())
            {
                var book = context.Books.Include(n => n.Reader).FirstOrDefault(n => n.Id == bookId);
                if (book != null)
                {
                    book.Available = true;
                    book.Reader = null;
                    context.SaveChanges();
                }
                return book;
            }
        }

        public IEnumerable<Book> GetAllBooks()
        {
            using (var context = new DataContext())
            {
                var books = context.Books.Include(n => n.Reader).ToList();
                return books;
            }
        }

        public string GetBookReader(string title)
        {
            using (var context = new DataContext())
            {
                var book = context.Books.Include(n => n.Reader).FirstOrDefault(n => n.Title == title);
                return book?.Reader != null ? book.Reader.Name : "Library";
            }
        }
    }
}
