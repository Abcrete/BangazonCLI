using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using Bangazon.Models;

namespace Bangazon.Managers
{
    public class PaymentManager
    {
        private List<PaymentType> _pt = new List<PaymentType>();
        private DatabaseInterface _db;

        public PaymentManager(DatabaseInterface db)
        {
            _db = db;
        }

        public List<Customer>AddPaymentToCustomer (PaymentType pt, Customer cust) 
        {
            return new List<Customer>();
        }

       public List<PaymentType> GetPaymentsForCustomer (int id)
        {

            return new List<PaymentType>();
        }
    }
}