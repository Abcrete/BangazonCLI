using System;
using System.Collections.Generic;
using Bangazon.Managers;
using Bangazon.Models;

namespace Bangazon.Actions
{
     /* Class that handles Console interaction to get stale products
     Authored by : Aarti Jaisinghani
     */

     public class GetStaleProducts
     {
         public static void DoAction(ProductManager pm)
         {
              Console.Clear();
              Console.WriteLine("List of Stale Products");
              Program.Warning("%%%%%%%%%%%%%%%%%%%%%%%%");
              List<Product> staleprod = pm.GetStaleProducts();
              if(staleprod.Count == 0) {
                Program.Warning("There are no stale products");
              }

              //Console.WriteLine(staleprod.Count);
              int counter = 1;
              foreach(Product prod in staleprod)
              {
                
                
                    Console.WriteLine(counter + ". " + prod.title);
                    counter++;
                
              }
              Console.WriteLine("Press any key to return to main menu");
              Console.Write ("> ");
              Console.ReadKey();
              Console.Clear();
         }
     }
}