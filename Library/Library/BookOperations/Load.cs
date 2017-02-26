using System;
using System.Diagnostics;
using System.Linq;
using Library.Contracts;
using Library.Helpers;
using Library.Sql;
using Library.Sql.Models;

namespace Library.BookOperations
{
    class Load
    {
        private readonly IBookRepository _bookrepository;
        readonly ScreenHelper _screenHelper = new ScreenHelper();
        readonly MenuHelper _menuHelper = new MenuHelper();

        public Load(IBookRepository repo)
        {
            _bookrepository = repo;
        }

        public void LazyLoad()
        {
            _screenHelper.Reset();
            var sw = new Stopwatch();

            //Lazy loading: delaying the loading of related data, until you specifically request for it.
            Console.WriteLine("Lazy loading happens");
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
            
            Console.WriteLine("Elapsed time: {0}", sw.ElapsedMilliseconds);
            Console.WriteLine();

            _menuHelper.NavigateToMainMenu();
        }
        
        public void EagerLoad()
        {
            _screenHelper.Reset();
            var sw = new Stopwatch();

            //Eager loading: a query for one type of entity also loads related entities as part of the query.
            //Eager loading is achieved using the Include() method.
            Console.WriteLine("Eager loading happens");
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

            Console.WriteLine("Elapsed time: {0}", sw.ElapsedMilliseconds);
            Console.WriteLine();

            _menuHelper.NavigateToMainMenu();
        }
    }
}