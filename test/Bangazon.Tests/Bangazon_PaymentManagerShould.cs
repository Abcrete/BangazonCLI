using System;
using Xunit;

namespace Bangazon.Tests
{
    public class PaymentManagerShould
    {

        private readonly PaymentManager _manager;

        public PaymentManagerShould()
        {
            _manager = new PaymentManager();
        }

        [Fact]
        public void AddPaymenttoCustomer()
        {
            Product kite = new Product();
            _manager.CreateOrder(kite);
        }

        [Fact]
        public void ListPaymentsforCustomer()
        {

        }
    }
}
