using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    public class BookHandler
    {
        public readonly List<Book> Books = new List<Book>();

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
            foreach (var book in Books.Where(book => book.Id == Convert.ToInt32(id)))
            {
                book.Available = true;
                book.Reader = new Reader("In Library");
            }
        }

        public int NewBookingId => Books.Count;

        //ToDo: generalize the search methods?
        //ToDo: Indicate if there is no match
        private void SearchByReader()
        {
            Console.WriteLine("\r\nReader of the book searched for: ");
            var reader = Console.ReadLine();
            foreach (var book in Books)
            {
                if (book.Reader != null && book.Reader.Name == reader)
                {
                    PrintBookDetails(book);
                }
            }
            GoToMainMenu();
        }

        private void SearchByTitle()
        {
            Console.WriteLine("\r\nTitle of book searched for: ");
            var title = Console.ReadLine();
            foreach (var book in Books)
            {
                if (book.Title == title)
                {
                    PrintBookDetails(book);
                }
            }
            GoToMainMenu();
        }

        private void SearchByAuthor()
        {
            Console.WriteLine("\r\nAuthor of book searched for: ");
            var author = Console.ReadLine();
            foreach (var book in Books)
            {
                if (book.Author == author)
                {
                    PrintBookDetails(book);
                }
            }
            GoToMainMenu();
        }

        private void SearchByYear()
        {
            Console.WriteLine("\r\nPublication year of book searched for: ");
            var year = Console.ReadLine();
            foreach (var book in Books)
            {
                if (book.Year == Convert.ToInt32(year))
                {
                    PrintBookDetails(book);
                }
            }
            GoToMainMenu();
        }

        private void SearchBeforeYear()
        {
            Console.WriteLine("\r\nPublication year BEFORE the book is searched for: ");
            var year = Console.ReadLine();
            foreach (var book in Books)
            {
                if (book.Year < Convert.ToInt32(year))
                {
                    PrintBookDetails(book);
                }
            }
            GoToMainMenu();
        }

        private void SearchAfterYear()
        {
            Console.WriteLine("\r\nPublication year AFTER the book is searched for: ");
            var year = Console.ReadLine();
            foreach (var book in Books)
            {
                if (book.Year > Convert.ToInt32(year))
                {
                    PrintBookDetails(book);
                }
            }
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
            foreach (var book in Books.Where(book => book.Id == Convert.ToInt32(id) && book.Available))
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
            var name = Console.ReadLine();
            Console.Write("Author of the book: ");
            var author = Console.ReadLine();
            Console.Write("Publication year of the book: ");
            var year = Console.ReadLine();

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
    }
}