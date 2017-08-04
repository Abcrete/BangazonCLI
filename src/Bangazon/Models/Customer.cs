using System;
using System.Collections.Generic;
using Bangazon.Managers;

namespace Bangazon.Models
{
    public class Customer
    {
        public int CustomerId {get; set;}
        public string Name {get; set;}
        public string StreetAddress {get; set;}
        public string City {get; set;}
        public string ZipCode {get; set;}
        public string State {get; set;}
        public string Phone {get; set;}

    }
}