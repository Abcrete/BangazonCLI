using System;
using System.Collections.Generic;
using Bangazon.Models;
using Bangazon.Managers;

namespace Bangazon.Actions
{
    // Class to create a new Product and Product Type for a Customer from terminal interaction
    // Authored by : Tamela Lerma
    public class CreateProductAction 
    {
        // Method called from Program.cs switch case: 4
        // Accepts 3 arguments, Instance of ProductManager, ProductTypeManager, and an int containing Customer ID
        // this method prompts user to first define new productType then product
        // Authored by : Tamela Lerma
        public static void DoAction(ProductManager prodManager, int custId, ProductTypeManager prodType)
        {
            Console.Clear();
            int counter = 1;
            int prodTypeId;
            int choice; // used to store selected menu key value  T.L.
            bool isANumber = true; // used to check that number value is entered   T.L.
            Product product = new Product();
            product.customerId = custId; // set CustomerId on Product from int that is passed in DoAction Method    T.L.
            
            List<ProductType> prodList = prodType.GetProductTypes();

            Console.WriteLine("Select Product Type");

            foreach (ProductType item in prodList)
            {
                Console.WriteLine($"{counter}. {item.Name}");
                counter++;
            }
            Console.WriteLine($"{counter}. Add New Product Type");
            Console.Write(">");

            choice = int.Parse(Console.ReadLine());
           
            if (counter == choice) {
                Console.Clear();
                Console.WriteLine("Enter Type of Product");
                prodTypeId = prodType.AddProductType(Console.ReadLine());
            }else {
                prodTypeId = prodList[choice -1].Id;
            }
                
            product.productTypeId = prodTypeId;
            // Last Id entered into DB is returned in  a ProductTypeManager ClassMethod
            // AddProductType returns that ID
            // that ID for the new ProductType is stored in an Int
            // Authored By : Tamela Lerma
            // The ProductTypeId property for product is set from this variable  T.L.

            do {
                // set title
                Console.WriteLine("Enter Product Name");
                product.title = Console.ReadLine();
                Console.Write(">");
            } while (product.title == "");

            do {
                // set description
                Console.WriteLine("Enter Product Description");
                product.description = Console.ReadLine();
                Console.Write(">");
            } while (product.description == "");


            do {
                Console.WriteLine("Enter Product Price");
                try {
                    // set price
                    product.price = double.Parse(Console.ReadLine());
                    Console.Write(">");
                    isANumber = true;
                } catch(System.FormatException) {
                    Console.Clear();
                    Console.WriteLine("Invalid entry, try again.");
                    isANumber = false;
                }
            }while (isANumber  == false);


            do{
                Console.WriteLine("Enter Quantity");
                try {
                    // set quantity
                    product.quantity = int.Parse(Console.ReadLine());
                    Console.Write(">");
                    isANumber = true;
                } catch (System.FormatException) {
                    Console.Clear();
                    Console.WriteLine("Invalid entry, try again.");
                    isANumber = false;
                }
            } while (isANumber == false);
            
            // Call Method from ProductManager Class and pass in new Product Object to be added to DB  T.L. 
            prodManager.AddProduct(product);
        }
    }
}