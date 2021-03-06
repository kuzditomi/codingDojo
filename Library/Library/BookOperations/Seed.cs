﻿using System;
using System.Collections.Generic;
using Library.Contracts;
using Library.Contracts.Models;
using Library.Contracts.PublicAPI.BookOperations;
using Library.Contracts.PublicAPI.Helpers;

namespace Library.BookOperations
{
    public class Seed : ISeed
    {
        private readonly IBookRepository _bookRepository;
        private readonly IScreenHelper _screenHelper;
        private readonly IMenuHelper _menuHelper;

        public Seed(IBookRepository repo, IScreenHelper screenHelper, IMenuHelper menuHelper)
        {
            _bookRepository = repo;
            _screenHelper = screenHelper;
            _menuHelper = menuHelper;
        }

        private readonly List<Book> _books = new List<Book>();

        public void GenerateData(int amount, IBorrow borrow)
        {
            GenerateBooks(amount);
            GenerateReaders(amount, borrow);

            _screenHelper.Reset();
            _screenHelper.PrintSuccessfulSeeding();
            _menuHelper.NavigateToMainMenu();
        }
        
        private void GenerateBooks(int amount)
        {
            var rnd = new Random();
            for (var i = 0; i < amount; i++)
            {
                _books.Add(new Book(
                    "Book-" + Guid.NewGuid().ToString().Substring(0, 4),
                    "Author-" + Guid.NewGuid().ToString().Substring(0, 4),
                    rnd.Next(1500, 2017)));
            }
            _bookRepository.StoreMultipleBooks(_books);
        }

        private static void GenerateReaders(int amount, IBorrow borrow)
        {
            var rnd = new Random();
            for (var i = 1; i < amount; i = i + 10)
            {
                borrow.BorrowSingleBook(i, new Reader
                {
                    Name = "Reader-" + Guid.NewGuid().ToString().Substring(0, 5)
                }, rnd.Next(1, 30));
            }
        }
    }
}
