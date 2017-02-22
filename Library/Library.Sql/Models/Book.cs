using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Sql.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public bool Available { get; set; } = true;
        public int? ReaderId { get; set; }
        [ForeignKey("ReaderId")]
        public Reader Reader { get; set; }

        public DateTime DueDate { get; set; } = new DateTime(1900,1,1);

        public Book() { }

        public Book(string title, string author, int year)
        {
            Title = title;
            Author = author;
            Year = year;
        }
    }
}
