using System;
using Library.Menu;

namespace Library.Helpers
{
    public static class MenuHelper
    {
        public static void NavigateToMainMenu()
        {
            Console.WriteLine(Texts.GoToMainMenu);
            Console.ReadLine();
        }

        public static MainMenu DoMainMenuSelection()
        {
            PrintMainMenu();
            return ReadMainMenuSelection();
        }

        public static SearchFor DoSearchMenuSelection()
        {
            PrintSearchMenu();
            return ReadSearchMenuSelection();
        }

        private static void PrintMainMenu()
        {
            ScreenHelper.Reset();
            Console.WriteLine("Choose a number from the library options:\r\n");

            Console.WriteLine("0 - Add book");
            Console.WriteLine("1 - Search book");
            Console.WriteLine("2 - Borrow book");
            Console.WriteLine("3 - Return book");
            Console.WriteLine("4 - List all books");
            Console.WriteLine("5 - List expired books");
            Console.WriteLine("6 - Seed data");
            Console.WriteLine("7 - LazyLoad");;
            Console.WriteLine("8 - EagerLoad");;
            Console.WriteLine("9 - Exit");
        }

        private static void PrintSearchMenu()
        {
            ScreenHelper.Reset();
            Console.WriteLine("Choose a number from the search options:\r\n");

            Console.WriteLine("0 - Search by title");
            Console.WriteLine("1 - Search by author");
            Console.WriteLine("2 - Search by reader");
            Console.WriteLine("3 - Search by year");
            Console.WriteLine("4 - Search books published before ...");
            Console.WriteLine("5 - Search books published after ...");
        }

        private static SearchFor ReadSearchMenuSelection()
        {
            return (SearchFor) ReadMenuSelection.Reader.Read(boundary:5);
        }

        private static MainMenu ReadMainMenuSelection()
        {
            return (MainMenu) ReadMenuSelection.Reader.Read(boundary:9);
        }
    }
}
