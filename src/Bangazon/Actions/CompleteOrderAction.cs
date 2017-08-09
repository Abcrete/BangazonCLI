using System;
using System.Collections.Generic;
using Bangazon.Models;
using Bangazon.Managers;

namespace Bangazon.Actions
{
    // Class that holds Methods to Complete an Order, Add Payment
    // Called in Program.cs case : 6
    // Authored by : Jason Smith & Tamela Lerma
    public class CompleteOrderAction
    {   // Method handles accepts 3 arguments: Customer ID, Instance of PaymentManager, and Instance of OrderManager
        // Method handles actions to retieve a list of payment types for customer, and add customer payment to Order Table
        // Authored by : Jason Smith & Tamela Lerma
        public static void DoAction(int custId, PaymentManager pm, OrderManager om)  
        {
        
            int choice = 0;
            // Method OrderTotal is from OrderManager
            // accepts 1 argument: CustomerID
            // returns the Total of a Customers product prices from DB  J.S. & T.L.
            Order orderToComplete = om.OrderTotal(custId);
            // First, check to make sure Customer has an open Order
            // If no order , prompt user to add Products and return to Main Menu    J.S. & T.L.
            if (orderToComplete.id == 0) {
                Console.WriteLine("Please add some products to your order first. Press any key to return to Main Menu");
                Console.ReadKey();
                return;
            }
            // Return List of Payment types for customer
            // if List Length is empty, Redirect Customer to Add Payment option
            // then return to this view
            // Authored by : Jason Smith & Tamela Lerma
            List<PaymentType> payment = pm.GetPaymentsForCustomer(custId);
            if (payment.Count == 0)
            {
                Console.WriteLine("No payment types, press any key to add payment method");
                Console.ReadKey();
                CreatePaymentAction.DoAction(pm, custId);
                payment = pm.GetPaymentsForCustomer(custId);
                Console.Clear();
            }

            // Once ensured payment method exists
            // Prompt user to choose to Y or N to complete order
            // Authored by : Jason Smith & Tamela Lerma
            do {                
                Console.WriteLine($"Your order total is ${orderToComplete.total}. Ready to purchse");
                Console.Write("(Y/N) >");
                string entry = Console.ReadKey().KeyChar.ToString().ToUpper();
                Console.WriteLine();

                if (entry == "Y")
                {
                    do {
                        int count = 1;

                        Console.WriteLine("Choose a payment option");
                        // Prompt user to select payment method
                        // Display List of Customers payment types
                        foreach (PaymentType item in payment)
                        {
                            Console.WriteLine($"{count++}. {item.Type}");
                        }
                        Console.WriteLine($"{count}. Cancel");
                        // ensure customer cannot enter a value that does not exist  J.S. & T.L.
                        try {
                            // if user enters the value of count it will return to Main Menu
                            choice = int.Parse(Console.ReadLine());
                            if(count == choice) {
                                return;
                            }
                            // else set the paymentTypeID
                            choice = payment[choice -1].PaymentTypeID;
                            // Call Method in OrderManager that accepts 2 arguments: paymentTypeId and OrderId
                            om.AddPayment(choice, orderToComplete.id);
                        } catch (System.FormatException) {
                            Console.Clear();
                            Console.WriteLine("Invalid entry Please try again");
                        } catch (System.ArgumentOutOfRangeException) {
                            // set choice back to 0 so loop will continue if invalid entry
                            choice = 0;
                            Console.Clear();
                            Console.WriteLine("Invalid entry Please try again");
                        }
                    } while (choice == 0);
                // if User chooses N will be redirected to Main Menu
                } else if (entry == "N") {
                    return;
                } else {
                    Console.Clear();
                    Console.WriteLine("Invalid Entry, Please try again");
                }
            } while (choice == 0);   
        }
    }
}