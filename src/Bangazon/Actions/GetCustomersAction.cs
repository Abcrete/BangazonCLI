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
    public class GetCustomersAction
    {
        public static int DoAction(CustomerManager customer)
        {
            int choice = 0;
            // Clear Console for Menu prompts on Customer information
            Console.Clear();
            List<Customer> customers ;
            do {
                // Call Method From CustomerManager.cs to return list of Customers   T.L.
                customers = customer.GetCustomers();
                
                
                int counter = 1;

                Console.WriteLine("Which customer will be active?");
                foreach (var person in customers)
                {
                    Console.WriteLine($"{counter}. {person.Name}");
                    counter++;
                }

                Console.Write(">");
                // Try to int parse the input and then read that customer in the customers list, catch invalid input exceptions
                try {
                    int CustomerChoice = int.Parse(Console.ReadLine());
                    choice = customers[CustomerChoice - 1].CustomerId;
                } catch(System.FormatException) {
                    Console.Clear();
                    Program.Warning("Invalid entry, try again.");
                } catch(System.ArgumentOutOfRangeException) {
                    Console.Clear();
                    Program.Warning("Invalid entry, try again.");
                }
            }while(choice == 0); // repeat process if input was invalid and didn't lead to a valid choice

            // take the number that was entered, minus 1 to get the index position from the list. Then Print out CustomerId and Name    T.L.
            return choice;    
        }
    }
}