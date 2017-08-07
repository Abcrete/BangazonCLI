using System;
using Xunit;
using System.Collections.Generic;
using Bangazon.Models;
using Bangazon.Managers;

namespace Bangazon.Tests
{
    // public Unit Testing Class
    // Should provide ability to Add a Customer, Retrieve a List of Customers, and a single Customer
    // Authored by : Tamela Lerma
    public class CustomerManagerShould
    {
        // Internal Property to set reference to Type CustomerManager   T.L.
        private readonly CustomerManager _register;
        // Internal Property to set reference to DatabaseInterface and DB   T.L.
        private readonly DatabaseInterface _db;
        // Constructor method to access internal properties and set their values
        // Environment variable is passed as argument to Instance of DatabaseInterface
        // that Instance is then passed as an argument to the CustomerManager Instance to establish connection to SQL DB
        //  Authored by : Tamela Lerma
        public CustomerManagerShould()
        {
            _db = new DatabaseInterface("BANGAZON_TEST_DB");
            _register = new CustomerManager(_db);
        }


        [Theory]
        [InlineData("Sarah Jones", "787878", "Nash", "TN", "37128", "615-676-6767")]
        // This method requires 7 arguments to set Customer propeties
        // returns CustomerId
        // Authored by : Tamela Lerma
        public void AddNewCustomer(string name, string streetAddress, string city, string state, string zip, string phone)
        {
            int id = _register.AddCustomer(name, streetAddress, city, state, zip, phone);
            Assert.True(id != 0);
        }

        // Method that returns a List with type Customer
        // Authored by : Tamela Lerma
        [Fact]
        public void ListCustomers()
        {
            List<Customer> customers = _register.GetCustomers();

            Assert.IsType<List<Customer>>(customers);
        }


        [Theory]
        [InlineData("Sarah Jones", "787878", "Nash", "TN", "37128", "615-676-6767")]
        // Method that returns a single Customer
        // Accepts 1 arguement which is CustomerId
        // Authored by : Tamela Lerma
        public void GetSingleCustomer (string name, string streetAddress, string city, string state, string zip, string phone)
        {
            int id = _register.AddCustomer(name, streetAddress, city, state, zip, phone);

            Customer customer = _register.GetCustomer(id);
            Assert.True(customer.CustomerId == id);
        }

        // Method that deletes test entries of Customers in the DB  T.L.
        public void Dispose()
        {
            _db.Delete("DELETE FROM Customer");
        }
    }
}
