﻿namespace Library.Contracts.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }

        public Address() { }

        public Address(string city, int postalCode, string street, int houseNumber)
        {
            City = city;
            PostalCode = postalCode;
            Street = street;
            HouseNumber = houseNumber;
        }
    }
}
