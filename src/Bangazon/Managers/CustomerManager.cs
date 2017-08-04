using System;
using System.Collections.Generic;
using Bangazon.Models;

namespace Bangazon.Managers
{
    public class CustomerManager
    {
        private List<Customer> _customers = new List<Customer>();


        public bool AddCustomer (string firstName, string lastName, string streetAddress, string city, string state, string zip, string phone)
        {
            int id = 1;
            _customers.Add(
                new Customer()
                {
                    CustomerId = id,
                    FirstName = firstName,
                    LastName = lastName,
                    StreetAddress = streetAddress,
                    City = city,
                    State = state,
                    ZipCode = zip,
                    Phone = phone


                }
            );

            return true;
        }

    }
}