using System.Collections.Generic;
using System.Web.Mvc;
using Library.Contracts.Models;
using Library.Contracts;
using Library.Web.Models;

namespace Library.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public PartialViewResult Add(SearchViewModel input)
        {
            var book = new Book(input.Title, input.Author, input.Year);
            _bookRepository.StoreABook(book);

            var books = _bookRepository.GetBookByTitle(input.Title);
            return PartialView("AddResult", books);
        }

        [HttpGet]
        public ActionResult Borrow(int bookId)
        {
            var book = _bookRepository.GetBookById(bookId);
            var bookModel = new BorrowViewModel
            {
                Id = book.Id,
                Title = book.Title
            };

            return View(bookModel);
        }

        [HttpPost]
        public PartialViewResult Borrow(BorrowViewModel bookModel)
        {
            var reader = new Reader(bookModel.Reader);
            _bookRepository.BorrowABook(bookModel.Id, reader, bookModel.Days);

            var book = _bookRepository.GetBookById(bookModel.Id);
            var books = new List<Book> { book };
            return PartialView("BorrowResult", books);
        }

        public ActionResult Return(int bookId)
        {
            _bookRepository.ReturnABook(bookId);
            var book = _bookRepository.GetBookById(bookId);
            var books = _bookRepository.GetBookByTitle(book.Title);
            return View("ReturnResult", books);
        }

        [HttpGet]
        public PartialViewResult SearchForTitle(SearchViewModel input)
        {
            var books = _bookRepository.GetBookByTitle(input.Title);
            return PartialView("SearchResult", books);
        }

        [HttpGet]
        public PartialViewResult SearchForAuthor(SearchViewModel input)
        {
            var books = _bookRepository.GetBookByAuthor(input.Author);
            return PartialView("SearchResult", books);
        }

        [HttpGet]
        public PartialViewResult SearchForYear(SearchViewModel input)
        {
            var books = _bookRepository.GetBookByYear(input.Year);
            return PartialView("SearchResult", books);
        }

        public ActionResult Delete(int bookId)
        {
            var book = _bookRepository.GetBookById(bookId);
            _bookRepository.DeleteBook(book);

            return View("DeleteResult", book);
        }

        public ActionResult Edit(int bookId)
        {
            var book = _bookRepository.GetBookById(bookId);
            return View(book);
        }

        [HttpPost]
        public PartialViewResult Edit(Book bookToEdit)
        {
            var book = _bookRepository.GetBookById(bookToEdit.Id);

            book.Title = bookToEdit.Title;
            book.Author = bookToEdit.Author;
            book.Year = bookToEdit.Year;
            _bookRepository.StoreABook(book);

            var books = _bookRepository.GetBookByTitle(bookToEdit.Title);

            return PartialView("EditResult", books);
        }
    }
}