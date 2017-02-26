﻿using Library.BookOperations;
using Library.Contracts;
using Library.File;
using Library.Helpers;
using Library.Menu;
using Library.Sql;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenu menuItem;
            IBookRepository repo = new SqlBookRepostiroy();
            var screenHelper = new ScreenHelper();
            var menuHelper = new MenuHelper();
            //IBookRepository repo = new FileBookRepository();
            
            var _add = new Add(repo, screenHelper, menuHelper);
            var _borrow = new Borrow(repo);
            var _list = new List(repo);
            var _return = new Return(repo);
            var _search = new Search(repo);
            var _seed = new Seed(repo);
            var _load = new Load(repo);

            do
            {
                menuItem = menuHelper.DoMainMenuSelection();

                switch (menuItem)
                {
                    case MainMenu.Add:
                        _add.AddNewBook();
                        break;
                    case MainMenu.Search:
                        _search.SingleBook();
                        break;
                    case MainMenu.Borrow:
                        _borrow.SingleBook();
                        break;
                    case MainMenu.Return:
                        _return.ReturnBook();
                        break;
                    case MainMenu.List:
                        _list.AllBooks();
                        break;
                    case MainMenu.Expiring:
                        _list.ExpiringBooks();
                        break;
                    case MainMenu.Seed:
                        _seed.GenerateData(100, _borrow);
                        break;
                    case MainMenu.LazyLoad:
                        _load.LazyLoad();
                        break;
                    case MainMenu.EagerLoad:
                        _load.EagerLoad();
                        break;
                }
            } while (menuItem != MainMenu.Exit);
        }
    }
}