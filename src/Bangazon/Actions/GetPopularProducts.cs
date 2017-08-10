using System;
using System.Collections.Generic;
using Bangazon.Managers;
using Bangazon.Models;

namespace Bangazon.Actions
{
     /* Class that handles Console interaction to get stale products

     Given a user wants to see the most popular products in the system
        When the user selects the corresponding option from the main menu
        Then the user should see the following report

        Product             Orders      Purchasers    Revenue
        *******************************************************
        AA Batteries        100         20            $990.90 
        Diapers             50          10            $640.95
        Case of Cracking... 40          30            $270.96
        *******************************************************
        Totals:             190         60            $1,902.81

        -> Press any key to return to main menu
        User will see the top 3 revenue generating products
        The product column must be 20 characters wide, and will display a maximum of 18 characters for the product name.
        The orders column must be 11 characters wide.
        The purchasers column must be 15 characters wide.
        The revenue column must be 15 characters wide.
                
        Authored by : Aarti Jaisinghani and Azim Sodokov
         */

     public class GetPopularProducts
     {
         public static void DoAction(ProductManager pm)
         {
              Console.Clear();
              List<PopularProduct> popprod = pm.GetPopularProducts();
              int counter = 1;
              int ordercount = 0, purchasercount = 0;
              double revenuetotal = 0;

              //print out headers
              Console.WriteLine("{0,-20} {1,-11} {2,-15} {3,-15}", "Product", "Orders", "Purchasers", "Revenue");
                    
              Program.Warning("********************************************************************");
              //iterate through popular products and print to console
              foreach(PopularProduct prod in popprod)
              {
                    
                    ordercount += prod.orders;
                    purchasercount += prod.purchasers;
                    revenuetotal += prod.revenue;
                    Console.WriteLine("{0,-20} {1,-11} {2,-15} {3,-15}", prod.product, prod.orders, prod.purchasers, "$" + prod.revenue);

                    
                    counter++;
                
              }
              Program.Warning("********************************************************************");
              //print out totals
              Console.WriteLine("{0,-20} {1,-11} {2,-15} {3,-15}", "Totals:" ,ordercount,purchasercount,"$" + revenuetotal);
                    
              Console.Write ("-> Press any key to return to main menu ");
              Console.ReadKey();
              Console.Clear();
              return;

         }
     }
}