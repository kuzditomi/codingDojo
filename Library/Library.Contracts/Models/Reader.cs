using System;

namespace Library.Contracts.Models
{
    public class Reader
    {
        public string Name { get; set; }
        public Reader(string name)
        {
            Name = name;
        }
    }
}
