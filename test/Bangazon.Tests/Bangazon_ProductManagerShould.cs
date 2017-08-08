using System;
using Bangazon.Models;
using Bangazon.Managers;
using System.Collections.Generic;
using Xunit;

namespace Bangazon.Tests
{
    public class ProductManagerShould: IDisposable
    {

        private readonly ProductManager _manager;
        private readonly DatabaseInterface _db;
        Customer customer = new Customer();
        ProductType prodType = new ProductType();
        Product newProduct = new Product();

        public ProductManagerShould()
        {
            _db = new DatabaseInterface("BANGAZON_TEST_DB");
            _manager = new ProductManager(_db);
                      
            _db.CheckCustomerTable();
            _db.CheckOrderTable();
            _db.CheckPaymentTypeTable();
            _db.CheckProductTable();
            _db.CheckProductTypeTable();
            _db.CheckProdOrderTable();
            _db.CheckProductTable();

        }

        // Add product to customer
        // Tests to see if Products are really being added by our methods.
        [Fact]
        public void AddNewProduct()
        {
                newProduct.title = "Rocket"; 
                newProduct.description= "It flies"; 
                newProduct.price = 200000; 
                newProduct.quantity = 10;
                customer.CustomerId = 1;
                prodType.Id = 1;
                newProduct.dateCreated= DateTime.Today; 
                
            
            var result = _manager.AddProduct(newProduct);
            Assert.True(result !=0);
        }
        // Return all the products
        [Fact]
        public void GetAllProducts()
        {            
            var products = _manager.GetProducts();
            Assert.IsType<List<Product>>(products);
        }
        
        // Remove Product by its id
        [Fact]
        public void DeleteProduct()
        {  
            var result = _manager.AddProduct(newProduct);
            var deleted = _manager.RemoveProduct(result);
            Assert.True(deleted);
        }
        // Return one product by its id
        [Fact]
        public void GetProduct()
        {  
            var result = _manager.AddProduct(newProduct);
            Product product = _manager.GetProduct(result);
            Assert.True(product.ProductId == result);
        }
        // Return stale product(see the req!)

        // Return most popular product


        public void Dispose()
        {
            _db.Delete("DELETE FROM product");
        }
    }
}
