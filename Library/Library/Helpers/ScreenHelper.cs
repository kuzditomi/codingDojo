using System;
using System.Collections.Generic;
using System.Linq;
using Library.Contracts.Models;

namespace Library.Helpers
{
    public class ScreenHelper : IScreenHelper
    {
        private readonly IInputReader<int> _numberReader;
        private readonly IInputReader<string> _stringReader;
        private readonly IInputReader<string> _bookReader;

        public ScreenHelper(IInputReader<int> numberReader, IInputReader<string> stringReader, IInputReader<string> bookReader)
        {
            _numberReader = numberReader;
            _stringReader = stringReader;
            _bookReader = bookReader;
        }

        public void Reset()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void PrintBookDetails(Book book, string readerName)
        {
            if (book.Reader != null && book.DueDate.Date < DateTime.Today.AddDays(3) &&
                book.DueDate.Date != new DateTime(1900, 01, 01))
                Console.ForegroundColor = ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.Gray;

            Console.Write("{0} - {1} \tby {2} \t{3}",
                book.Id, book.Title, book.Author, book.Year);

            if (readerName != null)
            {
                Console.Write(" \tCurrent holder: {0}", readerName);
                Console.Write("\tDue Date: {0}", book.DueDate);
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void PrintSearchResult(IEnumerable<Book> result)
        {
            if (result.ToArray().Length == 0)
            {
                Console.WriteLine(Texts.NoBook);
            }
            else
            {
                foreach (var book in result)
                {
                    var readerName = book.Reader?.Name != null ? book.Reader.Name : "Library";
                    PrintBookDetails(book, readerName);
                }
            }
        }

        public Reader GetNewReader()
        {
            Console.WriteLine("\r\nReader name: ");
            return new Reader(_stringReader.Read());
        }

        public int GetBookId()
        {
            Console.WriteLine("\r\nId of the book: ");
            return _numberReader.Read();
        }

        public int GetBorrowDays()
        {
            Console.WriteLine("\r\nFor how many days: ");
            return _numberReader.Read();
        }

        public string ReadInputString(string property)
        {
            Console.Write("\r\n{0} of book searched for: ", property);
            return _stringReader.Read();
        }

        public Book GetNewBookDetails()
        {
            Console.WriteLine("====== Add a book ======");
            Console.Write("Name, Author, Publication Year of the book:");
            var input = _bookReader.Read();

            var data = input.Split(',');
            return new Book(data[0].Trim(), data[1].Trim(), int.Parse(data[2].Trim()));
        }

        public void PrintBookAddedMessage(Book book)
        {
            Console.WriteLine("\r\nBook added: {0}, by {1} from year {2}",
                book.Title, book.Author, book.Year);
        }

        public void PrintBookBorrowedMessage(Book book, Reader reader)
        {
            Console.WriteLine("{0} is borrowed by {1} until {2}.", book.Title, reader.Name, book.DueDate);
        }

        public void PrintBookListingText()
        {
            Console.WriteLine(Texts.ListOfBooks);
        }

        public void GetLimit()
        {
            Console.WriteLine("How many days should be the limit for the search:");
        }

        public void PrintLazyLoading()
        {
            Console.WriteLine("Lazy loading happens");
        }

        public void PrintEagerLoading()
        {
            Console.WriteLine("Eager loading happens");
        }

        public void PrintElapsedTime(TimeSpan elapsed)
        {
            Console.WriteLine("Elapsed time: {0}", elapsed.Milliseconds);
            Console.WriteLine();
        }

        public void PrintSuccessfulSeeding()
        {
            Console.WriteLine("\nTest data generation was successful");
        }
    }
}
