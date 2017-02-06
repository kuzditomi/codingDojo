using System;

namespace Library.File.Models
{
    [Serializable]
    public class Reader
    {
        public string Name { get; set; }
        public Reader(string name)
        {
            Name = name;
        }
    }
}
