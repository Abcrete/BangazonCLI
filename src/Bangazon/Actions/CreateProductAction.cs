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

            // create new Instance of Product Type in order to set its properties   T.L.
            Product product = new Product();
            
            // set CustomerId on Product Type from int that is passed in    T.L.
            product.customerId = custId;

            Console.WriteLine("Enter Type of Product");
            Console.Write(">");

            // Last Id entered into DB is returned in  a ProductTypeManager ClassMethod
            // AddProductType returns that ID
            // that ID for the new ProductType is stored in an Int
            // Authored By : Tamela Lerma
            int prodTypeId = prodType.AddProductType(Console.ReadLine());
            // The ProductTypeId property for product is set from this variable  T.L.
            product.productTypeId = prodTypeId;

            // set title
            Console.WriteLine("Enter Product Name");
            product.title = Console.ReadLine();
            Console.Write(">");
            // set description
            Console.WriteLine("Enter Product Description");
            product.description = Console.ReadLine();
            Console.Write(">");
            // set price
            Console.WriteLine("Enter Product Price");
            product.price = double.Parse(Console.ReadLine());
            Console.Write(">");
            // set quantity
            Console.WriteLine("Enter Quantity");
            product.quantity = int.Parse(Console.ReadLine());
            //set date
            Console.WriteLine("Entered Date Product was Created");
            product.dateCreated = DateTime.Parse(Console.ReadLine());
            Console.Write(">");
            
            // Call Method from Product Class and pass in new Product Object to be added to DB  T.L. 
            prodManager.AddProduct(product);
        }
    }
}