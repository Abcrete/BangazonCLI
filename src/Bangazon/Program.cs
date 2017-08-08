using System;
using Bangazon.Models;
using Bangazon.Managers;
using Bangazon.Actions;

namespace Bangazon
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Seed the database if none exists
            var db = new DatabaseInterface("BANGAZON_TEST_DB");
            db.CheckCustomerTable();
            db.CheckOrderTable();
            db.CheckPaymentTypeTable();
            db.CheckProductTable();
            db.CheckProductTypeTable();
            db.CheckProdOrderTable();

            // Create Instance of MenuManager   T.L.
            MenuManager menu = new MenuManager();
            CustomerManager customer = new CustomerManager(db);
            ProductTypeManager productType = new ProductTypeManager(db);
            PaymentManager payment = new PaymentManager(db);
            ProductTypeManager prodType = new ProductTypeManager(db);
            ProductManager productManager = new ProductManager(db);

            // int will hold active customer T.L.
            int activeCustomer = 0;

			// choice will hold the reference to the number the user selected   
            // after the MenuManager was displayed T.L.
			int choice;
			do
            {
                // Display Menu from MenuManager 
                // Save selected int to choice  T.L
                choice = menu.ShowMenu();

                switch (choice)
                {
                    // if Menu option 1 is selected: Add new Customer 
                    // Method is called in CreateNewCustomer which calls Method in CustomerManager  T.L.
                    case 1:
                        CreateNewCustomer.DoAction(customer);
                        break;
                    // if Menu option 2 is selected: 
                    // Method from GetCustomersAction makes a call to CustomersManager
                    // Returns a list of Customers to display in terminal
                    // The selected Customer's ID is stored in activeCustomer
                    // This variable will then be passed to case 3
                    // Authored by : Tamela Lerma & Jason Smith
                    case 2: 
                        activeCustomer = GetCustomersAction.DoAction(customer);
                        break;
                    // User will be prompted to first select a customer
                    // once customer is selected
                    // a Method in CreatePaymentAction is called which 
                    // calls a Method in PaymentTypeManager to create a new payment
                    // Authored by : Tamela Lerma & Jason Smith
                    case 3:
                        if (activeCustomer != 0)
                        {
                            CreatePaymentAction.DoAction(payment, activeCustomer);
                            break;
                        } else {
                            Console.WriteLine("Stop being Stupid, choose a customer");
                            break;
                        }
                    // User will first be prompted to select an active customer
                    // after customer is selected, a CreateProductAction Class Method is called
                    // this Method accepts 3 arguments: Instance of ProductManager, Instance of ProductTypeManager, and the stored int for CustomerId
                    // Authored by : Tamela Lerma
                    case 4: 
                        if(activeCustomer != 0)
                        {
                            CreateProductAction.DoAction(productManager, activeCustomer, productType);
                            break;

                        }else {
                            Console.WriteLine("You must choose a customer to add product to");
                            break;
                        }
                }
            } while(choice != 0);
        }
    }
}
