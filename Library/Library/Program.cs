using Library.BookOperations;
using Library.Helpers;
using Library.Menu;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenu menuItem;

            do
            {
                menuItem = MenuHelper.DoMainMenuSelection();

                switch (menuItem)
                {
                    case MainMenu.Add:
                        Add.NewBook();
                        break;
                    case MainMenu.Search:
                        Search.SingleBook();
                        break;
                    case MainMenu.Borrow:
                        Borrow.SingleBook();
                        break;
                    case MainMenu.Return:
                        Return.ReturnBook();
                        break;
                    case MainMenu.List:
                        List.AllBooks();
                        break;
                    case MainMenu.Expiring:
                        List.ExpiringBooks();
                        break;
                    case MainMenu.Seed:
                        Seed.GenerateBooks();
                        break;
                }
            } while (menuItem != MainMenu.Exit);
        }
    }
}