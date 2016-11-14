using System;
using System.Text.RegularExpressions;

namespace Library.Menu
{
    public static class MenuHandler
    {
        public static MainMenu DoMainMenuSelection()
        {
            PrintMainMenu();
            return ReadMainMenuSelection();
        }

        public static Search DoSearchMenuSelection()
        {
            PrintSearchMenu();
            return ReadSearchMenuSelection();
        }

        private static void PrintMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose a number from the library options:\r\n");

            Console.WriteLine("0 - Add new book");
            Console.WriteLine("1 - Search book");
            Console.WriteLine("2 - Borrow book");
            Console.WriteLine("3 - Return book");
            Console.WriteLine("4 - Get books' list");
            Console.WriteLine("5 - Generate books' data");
            Console.WriteLine("6 - Exit");
        }

        private static void PrintSearchMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose a number from the search options:\r\n");

            Console.WriteLine("0 - Search by title");
            Console.WriteLine("1 - Search by author");
            Console.WriteLine("2 - Search by reader");
            Console.WriteLine("3 - Search by year");
            Console.WriteLine("4 - Search books published before ...");
            Console.WriteLine("5 - Search books published after ...");
        }

        private static Search ReadSearchMenuSelection()
        {
            return (Search)ReadMenuSelection(5);
        }

        private static MainMenu ReadMainMenuSelection()
        {
            return (MainMenu)ReadMenuSelection(7);
        }

        private static int ReadMenuSelection(int boundary)
        {
            var pattern = string.Format("^[0-{0}]$", boundary);
            var result = -1;
            do
            {
                Console.Write("\r\nSelected option: ");
                var input = Console.ReadLine();
                if (input != null && Regex.IsMatch(input, @pattern))
                {
                    Console.Clear();
                    result = input == "" ? 0 : Convert.ToInt32(input);
                }
                Console.WriteLine("Please select a number from the list above!");
            } while (result <= -1);
            return result;
        }
    }
}
