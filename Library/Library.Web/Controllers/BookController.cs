using System.Web.Mvc;
using Library.Contracts.Models;
using Library.File;
using Library.Sql;

namespace Library.Web.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            //          var bookRepository = new FileBookRepository();
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