using System;
using Library.Helpers;

namespace Library.Book
{
    public class Book
    {
        public int Id { get; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public bool Available { get; set; } = true;
        public Reader.Reader Reader { get; set; }
        public DateTime DueDate { get; set; }

        public Book(int id, string title, string author, int year)
        {
            Id = id;
            Title = title;
            Author = author;
            Year = year;
        }
    }
}
