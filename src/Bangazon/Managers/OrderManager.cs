using System;
using System.Collections.Generic;
using Bangazon.Models;
using Microsoft.Data.Sqlite;

namespace Bangazon.Managers
{
    /*  OrderManager completes all methods against the order table
        Authored by Jason Smith */
    public class OrderManager
    {
        private List<Order> _orders;
        private DatabaseInterface _db;

        /*  Instantiate OrderManager passing a refernce to the db interface
            Authored by Jason Smith */
        public OrderManager(DatabaseInterface db)
        {
            _db = db;
        }

        /*  Add an order to the database and return the id, which is the PK where it was placed 
            Authored by Jason Smith */
        public int CreateOrder(Product prod, Customer cust)
        {
            int id = _db.Insert( $"INSERT INTO [order] VALUES (null, null, {cust.CustomerId}, null)");
            _orders.Add(
                new Order()
                {
                    id = id,
                    customer = cust,
                    payment = null,
                    dateCreated = DateTime.Now
                }
            );
            return id;
        }

        /*  Return a list of all orders 
            Authored by Jason Smith */
        public List<Order> GetOrders()
        {
            _orders = _db.Query($"SELECT id, customerId, paymentTypeId, dateCreated FROM [order]", 
            (SqliteDataReader reader) => {
                    _orders.Clear();
                    while (reader.Read ())
                    {
                        _orders.Add(new Order(){
                            id = reader.GetInt32(0),
                            dateCreated = reader[1].ToString(),
                            customerId = reader.GetInt32(2),
                            paymentId = reader.GetInt32(3)
                        });
                    }
                });
            return _orders;            
        }

        /*  Add a payment to the null field "payment" in an order, return true once complete
            Authored by Jason Smith */
        public bool AddPayment(int payId, int orderId)
        {
            return true;
        }
    }
}