using System;
using System.Collections.Generic;
using Bangazon.Managers;
using Bangazon.Models;

namespace Bangazon.Actions
{
     /* Class that handles Console interaction to get most popular products
     Authored by : Aarti Jaisinghani
     */

     public class GetMostPopularProduct
     {
         public static void DoAction(ProductManager pm)
         {
              Console.Clear();
              Console.WriteLine("List of Most Popular Products");
              Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%");
              List<Product> popprod = pm.GetPopularProducts();
              //Console.WriteLine(staleprod.Count);
              int counter = 1;
              foreach(Product prod in popprod)
              {
                
                
                    Console.WriteLine(counter + ". " + prod.title);
                    counter++;
                
              }
              Console.Write ("> ");
              Console.ReadKey();


         }
     }


}