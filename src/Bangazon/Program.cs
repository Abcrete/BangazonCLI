﻿using System;
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
                    // if Menu option 1 is selected: Add new Customer   T.L.
                    case 1:
                        CreateNewCustomer.DoAction(customer);
                        break;

                    case 2: 
                        activeCustomer = GetCustomers.DoAction(customer);
                        break;

                    case 3:
                        if (activeCustomer != 0)
                        {
                            CustomerPaymentAction.DoAction(payment, activeCustomer);
                            break;
                        } else {
                            Console.WriteLine("Stop being Stupid, choose a customer");
                            break;
                        }

                }
            } while(choice != 0);
        }
    }
}
