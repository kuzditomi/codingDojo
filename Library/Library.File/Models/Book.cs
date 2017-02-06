using System;

namespace Library.File.Models
{
    [Serializable]
    public class Book
    {
        public int Id { get; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public bool Available { get; set; } = true;
        public Reader Reader { get; set; }
        public DateTime DueDate { get; set; }

        public Book(string title, string author, int year)
        {
            Title = title;
            Author = author;
            Year = year;
        }
    }
}
