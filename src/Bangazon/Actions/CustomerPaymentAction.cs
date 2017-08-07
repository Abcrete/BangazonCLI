using System;
using System.Collections.Generic;
using Bangazon.Managers;
using Bangazon.Models;

namespace Bangazon.Actions
{
    public class CustomerPaymentAction
    {

        public PaymentType payment = new PaymentType();
        public void AddCustomerPayment(int customerId)
        {
            Console.WriteLine("Enter payment type");
            payment.Type = Console.ReadLine();
            Console.Write(">");

            Console.WriteLine("Enter Account Number");
        }
    }
}

    public class PaymentType
    {
        public int PaymentTypeID { get; set;}
        public string AccountNumber {get; set;}
        public string Type {get; set;}
        public int CustomerID {get; set;}
    }