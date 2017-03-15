using System;
using System.Linq;
using System.Text.RegularExpressions;
using Library.Contracts.PublicAPI.Helpers;

namespace Library.Helpers
{
    public abstract class InputReaderBase<T> : IInputReader<T>
    {
        private T ReadFromInput()
        {
            bool canConvert;
            string input;
            do
            {
                input = Console.ReadLine();
                canConvert = CanConvert(input);
                if (!canConvert)
                    Console.WriteLine("Please type the required data: ");
            } while (!canConvert);

            return Convert(input);
        }

        protected abstract bool CanConvert(string input);
        protected abstract T Convert(string input);

        public T Read()
        {
            return ReadFromInput();
        }
    }

    public class StringInputReader : InputReaderBase<string>
    {
        protected override bool CanConvert(string input)
        {
            return input != "";
        }

        protected override string Convert(string input)
        {
            return input;
        }
    }

    public class NumberInputReader : InputReaderBase<int>
    {
        protected override bool CanConvert(string input)
        {
            return input != null && Regex.IsMatch(input, @"^[0-9]+$");
        }

        protected override int Convert(string input)
        {
            return int.Parse(input);
        }
    }

    public class BookDataInputReader : InputReaderBase<string>
    {
        protected override bool CanConvert(string input)
        {
            int n;
            var valid = true;
            var data = input.Split(',');
            if (data.Count() < 3 || data[0].Length < 1 || data[1].Length < 1 || !Int32.TryParse(data[2], out n))
                valid = false;
            return valid;
        }

        protected override string Convert(string input)
        {
            return input;
        }
    }
}
