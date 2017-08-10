using Bangazon.Managers;
using Bangazon.Models;
using System;
using System.Collections.Generic;

namespace Bangazon.Actions
{
    public class GetCustomerReport
    {
        /*  Class generates a revenue report for a customer showing their products sold for each order and totals
            Authored by Jason and Tamela */
        public static void DoAction(int custId, CustomerManager cm, OrderManager om)
        {
            // Get the current customer so the name can be displayed
            Customer current = cm.GetCustomer(custId);
            Console.WriteLine($"Revenue Report for {current.Name}:");
            // First int is the orderId, second is quantity of the item in that order, string is the name of item, fourth is price of the item
            // Data is read from product table, order table and orderProd table
            List<(int, int, string, double)> revReport = om.RevenueReport(custId);
            // order holds the current orderId while iterating through revReport
            int order = 0;
            // total of price * quantity for items sold, calculated as revReport is traversed through
            double total = 0;
            foreach((int o, int quant, string prod, double price) in revReport)
            {
                // Write a new line naming the order if the orderId is different from the previous iteration in revReport
                if(o != order){
                    Console.WriteLine(Environment.NewLine + $"Order #{o}" + Environment.NewLine + "-----------------------------------------------------");
                    order = o;
                }
                Console.Write($"{prod}");
                // Set a number of spaces equal to the product name length minus 30
                for(int i = prod.Length; i < 30; i++){Console.Write(" ");}  
                Console.Write($"{quant}");
                // Set a number of spaces equal to the quantity length minus 12
                for(int i = quant.ToString().Length; i < 12; i++){Console.Write(" ");}
                Console.Write($"{String.Format("{0:C}", price * quant)}" + Environment.NewLine);
                // calculate total sales for each item price and quantity and add to previous total
                total += quant * price;
            }
            Console.WriteLine(Environment.NewLine + $"Total Revenue: {String.Format("{0:C}", total)}" + Environment.NewLine + "Press any key to return to main menu");
            Console.ReadKey();
            Console.Clear();
        }
    }
}