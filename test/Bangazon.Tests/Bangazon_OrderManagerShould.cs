using System;
using System.Collections.Generic;
using Bangazon.Managers;
using Bangazon.Models;
using Xunit;

// Authored by Jason Smith
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
            // Product kite = new Product();  // create a product 
            int id = _manager.CreateOrder(2, 1); // add that product to new order, that is created on call, second argument is customer id
            Assert.IsType<int>(id);
        }

        [Fact]
        public void ListOrders()
        {
            var orders = _manager.GetOrders(); // retrieve all orders as a List
            foreach(Order o in orders) // iterate through all orders
            {
                Assert.IsType<Order>(o); // assert that what is returned is a list of orders
            }
            Assert.True(orders.Count > 0);
        }


        [Fact]
        public void AddPaymentTypeToOrder()
        {
            var added = _manager.AddPayment(1, 1);  // pass payment id and order id to the manager
            Assert.True(added);
        }

        [Fact]
        public void AddProductToOrder()
        {
            var id = _manager.AddProductToOrder(1, 2); // pass product id and order id to the manager 
            Assert.IsType<int>(id);
        }

        [Fact]
        public void RetrieveOrdersForCustomersProducts()
        {
            var orders = _manager.RevenueReport(1); // pass customer id, returns all orders that contain a product from that customer
            Assert.IsType<List<(int, string, int, double)>>(orders);
        }
    }
}

