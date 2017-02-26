using System;
using System.Linq;
using Library.Contracts;
using Library.Contracts.Models;
using Library.Helpers;
using Library.Menu;

namespace Library.BookOperations
{
    public class Search
    {
        private readonly IBookRepository _bookRepository;
        private readonly IScreenHelper _screenHelper;
        private readonly IMenuHelper _menuHelper;

        public Search(IBookRepository repo, IScreenHelper screenHelper, IMenuHelper menuHelper)
        {
            _bookRepository = repo;
            _screenHelper = screenHelper;
            _menuHelper = menuHelper;
        }

        public void SingleBook()
        {
            var searchMenuItem = _menuHelper.DoSearchMenuSelection();
            switch (searchMenuItem)
            {
                case SearchFor.Title:
                    SearchForTitle();
                    break;
                case SearchFor.Author:
                    SearchForAuthor();
                    break;
                case SearchFor.Reader:
                    SearchForReader();
                    break;
                case SearchFor.Year:
                    SearchForYear();
                    break;
                case SearchFor.BeforeYear:
                    SearchBeforeYear();
                    break;
                case SearchFor.AfterYear:
                    SearchAfterYear();
                    break;
            }
        }

        private void SearchForTitle()
        {
            SearchBy("Title", (b, input) => b.Title.ToLower().Contains(input.ToLower()));
        }

        private void SearchForAuthor()
        {
            SearchBy("Author", (b, input) => b.Author.ToLower().Contains(input.ToLower()));
        }

        private void SearchForReader()
        {
            SearchBy("Reader", (b, input) => b.Reader != null && b.Reader.Name.ToLower().Contains(input.ToLower()));
        }

        private void SearchForYear()
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

        private void SearchBy(string property, Func<Book, string, bool> expression)
        {
            _screenHelper.Reset();
            var input = _screenHelper.ReadInputString(property);

            var books = _bookRepository.GetAllBooks();
            var result = books.Where(b => expression(b, input)).ToList();
            _screenHelper.PrintSearchResult(result);

            _menuHelper.NavigateToMainMenu();
        }
    }
}
