using System;

namespace Library.Data.Models
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
