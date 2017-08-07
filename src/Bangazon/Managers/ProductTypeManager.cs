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
    public class ProductTypeManager
    {
        // Customers will be added to this list when GetCustomers Method makes a call to the DB     T.L
        private List<ProductType> _productTypes = new List<ProductType>();

        private DatabaseInterface _db;

        // An instance of DatabaseInterface is made in the Program.cs file
        // When the CustomerManager Instance is created, it is passed the DatabaseInterface instance
        // Authored by : Tamela Lerma
        public ProductTypeManager(DatabaseInterface db)
        {
            _db = db;
        }

        // Method to Add a Customer to DataBase
        // No Dependencies/FK
        // requires 7 arguments
        // Returns ID of last Customer entered
        // Authored by Tamela Lerma
        public int AddProductType (string name)
        {
            int id = _db.Insert($"insert into ProductType values(null, '{name}')"); // int to Store the Last ID for object that is added
            
            // A new Instance of Customer is made and it's properties are set
            // Once properties are set, it is added to the List<Customers>
            _productTypes.Add(
                new ProductType()
                {
                    Id = id,
                    Name = name,
                }
            );
           
            return id;
        }

        // public method that returns a type Customer
        // method Queries DB to return all Customers in a table
        // Their properties are set and added to List<Customer>
        // Authored by : Tamela Lerma
        public List<ProductType> GetProductTypes ()
        {
            _db.Query("select * from Customer", (SqliteDataReader reader) =>{
                _productTypes.Clear();
                while(reader.Read())
                {
                    _productTypes.Add(new ProductType(){
                        Id = reader.GetInt32(0),
                        Name =  reader[1].ToString()
                    });
                }
            });

            return _productTypes;
        }

        // Method that accepts 1 argument which is CustomerId
        // Returns a single type Customer from DB
        // Authored by Tamela Lerma
        public ProductType GetProductType (int id) => _productTypes.SingleOrDefault(type => type.Id == id);
    }
}