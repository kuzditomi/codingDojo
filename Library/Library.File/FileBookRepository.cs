using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Library.Contracts;
using Library.Contracts.Models;

namespace Library.File
{
    public class FileBookRepository : IBookRepository
    {
        private int _numOfBooks;

        public void StoreABook(Book book)
        {
            book.Id = _numOfBooks++;
            var books = new List<Book> {book};
            var bookDtos = books.Select(b => new Book(b.Title, b.Author, b.Year)).ToList();
            IFormatter formatter = new BinaryFormatter();
            using (var stream = new FileStream("books.yeti", FileMode.Append, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, bookDtos);
            }
        }

        public void StoreMultipleBooks(List<Book> books)
        {
            foreach (var book in books)
            {
                StoreABook(book);
            }
        }

        public Book BorrowABook(int id, Reader reader, int daysToBorrow)
        {
            var books = GetAllBooks().ToList();
            DeleteFile();
            Book chosenBook = null;
            foreach (var book in books)
            {
                if (book.Id == id && book.Available)
                {
                    book.Reader = reader;
                    book.Available = false;
                    book.DueDate = DateTime.Now.AddDays(daysToBorrow);
                    chosenBook = book;
                }
            }
            StoreMultipleBooks(books);
            return chosenBook;
        }

        public Book ReturnABook(int id)
        {
            var books = GetAllBooks().ToList();
            DeleteFile();
            Book chosenBook = null;
            foreach (var book in books)
            {
                if (book.Id == id)
                {
                    book.Available = true;
                    book.Reader = null;
                    chosenBook = book;
                }
            }
            StoreMultipleBooks(books);
            return chosenBook;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            _numOfBooks = 0;
            IFormatter formatter = new BinaryFormatter();
            using (var stream = new FileStream("books.yeti", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                List<Book> books = new List<Book>();
                while (stream.Position != stream.Length)
                {
                    var bookDtOs = (List<Book>) formatter.Deserialize(stream);
                    bookDtOs[0].Id = _numOfBooks++;
                    books.AddRange(bookDtOs);
                }
                return books;
            }
        }

        public string GetBookReader(string title)
        {
            var books = GetAllBooks();
            foreach (var book in books)
            {
                if (book.Title == title && book.Reader != null)
                    return book.Reader.Name;
            }
            return "Library";
        }

        private void DeleteFile()
        {
            System.IO.File.Delete("books.yeti");
        }
    }
}
