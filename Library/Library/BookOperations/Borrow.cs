﻿using Library.Contracts;
using Library.Contracts.Models;
using Library.Contracts.PublicAPI.BookOperations;
using Library.Contracts.PublicAPI.Helpers;

namespace Library.BookOperations
{
    public class Borrow : IBorrow
    {
        private readonly IBookRepository _bookrepository;
        private readonly IScreenHelper _screenHelper;
        private readonly IMenuHelper _menuHelper;

        public Borrow(IBookRepository repo, IScreenHelper screenHelper, IMenuHelper menuHelper)
        {
            _bookrepository = repo;
            _screenHelper = screenHelper;
            _menuHelper = menuHelper;
        }

        public void PerformBorrowingProcess()
        {
            _screenHelper.Reset();
            var id = _screenHelper.GetBookId();
            var reader = _screenHelper.GetNewReader();
            var daysToBorrow = _screenHelper.GetBorrowDays();

            var book = BorrowSingleBook(id, reader, daysToBorrow);
            _screenHelper.PrintBookBorrowedMessage(book, reader);

            _menuHelper.NavigateToMainMenu();
        }

        public Book BorrowSingleBook(int id, Reader reader, int daysToBorrow)
        {
            return _bookrepository.BorrowABook(id, reader, daysToBorrow);
        }
    }
}
