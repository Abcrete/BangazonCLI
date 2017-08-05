using System;
using Bangazon.Managers;
using Bangazon.Models;

// Class that handles the entry of data for a new Customer
// excepts 1 argument : Type CustomerManager which is a new instance of that class
// Will store each entry in a variable, then call the AddCustomer method
// All values will be passed in and inserted into the DB
// Authored by: Tamela Lerma
namespace Bangazon.Actions
{
    public class CreateNewCustomer
    {
        public static void DoAction(CustomerManager register)
        {
            // Clear Console for Menu prompts on Customer information
            Console.Clear();

            Console.WriteLine("Enter Customer's Name");
            Console.Write(">");
            string Name = Console.ReadLine();

            Console.WriteLine("Enter Customer's Street Address");
            Console.Write(">");
            string Address = Console.ReadLine();

            Console.WriteLine("Enter Customer's City");
            Console.Write(">");
            string City = Console.ReadLine();

            Console.WriteLine("Enter Customer's State");
            Console.Write(">");
            string State = Console.ReadLine();

            Console.WriteLine("Enter Customer's Zip Code");
            Console.Write(">");
            string Zip = Console.ReadLine();

            Console.WriteLine("Enter Customer's Phone Number");
            Console.Write(">");
            string Phone = Console.ReadLine();

            int customerId = register.AddCustomer(Name, Address, City, State, Zip, Phone);
            Console.WriteLine(customerId);
        }
    }
}