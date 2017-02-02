using System;
using System.Collections.Generic;
using Library.Contracts;
using Library.Contracts.Models;
using Library.Helpers;

namespace Library.BookOperations
{
    public class Seed
    {
        private readonly IBookRepository _bookrepository;

        public Seed(IBookRepository repo)
        {
            _bookrepository = repo;
        }

        private List<Book> books = new List<Book>();

        public void GenerateBooks()
        {
            books.Add(new Book("apple", "newton", 2006));
            books.Add(new Book("car", "bela", 2000));
            books.Add(new Book("pineapple", "peti", 1998));
            books.Add(new Book("phone", "tamas", 2002));
            books.Add(new Book("tabs", "newton", 2000));
            books.Add(new Book("nivea", "bela", 2009));
            books.Add(new Book("lufi", "peti", 1992));
            _bookrepository.StoreMultipleBooks(books);

            ScreenHelper.Reset();
            Console.WriteLine("\nTest data generation was successful");
            MenuHelper.NavigateToMainMenu();
        }
    }
}
