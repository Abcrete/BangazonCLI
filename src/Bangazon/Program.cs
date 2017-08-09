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
            db.CheckProdOrderTable();
            db.CheckCustomerTable();
            db.CheckOrderTable();
            db.CheckPaymentTypeTable();
            db.CheckProductTable();
            db.CheckProductTypeTable();

            // Create Instance of MenuManager   T.L.
            MenuManager menu = new MenuManager();
            CustomerManager customer = new CustomerManager(db);
            ProductTypeManager productType = new ProductTypeManager(db);
            PaymentManager payment = new PaymentManager(db);
            ProductTypeManager prodType = new ProductTypeManager(db);
            ProductManager product = new ProductManager(db);
            OrderManager order = new OrderManager(db);

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
                Console.Clear();

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
                            Console.WriteLine("Please choose a customer first");
                            break;
                        }
                    // User will first be prompted to select an active customer
                    // after customer is selected, a CreateProductAction Class Method is called
                    // this Method accepts 3 arguments: Instance of ProductManager, Instance of ProductTypeManager, and the stored int for CustomerId
                    // Authored by : Tamela Lerma
                    case 4: 
                        if(activeCustomer != 0)
                        {
                            CreateProductAction.DoAction(product, activeCustomer, productType);
                            break;

                        }else {
                            Console.WriteLine("You must choose a customer to add product to");
                            break;
                        }
                    // User will need to first select a active customer
                    // once customer is selected
                    // a Method in AddProductToCartAction is called which 
                    // calls a Method in ProductManager to add a product to customers order
                    // Authored by : Azim
                    case 5: 
                        if (activeCustomer != 0)
                        {
                            AddProductToCartAction.DoAction(order, product, activeCustomer);
                            break;
                        } else {
                            Console.WriteLine("Please choose a customer first");
                            break;
                        }
                    // User will be prompted to first choose active customer
                    // then will call Method in CompleteOrderAction which
                    // References PaymentManager and OrderManager 
                    // checks that payment exists for customer
                    // payment is added to an order
                    // Authored by : Jason Smith & Tamela Lerma
                    case 6:
                        if (activeCustomer != 0)
                        {
                            CompleteOrderAction.DoAction(activeCustomer, payment, order);
                            break;
                        } else {
                            Console.WriteLine("Please choose a customer first");
                            break;
                        }
                    // User will first be prompted to select a customer
                    // after customer is selected, a DeleteProductAction Class Method is called
                    // this Method accepts 2 arguments: Instance of ProductManager and the stored int for CustomerId
                    // Will Delete Customer Products in DB
                    // Authored by : Tamela Lerma
                    case 7: 
                        if (activeCustomer != 0)
                        {
                            DeleteProductAction.DoAction(product, activeCustomer);
                            break;
                        } else {
                            Console.WriteLine("You must enter a customer first");
                            break;
                        }
                    case 9:
                        GetStaleProducts.DoAction(product);
                        break;
                }
            } while(choice != 0);
        }
    }
}
