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
            var bookId = bookRepository.GetBookByTitle(tbTitle).Id;

            return RedirectToAction("SearchResults", new {id = bookId });
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

            var book = bookRepository.GetBookById(int.Parse(tbId));
            return RedirectToAction("SearchResults", new { id = book.Id });
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

            return RedirectToAction("SearchResults", new { id = book.Id });
        }

        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string tbId)
        {
            return RedirectToAction("SearchResults", new { id = int.Parse(tbId) });
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
            var book = bookRepository.GetBookById(bookId);

            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book bookToEdit)
        {
            var bookRepository = new SqlBookRepostiroy();
            var book = bookRepository.GetBookById(bookToEdit.Id);

            book.Title = bookToEdit.Title;
            bookRepository.StoreABook(book);

            return View(book);
        }

        public ActionResult SearchResults(int id)
        {
            var bookRepository = new SqlBookRepostiroy();
            var book = bookRepository.GetBookById(id);

            return View(book);
        }
    }
}