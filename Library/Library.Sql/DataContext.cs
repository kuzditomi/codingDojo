using System.Data.Entity;
using Library.Sql.Models;

namespace Library.Sql
{
    public class DataContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Reader> Readers { get; set; }
        //public DbSet<Address> Addresses { get; set; }
    }

    //public class BookEntity
    //{
    //    [Key]
    //    public int Id { get; set; }

    //    [ForeignKey(nameof(Reader))]
    //    public int? ReaderId { get; set; }

    //    public virtual ReaderEntity Reader { get; set; }
    //}

    //public class ReaderEntity
    //{
    //    [Key]
    //    public int Id { get; set; }

    //    public string Name { get; set; }

    //}
}
