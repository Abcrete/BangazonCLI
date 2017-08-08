using System;
using System.Collections.Generic;
using System.Linq;
using Bangazon.Models;
using Microsoft.Data.Sqlite;

namespace Bangazon.Managers
{
    // Class to create Methods pertaining to Product Class
    // Add product to customer
    // Return all products
    // Remove Product
    // Return one product
    // Return stale product
    // Return most popular products

    //This class is authored by Azim. 
    public class ProductManager
    {
        // Product will be added to this list when GetProducts Method makes a call to the DB     T.L
        private List<Product> _products = new List<Product>();

        private DatabaseInterface _db;

        // An instance of DatabaseInterface is made in the Program.cs file
        // Authored by : Azim
        public ProductManager(DatabaseInterface db)
        {
            _db = db;
        }

        // Method to Add a Product to DataBase
        // Dependencies/FK
        // 1. Customer table
        // 2. ProductType table
        // requires 8 arguments
        // Returns ID of last Product entered
        // Authored by Azim
        public int AddProduct(Product newProduct, Customer customer, ProductType prodType){
            // Insert into DB
            int newProductId = _db.Insert($"INSERT INTO product VALUES (null, '{newProduct.title}', '{newProduct.description}', {newProduct.price}, {newProduct.quantity}, {customer.CustomerId}, {prodType.id}, '{newProduct.dateCreated}')");
            
                _products.Add(new Product(){
                id = newProductId,
                title = newProduct.title,
                description= newProduct.description,
                price = newProduct.price,
                quantity = newProduct.quantity,
                dateCreated= newProduct.dateCreated
            });

            return newProductId;
        }
        // This method gets all Products from databse
        // Dependencies/FK
        // 1. Customer table
        // 2. ProductType table
        // requires 8 arguments
        // Returns List of Products
        // Authored by Azim


        public List<Product> GetProducts(){
            _db.Query("select * from product",
                (SqliteDataReader reader) => {
                    _products.Clear();
                    while (reader.Read ())
                    {
                        _products.Add(new Product(){
                            id = reader.GetInt32(0),
                            title = reader[1].ToString(),
                            description = reader[2].ToString(),
                            price = reader.GetInt32(3),
                            customerId = reader.GetInt32(4),
                            productTypeId = reader.GetInt32(5),
                            dateCreated = reader.GetDateTime(7)
                        });
                    }
                }
            );
            return _products;
        }
        // This method removes a product by its id passed in from databse
        // requires id of the product
        // Authored by Azim
        public bool RemoveProduct(int id)
        {
            _db.Insert($"DELETE FROM Product WHERE ProductID = {id}");
            return true;
        }
        // This method gets a single Product from databse
        // requires id of the product
        // Authored by Azim
        public Product GetProduct(int id)  => _products.SingleOrDefault(prod => prod.id == id);

    }
}