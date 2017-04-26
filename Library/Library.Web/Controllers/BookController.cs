using System.Collections.Generic;
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
        
        [HttpPost]
        public List<Book> Add(SearchViewModel input)
        {
            var book = new Book(input.Title, input.Author, input.Year);
            _bookRepository.StoreABook(book);

            var books = _bookRepository.GetBookByTitle(input.Title);
            return books;
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
        
        public ActionResult Return(int bookId)
        {
            _bookRepository.ReturnABook(bookId);
            return View("Index");
        }

        [HttpGet]
        public PartialViewResult Search(SearchViewModel input)
        {
            List<Book> books=new List<Book>();
            if (input.Title != null)
            {
                books = _bookRepository.GetBookByTitle(input.Title);
            }
            else if(input.Author!= null)
            {
                books = _bookRepository.GetBookByAuthor(input.Author);
            }
            else if(input.Year != 0)
            {
                books = _bookRepository.GetBookByYear(input.Year);
            }

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