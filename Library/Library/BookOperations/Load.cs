using System;
using System.Diagnostics;
using System.Linq;
using Library.Contracts;
using Library.Helpers;
using Library.Sql;
using Library.Sql.Models;

namespace Library.BookOperations
{
    internal interface ILoad
    {
        void LazyLoad();
        void EagerLoad();
    }

    class Load : ILoad
    {
        private readonly IBookRepository _bookRepository;
        private readonly IScreenHelper _screenHelper;
        private readonly IMenuHelper _menuHelper;

        public Load(IBookRepository repo, IScreenHelper screenHelper, IMenuHelper menuHelper)
        {
            _bookRepository = repo;
            _screenHelper = screenHelper;
            _menuHelper = menuHelper;
        }

        public void LazyLoad()
        {
            _screenHelper.Reset();
            var sw = new Stopwatch();

            _screenHelper.PrintLazyLoading();
            using (var context = new DataContext())
            {
                sw.Start();
                var books = context.Books.ToList<Book>();

                foreach (var book in books)
                {
                    if(book.Reader?.Address?.Street != null)
                        Console.WriteLine(book.Reader.Address.Street);
                }

                sw.Stop();
            }

            _screenHelper.PrintElapsedTime(sw.Elapsed);

            _menuHelper.NavigateToMainMenu();
        }
        
        public void EagerLoad()
        {
            _screenHelper.Reset();
            var sw = new Stopwatch();

            _screenHelper.PrintEagerLoading();
            using (var context = new DataContext())
            {
                sw.Start();
                var books = context.Books.Include("Reader.Address").ToList();
                foreach (var book in books)
                {
                    if(book.Reader?.Address.Street!=null)
                        Console.WriteLine(book.Reader.Address.Street);
                }
                sw.Stop();
            }

            _screenHelper.PrintElapsedTime(sw.Elapsed);

            _menuHelper.NavigateToMainMenu();
        }
    }
}