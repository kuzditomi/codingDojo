using System;
using System.Collections.Generic;
using Library.Contracts.Models;

namespace Library.Helpers
{
    public static class ScreenHelper
    {
        public static void Reset()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void PrintBookDetails(Book book, string reader)
        {
            if (book.Reader != null && book.DueDate.Date < DateTime.Today.AddDays(3) &&
                book.DueDate.Date != new DateTime(1900, 01, 01))
                Console.ForegroundColor = ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.Gray;

            Console.Write("{0} - {1} \tby {2} \t{3}" +
                          " \tCurrent holder: {4}",
                book.Id, book.Title, book.Author, book.Year, reader);

            if (reader == "Library")
                Console.WriteLine();
            else
                Console.WriteLine("\tDue Date: {0}", book.DueDate);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void PrintSearchResult(List<Book> result)
        {
            if (result.Count == 0)
            {
                Console.WriteLine(Texts.NoBook);
            }
            else
            {
                foreach (var book in result)
                {
                    var reader = book.Reader?.Name != null ? book.Reader.Name : "Library";
                    PrintBookDetails(book, reader);
                }
            }
        }

        public static Reader GetNewReader()
        {
            Console.WriteLine("\r\nReader name: ");
            return new Reader(StringInputReader.Reader.Read());
        }

        public static int GetBookId()
        {
            Console.WriteLine("\r\nId of the book: ");
            return NumberInputReader.Reader.Read();
        }

        public static int GetDueDate()
        {
            Console.WriteLine("\r\nFor how many days: ");
            return NumberInputReader.Reader.Read();
        }

        public static string ReadInputString(string property)
        {
            Console.Write("\r\n{0} of book searched for: ", property);
            return StringInputReader.Reader.Read();
        }

        public static Book GetNewBookDetails()
        {
            Console.WriteLine("====== Add a book ======");
            Console.Write("Name, Author, Publication Year of the book:");
            var input = BookDataInputReader.Reader.Read();

            var data = input.Split(',');
            return new Book(data[0].Trim(), data[1].Trim(), int.Parse(data[2].Trim()));
        }
    }
}
