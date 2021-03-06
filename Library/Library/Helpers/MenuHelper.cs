﻿using System;
using Library.Contracts.Models.Menu;
using Library.Contracts.PublicAPI.Helpers;

namespace Library.Helpers
{
    public class MenuHelper : IMenuHelper
    {
        private readonly ISimpleIntInputReader _menuReader;
        private readonly IScreenHelper _screenHelper;

        public MenuHelper(ISimpleIntInputReader menuReader, IScreenHelper screenHelper)
        {
            _menuReader = menuReader;
            _screenHelper = screenHelper;
        }

        public void NavigateToMainMenu()
        {
            Console.WriteLine(Texts.GoToMainMenu);
            Console.ReadLine();
        }

        public MainMenu DoMainMenuSelection()
        {
            PrintMainMenu();
            return ReadMainMenuSelection();
        }

        public SearchFor DoSearchMenuSelection()
        {
            PrintSearchMenu();
            return ReadSearchMenuSelection();
        }

        private void PrintMainMenu()
        {
            _screenHelper.Reset();
            Console.WriteLine("Choose a number from the library options:\r\n");

            Console.WriteLine("0 - Add book");
            Console.WriteLine("1 - Search book");
            Console.WriteLine("2 - Borrow book");
            Console.WriteLine("3 - Return book");
            Console.WriteLine("4 - List all books");
            Console.WriteLine("5 - List expired books");
            Console.WriteLine("6 - Seed data");
            Console.WriteLine("7 - LazyLoad");
            Console.WriteLine("8 - EagerLoad");
            Console.WriteLine("9 - Exit");
        }

        private void PrintSearchMenu()
        {
            _screenHelper.Reset();
            Console.WriteLine("Choose a number from the search options:\r\n");

            Console.WriteLine("0 - Search by title");
            Console.WriteLine("1 - Search by author");
            Console.WriteLine("2 - Search by reader");
            Console.WriteLine("3 - Search by year");
            Console.WriteLine("4 - Search books published before ...");
            Console.WriteLine("5 - Search books published after ...");
        }

        private SearchFor ReadSearchMenuSelection()
        {
            return (SearchFor) _menuReader.Read();
        }

        private MainMenu ReadMainMenuSelection()
        {
            return (MainMenu) _menuReader.Read();
        }
    }
}
