using System;
using System.Collections.Generic;
using Bangazon.Managers;
using Bangazon.Models;

namespace Bangazon.Actions
{
    
    // Class that handles Console interaction for updating the customers product
    // Authored by : Azim
    public class UpdateProduct
    {
        // method that is called from Program.cs switch case: 8
        // accepts 2 arguments
        // first argument is an Instance of the Class ProductManager
        // second is the CustomerId that was selected and stored from switch case: 2
        // Updates the product information
        // Authored by : Azim
        public static void DoAction(ProductManager pm,  int customerId)
        {  
            // Authored by : Azim
                int choice;
                int counter;
                // Main try catch block to catch the exception caused by entering incorrect character
                try
                {    
                    do
                    {
                        // get all products in the system
                        // Authored by : Azim
                        Console.Clear();
                        counter = 1;
                        // Get all the products for the customer
                        var products = pm.GetProducts(customerId);
                        if(products.Count == 0) {
                            Program.Warning("Customer has no Products");
                            return;
                        }
                        Console.WriteLine($"Select a product to update:");
                        foreach (var item in products)
                        {
                            Console.WriteLine($"{counter++}. {item.title}");
                        }
                        Console.WriteLine($"{counter}. Exit");
                        Console.Write(">");

                        int CustomerChoice = int.Parse(Console.ReadLine());
                    
                        choice = CustomerChoice;
                        // I put these methods in the try catch block to catch a an exception
                        // Authored by : Azim
                        try
                        {
                            //This method will identify the product id from the array of products
                            var prodId = products[CustomerChoice - 1].id;
                            counter = 1;
                            var product = pm.GetProduct(prodId);
                            Console.Clear();

                            //Customer will be presented with these options to update their product
                            // Authored by : Azim
                            Console.WriteLine($"1. Change title '{product.title}'");
                            Console.WriteLine($"2. Change description '{product.description}'");
                            Console.WriteLine($"3. Change price '{product.price}'");
                            Console.WriteLine($"4. Change quantity  '{product.quantity}'");
                            Console.Write(">");
                            int ChoosenProduct = int.Parse(Console.ReadLine());

                            //Once customer selects option to update which property of the product to update
                            //These options will be presented and UpdateProduct method in the product manager will be called
                            //and column of the table, product id and entered value will be passed that method to update the product
                            // Authored by : Azim
                            switch(ChoosenProduct)
                            {
                                case 1:
                                Console.Clear();
                                Console.WriteLine($"Enter new title:");
                                var NewValue = Console.ReadLine();
                                Console.Write(">");
                                pm.UpdateProduct(prodId, "title", NewValue);
                                break;
                                case 2:
                                Console.WriteLine($"Enter new description:");
                                NewValue = Console.ReadLine();
                                Console.Write(">");
                                pm.UpdateProduct(prodId, "description", NewValue);
                                break;
                                case 3:
                                Console.WriteLine($"Enter new price:");
                                NewValue = Console.ReadLine();
                                Console.Write(">");
                                pm.UpdateProduct(prodId, "price", NewValue);
                                break;
                                case 4:
                                Console.WriteLine($"Enter new quantity:");
                                NewValue = Console.ReadLine();
                                Console.Write(">");
                                pm.UpdateProduct(prodId, "quantity", NewValue);
                                break;
                            }
                        // If out of range exception thrown this will catch it and lets the user know what is wrong
                        }catch(ArgumentOutOfRangeException)
                        {
                            Console.Clear();
                        }
                    }while(choice != 0 && choice < counter);

                }catch(FormatException)
                {
                    Console.Clear();
                    Program.Warning("Incorrect Input! Please enter only numbers");
                }
        }

    }
}
