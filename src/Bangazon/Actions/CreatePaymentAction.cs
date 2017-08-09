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
        {   
            Console.Clear();

            // create new instance of payment class to set it's properties  T.L.
            PaymentType payment = new PaymentType();
        
            // Store entered value as PaymentType property Type  T.L.
            // Do this action if value not empty J.S.
            do {
                Console.WriteLine($"Enter payment type");
                Console.Write(">");
                payment.Type = Console.ReadLine();
            }while(payment.Type == "");
            // Store entered value as PaymentType property AccountNumber  T.L.
             // Do this action if value not empty  J.S.
            do {
                Console.WriteLine("Enter Account Number");
                Console.Write(">");
                payment.AccountNumber = Console.ReadLine();
            }while(payment.AccountNumber == "");
            // Call method in PaymentManager Class that adds the object to the DB   T.L.
            pm.AddPaymentToCustomer(payment, customerId); 
        }

    }
}
