using System;
using System.Text.RegularExpressions;

namespace Library.Helpers
{

    public interface ISimpeStringReader
    {
        string Read();
    }

    public class SimpleStringInputReader: ISimpeStringReader
    {
        public string Read()
        {
            return Console.ReadLine();
        }
    }

    public interface ISimpleIntInputReader
    {
        int Read();
    }

    public class SimpleIntInputReader : ISimpleIntInputReader
    {
        public int Read()
        {
            var input = Console.ReadLine();
            if (input != null && Regex.IsMatch(input, @"^[0-9]+$"))
                return int.Parse(input);
            return 0;
        }
    }
}
