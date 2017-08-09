using System;
using System.Collections.Generic;
using Bangazon.Models;
using Bangazon.Managers;

namespace Bangazon.Actions
{
    public class CompleteOrderAction
    {
        public static void DoAction(int custId, PaymentManager pm, OrderManager om)  
        {
            int choice = 0;

            Order orderToComplete = om.OrderTotal(custId);
            if (orderToComplete.id == 0) {
                Console.WriteLine("Please add some products to your order first. Press any key to return to Main Menu");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Your order total is ${orderToComplete.total}. Ready to purchse");
            Console.Write("(Y/N) >");
            
            string entry = Console.ReadKey().ToString().ToUpper();

            if (entry == "Y")
            {
                int count = 1;
                
                List<PaymentType> payment = pm.GetPaymentsForCustomer(custId);
                foreach (PaymentType item in payment)
                {
                    Console.WriteLine($"{count++}. {item.Type}");
                }

                choice = int.Parse(Console.ReadLine());
                choice = payment[choice -1].PaymentTypeID;
                om.AddPayment(choice, orderToComplete.id);

            } else if (entry == "N") {
                return;
            } else {
                Console.WriteLine("Invalid Entry, Please try again");
            }


            
        }
    }
}