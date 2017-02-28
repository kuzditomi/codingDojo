using System;
using System.Collections.Generic;
using Library.Contracts;
using Library.Contracts.Models;
using Library.Helpers;
using Moq;
using NUnit.Framework;
using List = Library.BookOperations.List;

namespace Library.BookOperationsTests
{
    [TestFixture]
    public class BookListingTests
    {
        private Mock<IBookRepository> _bookRepository;
        private Mock<IScreenHelper> _screenHelper;
        private Mock<IMenuHelper> _menuHelper;
        private Mock<IInputReader<int>> _inputReader;
        private List _list;

        [SetUp]
        public void Setup()
        {
            _bookRepository = new Mock<IBookRepository>();
            _screenHelper = new Mock<IScreenHelper>();
            _menuHelper = new Mock<IMenuHelper>();
            _inputReader = new Mock<IInputReader<int>>();
            _list = new List(_bookRepository.Object, _screenHelper.Object, _menuHelper.Object);
        }

        [Test]
        public void ListAllBooks_AllBooksGetListed()
        {
            //Arrange
            var books = new List<Book>();
            var book1 = new Book
            {
                Id = 1,
                Title = "Cím",
                Author = "Szerző",
                Available = true,
                DueDate = DateTime.Today.AddDays(2),
                Reader = new Reader {Id = 1, Name = "Olvasó"},
                Year = 2001
            };
            var book2 = new Book
            {
                Id = 2,
                Title = "Cím2",
                Author = "Szerző2",
                Available = true,
                DueDate = DateTime.Today.AddDays(5),
                Reader = new Reader {Id = 2, Name = "Olvasó2"},
                Year = 2002
            };
            books.Add(book1);
            books.Add(book2);
            _bookRepository.Setup(b => b.GetAllBooks()).Returns(books);

            _bookRepository.Setup(b => b.GetBookReader("Cím")).Returns("Olvasó");
            _bookRepository.Setup(b => b.GetBookReader("Cím2")).Returns("Olvasó2");

            //Act
            _list.ListAllBooks();

            //Assert
            _screenHelper.Verify(s => s.PrintBookDetails(book1, book1.Reader.Name), Times.Once);
            _screenHelper.Verify(s => s.PrintBookDetails(book2, book2.Reader.Name), Times.Once);
        }

        [Test]
        public void ListExpiringBooks_ExpiredBooksGetListed()
        {
            //Arrange
            _inputReader.Setup(i => i.Read(3)).Returns(3);
            var books = new List<Book>();
            var book1 = new Book
            {
                Id = 1,
                Title = "Cím",
                Author = "Szerző",
                Available = true,
                DueDate = DateTime.Today.AddDays(2),
                Reader = new Reader {Id = 1, Name = "Olvasó"},
                Year = 2001
            };
            var book2 = new Book
            {
                Id = 2,
                Title = "Cím2",
                Author = "Szerző2",
                Available = true,
                DueDate = DateTime.Today.AddDays(5),
                Reader = new Reader {Id = 2, Name = "Olvasó2"},
                Year = 2002
            };
            books.Add(book1);
            books.Add(book2);
            _bookRepository.Setup(b => b.GetAllBooks()).Returns(books);

            _bookRepository.Setup(b => b.GetBookReader("Cím")).Returns("Olvasó");
            _bookRepository.Setup(b => b.GetBookReader("Cím2")).Returns("Olvasó2");

            //Act
            _list.ListExpiringBooks();

            //Assert
            _screenHelper.Verify(s => s.PrintBookDetails(book1, book1.Reader.Name), Times.Once);
            _screenHelper.Verify(s => s.PrintBookDetails(book2, book2.Reader.Name), Times.Never);
        }
    }
}
