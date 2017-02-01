using System.Data.Entity;
using Library.Contracts.Models;

namespace Library.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Reader> Readers { get; set; }
    }
}
