using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Sql.Models
{
    public class Reader
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
        [ForeignKey("AddressId")]
        public Address Address { get; set; }

        public Reader() { }
        
        public Reader(string name)
        {
            Name = name;
        }
    }
}
