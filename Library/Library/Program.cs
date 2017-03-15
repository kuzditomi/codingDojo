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
        private static readonly IInputReader<int> NumberReader = new NumberInputReader();
        private static readonly IInputReader<string> StringInputReader = new StringInputReader();
        private static readonly IInputReader<string> BookReader = new BookDataInputReader();

        private static readonly IScreenHelper ScreenHelper = new ScreenHelper(NumberReader, StringInputReader, BookReader);
        private static readonly IMenuHelper MenuHelper = new MenuHelper(NumberReader, ScreenHelper);

        private static readonly IBookRepository Repository = new SqlBookRepostiroy();
        private static readonly Add Add = new Add(Repository, ScreenHelper, MenuHelper);
        private static readonly Borrow Borrow = new Borrow(Repository, ScreenHelper, MenuHelper);
        private static readonly Fetch Fetch = new Fetch(Repository, ScreenHelper, MenuHelper, NumberReader);
        private static readonly TakeBack TakeBack = new TakeBack(Repository, ScreenHelper, MenuHelper);
        private static readonly Search Search = new Search(Repository, ScreenHelper, MenuHelper);
        private static readonly Seed Seed = new Seed(Repository, ScreenHelper, MenuHelper);
        private static readonly Load Load = new Load(Repository, ScreenHelper, MenuHelper);

        static void Main(string[] args)
        {
            MainMenu menuItem;
            //IBookRepository repo = new FileBookRepository();
            
            do
            {
                menuItem = MenuHelper.DoMainMenuSelection();

                switch (menuItem)
                {
                    case MainMenu.Add:
                        Add.AddNewBook();
                        break;
                    case MainMenu.Search:
                        Search.SingleBook();
                        break;
                    case MainMenu.Borrow:
                        Borrow.PerformBorrowingProcess();
                        break;
                    case MainMenu.Return:
                        TakeBack.ReturnBook();
                        break;
                    case MainMenu.List:
                        Fetch.ListAllBooks();
                        break;
                    case MainMenu.Expiring:
                        Fetch.ListExpiringBooks();
                        break;
                    case MainMenu.Seed:
                        Seed.GenerateData(100, Borrow);
                        break;
                    case MainMenu.LazyLoad:
                        Load.LazyLoad();
                        break;
                    case MainMenu.EagerLoad:
                        Load.EagerLoad();
                        break;
                }
            } while (menuItem != MainMenu.Exit);
        }
    }
}