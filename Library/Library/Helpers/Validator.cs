using System;
using System.Text.RegularExpressions;

namespace Library.Helpers
{
    interface InputReader<T>
    {
        T Read();
    }

    abstract class InputReaderBase<T> : InputReader<T>
    {
        private T ReadFromInput()
        {
            var canConvert = false;
            string input;
            do
            {
                Console.WriteLine("Please type a number");
                input = Console.ReadLine();
                canConvert = this.CanConvert(input);
            } while (!canConvert);

            return this.Convert(input);
        }

        protected abstract bool CanConvert(string input);
        protected abstract T Convert(string input);

        public T Read()
        {
            return this.ReadFromInput();
        }
    }

    class StringInputReader : InputReaderBase<string>
    {
        protected override bool CanConvert(string input)
        {
            throw new NotImplementedException();
        }

        protected override string Convert(string input)
        {
            throw new NotImplementedException();
        }
    }

    class NumberInputReader : InputReader<int>
    {
        public int Read()
        {
            throw new NotImplementedException();
        }
    }

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
