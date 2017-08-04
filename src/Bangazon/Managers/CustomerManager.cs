using System;
using System.Collections.Generic;
using System.Linq;
using Bangazon.Models;

namespace Bangazon.Managers
{
    public class CustomerManager
    {
        private List<Customer> _customers = new List<Customer>();


        public int AddCustomer (string name, string streetAddress, string city, string state, string zip, string phone)
        {
            int id = 1;
            _customers.Add(
                new Customer()
                {
                    CustomerId = id,
                    Name = name,
                    StreetAddress = streetAddress,
                    City = city,
                    State = state,
                    ZipCode = zip,
                    Phone = phone
                }
            );

            return id;
        }

        public List<Customer> GetCustomers ()
        {
            foreach (var item in _customers)
            {
                Console.WriteLine($"item");
                
            }
            return _customers;
        }

        public Customer GetCustomer (int id) => _customers.SingleOrDefault(person => person.CustomerId == id);
    }
}