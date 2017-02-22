using System;

namespace Library.Contracts.Models
{
    public class Book
    {
        public Book(){}

        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public bool Available { get; set; } = true;
        public Reader Reader { get; set; }
        public DateTime DueDate { get; set; } = new DateTime(1900,1,1);

        public Book(string title, string author, int year)
        {
            Title = title;
            Author = author;
            Year = year;
        }
    }
}
