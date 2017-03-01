using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Library.BookOperations;
using Library.Contracts;
using Library.Contracts.Models;
using Library.Helpers;
using Moq;
using NUnit.Framework;

namespace Library.BookOperationsTests
{
    [TestFixture]
    public class BookSearchingTests
    {
        private Mock<IBookRepository> _bookRepository;
        private Mock<IScreenHelper> _screenHelper;
        private Mock<IMenuHelper> _menuHelper;
        private Search _search;

        [SetUp]
        public void Setup()
        {
            _bookRepository = new Mock<IBookRepository>();
            _screenHelper = new Mock<IScreenHelper>();
            _menuHelper = new Mock<IMenuHelper>();
            _search = new Search(_bookRepository.Object, _screenHelper.Object, _menuHelper.Object);
        }

        [Test]
        public void SearchForTitle_FindsSingleMatchingBook()
        {
            //Arrange
            var books = GenerateTestBooks();
            var expectedBook = books.FirstOrDefault(b => b.Title == "Cim3");
            var expectedList = new List<Book> {expectedBook};
            _screenHelper.Setup(s => s.ReadInputString("Title")).Returns("Cim3");
            _bookRepository.Setup(b => b.GetAllBooks()).Returns(books);

            //Act
            _search.SearchForTitle();

            //Assert
            _screenHelper.Verify(s => s.PrintSearchResult(expectedList), Times.Once);
        }

        private IEnumerable<Book> GenerateTestBooks()
        {
            var book1 = new Book
            {
                Id = 1,
                Title = "Cim1",
                Author = "Szerző1",
                Available = true,
                DueDate = DateTime.Today,
                Reader = null
            };
            var book2 = new Book
            {
                Id = 2,
                Title = "Cim2",
                Author = "Szerző2",
                Available = false,
                DueDate = DateTime.Today.AddDays(2),
                Reader = new Reader() { Id = 2, Name = "Olvasó2" }
            };
            var book3 = new Book
            {
                Id = 3,
                Title = "Cim3",
                Author = "Szerző3",
                Available = true,
                DueDate = DateTime.Today,
                Reader = null
            };
            var book4 = new Book
            {
                Id = 4,
                Title = "Cim4",
                Author = "Szerző4",
                Available = false,
                DueDate = DateTime.Today.AddDays(10),
                Reader = new Reader { Id = 4, Name = "Olvasó4" }
            };

            return new List<Book> { book1, book2, book3, book4 };
        }
    }
}
