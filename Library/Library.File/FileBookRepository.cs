using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Library.Contracts;
using Library.Contracts.Models;

namespace Library.File
{
    public class FileBookRepository : IBookRepository
    {
        private const string FileName = "books.xml";
        private const string RootElement = "Library";

        public void StoreABook(Book book)
        {
            if (!System.IO.File.Exists(FileName))
            {
                CreateFile();
            }
            SaveBookToFile(book, GetBooksCount());
        }

        private void CreateFile()
        {
            XDocument inventoryDoc =
                new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XComment("Library of books"),
                    new XElement(RootElement));

            inventoryDoc.Save(FileName);
        }

        private void SaveBookToFile(Book book, int id)
        {
            var doc = XDocument.Load(FileName);
            doc.Element(RootElement).Add(
                new XElement("Book",
                    new XElement("Id", id),
                    new XElement("Title", book.Title),
                    new XElement("Author", book.Author),
                    new XElement("Year", book.Year),
                    new XElement("Available", book.Available),
                    new XElement("Reader", book.Reader?.Name),
                    new XElement("DueDate", book.DueDate)));
            doc.Save(FileName);
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
                    book.DueDate = new DateTime(1900, 1, 1);
                    chosenBook = book;
                }
            }
            StoreMultipleBooks(books);
            return chosenBook;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            if (!System.IO.File.Exists(FileName))
            {
                Console.WriteLine("Library is empty.");
                return new List<Book>();
            }
            var booksList = new List<Book>();

            var xelement = XElement.Load(FileName);
            var books = xelement.Elements();

            foreach (var book in books)
            {
                booksList.Add(new Book()
                {
                    Id = int.Parse(book.Element("Id").Value),
                    Title = book.Element("Title").Value,
                    Author = book.Element("Author").Value,
                    Year = int.Parse(book.Element("Year").Value),
                    Available = bool.Parse(book.Element("Available").Value),
                    Reader = new Reader(book.Element("Reader").Value),
                    DueDate = DateTime.Parse(book.Element("DueDate").Value)
                });
            }
            return booksList;
        }

        private int GetBooksCount()
        {
            var xelement = XElement.Load(FileName);
            return xelement.Elements().Count();
        }

        public string GetBookReader(string title)
        {
            var books = GetAllBooks();
            foreach (var book in books)
            {
                if (book.Title == title && book.Reader.Name != "")
                    return book.Reader.Name;
            }
            return null;
        }

        private void DeleteFile()
        {
            System.IO.File.Delete(FileName);
        }

        public Book GetBookByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public List<Book> GetBookByAuthor(string author)
        {
            throw new NotImplementedException();
        }

        public List<Book> GetBookByYear(int year)
        {
            throw new NotImplementedException();
        }

        public void DeleteBook(Book id)
        {
            throw new NotImplementedException();
        }

        public Book GetBookById(int id)
        {
            throw new NotImplementedException();
        }

        List<Book> IBookRepository.GetBookByTitle(string title)
        {
            throw new NotImplementedException();
        }
    }
}
