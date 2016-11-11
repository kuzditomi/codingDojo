using System;
using System.Text.RegularExpressions;

namespace Library
{
    public static class MenuHandler
    {
        public static int DoMainMenuSelection()
        {
            PrintMainMenu();
            return ReadMenuSelection();
        }

        public static int DoSearchMenuSelection()
        {
            PrintSearchMenu();
            return ReadMenuSelection();
        }

        private static void PrintMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose a number from the library options:\r\n");

            Console.WriteLine("1 - Add new book");
            Console.WriteLine("2 - Search book");
            Console.WriteLine("3 - Borrow book");
            Console.WriteLine("4 - Return book");
            Console.WriteLine("5 - Get books' list");
            Console.WriteLine("6 - Generate books' data");
            Console.WriteLine("0 - Exit");
        }

        private static void PrintSearchMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose a number from the search options:\r\n");

            Console.WriteLine("1 - Search by title");
            Console.WriteLine("2 - Search by author");
            Console.WriteLine("3 - Search by reader");
            Console.WriteLine("4 - Search by year");
            Console.WriteLine("5 - Search books published before ...");
            Console.WriteLine("6 - Search books published after ...");
        }

        private static int ReadMenuSelection()
        {
            var result = -1;
            do
            {
                Console.Write("\r\nSelected option: ");
                var input = Console.ReadLine();
                if (input != null && Regex.IsMatch(input, @"^[0-6]$"))
                {
                    Console.Clear();
                    result = input == "" ? 0 : Convert.ToInt32(input);
                }
                Console.WriteLine("Please select a number between 0 and 6!");
            } while (result <= -1);
            return result;
        }
    }
}
