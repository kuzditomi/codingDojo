using Library.BookOperations;
using Library.Contracts;
using Library.File;
using Library.Helpers;
using Library.Menu;
using Library.Sql;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenu menuItem;
            IBookRepository repository = new SqlBookRepostiroy();
            var screenHelper = new ScreenHelper();
            var menuHelper = new MenuHelper();
            //IBookRepository repo = new FileBookRepository();
            
            var _add = new Add(repository, screenHelper, menuHelper);
            var _borrow = new Borrow(repository, screenHelper, menuHelper);
            var _list = new List(repository, screenHelper, menuHelper);
            var _return = new Return(repository, screenHelper, menuHelper);
            var _search = new Search(repository, screenHelper, menuHelper);
            var _seed = new Seed(repository, screenHelper, menuHelper);
            var _load = new Load(repository, screenHelper, menuHelper);

            do
            {
                menuItem = menuHelper.DoMainMenuSelection();

                switch (menuItem)
                {
                    case MainMenu.Add:
                        _add.AddNewBook();
                        break;
                    case MainMenu.Search:
                        _search.SingleBook();
                        break;
                    case MainMenu.Borrow:
                        _borrow.PerformBorrowingProcess();
                        break;
                    case MainMenu.Return:
                        _return.ReturnBook();
                        break;
                    case MainMenu.List:
                        _list.AllBooks();
                        break;
                    case MainMenu.Expiring:
                        _list.ExpiringBooks();
                        break;
                    case MainMenu.Seed:
                        _seed.GenerateData(100, _borrow);
                        break;
                    case MainMenu.LazyLoad:
                        _load.LazyLoad();
                        break;
                    case MainMenu.EagerLoad:
                        _load.EagerLoad();
                        break;
                }
            } while (menuItem != MainMenu.Exit);
        }
    }
}