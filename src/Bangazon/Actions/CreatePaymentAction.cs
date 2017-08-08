using System;
using System.Collections.Generic;
using Bangazon.Managers;
using Bangazon.Models;

namespace Bangazon.Actions
{
    
    // Class that handles Console interaction for entering a new payment type for a customer
    // Authored by : Tamela Lerma
    public class CreatePaymentAction
    {
        // method that is called from Program.cs switch case: 3
        // accepts 2 arguments
        // first argument is an Instance of the Class PaymentManager
        // second is the CustomerId that was selected and stored from switch case: 2
        // Adds new Payment type class object to DB
        // Authored by : Tamela Lerma
        public static void DoAction(PaymentManager pm, int customerId)
        {   // create new instance of payment class to set it's properties  T.L.
            PaymentType payment = new PaymentType();
        
            Console.WriteLine($"Enter payment type");
            // Store entered value as PaymentType property Type  T.L.
            payment.Type = Console.ReadLine();
            Console.Write(">");
            // Store entered value as PaymentType property AccountNumber  T.L.
            Console.WriteLine("Enter Account Number");
            payment.AccountNumber = Console.ReadLine();
            // Call methid in PaymentManager Class that adds the object to the DB   T.L.
            pm.AddPaymentToCustomer(payment, customerId); 
        }

    }
}
