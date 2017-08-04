using System;
using System.Collections.Generic;
using Bangazon.Managers;
using Bangazon.Models;

namespace Bangazon
{
    /*
      SUMMARY
      This class is authored by Aarti. 
      PaymentType Class identifies the properties for the entries to the PaymentType in database
    */
    public class PaymentType
    {
        public int PaymentTypeID { get; set;}
        public string AccountNumber {get; set;}
        public string Type {get; set;}
        public Customer CustomerID {get; set;}
    }
}