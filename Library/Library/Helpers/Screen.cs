using System;

namespace Library.Helpers
{
    class Screen
    {
        public static void Reset()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
