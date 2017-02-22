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
                context.Books.Add((Library.Sql.Models.Book)book);
                context.SaveChanges();
            }
        }

        public void StoreMultipleBooks(List<Book> books)
        {
            var bookDtos = books.Select(book => (Library.Sql.Models.Book) book).ToList();
            using (var context = new DataContext())
            {
                context.Books.AddRange(bookDtos);
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
                    book.Reader = (Library.Sql.Models.Reader)reader;
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
                var books = context.Books.Include(n => n.Reader).ToArray();
                return books.OrderBy(book => book.Id);
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

        private Contracts.Models.Book ConvertSqlBook(Models.Book sqlBook)
        {
            var book = new Contracts.Models.Book
            {
                Id = sqlBook.Id,
                Reader = new Models.Reader(sqlBook.Reader.Name),
                Author = sqlBook.Author,
                Available = sqlBook.Available,
                DueDate = sqlBook.DueDate,
                Year = sqlBook.Year
            };
        }
    }
}
