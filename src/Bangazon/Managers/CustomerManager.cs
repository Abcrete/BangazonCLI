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
        // Customers will be added to this list when GetCustomers Method makes a call to the DB     T.L
        private List<Customer> _customers = new List<Customer>();

        private DatabaseInterface _db;

        // An instance of DatabaseInterface is made in the Program.cs file
        // When the CustomerManager Instance is created, it is passed the DatabaseInterface instance
        // Authored by : Tamela Lerma
        public CustomerManager(DatabaseInterface db)
        {
            _db = db;
        }

        // Method to Add a Customer to DataBase
        // No Dependencies/FK
        // requires 7 arguments
        // Returns ID of last Customer entered
        // Authored by Tamela Lerma
        public int AddCustomer (string name, string streetAddress, string city, string state, string zip, string phone)
        {
            int id = _db.Insert($"insert into Customer values(null, '{name}', '{streetAddress}', '{city}', '{state}', '{zip}', '{phone}')"); // int to Store the Last ID for object that is added
            
            // A new Instance of Customer is made and it's properties are set
            // Once properties are set, it is added to the List<Customers>
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

        // public method that returns a type Customer
        // method Queries DB to return all Customers in a table
        // Their properties are set and added to List<Customer>
        // Authored by : Tamela Lerma
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