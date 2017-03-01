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

            var consoleReader = new ReadConsoleInput();
            var numberReader = new NumberInputReader(consoleReader);
            var stringInputReader = new StringInputReader(consoleReader);
            var bookReader = new BookDataInputReader(consoleReader);
            var menuReader = new ReadMenuSelection(consoleReader);

            var screenHelper = new ScreenHelper(numberReader, stringInputReader, bookReader);
            var menuHelper = new MenuHelper(menuReader, screenHelper);
            //IBookRepository repo = new FileBookRepository();

            var add = new Add(repository, screenHelper, menuHelper);
            var borrow = new Borrow(repository, screenHelper, menuHelper);
            var fetch = new Fetch(repository, screenHelper, menuHelper, numberReader);
            var takeBack = new TakeBack(repository, screenHelper, menuHelper);
            var search = new Search(repository, screenHelper, menuHelper);
            var seed = new Seed(repository, screenHelper, menuHelper);
            var load = new Load(repository, screenHelper, menuHelper);

            do
            {
                menuItem = menuHelper.DoMainMenuSelection();

                switch (menuItem)
                {
                    case MainMenu.Add:
                        add.AddNewBook();
                        break;
                    case MainMenu.Search:
                        search.SingleBook();
                        break;
                    case MainMenu.Borrow:
                        borrow.PerformBorrowingProcess();
                        break;
                    case MainMenu.Return:
                        takeBack.ReturnBook();
                        break;
                    case MainMenu.List:
                        fetch.ListAllBooks();
                        break;
                    case MainMenu.Expiring:
                        fetch.ListExpiringBooks();
                        break;
                    case MainMenu.Seed:
                        seed.GenerateData(100, borrow);
                        break;
                    case MainMenu.LazyLoad:
                        load.LazyLoad();
                        break;
                    case MainMenu.EagerLoad:
                        load.EagerLoad();
                        break;
                }
            } while (menuItem != MainMenu.Exit);
        }
    }
}