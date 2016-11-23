using System;
using System.Text.RegularExpressions;

namespace Library.Helpers
{
    interface InputReader<T>
    {
        T Read(int boundary);
    }

    abstract class InputReaderBase<T> : InputReader<T>
    {
        private T ReadFromInput(int boundary)
        {
            bool canConvert;
            string input;
            do
            {
                input = Console.ReadLine();
                canConvert = CanConvert(input, boundary);
                if (!canConvert)
                    Console.WriteLine("Please type the required data: ");
            } while (!canConvert);

            return Convert(input);
        }

        protected abstract bool CanConvert(string input, int boundary);
        protected abstract T Convert(string input);

        public T Read(int boundary = 0)
        {
            return ReadFromInput(boundary);
        }
    }

    class StringInputReader : InputReaderBase<string>
    {
        private static StringInputReader _reader;
        private StringInputReader() { }
        public static StringInputReader Reader => _reader ?? (_reader = new StringInputReader());

        protected override bool CanConvert(string input, int boundary)
        {
            return input != "";
        }

        protected override string Convert(string input)
        {
            return input;
        }
    }

    class NumberInputReader : InputReaderBase<int>
    {
        private static NumberInputReader _reader;
        private NumberInputReader() { }
        public static NumberInputReader Reader => _reader ?? (_reader = new NumberInputReader());

        protected override bool CanConvert(string input, int boundary)
        {
            return Regex.IsMatch(input, @"^[0-9]+$");
        }

        protected override int Convert(string input)
        {
            return int.Parse(input);
        }
    }

    class ReadMenuSelection : InputReaderBase<int>
    {
        private static ReadMenuSelection _reader;
        private ReadMenuSelection() { }
        public static ReadMenuSelection Reader => _reader ?? (_reader = new ReadMenuSelection());

        protected override bool CanConvert(string input, int boundary)
        {
            var pattern = string.Format("^[0-{0}]$", boundary);
            return input != null && Regex.IsMatch(input, @pattern);
        }

        protected override int Convert(string input)
        {
            return int.Parse(input);
        }
    }
}
