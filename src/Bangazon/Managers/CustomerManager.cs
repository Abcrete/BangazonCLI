using System;
using System.Collections.Generic;
using System.Linq;
using Bangazon.Models;
using Microsoft.Data.Sqlite;

namespace Bangazon.Managers
{
    // Class to create Methods pertaining to Customer Class
    // Add a Customer
    // Retrieve a list of Customers
    // Retrieve a single Customer
    // Authored by Tamela Lerma
    public class CustomerManager
    {
        private List<Customer> _customers = new List<Customer>();

        private DatabaseInterface _db;

        public CustomerManager(DatabaseInterface db)
        {
            _db = db;
        }

        // Method to Add a Customer to DataBase
        // No Dependencies/FK
        // requires 7 arguments
        // Authored by Tamela Lerma
        public int AddCustomer (string name, string streetAddress, string city, string state, string zip, string phone)
        {
            int id = _db.Insert($"insert into Customer values(null, '{name}', '{streetAddress}', '{city}', '{state}', '{zip}', '{phone}')"); // int to Store the Last ID for object that is added
            
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
            _db.Query("select * from Customer", (SqliteDataReader reader) =>{
                _customers.Clear();
                while(reader.Read())
                {
                    _customers.Add(new Customer(){
                        CustomerId = reader.GetInt32(0),
                        Name =  reader[1].ToString(),
                        StreetAddress = reader[2].ToString(),
                        State = reader[3].ToString(),
                        ZipCode = reader[4].ToString(),
                        Phone = reader[6].ToString()
                    });
                }
            });

            return _customers;
        }

        // Method that accepts 1 argument which is CustomerId
        // Returns a single type Customer from DB
        // Authored by Tamela Lerma
        public Customer GetCustomer (int id) => _customers.SingleOrDefault(person => person.CustomerId == id);
    }
}