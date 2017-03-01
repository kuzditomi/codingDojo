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

            var _consoleReader = new ReadConsoleInput();
            var _numberReader = new NumberInputReader(_consoleReader);
            var _stringInputReader = new StringInputReader(_consoleReader);
            var _bookReader = new BookDataInputReader(_consoleReader);
            var _menuReader = new ReadMenuSelection(_consoleReader);

            var _screenHelper = new ScreenHelper(_numberReader, _stringInputReader, _bookReader);
            var _menuHelper = new MenuHelper(_menuReader, _screenHelper);
            //IBookRepository repo = new FileBookRepository();

            var _add = new Add(repository, _screenHelper, _menuHelper);
            var _borrow = new Borrow(repository, _screenHelper, _menuHelper);
            var _list = new List(repository, _screenHelper, _menuHelper, _numberReader);
            var _return = new Return(repository, _screenHelper, _menuHelper);
            var _search = new Search(repository, _screenHelper, _menuHelper);
            var _seed = new Seed(repository, _screenHelper, _menuHelper);
            var _load = new Load(repository, _screenHelper, _menuHelper);

            do
            {
                menuItem = _menuHelper.DoMainMenuSelection();

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
                        _list.ListAllBooks();
                        break;
                    case MainMenu.Expiring:
                        _list.ListExpiringBooks();
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