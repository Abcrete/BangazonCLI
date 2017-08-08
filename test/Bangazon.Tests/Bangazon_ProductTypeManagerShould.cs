using System;
using Xunit;
using System.Collections.Generic;
using Bangazon.Models;
using Bangazon.Managers;

namespace Bangazon.Tests
{
    // public Unit Testing Class
    // Should provide ability to Add a ProductType, Retrieve a List of ProductTypes, and a single ProductType
    // Authored by : Tamela Lerma
    public class ProductTypeManagerShould
    {
        // Internal Property to set reference to Type ProductTypeManager   T.L.
        private readonly ProductTypeManager _register;
        // Internal Property to set reference to DatabaseInterface and DB   T.L.
        private readonly DatabaseInterface _db;
        // Constructor method to access internal properties and set their values
        // Environment variable is passed as argument to Instance of DatabaseInterface
        // that Instance is then passed as an argument to the ProductTypeManager Instance to establish connection to SQL DB
        //  Authored by : Tamela Lerma
        public ProductTypeManagerShould()
        {
            _db = new DatabaseInterface("BANGAZON_TEST_DB");
            _register = new ProductTypeManager(_db);
        }


        [Theory]
        [InlineData("food")]
        [InlineData("toys")]
        [InlineData("clothing")]
        // This method requires 1 arguments to set 
        // returns ProductTypeId
        // Authored by : Tamela Lerma
        public void AddNewProductType(string name)
        {
            int id = _register.AddProductType(name);
            Assert.True(id != 0);
        }

        // Method that returns a List with type ProductType
        // Authored by : Tamela Lerma
        [Fact]
        public void ListCustomers()
        {
            List<ProductType> productTypes = _register.GetProductTypes();

            Assert.IsType<List<ProductType>>(productTypes);
        }


        [Theory]
        [InlineData("books")]
        [InlineData("kitchen")]
        [InlineData("tech")]
        // Method that returns a single ProductType
        // Accepts 1 arguement which is ProductTypeId
        // Authored by : Tamela Lerma
        public void GetSingleProductType (string name)
        {
            int productTypeId = _register.AddProductType(name);

            ProductType productType = _register.GetProductType(productTypeId);
            Assert.True(productType.Id == productTypeId);
        }

        // Method that deletes test entries of ProductType in the DB  T.L.
        public void Dispose()
        {
            _db.Delete("DELETE FROM Customer");
        }
    }
}
