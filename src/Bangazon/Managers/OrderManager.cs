using System.Collections.Generic;
using Bangazon.Models;

namespace Bangazon.Managers
{
    public class OrderManager
    {
        private List<Order> _orders;
        private DatabaseInterface _db;

        // Instantiate 
        public OrderManager(DatabaseInterface db)
        {
            _db = db;
        }

        public bool CreateOrder(Product prod, int custId)
        {
            return true;
        }

        public List<Order> GetOrders()
        {
            return _orders;            
        }
    }
}