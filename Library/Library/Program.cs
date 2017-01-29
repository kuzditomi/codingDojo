using Library.Contracts;
using Library.Menu;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            IBookHandler bookHandler = BookHandler.Instance;
            MainMenu menuItem;

            do
            {
                menuItem = MenuHandler.DoMainMenuSelection();

                switch (menuItem)
                {
                    case MainMenu.Add:
                        bookHandler.AddNewBook();
                        break;
                    case MainMenu.Search:
                        bookHandler.SearchForBook();
                        break;
                    case MainMenu.Borrow:
                        bookHandler.BorrowBook();
                        break;
                    case MainMenu.Return:
                        bookHandler.ReturnBook();
                        break;
                    case MainMenu.List:
                        bookHandler.GetBooksList();
                        break;
                    case MainMenu.Expiring:
                        bookHandler.GetExpiringBooks();
                        break;
                    case MainMenu.Generate:
                        bookHandler.GenerateBooks();
                        break;
                    case MainMenu.Save:
                        bookHandler.SaveBooks();
                        break;
                    case MainMenu.Load:
                        bookHandler.LoadBooks();
                        break;
                }
            } while (menuItem != MainMenu.Exit);
        }
    }
}