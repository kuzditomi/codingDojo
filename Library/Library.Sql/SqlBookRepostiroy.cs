﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Library.Contracts;
using Library.Contracts.Models;

namespace Library.Sql
{
    public class SqlBookRepostiroy : IBookRepository
    {
        readonly ModelConverter _converter = new ModelConverter();

        public void StoreABook(Book book)
        {
            using (var context = new DataContext())
            {
                if (book.Id != 0)
                {
                    var bookToEdit = _converter.ConverToSqlBook(GetBookById(book.Id));
                    context.Books.Attach(bookToEdit);
                    context.Books.Remove(bookToEdit);
                }
                context.Books.Add(_converter.ConverToSqlBook(book));
                context.SaveChanges();
            }
        }

        public void StoreMultipleBooks(List<Book> books)
        {
            var bookDtos = books.Select(_converter.ConverToSqlBook).ToList();
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
                var book = context.Books.FirstOrDefault(b => b.BookId == id);
                if (book != null)
                {
                    book.Reader = _converter.ConverToSqlReader(reader);
                    book.Available = false;
                    book.DueDate = DateTime.Now.AddDays(daysToBorrow);
                    book.Reader.Address = GenerateAddress();
                    context.SaveChanges();
                    return _converter.ConverToContractBook(book);
                }
                return null;
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
                    return _converter.ConverToContractBook(book);
                }
                return null;
            }
        }

        public IEnumerable<Book> GetAllBooks()
        {
            using (var context = new DataContext())
            {
                var books = context.Books.Include("Reader.Address").ToArray();
                var bookList = books.OrderBy(book => book.BookId).ToList();
                return bookList.Select(_converter.ConverToContractBook).ToList();
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

        private Sql.Models.Address GenerateAddress()
        {
            var rnd = new Random();
            return new Sql.Models.Address
            {
                City = "City-" + Guid.NewGuid().ToString().Substring(0, 5),
                PostalCode = rnd.Next(1000, 9999),
                Street = "Street-" + Guid.NewGuid().ToString().Substring(0, 5)
            };
        }

        public void DeleteBook(Book book)
        {
            using (var context = new DataContext())
            {
                if (book.Id != 0)
                {
                    var bookToEdit = _converter.ConverToSqlBook(GetBookById(book.Id));
                    context.Books.Attach(bookToEdit);
                    context.Books.Remove(bookToEdit);
                    context.SaveChanges();
                }
            }
        }

        public Book GetBookById(int bookId)
        {
            var books = GetAllBooks();
            return books.FirstOrDefault(b => b.Id == bookId);
        }

        public List<Book> GetBookByTitle(string title)
        {
            var books = GetAllBooks();
            return books.Where(book => book?.Title != null && book.Title.ToLower().Contains(title.ToLower())).ToList();
        }

        public List<Book> GetBookByAuthor(string author)
        {
            var books = GetAllBooks();
            return books.Where(book => book?.Author != null && book.Author.ToLower().Contains(author.ToLower())).ToList();
        }

        public List<Book> GetBookByYear(int year)
        {
            var books = GetAllBooks();
            return books.Where(book => book.Year == year).ToList();
        }
    }
}
