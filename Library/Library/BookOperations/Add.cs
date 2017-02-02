using System;
using Library.Contracts;
using Library.Helpers;

namespace Library.BookOperations
{
    public class Add
    {
        private readonly IBookRepository _bookrepository;

        public Add(IBookRepository repo)
        {
            _bookrepository = repo;
        }

        public void NewBook()
        {
            ScreenHelper.Reset();

            var newBook = ScreenHelper.GetNewBookDetails();

            _bookrepository.StoreABook(newBook);

            Console.WriteLine("\r\nBook added: {0}, by {1} from year {2}",
                newBook.Title, newBook.Author, newBook.Year);
            MenuHelper.NavigateToMainMenu();
        }
    }
}
