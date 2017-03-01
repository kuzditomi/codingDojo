using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Library.Helpers
{
    public abstract class InputReaderBase<T> : IInputReader<T>
    {
        public IConsoleReader Reader { get; set; }

        public InputReaderBase(IConsoleReader reader)
        {
            Reader = reader;
        }

        public InputReaderBase()
        {
        }

        private T ReadFromInput()
        {
            bool canConvert;
            string input;
            do
            {
                input = Reader.ReadInput();
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

    public class ReadConsoleInput : IConsoleReader
    {
        public string ReadInput()
        {
            return Console.ReadLine();
        }
    }

    public class StringInputReader : InputReaderBase<string>
    {
        public IConsoleReader Reader { get; set; }

        public StringInputReader(IConsoleReader reader)
        {
            Reader = reader;
        }

        protected override bool CanConvert(string input)
        {
            return input != "";
        }

        protected override string Convert(string input)
        {
            return input;
        }
    }

    public class BookDataInputReader : InputReaderBase<string>
    {
        public IConsoleReader Reader { get; set; }

        public BookDataInputReader(IConsoleReader reader)
        {
            Reader = reader;
        }

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

    public class NumberInputReader : InputReaderBase<int>
    {
        public IConsoleReader Reader { get; set; }

        public NumberInputReader(IConsoleReader reader)
        {
            Reader = reader;
        }

        protected override bool CanConvert(string input)
        {
            return Regex.IsMatch(input, @"^[0-9]+$");
        }

        protected override int Convert(string input)
        {
            return int.Parse(input);
        }
    }

    public class ReadMenuSelection : InputReaderBase<int>
    {
        public IConsoleReader Reader { get; set; }

        public ReadMenuSelection(IConsoleReader reader)
        {
            Reader = reader;
        }

        protected override bool CanConvert(string input)
        {
            var pattern = string.Format("^[0-{0}]$");
            return input != null && Regex.IsMatch(input, @pattern);
        }

        protected override int Convert(string input)
        {
            return int.Parse(input);
        }
    }
}
