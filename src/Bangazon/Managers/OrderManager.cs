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
            _orders = new List<Order>();
        }

        /*  Add an order to the database and return the id, which is the PK where it was placed 
            Authored by Jason Smith */
        public int CreateOrder(int prodId, int custId)
        {
            int index = _db.Insert( $"INSERT INTO [order] VALUES (null, null, {custId}, null)");
            _db.Insert( $"INSERT INTO prodOrder VALUES (null, {index}, {prodId})");
            _orders.Add(
                new Order()
                {
                    id = index,
                    customerId = custId,
                    paymentTypeId = null,
                    dateCreated = DateTime.Now.ToString()
                }
            );
            return index;
        }

        /*  Return a list of all orders 
            Authored by Jason Smith */
        public List<Order> GetOrders()
        {
            _db.Query($"SELECT orderId, dateCreated, customerId, paymentTypeId FROM [order]", 
                (SqliteDataReader reader) => {
                    _orders.Clear();
                    while (reader.Read())
                    {
                        _orders.Add(
                            new Order(){
                                id = reader.GetInt32(0),
                                dateCreated = reader[1].ToString(),
                                customerId = reader.GetInt32(2),
                                paymentTypeId = reader[3] as int? ?? null
                            }
                        );
                    }
                }
            );
            return _orders;            
        }

        /*  Add a payment to the null field "payment" in an order, return true once complete
            Authored by Jason Smith */
        public bool AddPayment(int payId, int orderId)
        {
            
            return true;
        }

        /*  Add a product by productId to an order by orderId return the index of the item added to the productorder join table
            Authored by Jason Smith */
        public int AddProductToOrder(int prodId, int orderId)
        {
            int index =  _db.Insert( $"INSERT INTO prodOrder VALUES (null, {orderId}, {prodId})");
            return index;
        }

        /*  Return a list that contains orderId, product name, productId, product price for all items created by the given customer
            Authored by Jason Smith */
        public List<(int, string, int, double)> RevenueReport(int custId)
        {
             List<(int orderId, string prodTitle, int prodId, double price)> _report = new List<(int, string, int, double)>();
            _db.Query($"SELECT o.orderId, p.productId, p.title, p.price FROM [order] o LEFT JOIN prodOrder po ON po.orderId = o.orderId LEFT JOIN product p ON p.productId = po.productId WHERE p.customerId = {custId}",
                (SqliteDataReader reader) => {
                    while(reader.Read())
                    {
                        _report.Add((reader.GetInt32(0), reader[1].ToString(), reader.GetInt32(2), reader.GetDouble(3)));
                    }
                }
            );
            return _report;
        }
    }
}