using System;
using System.Linq;
using Library.Contracts.Models;
using Library.DatabaseOperations;
using Library.Helpers;
using Library.Menu;

namespace Library.BookOperations
{
    public static class Search
    {
        public static void SingleBook()
        {
            var searchMenuItem = MenuHelper.DoSearchMenuSelection();
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
        
        private static void SearchForTitle()
        {
            SearchBy("Title", (b, input) => b.Title.ToLower().Contains(input.ToLower()));
        }

        private static void SearchForAuthor()
        {
            SearchBy("Author", (b, input) => b.Author.ToLower().Contains(input.ToLower()));
        }

        private static void SearchForReader()
        {
            SearchBy("Reader", (b, input) => b.Reader.Name.ToLower().Contains(input.ToLower()));
        }

        private static void SearchForYear()
        {
            SearchBy("Year", (b, input) => b.Year == int.Parse(input));
        }

        private static void SearchBeforeYear()
        {
            SearchBy("Year", (b, input) => b.Year < int.Parse(input));
        }

        private static void SearchAfterYear()
        {
            SearchBy("Year", (b, input) => b.Year > int.Parse(input));
        }
        private static void SearchBy(string property, Func<Book, string, bool> expression)
        {
            ScreenHelper.Reset();
            Console.WriteLine("\r\n{0} of book searched for: ", property);
            var input = StringInputReader.Reader.Read();

            var books = Fetch.GetAllBooks();
            var result = books.Where(b => expression(b, input)).ToList();
            if (result.Count == 0)
            {
                Console.WriteLine(Texts.NoBook);
            }
            else
            {
                foreach (var book in result)
                {
                    ScreenHelper.PrintBookDetails(book);
                }
            }

            MenuHelper.NavigateToMainMenu();
        }
    }
}
