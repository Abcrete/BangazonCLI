using System;
using Xunit;
using System.Collections.Generic;
using Bangazon.Models;
using Bangazon.Managers;

namespace Bangazon.Tests
{
    
    public class PaymentManagerShould
    {
        private readonly PaymentManager _manager;
        private readonly DatabaseInterface _db;

        public PaymentManagerShould()
        {
             _db = new DatabaseInterface("BANGAZON_TEST_DB");
            _manager = new PaymentManager(_db);  // initialize PaymentManager
        }

        [Fact]
        public void CheckAddPaymentToCustomer()
        {
            PaymentType pt = new PaymentType();
            Customer cust = new Customer();
            List<PaymentType> newlist = _manager.AddPaymentToCustomer(pt); 
            Assert.Contains(pt, newlist);
          //  Assert.IsType<List<PaymentType>>(newlist);
        }

        [Fact]
        public void ListPaymentTypes()
        {
            Customer cust = new Customer();
            List<PaymentType> paymenttypes = _manager.GetPaymentsForCustomer(cust.CustomerId); 
            Assert.IsType<List<PaymentType>>(paymenttypes);
            Assert.True(paymenttypes.Count >= 0);
        }

    }
}
