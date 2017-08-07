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
            int id = 1;
            int  newid = _manager.AddPaymentToCustomer(pt,id); 
            Assert.IsType<int>(newid);
            Assert.True(newid != 0);

           
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
