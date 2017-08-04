using System;
using Bangazon.Models;
using Bangazon.Managers;

namespace Bangazon
{
    class Program
    {
        static void Main(string[] args)
        {
            // Seed the database if none exists
            var db = new DatabaseInterface("BANGAZON_TEST_DB");
            db.CheckCustomerTable();
            db.CheckOrderTable();
            db.CheckPaymentTypeTable();
            db.CheckProductTable();
            db.CheckProductTypeTable();
            db.CheckProdOrderTable();
            //db.VerifyDataExists();

            // Present the main menu
            Console.WriteLine ("*************************************************");
            Console.WriteLine ("Welcome to Bangazon! Command Line Ordering System");
            Console.WriteLine ("*************************************************");
            Console.WriteLine ("1. Create a customer account");
			Console.Write ("> ");

			// Read in the user's choice
			int choice;
			Int32.TryParse (Console.ReadLine(), out choice);

            // If option 1 was chosen, create a new customer account
            if (choice == 1)
            {
                Console.WriteLine ("Enter customer first name");
                Console.Write ("> ");
                string firstName = Console.ReadLine();
                Console.WriteLine ("Enter customer last name");
                Console.Write ("> ");
                string lastName = Console.ReadLine();
                Console.WriteLine ("Enter customer city");
                Console.Write ("> ");
                string city = Console.ReadLine();
                Console.WriteLine ("Enter customer state");
                Console.Write ("> ");
                string state = Console.ReadLine();
                Console.WriteLine ("Enter customer postal code");
                Console.Write ("> ");
                string postalCode = Console.ReadLine();
                Console.WriteLine ("Enter customer phone number");
                Console.Write ("> ");
                string phoneNumber = Console.ReadLine();
                CustomerManager manager = new CustomerManager();
            }
        }
    }
}
