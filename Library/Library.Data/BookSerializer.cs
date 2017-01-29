using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Library.Contracts;
using Library.Contracts.Models;

namespace Library.Data
{
    public class BookSerializer : IBookSerializer
    {
        public void Save(IEnumerable<Book> book)
        {
            var bookDtos = book.Select(b => new Models.Book(b.Id, b.Title, b.Author, b.Year)).ToList();

            IFormatter formatter = new BinaryFormatter();
            using (var stream = new FileStream("books.yeti", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, bookDtos);
            }
        }

        public IEnumerable<Book> Load()
        {
            IFormatter formatter = new BinaryFormatter();
            using (var stream = new FileStream("books.yeti", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var bookDtos = (List<Models.Book>)formatter.Deserialize(stream);
                return bookDtos.Select(b => new Book(b.Id, b.Title, b.Author, b.Year)).ToList();
            }
        }
    }
}
