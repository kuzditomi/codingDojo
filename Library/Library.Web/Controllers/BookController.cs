﻿using System.Web.Mvc;
using Library.Contracts.Models;
using Library.IOC;
using Library.Sql;
using Autofac;
using Library.Contracts;

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
        public ActionResult Add(string tbTitle, string tbAuthor, string tbYear)
        {
            var book = new Book(tbTitle, tbAuthor, int.Parse(tbYear));
            _bookRepository.StoreABook(book);
            var bookId = _bookRepository.GetBookByTitle(tbTitle).Id;

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
            var reader = new Reader(tbName);
            _bookRepository.BorrowABook(int.Parse(tbId), reader, int.Parse(tbDays));

            var book = _bookRepository.GetBookById(int.Parse(tbId));
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
            var book = _bookRepository.ReturnABook(int.Parse(tbId));

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
            _bookRepository.StoreABook(book);

            return View(book);
        }

        public ActionResult SearchResults(int id)
        {
            var book = _bookRepository.GetBookById(id);

            return View(book);
        }
    }
}