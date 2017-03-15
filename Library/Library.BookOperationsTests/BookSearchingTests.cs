using System;
using System.Collections.Generic;
using System.Linq;
using Library.BookOperations;
using Library.Contracts;
using Library.Contracts.Models;
using Library.Contracts.Models.Menu;
using Library.Contracts.PublicAPI.Helpers;
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
            var books = GenerateTestBooks();
            var expectedBook = books.FirstOrDefault(b => b.Title == "Cim3");
            var expectedList = new List<Book> {expectedBook};
            _screenHelper.Setup(s => s.ReadInputString("Title")).Returns("Cim3");
            _bookRepository.Setup(b => b.GetAllBooks()).Returns(books);
            
            _search.SearchForTitle();
            
            _screenHelper.Verify(s => s.PrintSearchResult(expectedList), Times.Once);
        }

        [Test]
        public void SearchForAuthor_FindsSingleMatchingBook()
        {
            var books = GenerateTestBooks();
            var expectedBook = books.FirstOrDefault(b => b.Author == "Szerző1");
            var expectedList = new List<Book> {expectedBook};
            _screenHelper.Setup(s => s.ReadInputString("Author")).Returns("Szerző1");
            _bookRepository.Setup(b => b.GetAllBooks()).Returns(books);
            
            _search.SearchForAuthor();
            
            _screenHelper.Verify(s => s.PrintSearchResult(expectedList), Times.Once);
        }

        [Test]
        public void SearchForYear_FindsMultipleMatchingBooks()
        {
            var books = GenerateTestBooks();
            var expectedBooks = books.Where(b => b.Year == 2000).ToList();
            _screenHelper.Setup(s => s.ReadInputString("Year")).Returns("2000");
            _bookRepository.Setup(b => b.GetAllBooks()).Returns(books);
            
            _search.SearchForYear();
            
            _screenHelper.Verify(s => s.PrintSearchResult(expectedBooks), Times.Once);
        }

        [Test]
        public void SearchBeforeYear_DoesNotFindAnyMatchingBooks()
        {
            var books = GenerateTestBooks();
            var expectedBooks = books.Where(b => b.Year < 1).ToList();
            _screenHelper.Setup(s => s.ReadInputString("Year")).Returns("1");
            _bookRepository.Setup(b => b.GetAllBooks()).Returns(books);
            
            _search.SearchBeforeYear();
            
            _screenHelper.Verify(s => s.PrintSearchResult(expectedBooks), Times.Once);
        }

        [Test]
        public void SearchAfterYear_FindsMultipleMatchingBooks()
        {
            var books = GenerateTestBooks();
            var expectedBooks = books.Where(b => b.Year > 1000).ToList();
            _screenHelper.Setup(s => s.ReadInputString("Year")).Returns("1000");
            _bookRepository.Setup(b => b.GetAllBooks()).Returns(books);
            
            _search.SearchAfterYear();
            
            _screenHelper.Verify(s => s.PrintSearchResult(expectedBooks), Times.Once);
        }

        [Test]
        public void SearchForReader_FindsSingleMatchingBook()
        {
            var books = GenerateTestBooks();
            var expectedBooks = books.Where(b => b.Reader?.Name == "Olvasó2").ToList();
            _screenHelper.Setup(s => s.ReadInputString("Reader")).Returns("Olvasó2");
            _bookRepository.Setup(b => b.GetAllBooks()).Returns(books);
            
            _search.SearchForReader();
            
            _screenHelper.Verify(s => s.PrintSearchResult(expectedBooks), Times.Once);
        }

        [Test]
        [TestCase(SearchFor.Title)]
        [TestCase(SearchFor.Reader)]
        [TestCase(SearchFor.Author)]
        [TestCase(SearchFor.Year)]
        public void SearchBooks_PicksUpProperMenuCase(SearchFor menuItem)
        {
            _menuHelper.Setup(m => m.DoSearchMenuSelection()).Returns(menuItem);

            _search.SearchBooks();
            _screenHelper.Verify(s => s.ReadInputString(menuItem.ToString()), Times.Once);
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
                Reader = null,
                Year = 2000
            };
            var book2 = new Book
            {
                Id = 2,
                Title = "Cim2",
                Author = "Szerző2",
                Available = false,
                DueDate = DateTime.Today.AddDays(2),
                Reader = new Reader() { Id = 2, Name = "Olvasó2" },
                Year = 2000
            };
            var book3 = new Book
            {
                Id = 3,
                Title = "Cim3",
                Author = "Szerző3",
                Available = true,
                DueDate = DateTime.Today,
                Reader = null,
                Year = 2002
            };
            var book4 = new Book
            {
                Id = 4,
                Title = "Cim4",
                Author = "Szerző4",
                Available = false,
                DueDate = DateTime.Today.AddDays(10),
                Reader = new Reader { Id = 4, Name = "Olvasó4" },
                Year = 2003
            };

            return new List<Book> { book1, book2, book3, book4 };
        }
    }
}
