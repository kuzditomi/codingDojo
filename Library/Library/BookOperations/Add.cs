using System;
using Library.Contracts;
using Library.Helpers;

namespace Library.BookOperations
{
    public class Add
    {
        private readonly IBookRepository _bookrepository;
        readonly ScreenHelper _screenHelper = new ScreenHelper();
        readonly MenuHelper _menuHelper = new MenuHelper();

        public Add(IBookRepository repo)
        {
            _bookrepository = repo;
        }

        public void NewBook()
        {
            //reset meghivodott? 
            //azt az értéket kapta amit mockoltam neki? 
            //azt irta ki a vegen amit a mocktol kapott?
            //5-6 assert egy teszthez már smell
            _screenHelper.Reset();
            var newBook = _screenHelper.GetNewBookDetails();

            _bookrepository.StoreABook(newBook);
            
            _screenHelper.PrintBookAddedMessage(newBook);
            _menuHelper.NavigateToMainMenu();
        }
    }
}
