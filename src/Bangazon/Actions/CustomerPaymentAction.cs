using System;
using System.Collections.Generic;
using Bangazon.Managers;
using Bangazon.Models;

namespace Bangazon.Actions
{
    public class CustomerPaymentAction
    {
        
        public DatabaseInterface db;
        public static void DoAction(PaymentManager pm, int customerId)
        {
            PaymentType payment = new PaymentType();
            Console.WriteLine($"Enter payment type");
            payment.Type = Console.ReadLine();
            Console.Write(">");

            Console.WriteLine("Enter Account Number");
            payment.AccountNumber = Console.ReadLine();

            pm.AddPaymentToCustomer(payment, customerId);
            

            
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