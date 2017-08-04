using System;
using Xunit;
using System.Collections.Generic;
using Bangazon.Models;
using Bangazon.Managers;

namespace Bangazon.Tests
{
    public class CustomerManagerShould
    {
        private readonly CustomerManager _register;

        public CustomerManagerShould()
        {
            _register = new CustomerManager();
        }


        [Theory]
        [InlineData("Sarah Jones", "787878", "Nash", "TN", "37128", "615-676-6767")]
  
        public void AddNewCustomer(string name, string streetAddress, string city, string state, string zip, string phone)
        {
            int id = _register.AddCustomer(name, streetAddress, city, state, zip, phone);
            Assert.True(id != 0);
        }

        [Fact]
        public void ListCustomers()
        {
            List<Customer> customers = _register.GetCustomers();

            Assert.IsType<List<Customer>>(customers);
        }


        [Theory]
        [InlineData("Sarah Jones", "787878", "Nash", "TN", "37128", "615-676-6767")]
        public void GetSingleCustomer (string name, string streetAddress, string city, string state, string zip, string phone)
        {
            
            int id = _register.AddCustomer(name, streetAddress, city, state, zip, phone);

            Customer customer = _register.GetCustomer(id);
            Assert.True(customer.CustomerId == id);
        }
    }
}
