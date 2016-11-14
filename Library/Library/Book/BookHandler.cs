using System;
using System.Collections.Generic;
using System.Linq;
using Library.Helpers;
using Library.Menu;

namespace Library.Book
{
    public class BookHandler : IBookHandler
    {
        private readonly List<Book> _books = new List<Book>();

        private int NewBookingId
        {
            get { return _books.Count; }
        }

        public void AddNewBook()
        {
            Console.Clear();

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
            _books.Add(new Book(NewBookingId, "tv", "newton", 2000));
            _books.Add(new Book(NewBookingId, "nivea", "bela", 2009));
            _books.Add(new Book(NewBookingId, "lufi", "peti", 1992));
        }

        public void ReturnBook()
        {
            var id = GetBookId();

            var book = _books.FirstOrDefault(b => b.Id == Convert.ToInt32(id));

            if (book != null)
            {
                book.Available = true;
                book.Reader = new Reader.Reader(Texts.InLibrary);
            }
            else
            {
                Console.WriteLine(Texts.NoBook);
            }
        }

        private void SearchBy(string property, Func<Book, string, bool> expression)
        {
            Console.Clear();
            Console.WriteLine("\r\n{0} of book searched for: ", property);
            var input = Validator.GetAString();
            
            var result = _books.Where(b => expression(b,input)).ToList();
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
            SearchBy("Reader", (b, input)=>b.Reader.Name.ToLower().Contains(input.ToLower()));
        }

        private void SearchByYear()
        {
            Console.Clear();
            Console.WriteLine(Texts.BooksFound);
            var input = Validator.GetANumber();

            var isMatch = false;
            foreach (var book in _books)
            {
                if (book.Year == input)
                {
                    PrintBookDetails(book);
                    isMatch = true;
                }
            }
            if (!isMatch)
                Console.WriteLine(Texts.NoBook);
            GoToMainMenu();
        }

        private void SearchBeforeYear()
        {
            Console.Clear();
            Console.WriteLine(Texts.BooksFound);
            var input = Validator.GetANumber();

            var isMatch = false;
            foreach (var book in _books)
            {
                if (book.Year < input)
                {
                    PrintBookDetails(book);
                    isMatch = true;
                }
            }
            if (!isMatch)
                Console.WriteLine(Texts.NoBook);
            GoToMainMenu();
        }

        private void SearchAfterYear()
        {
            Console.Clear();
            Console.WriteLine(Texts.BooksFound);
            var input = Validator.GetANumber();

            var isMatch = false;
            foreach (var book in _books)
            {
                if (book.Year > input)
                {
                    PrintBookDetails(book);
                    isMatch = true;
                }
            }
            if (!isMatch)
                Console.WriteLine(Texts.NoBook);
            GoToMainMenu();
        }

        public void GetBooksList()
        {
            Console.Clear();
            Console.WriteLine("====== List of books ======");

            foreach (var book in _books)
            {
                PrintBookDetails(book);
            }
            GoToMainMenu();
        }

        public void BorrowBook()
        {
            var id = GetBookId();
            var reader = GetNewReader();
            foreach (var book in _books.Where(book =>
            book.Id == Convert.ToInt32(id) && book.Available))
            {
                book.Reader = reader;
                book.Available = false;
                Console.WriteLine("{0} is borrowed by {1} now.", book.Title, reader.Name);
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

        private static Reader.Reader GetNewReader()
        {
            Console.WriteLine("\r\nReader name: ");
            return new Reader.Reader(Console.ReadLine());
        }

        private static string GetBookId()
        {
            Console.WriteLine("\r\nId of the book: ");
            return Console.ReadLine();
        }

        private Book GetNewBookDetails()
        {
            Console.WriteLine("====== Add a book ======");
            Console.Write("Name of the book: ");
            var name = Validator.GetAString();
            Console.Write("Author of the book: ");
            var author = Validator.GetAString();
            Console.Write("Publication year of the book: ");
            var year = Validator.GetANumber();

            return new Book(NewBookingId, name, author, Convert.ToInt32(year));
        }

        private static void GoToMainMenu()
        {
            Console.WriteLine("\r\nPress Enter to go back to main menu.");
            Console.ReadLine();
        }

        private void PrintBookDetails(Book book)
        {
            Console.WriteLine("{0} - {1}, by {2} from year {3}," +
                              " Current holder: {4}", 
                              book.Id, book.Title, book.Author, book.Year, GetReaderOfBook(book.Title));
        }
    }
}