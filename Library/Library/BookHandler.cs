using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Library.Contracts;
using Library.Contracts.Models;
using Library.Data;
using Library.Helpers;
using Library.Menu;

namespace Library
{
    public class BookHandler : IBookHandler
    {
        private static BookHandler _instance;

        private BookHandler() { }

        public static BookHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BookHandler();
                }
                return _instance;
            }
        }

        private List<Contracts.Models.Book> _books = new List<Book>();

        public int NewBookingId
        {
            get { return _books.Count; }
        }

        public void AddNewBook()
        {
            Screen.Reset();

            var newBook = GetNewBookDetails();
            _books.Add(newBook);
            Console.WriteLine("\r\nBook added: {0}, by {1} from year {2}",
                newBook.Title, newBook.Author, newBook.Year);

            GoToMainMenu();
        }

        public void SearchForBook()
        {
            var searchMenuItem = MenuHandler.DoSearchMenuSelection();
            switch (searchMenuItem)
            {
                case Search.ByTitle:
                    SearchByTitle();
                    break;
                case Search.ByAuthor:
                    SearchByAuthor();
                    break;
                case Search.ByReader:
                    SearchByReader();
                    break;
                case Search.ByYear:
                    SearchByYear();
                    break;
                case Search.BeforeYear:
                    SearchBeforeYear();
                    break;
                case Search.AfterYear:
                    SearchAfterYear();
                    break;
            }
        }

        public void GenerateBooks()
        {
            _books.Add(new Book(NewBookingId, "apple", "newton", 2006));
            _books.Add(new Book(NewBookingId, "car", "bela", 2000));
            _books.Add(new Book(NewBookingId, "pineapple", "peti", 1998));
            _books.Add(new Book(NewBookingId, "phone", "tamas", 2002));
            _books.Add(new Book(NewBookingId, "tabs", "newton", 2000));
            _books.Add(new Book(NewBookingId, "nivea", "bela", 2009));
            _books.Add(new Book(NewBookingId, "lufi", "peti", 1992));

            Screen.Reset();
            Console.WriteLine("\nTest data generation was successful");
            GoToMainMenu();
        }

        public void ReturnBook()
        {
            Screen.Reset();
            var id = GetBookId();

            var book = _books.FirstOrDefault(b => b.Id == Convert.ToInt32(id));

            if (book != null)
            {
                book.Available = true;
                book.Reader = null;
            }
            else
            {
                Console.WriteLine(Texts.NoBook);
            }
            GoToMainMenu();
        }

        private void SearchBy(string property, Func<Book, string, bool> expression)
        {
            Screen.Reset();
            Console.WriteLine("\r\n{0} of book searched for: ", property);
            var input = StringInputReader.Reader.Read();

            var result = _books.Where(b => expression(b, input)).ToList();
            if (result.Count == 0)
            {
                Console.WriteLine(Texts.NoBook);
            }
            else
            {
                foreach (var book in result)
                {
                    PrintBookDetails(book);
                }
            }

            GoToMainMenu();
        }

        private void SearchByTitle()
        {
            SearchBy("Title", (b, input) => b.Title.ToLower().Contains(input.ToLower()));
        }

        private void SearchByAuthor()
        {
            SearchBy("Author", (b, input) => b.Author.ToLower().Contains(input.ToLower()));
        }

        private void SearchByReader()
        {
            SearchBy("Reader", (b, input) => b.Reader.Name.ToLower().Contains(input.ToLower()));
        }

        private void SearchByYear()
        {
            SearchBy("Year", (b, input) => b.Year == int.Parse(input));
        }

        private void SearchBeforeYear()
        {
            SearchBy("Year", (b, input) => b.Year < int.Parse(input));
        }

        private void SearchAfterYear()
        {
            SearchBy("Year", (b, input) => b.Year > int.Parse(input));
        }

        public void GetBooksList()
        {
            Screen.Reset();
            Console.WriteLine("====== List of books ======");

            foreach (var book in _books)
            {
                PrintBookDetails(book);
            }
            GoToMainMenu();
        }

        public void BorrowBook()
        {
            Screen.Reset();
            var id = GetBookId();
            var reader = GetNewReader();
            var daysToBorrow = GetDueDate();
            foreach (var book in _books.Where(book =>
            book.Id == id && book.Available))
            {
                book.Reader = reader;
                book.Available = false;
                book.DueDate = DateTime.Now.AddDays(daysToBorrow);
                Console.WriteLine("{0} is borrowed by {1} until {2}.", book.Title, reader.Name, book.DueDate);
            }
            GoToMainMenu();
        }

        private string GetReaderOfBook(string title)
        {
            foreach (var book in _books.Where(book => book.Title == title && book.Reader != null))
            {
                return book.Reader.Name;
            }
            return Texts.InLibrary;
        }

        private static Reader GetNewReader()
        {
            Console.WriteLine("\r\nReader name: ");
            return new Reader(StringInputReader.Reader.Read());
        }

        private static int GetBookId()
        {
            Console.WriteLine("\r\nId of the book: ");
            return NumberInputReader.Reader.Read();
        }

        private static int GetDueDate()
        {
            Console.WriteLine("\r\nFor how many days: ");
            return NumberInputReader.Reader.Read();
        }

        private Book GetNewBookDetails()
        {
            Console.WriteLine("====== Add a book ======");

            Console.Write("Name, Author, Publication Year of the book:");
            var input = BookDataInputReader.Reader.Read();

            var data = input.Split(',');
            return new Book(NewBookingId, data[0].Trim(), data[1].Trim(), int.Parse(data[2].Trim()));
        }

        private static void GoToMainMenu()
        {
            Console.WriteLine("\r\nPress Enter to go back to main menu.");
            Console.ReadLine();
        }

        private void PrintBookDetails(Book book)
        {
            if (book.Reader != null && book.DueDate.Date < DateTime.Today.AddDays(3))
                Console.ForegroundColor = ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.Gray;

            Console.Write("{0} - {1} \tby {2} \t{3}" +
                              " \tCurrent holder: {4}",
                book.Id, book.Title, book.Author, book.Year,
                GetReaderOfBook(book.Title));

            if (book.Reader == null)
                Console.WriteLine();
            else
                Console.WriteLine("\tDue Date: {0}", book.DueDate);
        }

        public void GetExpiringBooks()
        {
            Screen.Reset();
            Console.WriteLine("How many days should be the limit for the search:");
            var limit = NumberInputReader.Reader.Read();

            foreach (var book in _books)
            {
                if (book.Reader != null && book.DueDate < DateTime.Now.AddDays(limit))
                    PrintBookDetails(book);
            }

            GoToMainMenu();
        }

        public void SaveBooks()
        {
            var serializer = new BookSerializer();
            serializer.Save(_books);

            Screen.Reset();
            Console.WriteLine("\nSaving data to file was successful");
            GoToMainMenu();
        }

        public void LoadBooks()
        {
            var serializer = new BookSerializer();
            _books = serializer.Load().ToList();
            
            Screen.Reset();
            Console.WriteLine("\nLoading data from file was successful");
            GoToMainMenu();
        }
    }
}