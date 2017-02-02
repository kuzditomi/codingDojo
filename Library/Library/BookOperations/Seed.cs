using System;
using System.Collections.Generic;
using Library.Contracts.Models;
using Library.DatabaseOperations;
using Library.Helpers;

namespace Library.BookOperations
{
    public static class Seed
    {
        private static List<Book> books = new List<Book>();

        public static void GenerateBooks()
        {
            books.Add(new Book("apple", "newton", 2006));
            books.Add(new Book("car", "bela", 2000));
            books.Add(new Book("pineapple", "peti", 1998));
            books.Add(new Book("phone", "tamas", 2002));
            books.Add(new Book("tabs", "newton", 2000));
            books.Add(new Book("nivea", "bela", 2009));
            books.Add(new Book("lufi", "peti", 1992));
            Store.MultipleBooks(books);

            ScreenHelper.Reset();
            Console.WriteLine("\nTest data generation was successful");
            MenuHelper.NavigateToMainMenu();
        }
    }
}
