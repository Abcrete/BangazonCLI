using System;
using Bangazon.Models;
using Bangazon.Managers;
using System.Collections.Generic;
using Xunit;

namespace Bangazon.Tests
{
    // This class tests following methods that product manager needs
    // AddProduct to customer
    // Return all the products
    // Remove Product if it has not been added to the order
    // Return single product
    // Return stale product(see the req!)
    // Return most popular product
    // Authored by Azim
    public class ProductManagerShould: IDisposable
    {

        private readonly ProductManager _manager;
        private readonly DatabaseInterface _db;
        Product newProduct = new Product();

        public ProductManagerShould()
        {
            _db = new DatabaseInterface("BANGAZON_TEST_DB");
            _manager = new ProductManager(_db);
            _db.CheckProductTable();

        }

        // Add product to customer
        // Tests to see if Products are really being added by our methods.
        // Authored by Azim
        [Fact]
        public void AddNewProduct()
        {
            newProduct.title = "Rocket"; 
            newProduct.description= "It flies"; 
            newProduct.price = 200000; 
            newProduct.quantity = 10;
            newProduct.customerId = 1;
            newProduct.productTypeId = 1;
            var result = _manager.AddProduct(newProduct);
            Assert.True(result !=0);
        }
        // Return all the products
        // Authored by Azim
        [Fact]
        public void GetAllProducts()
        {            
            var products = _manager.GetProducts();
            Assert.IsType<List<Product>>(products);
        }

        // Return all the stale products
        // Authored by Aarti Jaisinghani
        public void GetAllStaleProducts()
        {            
            var products = _manager.GetStaleProducts();
            Assert.IsType<List<Product>>(products);
        }
        
        // Remove Product by its id
        // Authored by Azim
        [Fact]
        public void DeleteProduct()
        {  
            var result = _manager.AddProduct(newProduct);
            var deleted = _manager.RemoveProduct(result);
            Assert.True(deleted);
        }
        // Return one product by its id
        // Authored by Azim
        [Fact]
        public void GetProduct()
        {  
            var result = _manager.AddProduct(newProduct);
            Product product = _manager.GetProduct(result);
            Assert.True(product.id == result);
        }

        public void Dispose()
        {

            _db.Delete("DELETE FROM product");

        }
    }
}
