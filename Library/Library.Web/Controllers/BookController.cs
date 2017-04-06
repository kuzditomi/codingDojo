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

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Borrow()
        {
            return View();
        }

        public ActionResult Return()
        {
            return View();
        }
        public ActionResult Search()
        {
            return View();
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
    }
}