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
            string Name, Address, City, State, Zip, Phone;

            do {
                Console.WriteLine("Enter Customer's Name");
                Console.Write(">");
                Name = Console.ReadLine();
            } while(Name.Length == 0);

            do {
                Console.WriteLine("Enter Customer's Street Address");
                Console.Write(">");
                Address = Console.ReadLine();
            } while(Address.Length == 0);

            do {
                Console.WriteLine("Enter Customer's City");
                Console.Write(">");
                City = Console.ReadLine();
            } while(City.Length == 0);
            
            do {
                Console.WriteLine("Enter Customer's State");
                Console.Write(">");
                State = Console.ReadLine();
            } while(State.Length < 2);

            do {
                Console.WriteLine("Enter Customer's Zip Code");
                Console.Write(">");
                Zip = Console.ReadLine();
            } while(Zip.Length < 5 || Zip.Length > 10);

            do {
                Console.WriteLine("Enter Customer's Phone Number");
                Console.Write(">");
                Phone = Console.ReadLine();
            } while(Phone.Length < 10);

            int customerId = register.AddCustomer(Name, Address, City, State, Zip, Phone);
            Console.WriteLine(customerId);
        }
    }
}