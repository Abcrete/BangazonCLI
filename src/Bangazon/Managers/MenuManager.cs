using System;
using System.Collections.Generic;


namespace Bangazon.Managers
{
    // Class that displays Menu in Console
    // Authored by : Tamela Lerma
    public class MenuManager {

        // Method will return the number that user entered
        // that number will then be used in switch statement in Program.cs file
        // Authored by : Tamela Lerma
        public int ShowMenu() {
            Console.WriteLine("*************************************************");
            Console.WriteLine("Welcome to Bangazon! Command Line Ordering System");
            Console.WriteLine("*************************************************");
            Console.WriteLine("1. Create a customer account");
            Console.WriteLine("2. Choose active customer");
            Console.Write("> ");

            // Capture key char that was entered
            ConsoleKeyInfo enteredKey = Console.ReadKey();
            Console.WriteLine("");
            return int.Parse(enteredKey.KeyChar.ToString());
        }
    }
}