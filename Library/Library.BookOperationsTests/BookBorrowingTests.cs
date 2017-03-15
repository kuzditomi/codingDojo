using Library.BookOperations;
using Library.Contracts;
using Library.Contracts.Models;
using Library.Contracts.PublicAPI.Helpers;
using Moq;
using NUnit.Framework;

namespace Library.BookOperationsTests
{
    [TestFixture]
    public class BookBorrowingTests
    {
        private Mock<IBookRepository> _bookRepository;
        private Mock<IScreenHelper> _screenHelper;
        private Mock<IMenuHelper> _menuHelper;
        private Borrow _borrow;

        [SetUp]
        public void Setup()
        {
            _bookRepository = new Mock<IBookRepository>();
            _screenHelper = new Mock<IScreenHelper>();
            _menuHelper = new Mock<IMenuHelper>();
            _borrow = new Borrow(_bookRepository.Object, _screenHelper.Object, _menuHelper.Object);

            _screenHelper.Setup(s => s.GetBookId()).Returns(1);
            _screenHelper.Setup(s => s.GetBorrowDays()).Returns(2);
            _screenHelper.Setup(s => s.GetNewReader()).Returns(new Reader() { Id = 1, Name = "Test Reader" });
        }

        [Test]
        public void BorrowSingleBook_SingleBookGetsBorrowed()
        {
            var bookId = _screenHelper.Object.GetBookId();
            var reader = _screenHelper.Object.GetNewReader();
            var days = _screenHelper.Object.GetBorrowDays();
            
            _borrow.BorrowSingleBook(bookId, reader, days);
            
            _bookRepository.Verify(b => b.BorrowABook(bookId, reader, days), Times.Once);
        }

        [Test]
        public void PerformBorrowingProcess_CanFetchBorrowingDetails()
        {
            var bookId = _screenHelper.Object.GetBookId();
            var reader = _screenHelper.Object.GetNewReader();
            var days = _screenHelper.Object.GetBorrowDays();
            
            _borrow.PerformBorrowingProcess();
            
            _bookRepository.Verify(b => b.BorrowABook(bookId, reader, days), Times.Once);
        }
    }
}
