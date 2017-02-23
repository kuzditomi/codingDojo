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
            //make it non-static
            //reset meghivodott? 
            //azt az értéket kapta amit mockoltam neki? 
            //azt irta ki a vegen amit a mocktol kapott?
            //5-6 assert egy teszthez már smell
            ScreenHelper.Reset();

            var newBook = ScreenHelper.GetNewBookDetails();

            _bookrepository.StoreABook(newBook);

            //goto screenhelper
            Console.WriteLine("\r\nBook added: {0}, by {1} from year {2}",
                newBook.Title, newBook.Author, newBook.Year);
            MenuHelper.NavigateToMainMenu();
        }
    }
}
