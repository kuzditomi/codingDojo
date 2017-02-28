using System;
using NUnit.Framework;
using Moq;
using Library.BookOperations;
using Library.Contracts;
using Library.Contracts.Models;
using Library.Helpers;

namespace Library.BookOperationsTests
{
    [TestFixture]
    public class BookAdditionTests
    {
        private Mock<IBookRepository> _bookRepository;
        private Mock<IScreenHelper> _screenHelper;
        private Mock<IMenuHelper> _menuHelper;
        private Add _add;

        [SetUp]
        public void Setup()
        {
            _bookRepository = new Mock<IBookRepository>();
            _screenHelper = new Mock<IScreenHelper>();
            _menuHelper = new Mock<IMenuHelper>();
            _add = new Add(_bookRepository.Object, _screenHelper.Object, _menuHelper.Object);
        }

        [Test]
        public void AddBook_SingleBookGetsAdded()
        {
            //Arrange
            var book = new Book
            {
                Id = 1,
                Author = "Szerző",
                Title = "Cim",
                Year = 2000,
                Available = true,
                DueDate = new DateTime(2000,01,01),
                Reader = null
            };

            //Act
            _add.AddBook(book);

            //Assert
            _bookRepository.Verify(br=>br.StoreABook(book), Times.Once());
        }

        [Test]
        public void GetNewBookDetails_CanFetchNewBookDetails()
        {
            //Arrange
            var expectedBook = new Book
            {
                Id = 1,
                Author = "Szerző",
                Title = "Cim",
                Year = 2000,
                Available = true,
                DueDate = new DateTime(2000, 01, 01),
                Reader = null
            };
            _screenHelper.Setup(s => s.GetNewBookDetails()).Returns(expectedBook);

            //Act
            var storedBook = _add.GetNewBookDetails();

            //Assert
            Assert.AreEqual(expectedBook, storedBook);
        }
    }
}
