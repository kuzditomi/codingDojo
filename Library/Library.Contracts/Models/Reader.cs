﻿using System;

namespace Library.Contracts.Models
{
    [Serializable]
    public class Reader
    {
        public Reader(){}
        public int Id { get; set; }
        public string Name { get; set; }
        public Reader(string name)
        {
            Name = name;
        }
    }
}
