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
            //ToDo:Optimize this. Should not open and save file for each books
            foreach (var book in books)
            {
                StoreABook(book);
            }
        }

        public Book BorrowABook(int id, Reader reader, int daysToBorrow)
        {
            throw new NotImplementedException();
        }

        public Book ReturnABook(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetAllBooks()
        {
            IFormatter formatter = new BinaryFormatter();
            using (var stream = new FileStream("books.yeti", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var bookDTOs = (List<Book>)formatter.Deserialize(stream);
                return bookDTOs;
                //return bookDtos.Select(b => new Book(b.Id, b.Title, b.Author, b.Year)).ToList();
            }
        }

        public string GetBookReader(string title)
        {
            IFormatter formatter = new BinaryFormatter();
            using (var stream = new FileStream("books.yeti", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var bookDTOs = (List<Book>)formatter.Deserialize(stream);
                //if (bookDTOs.Title == title && bookDTOs.Reader != null)
                //    return bookDTOs.Reader.Name;
                return "Library";
            }
        }
    }
}
