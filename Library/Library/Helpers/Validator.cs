using System;
using System.Text.RegularExpressions;

namespace Library.Helpers
{
    class Validator
    {
        public static int GetANumber()
        {
            var result = -1;
            do
            {
                var input = Console.ReadLine();
                if (Regex.IsMatch(input, @"^[0-9]+$"))
                    result = Convert.ToInt32(input);
                else
                    Console.WriteLine("Please type a number");
            } while (result == -1);
            return result;
        }

        public static string GetAString()
        {
            var result = "";
            do
            {
                var input = Console.ReadLine();
                if (input != "")
                    result = input;
                else
                    Console.WriteLine("Please type the data defined above");
            } while (result == "");
            return result;
        }
    }
}
