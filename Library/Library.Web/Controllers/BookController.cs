using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Library.Contracts.Models;
using Library.IOC;
using Autofac;
using Library.Contracts;
using Library.Web.Models;

namespace Library.Web.Controllers
{
    public class BookController : Controller
    {
        private new static readonly Resolver Resolver = new Resolver();
        private static IContainer Container { get; set; }
        private readonly IBookRepository _bookRepository;

        public BookController()
        {
            Container = Resolver.BuildContainer().Build();
            _bookRepository = Container.Resolve<IBookRepository>();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add()
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
        public ActionResult Borrow()
        {
            return View();
        }

        [HttpPost]
        public PartialViewResult Borrow(string tbId, string tbName, string tbDays)
        {
            var reader = new Reader(tbName);
            _bookRepository.BorrowABook(int.Parse(tbId), reader, int.Parse(tbDays));

            var book = _bookRepository.GetBookById(int.Parse(tbId));
            var books = new List<Book> { book };
            return PartialView("SearchResult", books);
        }

        [HttpGet]
        public ActionResult Return()
        {
            return View();
        }

        [HttpPost]
        public PartialViewResult Return(string tbId)
        {
            var book = _bookRepository.ReturnABook(int.Parse(tbId));
            var books = new List<Book> { book };

            return PartialView("SearchResult", books);
        }

        [HttpGet]
        public PartialViewResult Search(SearchViewModel input)
        {
            var books = _bookRepository.GetBookByTitle(input.Title);

            return PartialView("SearchResult", books);
        }

        public ActionResult Delete(int bookId)
        {
            var book = _bookRepository.GetBookById(bookId);
            _bookRepository.DeleteBook(book);

            return View("DeleteResult", book);
        }

        public ActionResult List()
        {
            var books = _bookRepository.GetAllBooks();

            return View(books);
        }

        public ActionResult Edit(int bookId)
        {
            var book = _bookRepository.GetBookById(bookId);

            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book bookToEdit)
        {
            var book = _bookRepository.GetBookById(bookToEdit.Id);

            book.Title = bookToEdit.Title;
            book.Author = bookToEdit.Author;
            book.Year = bookToEdit.Year;
            _bookRepository.StoreABook(book);

            return View(book);
        }
    }
}