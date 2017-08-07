using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using System.Collections;

namespace Bangazon
{
    public class DatabaseInterface
    {
        private string _connectionString;
        private SqliteConnection _connection;

        public DatabaseInterface(string database)
        {
            string env = $"{Environment.GetEnvironmentVariable(database)}";
            _connectionString = $"Data Source={env}";
            _connection = new SqliteConnection(_connectionString);
        }

        public void Query(string command, Action<SqliteDataReader> handler)
        {
            using (_connection)
            {
                _connection.Open ();
                SqliteCommand dbcmd = _connection.CreateCommand ();
                dbcmd.CommandText = command;

                using (SqliteDataReader dataReader = dbcmd.ExecuteReader()) 
                {
                    handler (dataReader);
                }

                dbcmd.Dispose ();
                _connection.Close ();
            }
        }

        public void Delete(string command)
        {
            using (_connection)
            {
                _connection.Open ();
                SqliteCommand dbcmd = _connection.CreateCommand ();
                dbcmd.CommandText = command;
                
                dbcmd.ExecuteNonQuery ();

                dbcmd.Dispose ();
                _connection.Close ();
            }
        }

        public int Insert(string command)
        {
            int insertedItemId = 0;

            using (_connection)
            {
                _connection.Open ();
                SqliteCommand dbcmd = _connection.CreateCommand ();
                dbcmd.CommandText = command;
                
                dbcmd.ExecuteNonQuery ();

                this.Query("select last_insert_rowid()",
                    (SqliteDataReader reader) => {
                        while (reader.Read ())
                        {
                            insertedItemId = reader.GetInt32(0);
                        }
                    }
                );

                dbcmd.Dispose ();
                _connection.Close ();
            }

            return insertedItemId;
        }

        public void CheckCustomerTable ()
        {
            using (_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand ();

                // Query the customer table to see if table is created
                dbcmd.CommandText = $"select CustomerID from customer";

                try
                {
                    // Try to run the query. If it throws an exception, create the table
                    using (SqliteDataReader reader = dbcmd.ExecuteReader()) { }
                    dbcmd.Dispose ();
                }
                catch (Microsoft.Data.Sqlite.SqliteException ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.Message.Contains("no such table"))
                    {
                        dbcmd.CommandText = $@"create table customer (
                            `CustomerID`	integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                            `Name`	varchar(80) not null, 
                            `StreetAddress` varchar(80),
                            `City` varchar(80),
                            `State` varchar(2),
                            `ZipCode` varchar(10),
                            `Phone` varchar(20)
                        )";
                        try
                        {
                            dbcmd.ExecuteNonQuery ();
                        }
                        catch (Microsoft.Data.Sqlite.SqliteException)
                        {
                            Console.WriteLine("Table already exists. Ignoring");
                        }
                        dbcmd.Dispose ();
                    }
                }
                _connection.Close ();
            }
        }

        public void CheckOrderTable ()
        {
            using (_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand ();

                // Query the order table to see if table is created
                dbcmd.CommandText = $"select OrderID from Order";

                try
                {
                    // Try to run the query. If it throws an exception, create the table
                    using (SqliteDataReader reader = dbcmd.ExecuteReader()) { }
                    dbcmd.Dispose ();
                }
                catch (Microsoft.Data.Sqlite.SqliteException ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.Message.Contains("no such table"))
                    {
                        dbcmd.CommandText = $@"create table `order` (
                            `OrderID`	integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                            `DateCreated` DATE DEFAULT (datetime('now','localtime')),
                            `CustomerID` integer not null,
                            `PaymentTypeID` integer,
                             FOREIGN KEY(`CustomerID`) REFERENCES `Customer`(`CustomerID`),
                             FOREIGN KEY(`PaymentTypeID`) REFERENCES `PaymentType`(`PaymentTypeID`)
                        )";
                        try
                        {
                            dbcmd.ExecuteNonQuery ();
                        }
                        catch (Microsoft.Data.Sqlite.SqliteException)
                        {
                            Console.WriteLine("Table already exists. Ignoring");
                        }
                        dbcmd.Dispose ();
                    }
                }
                _connection.Close ();
            }
        }

        public void CheckPaymentTypeTable ()
        {
            using (_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand ();

                // Query the PaymentType table to see if table is created
                dbcmd.CommandText = $"select PaymentTypeID from PaymentType";

                try
                {
                    // Try to run the query. If it throws an exception, create the table
                    using (SqliteDataReader reader = dbcmd.ExecuteReader()) { }
                    dbcmd.Dispose ();
                }
                catch (Microsoft.Data.Sqlite.SqliteException ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.Message.Contains("no such table"))
                    {
                        dbcmd.CommandText = $@"create table PaymentType (
                            `PaymentTypeID`	integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                            `AccountNumber`	varchar(20) not null, 
                            `Type` varchar(20),
                            `CustomerID` integer not null,
                             FOREIGN KEY(`CustomerID`) REFERENCES `Customer`(`CustomerID`)
                        )";
                        try
                        {
                            dbcmd.ExecuteNonQuery ();
                        }
                        catch (Microsoft.Data.Sqlite.SqliteException)
                        {
                            Console.WriteLine("Table already exists. Ignoring");
                        }
                        dbcmd.Dispose ();
                    }
                }
                _connection.Close ();
            }
        }

        public void CheckProdOrderTable ()
        {
            using (_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand ();

                // Query the ProdOrder table to see if table is created
                dbcmd.CommandText = $"select OrderID,ProductID from ProdOrder";

                try
                {
                    // Try to run the query. If it throws an exception, create the table
                    using (SqliteDataReader reader = dbcmd.ExecuteReader()) { }
                    dbcmd.Dispose ();
                }
                catch (Microsoft.Data.Sqlite.SqliteException ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.Message.Contains("no such table"))
                    {
                        dbcmd.CommandText = $@"create table ProdOrder (
                            `OrderID`	integer NOT NULL,
                            `ProductID`	integer NOT NULL
                        )";
                        try
                        {
                            dbcmd.ExecuteNonQuery ();
                        }
                        catch (Microsoft.Data.Sqlite.SqliteException)
                        {
                            Console.WriteLine("Table already exists. Ignoring");
                        }
                        dbcmd.Dispose ();
                    }
                }
                _connection.Close ();
            }
        }

        public void CheckProductTypeTable ()
        {
            using (_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand ();

                // Query the ProductType table to see if table is created
                dbcmd.CommandText = $"select ProductTypeID from ProductType";

                try
                {
                    // Try to run the query. If it throws an exception, create the table
                    using (SqliteDataReader reader = dbcmd.ExecuteReader()) { }
                    dbcmd.Dispose ();
                }
                catch (Microsoft.Data.Sqlite.SqliteException ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.Message.Contains("no such table"))
                    {
                        dbcmd.CommandText = $@"create table ProductType (
                            `ProductTypeID`	integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                            `Name`	varchar(80) not null
                        )";
                        try
                        {
                            dbcmd.ExecuteNonQuery ();
                        }
                        catch (Microsoft.Data.Sqlite.SqliteException)
                        {
                            Console.WriteLine("Table already exists. Ignoring");
                        }
                        dbcmd.Dispose ();
                    }
                }
                _connection.Close ();
            }
        }

        public void CheckProductTable ()
        {
            using (_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand ();

                // Query the prodcut table to see if table is created
                dbcmd.CommandText = $"select ProductID from Product";

                try
                {
                    // Try to run the query. If it throws an exception, create the table
                    using (SqliteDataReader reader = dbcmd.ExecuteReader()) { }
                    dbcmd.Dispose ();
                }
                catch (Microsoft.Data.Sqlite.SqliteException ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.Message.Contains("no such table"))
                    {
                        dbcmd.CommandText = $@"create table product (
                            `ProductID`	integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                            `Title`	varchar(80) not null, 
                            `Description`	varchar(80), 
                            `Price` float,
                            `Quantity` int,
                            `CustomerID` integer not null,
                            `CreateDate` DATE DEFAULT (datetime('now','localtime')),
                            FOREIGN KEY(`CustomerID`) REFERENCES `Customer`(`CustomerID`)
                        )";
                        try
                        {
                            dbcmd.ExecuteNonQuery ();
                        }
                        catch (Microsoft.Data.Sqlite.SqliteException)
                        {
                            Console.WriteLine("Table already exists. Ignoring");
                        }

                        dbcmd.Dispose ();
                    }
                }
                _connection.Close ();
            }
        }
    }
}
