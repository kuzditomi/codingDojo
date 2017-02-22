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
                context.Books.Add(ConverToSqlBook(book));
                context.SaveChanges();
            }
        }

        public void StoreMultipleBooks(List<Book> books)
        {
            var bookDtos = books.Select(ConverToSqlBook).ToList();
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
                var book = context.Books.FirstOrDefault(b => b.ReaderId == id);
                if (book != null)
                {
                    book.Reader = ConverToSqlReader(reader);
                    book.Available = false;
                    book.DueDate = DateTime.Now.AddDays(daysToBorrow);
                    context.SaveChanges();
                }
                return ConverToContractBook(book);
            }
        }

        public Book ReturnABook(int bookId)
        {
            using (var context = new DataContext())
            {
                var book = context.Books.Include(b => b.Reader).FirstOrDefault(b => b.BookId == bookId);
                if (book != null)
                {
                    book.Available = true;
                    book.Reader = null;
                    context.SaveChanges();
                }
                return ConverToContractBook(book);
            }
        }

        public IEnumerable<Book> GetAllBooks()
        {
            using (var context = new DataContext())
            {
                var books = context.Books.Include(n => n.Reader).ToArray();
                var bookList = books.OrderBy(book => book.BookId).ToList();
                return bookList.Select(ConverToContractBook).ToList();
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

        private Models.Book ConverToSqlBook(Contracts.Models.Book book)
        {
            Models.Reader sqlReader=new Models.Reader();
            if (book?.Reader != null)
            {
                sqlReader = ConverToSqlReader(book.Reader);
            }

            var sqlBook = new Models.Book
            {
                Title = book.Title,
                Author = book.Author,
                Year = book.Year,
                Available = book.Available,
                BookId = book.Id,
                DueDate = book.DueDate,
                Reader = sqlReader,
                ReaderId = book.Id
            };
            return sqlBook;
        }

        private Contracts.Models.Book ConverToContractBook(Models.Book book)
        {
            Contracts.Models.Reader contractReader = new Contracts.Models.Reader();
            if (book?.Reader != null)
            {
                contractReader = ConverToContractReader(book.Reader);
            }

            var contractBook = new Contracts.Models.Book
            {
                Title = book.Title,
                Author = book.Author,
                Year = book.Year,
                Available = book.Available,
                Id = book.BookId,
                DueDate = book.DueDate,
                Reader = contractReader
            };
            return contractBook;
        }

        private Models.Reader ConverToSqlReader(Contracts.Models.Reader reader)
        {
            return new Models.Reader {ReaderId = reader.Id, Name = reader.Name};
        }

        private Contracts.Models.Reader ConverToContractReader(Models.Reader reader)
        {
            return new Contracts.Models.Reader {Id = reader.ReaderId, Name = reader.Name};
        }
    }
}
