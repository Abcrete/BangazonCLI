using System;
using System.Collections.Generic;
using Bangazon.Managers;
using Bangazon.Models;

namespace Bangazon.Actions
{
    
    // Class that handles Console interaction for assigning a product to the order
    // Authored by : Azim
    public class AddProductToCartAction
    {
        // method that is called from Program.cs switch case: 5
        // accepts 3 arguments
        // first argument is an Instance of the Class ProductManager
        // second argument is an Instance of the Class OrderManager
        // third is the CustomerId that was selected and stored from switch case: 2
        // Assigns the product to the prodOrder table
        // Authored by : Azim
        public static void DoAction(OrderManager om, ProductManager pm,  int customerId)
        {   // create new instance of order and product class to set it's properties
            // Authored by : Azim
                int choice;
                int count;
                // Main try catch block to catch the exception caused by entering incorrect character
                try
                {    
                    do
                    {
                        // get all products in the system
                        // Authored by : Azim
                        Console.Clear();
                        var products = pm.GetProducts();
                        int counter = 1;
                        Console.WriteLine($"Please choose a product to add to cart");
                        foreach (var item in products)
                        {
                            Console.WriteLine($"{counter++}. {item.title}");
                        }
                        Console.WriteLine($"Press {counter} to quit");
                        count = counter;
                        Console.Write(">");

                        int CustomerChoice = int.Parse(Console.ReadLine());
                    
                        choice = CustomerChoice;
                        // I put these methods in the try catch block to catch a exception
                        // Authored by : Azim
                        try
                        {
                            //This method will identify the product id from the array of products
                            var prodId = products[CustomerChoice - 1].id;
                            //Create order method is called to create a an order and it will return id of created order
                            var orderId = om.CreateOrder(prodId, customerId);
                        }catch(ArgumentOutOfRangeException)
                        {
                            Program.Warning("Thanks for adding order to the cart!");
                        }
                    }while(choice != count);
                    
                }catch(FormatException)
                {
                    Console.Clear();
                    Program.Warning("Incorrect Input! Please enter only numbers");
                }
        }

    }
}
