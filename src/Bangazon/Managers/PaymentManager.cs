
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using Bangazon.Models;

 /* 
    Class to create Methods pertaining to Payment Type Class
     Add a payment type to Customer
     Retrieve list of payment types for customer
     Authored by Aarti Jaisinghani
*/

namespace Bangazon.Managers
{
    public class PaymentManager
    {

        private List<PaymentType> _pt = new List<PaymentType>();
        private DatabaseInterface _db;
        /* An instance of DatabaseInterface is made in the Program.cs file
         When the CustomerManager Instance is created, 
         it is passed the DatabaseInterface instance, through the constructor below
         Authored by : Aarti Jaisinghani
         */
        public PaymentManager(DatabaseInterface db)
        {
            _db = db;
        }

        /*
         Method to add a payment type to a customer
         Takes in two parameters - one of type paymenttype and the other an int
         Returns an int value
         Authored by: Aarti Jaisinghani
         */
        public int AddPaymentToCustomer (PaymentType pt, int custid) 
        {
            // Insert into DB
            int id = _db.Insert( $"insert into PaymentType values (null, '{pt.AccountNumber}', '{pt.Type}', {custid})");
            Console.WriteLine(id);
            //Create new payment type instance
             PaymentType newpt = new PaymentType(){
                PaymentTypeID = id,
                AccountNumber =pt.AccountNumber,
                Type = pt.Type,
                CustomerID = custid
            };

            //Add to private collection
            _pt.Add(newpt);
            return id;
        }

        /*
        Method to list all payment types for a given customer
        This method takes a customerid of type int
        It returns a list of type PaymentType
        Authored by: Aarti Jaisinghani
         */
       public List<PaymentType> GetPaymentsForCustomer (int id)
        {
            _db.Query($"select * from PaymentType where CustomerID = '{id}'",(SqliteDataReader reader) => {
                    _pt.Clear();
                    while (reader.Read ())
                    {
                        _pt.Add(new PaymentType(){
                            PaymentTypeID = reader.GetInt32(0),
                            AccountNumber = reader[1].ToString(),
                            Type = reader[2].ToString(),
                            CustomerID = reader.GetInt32(3)
                        });
                    }
                }
            );
            return _pt;

        }
    }
}
