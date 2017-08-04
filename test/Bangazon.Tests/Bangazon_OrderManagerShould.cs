using System;
using System.Collections.Generic;
using Bangazon.Managers;
using Bangazon.Models;
using Xunit;

namespace Bangazon.Tests
{
    public class OrderManagerShould
    {

        private readonly OrderManager _manager;
        private readonly DatabaseInterface _db;

        public OrderManagerShould()
        {
             _db = new DatabaseInterface("BANGAZON_TEST_DB");
            _manager = new OrderManager(_db);  // initialize OrderManager
        }

        [Fact]
        public void CreateNewOrder()
        {
            Product kite = new Product();  // create a product 
            bool success = _manager.CreateOrder(kite, 1); // add that product to new order, that is created on call, second argument is customer id
            Assert.True(success);
        }

        [Fact]
        public void ListOrders()
        {
            List<Order> orders = _manager.GetOrders(); // retrieve all orders as a List
            foreach(Order o in orders) // iterate through all orders
            {
                Assert.IsType<Order>(o); // assert that what is returned is a list of orders
            }
        }

        [Fact]
        public void AddPaymentTypeToOrder()
        {
            _manager.AddPayment(1, 1);  // pass order id and payment id to the manager
        }
    }
}
