using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Library
{
    public static class Texts
    {
        public const string InLibrary = "In library";

    }
    public class BookHandler : IBookHandler
    {
        private readonly List<Book> Books = new List<Book>();

        private int NewBookingId => Books.Count;

        public void AddNewBook()
        {
            Console.Clear();

            var newBook = GetNewBookDetails();
            Books.Add(newBook);
            Console.WriteLine($"\r\nBook added: {newBook.Title}, by {newBook.Author} from year {newBook.Year}");
            GoToMainMenu();
        }

        public void SearchForBook()
        {
            var searchMenuItem = MenuHandler.DoSearchMenuSelection();
            switch (searchMenuItem)
            {
                case 1:
                    SearchByTitle();
                    break;
                case 2:
                    SearchByAuthor();
                    break;
                case 3:
                    SearchByReader();
                    break;
                case 4:
                    SearchByYear();
                    break;
                case 5:
                    SearchBeforeYear();
                    break;
                case 6:
                    SearchAfterYear();
                    break;
            }
        }

        public void GenerateBooks()
        {
            Books.Add(new Book(NewBookingId, "apple", "newton", 2006));
            Books.Add(new Book(NewBookingId, "car", "bela", 2000));
            Books.Add(new Book(NewBookingId, "pineapple", "peti", 1998));
            Books.Add(new Book(NewBookingId, "phone", "tamas", 2002));
            Books.Add(new Book(NewBookingId, "tv", "newton", 2000));
            Books.Add(new Book(NewBookingId, "nivea", "bela", 2009));
            Books.Add(new Book(NewBookingId, "lufi", "peti", 1992));
        }

        public void ReturnBook()
        {
            var id = GetBookId();

            var book = Books.FirstOrDefault(b => b.Id == Convert.ToInt32(id));

            if (book != null)
            {
                book.Available = true;
                book.Reader = new Reader(Texts.InLibrary);
            }
            else
            {
                Console.WriteLine("Nincs ilyen könyv");
            }
        }

        private void SearchBy(string property, Func<Book, string, bool> expresssion)
        {
            Console.Clear();
            Console.WriteLine("\r\n{0} of book searched for: ", property);
            var input = GetTextInput();
            
            var result = Books.Where(b => expresssion(b,input)).ToList();
            if (result.Count == 0)
            {
                Console.WriteLine("\r\nNo book found for your search");
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

        //ToDo: generalize the search methods?
        //ToDo: Indicate if there is no match
        private void SearchByTitle()
        {
            this.SearchBy("Title", (b, input) => b.Title.ToLower().Contains(input.ToLower()));
        }

        private void SearchByAuthor()
        {
            Console.Clear();
            Console.WriteLine("\r\nAuthor of book searched for: ");
            var input = GetTextInput();

            var isMatch = false;
            foreach (var book in Books)
            {
                if (book.Author.ToLower().Contains(input))
                {
                    PrintBookDetails(book);
                    isMatch = true;
                }
            }
            if (!isMatch)
                Console.WriteLine("\r\nNo book found for your search");
            GoToMainMenu();
        }
        private void SearchByReader()
        {
            Console.Clear();
            Console.WriteLine("\r\nReader of the book searched for: ");
            var input = GetTextInput();

            var isMatch = false;
            foreach (var book in Books)
            {
                if (book.Reader != null && book.Reader.Name.ToLower().Contains(input))
                {
                    PrintBookDetails(book);
                    isMatch = true;
                }
            }
            if (!isMatch)
                Console.WriteLine("\r\nNo book found for your search");
            GoToMainMenu();
        }

        private void SearchByYear()
        {
            Console.Clear();
            Console.WriteLine("\r\nPublication year of book searched for: ");
            var input = GetANumber();

            var isMatch = false;
            foreach (var book in Books)
            {
                if (book.Year == input)
                {
                    PrintBookDetails(book);
                    isMatch = true;
                }
            }
            if (!isMatch)
                Console.WriteLine("\r\nNo book found for your search");
            GoToMainMenu();
        }

        private int GetANumber()
        {
            var result = -1;
            do
            {
                var input = Console.ReadLine();
                if (Regex.IsMatch(input, @"^[0-9]+$"))
                    result = Convert.ToInt32(input);
                else
                    Console.WriteLine("Please type a year with numbers");
            } while (result == -1);
            return result;
        }

        private void SearchBeforeYear()
        {
            Console.Clear();
            Console.WriteLine("\r\nPublication year BEFORE the book is searched for: ");
            var input = GetANumber();

            var isMatch = false;
            foreach (var book in Books)
            {
                if (book.Year < input)
                {
                    PrintBookDetails(book);
                    isMatch = true;
                }
            }
            if (!isMatch)
                Console.WriteLine("\r\nNo book found for your search");
            GoToMainMenu();
        }

        private void SearchAfterYear()
        {
            Console.Clear();
            Console.WriteLine("\r\nPublication year AFTER the book is searched for: ");
            var input = GetANumber();

            var isMatch = false;
            foreach (var book in Books)
            {
                if (book.Year > input)
                {
                    PrintBookDetails(book);
                    isMatch = true;
                }
            }
            if (!isMatch)
                Console.WriteLine("\r\nNo book found for your search");
            GoToMainMenu();
        }

        public void GetBooksList()
        {
            Console.Clear();

            Console.WriteLine("====== List of books ======");

            foreach (var book in Books)
            {
                PrintBookDetails(book);
            }
            GoToMainMenu();
        }

        //ToDo: Include borrowing action in the borrow menu?
        public void BorrowBook()
        {
            var id = GetBookId();
            var reader = GetNewReader();
            foreach (var book in Books.Where(book =>
            book.Id == Convert.ToInt32(id) && book.Available))
            {
                book.Reader = reader;
                book.Available = false;
                Console.WriteLine($"{book.Title} is borrowed by {reader.Name} now.");
            }
            GoToMainMenu();
        }

        private string GetReaderOfBook(string title)
        {
            foreach (var book in Books.Where(book => book.Title == title && book.Reader != null))
            {
                return book.Reader.Name;
            }
            return "In Library";
        }

        private static Reader GetNewReader()
        {
            Console.WriteLine("\r\nReader name: ");
            return new Reader(Console.ReadLine());
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
            var name = GetAString();
            Console.Write("Author of the book: ");
            var author = GetAString();
            Console.Write("Publication year of the book: ");
            var year = GetANumber();

            return new Book(NewBookingId, name, author, Convert.ToInt32(year));
        }

        private static void GoToMainMenu()
        {
            Console.WriteLine("\r\nPress Enter to go back to main menu.");
            Console.ReadLine();
        }

        private void PrintBookDetails(Book book)
        {
            Console.WriteLine($"{book.Id} - {book.Title}, by {book.Author} from year {book.Year}, Current holder: {GetReaderOfBook(book.Title)}");
        }

        private string GetTextInput()
        {
            var text = string.Empty;
            do
            {
                var input = Console.ReadLine();
                if (input != null)
                {
                    text = input;
                }
                else
                {
                    Console.WriteLine("\r\nPlease enter the name of the book");
                }

            } while (text == string.Empty);
            return text;
        }

        private string GetAString()
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