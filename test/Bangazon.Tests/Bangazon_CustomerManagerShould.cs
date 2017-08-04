using System;
using Xunit;
using Bangazon.Models;
using Bangazon.Managers;

namespace Bangazon.Tests
{
    public class CustomerManagerShould
    {

        // private readonly RegisterCustomer _register;

        // public CustomerManagerShould()
        // {
        //     _register = new RegisterCustomer();
        // }


        [Fact]
        public void AddNewCustomer()
        {
            var custManager = new CustomerManager();
            var cust = new Customer("Sarah", "Jones", "563756", "Nash", "TN", "37128", "615-787-8898");
            bool result = custManager.AddCustomer(cust);
            Assert.True(result);
        }

        [Fact]
        public void ListCustomers()
        {

        }
    }
}
