﻿using System;
using Library.Contracts;
using Library.Helpers;

namespace Library.BookOperations
{
    public class Return
    {
        private readonly IBookRepository _bookRepository;
        private readonly IScreenHelper _screenHelper;
        private readonly IMenuHelper _menuHelper;

        public Return(IBookRepository repo, IScreenHelper screenHelper, IMenuHelper menuHelper)
        {
            _screenHelper = screenHelper;
            _menuHelper = menuHelper;
            _bookRepository = repo;
        }

        public void ReturnBook()
        {
            _screenHelper.Reset();
            var id = _screenHelper.GetBookId();
            var book = _bookRepository.ReturnABook(id);
            if (book == null)
                Console.WriteLine(Texts.NoBook);
            _menuHelper.NavigateToMainMenu();
        }
    }
}
