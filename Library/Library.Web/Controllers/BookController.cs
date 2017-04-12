using System.Web.Mvc;
using Library.Contracts.Models;
using Library.Sql;

namespace Library.Web.Controllers
{
    public class BookController : Controller
    {
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
        public ActionResult Add(string tbTitle, string tbAuthor, string tbYear)
        {
            var bookRepository = new SqlBookRepostiroy();
            var book = new Book(tbTitle, tbAuthor, int.Parse(tbYear));
            bookRepository.StoreABook(book);

            return RedirectToAction("SearchResults", book);
        }

        [HttpGet]
        public ActionResult Borrow()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Borrow(string tbId, string tbName, string tbDays)
        {
            var bookRepository = new SqlBookRepostiroy();
            var reader = new Reader(tbName);
            bookRepository.BorrowABook(int.Parse(tbId), reader, int.Parse(tbDays));

            var book = bookRepository.GetBook(int.Parse(tbId));
            return RedirectToAction("SearchResults", book);
        }

        [HttpGet]
        public ActionResult Return()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Return(string tbId)
        {
            var bookRepository = new SqlBookRepostiroy();
            var book = bookRepository.ReturnABook(int.Parse(tbId));
            
            return RedirectToAction("SearchResults", book);
        }

        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string tbId)
        {
            var bookRepository = new SqlBookRepostiroy();
            var book = bookRepository.GetBook(int.Parse(tbId));

            return RedirectToAction("SearchResults", book);
        }

        public ActionResult List()
        {
            var bookRepository = new SqlBookRepostiroy();
            var books = bookRepository.GetAllBooks();

            return View(books);
        }

        public ActionResult Edit(int bookId)
        {
            var bookRepository = new SqlBookRepostiroy();
            var book = bookRepository.GetBook(bookId);

            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book bookToEdit)
        {
            var bookRepository = new SqlBookRepostiroy();
            var book = bookRepository.GetBook(bookToEdit.Id);

            book.Title = bookToEdit.Title;
            bookRepository.StoreABook(book);
            
            return View(book);
        }

        public ActionResult SearchResults(Book book)
        {
            return View(book);
        }
    }
}