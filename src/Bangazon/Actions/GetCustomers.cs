using System;
using System.Collections.Generic;
using Bangazon.Managers;
using Bangazon.Models;

namespace Bangazon.Actions
{

    // Method that displays Choice 2 from the Menu to display all Customers
    // User will be able to enter in the number for which customer to select
    // switch statement case 2 from Program.cs file
    // Authored by: Tamela Lerma
    public class GetCustomers 
    {
        public CustomerPaymentAction customerPayment = new CustomerPaymentAction();
        public static void DoAction(CustomerManager customer)
        {
            // Clear Console for Menu prompts on Customer information
            Console.Clear();
            // Call Method From CustomerManager.cs to return list of Customers   T.L.
            List<Customer> customers = customer.GetCustomers();
            
            
            int counter = 1;

            foreach (var person in customers)
            {
                Console.WriteLine($"{counter} {person.Name}");
                counter++;
            }

            Console.WriteLine("Choose Customer");
            int CustomerChoice = int.Parse(Console.ReadLine());
            // take the number that was entered, minus 1 to get the index position from the list. Then Print out CustomerId and Name    T.L.
            Console.WriteLine($"{customers[CustomerChoice -1].CustomerId} {customers[CustomerChoice -1].Name}");

            int CustomerId = customers[CustomerChoice - 1].CustomerId;
            CustomerPaymentAction.AddCustomerPayment(CustomerId);
        }
    }
}