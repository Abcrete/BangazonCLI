using System;
using System.Collections.Generic;
using Bangazon.Models;
using Bangazon.Managers;

namespace Bangazon.Actions
{
    // Class to Delete Product from a Selected Customer
    // Method Called from ProductManager that removes row from Product table
    // also queries ProdOrder table for matching customerID and DOES NOT delete if Product exists in ProdOrder table
    // accepts 2 aruguments: instance of ProductManager and customer ID from Program.cs switch statement case: 7
    // Authored by : Tamela Lerma
    public class DeleteProductAction
    {
        public static void DoAction(ProductManager pManager, int custId)
        {
            Console.Clear();

            List<Product> products;
            // store selected value to reference index in products  T.L.
            int choice = 0;
            // Use the Instance of ProductManager var: pManager to call Method to return a list of Products
            // OverLoaded Method accepts 1 argument: CustomerID to only return a Customer's Products    T.L.
            products = pManager.GetProducts(custId);
            // if the length of the products List is 0, then no products returned for the Customer  T.L. & J.S.
            if(products.Count == 0) {
                Program.Warning("Customer Has no Products for sale");
                return;
            } 

            // Display Product Title in Console     T.L.
            do 
            {
                int count = 1;
                Console.WriteLine("Select Product to Remove");
                foreach(Product p in products)
                {
                    Console.WriteLine($"{count}. {p.title}");
                    count++;
                }
                // if value entered cannot be parsed to a string, FormatException exception so user will be prompted to enter correct value T.L
                // if value is empty, ArgumentOutOfRangeException exception, prompt user to enter value T.L & J.S.
                try {
                    int ProductSelected = int.Parse(Console.ReadLine());
                    choice = products[ProductSelected -1].id;
                } catch(System.FormatException) {
                    Console.Clear();
                    Program.Warning("Invalid entry, try again.");
                } catch(System.ArgumentOutOfRangeException) {
                    Console.Clear();
                    Program.Warning("Invalid entry, try again.");
                }
            }while(choice == 0);
            // choice will pass in Product Id to Method in ProductManager to remove T.L.
            pManager.RemoveProduct(choice);
        }
    }
}
