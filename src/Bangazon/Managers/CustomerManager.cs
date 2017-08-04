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
        private string _connectionString = $"Data Source = {Environment.GetEnvironmentVariable("BANGAZON_TEST_DB")}";
        private SqliteConnection _connection;

        public CustomerManager()
        {
            _connection = new SqliteConnection(_connectionString);
        }

        // Method to Add a Customer to DataBase
        // No Dependencies/FK
        // requires 7 arguments
        // Authored by Tamela Lerma
        public int AddCustomer (string name, string streetAddress, string city, string state, string zip, string phone)
        {
            int _lastId = 0; // int to Store the Last ID for object that is added

            using (_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand();

                //Insert New Customer
                dbcmd.CommandText = $"insert into Customer values(null, '{name}', '{streetAddress}', '{city}', '{state}', '{zip}', '{phone}')";
                Console.WriteLine(dbcmd.CommandText);
                dbcmd.ExecuteNonQuery();

                // Return the ID of the new row
                dbcmd.CommandText = $"select last_insert_rowid()";
                using (SqliteDataReader dr = dbcmd.ExecuteReader())
                {
                    if(dr.Read()){
                        _lastId = dr.GetInt32(0);
                    }else {
                        throw new Exception("Unable to insert value");
                    }

                    // remove from memory
                    dbcmd.Dispose();
                    _connection.Close();
                }
            }
            return _lastId;
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