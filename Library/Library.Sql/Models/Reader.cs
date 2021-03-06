﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Sql.Models
{
    public class Reader
    {
        [Key]
        public int ReaderId { get; set; }
        public string Name { get; set; }
        public int? AddressId { get; set; }
        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }

        public Reader() { }
        
        public Reader(string name)
        {
            Name = name;
        }
    }
}
