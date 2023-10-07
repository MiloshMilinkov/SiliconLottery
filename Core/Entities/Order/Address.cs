using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.Order
{
    public class Address
    {
        public Address(){}
        public Address( string streetName, int streetNumber, string city, string zipCode)
        {
            StreetNumber = streetNumber;
            StreetName = streetName;
            City = city;
            ZipCode = zipCode;
        }

        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
        public string  City { get; set; }
        public string ZipCode { get; set; }
    }
}