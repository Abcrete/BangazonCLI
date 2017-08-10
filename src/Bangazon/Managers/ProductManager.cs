using System;
using System.Collections.Generic;
using System.Linq;
using Bangazon.Models;
using Microsoft.Data.Sqlite;

namespace Bangazon.Managers
{
    // Class to create Methods pertaining to Product Class
    // Add product to customer
    // Return all products
    // Remove Product
    // Return one product
    // Return stale product
    // Return most popular products

    //This class is authored by Azim. 
    public class ProductManager
    {
        // Product will be added to this list when GetProducts Method makes a call to the DB     T.L
        private List<Product> _products = new List<Product>();

        /* Product will be added to this list when GetStaleProducts Method makes a call to the DB  
        Authored by : Aarti Jaisinghani
        */

        

        private DatabaseInterface _db;

        // An instance of DatabaseInterface is made in the Program.cs file
        // Authored by : Azim
        public ProductManager(DatabaseInterface db)
        {
            _db = db;
        }

        // Method to Add a Product to DataBase
        // Dependencies/FK
        // 1. Customer table
        // 2. ProductType table
        // requires 8 arguments
        // Returns ID of last Product entered
        // Authored by Azim
        public int AddProduct(Product newProduct){
            // Insert into DB
            int newProductId = _db.Insert($"INSERT INTO product (productId, title, description, price, quantity, customerId, productTypeId) VALUES (null, '{newProduct.title}', '{newProduct.description}', {newProduct.price}, {newProduct.quantity}, {newProduct.customerId}, {newProduct.productTypeId})");
            
                _products.Add(new Product(){
                id = newProductId,
                title = newProduct.title,
                description= newProduct.description,
                price = newProduct.price,
                quantity = newProduct.quantity,
                customerId = newProduct.customerId,
                productTypeId = newProduct.productTypeId
            });

            return newProductId;
        }
        // This method gets all Products from databse
        // Dependencies/FK
        // 1. Customer table
        // 2. ProductType table
        // requires 8 arguments
        // Returns List of Products
        // Authored by Azim


        public List<Product> GetProducts(){
            _db.Query("SELECT prodId, title, description, price, custId, prodType, dateCreated, quant FROM (select p.createdate as dateCreated, p.productTypeId as prodType, p.customerId as custId, p.price as price, p.description as description, p.title as title, p.productId as prodId, p.quantity as quant, Count(po.productId) as tote from product p LEFT JOIN prodOrder po ON po.productId = p.productId GROUP BY po.ProductId ) WHERE quant > tote",
                (SqliteDataReader reader) => {
                    _products.Clear();
                    while (reader.Read ())
                    {
                        _products.Add(new Product(){
                            id = reader.GetInt32(0),
                            title = reader[1].ToString(),
                            description = reader[2].ToString(),
                            price = reader.GetInt32(3),
                            customerId = reader.GetInt32(4),
                            productTypeId = reader.GetInt32(5),
                            dateCreated = reader.GetDateTime(6),
                            quantity = reader.GetInt32(7)
                        });
                    }
                }
            );
            return _products;
        }


        // Overloaded Method to return a list of products for a customer
        public List<Product> GetProducts(int CustId){
            _db.Query($"select * from product Where CustomerId = {CustId}",
                (SqliteDataReader reader) => {
                    _products.Clear();
                    while (reader.Read ())
                    {
                        _products.Add(new Product(){
                            id = reader.GetInt32(0),
                            title = reader[1].ToString(),
                            description = reader[2].ToString(),
                            price = reader.GetInt32(3),
                            quantity = reader.GetInt32(4),
                            productTypeId = reader.GetInt32(5),
                            customerId = reader.GetInt32(6),
                            dateCreated = reader.GetDateTime(7)
                        });
                    }
                }
            );
            return _products;
        }


        /* This method gets all Stale Products from databse
         Dependencies/FK
         1. Customer table
         2. ProductType table
         requires 8 arguments
         Returns List of Products

        Given a user wants to see products that aren't selling
        When the user selects the option to view stale products
        Then the user should be presented with a list of all products that meet any of the following criteria

        Has never been added to an order, and has been in the system for more than 180 days
        Has been added to an order, but the order hasn't been completed, and the order was created more than 90 days ago
        Has been added to one, or more orders, and the order were completed, but there is remaining quantity for the product, and the product has been in the system for more than 180 days
        
        Authored by Aarti Jaisinghani
         */

        public List<Product> GetStaleProducts(){
            List<Product> _staleproducts = new List<Product>();
            _db.Query(@"select * from product p 
                    where productid not in (select productid from prodorder) 
                    and cast(julianday(datetime('now')) -  julianday(createdate) as integer)  > 180 
                    
                    union 
                    
                    select * from product p 
                    where productid in (
                        select productid from prodorder po 
                        join `order` o on po.orderid = o.orderid 
                        where o.paymenttypeid is null 
                        and cast(julianday(datetime('now')) -  julianday(o.datecreated) as integer) > 90) 
                        
                        union 
                        
                        select * from product p 
                        where p.productid in (
                            select po.productid from prodorder po 
                            join `order` o on po.orderid = o.orderid 
                            join product p on p.productid =po.productid 
                            where o.paymenttypeid is not null 
                            and cast(julianday(datetime('now')) -  julianday(p.createdate) as integer) > 180 
                            and p.quantity>(select count(t2.productid)from product t1 join prodorder t2 on t1.productid = t2.productid group by t1.productid))",
                (SqliteDataReader reader) => {
                    while (reader.Read ())
                    {
                        _staleproducts.Add(new Product(){
                            id = reader.GetInt32(0),
                            title = reader[1].ToString()
                        });
                    }
                }
            );
            return _staleproducts;
        }
        /*
        This method returns top 3 most popular products

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
         */

         //this method returns a list of type PopularProduct
        public List<PopularProduct> GetPopularProducts(){
            List<PopularProduct> _popproducts = new List<PopularProduct>();
            _db.Query(@"select x.productid, x.prodname, z.ordercount, y.purchasers, x.price*x.productcount as revenue
                    from
                    (select count(*) as productcount, w.productid as productid, w.productname as prodname, w.price as price
                    from (select po.prodorderid as id, po.orderid as orderid,  po.productid as productid, p.title as productname, p.price as price, o.customerid as customer
                    from prodorder po join product p
                    on po.productid=p.productid
                    join [order] o on o.orderid=po.orderid
                    join customer c on c.customerid=o.customerid) w
                    group by w.productid) x
                    join 
                    (select count(*) as ordercount, productid from prodorder
                    group by productid) z
                    on x.productid = z.productid
                    join 
                    (select count(*) as purchasers , po.productid from prodorder po
                    join `order` o on o.orderid=po.orderid
                    group by po.productid) y 
                    on y.productid = z.productid
                    order by revenue desc
                    limit 3",
                (SqliteDataReader reader) => {
                    while (reader.Read ())
                    {
                        _popproducts.Add(new PopularProduct(){
                            id = reader.GetInt32(0),
                            product = reader[1].ToString(),
                            orders = reader.GetInt32(2),
                            purchasers = reader.GetInt32(3),
                            revenue = reader.GetDouble(4)
                        });
                    }
                }
            );
            return _popproducts;
        }
        
        // This method removes a product if it is not added to the order yet
        // requires id of the product
        // Authored by Azim
        public bool RemoveProduct(int id)
        {
            int deleted = 0;
            deleted = _db.Insert($"DELETE FROM product WHERE productId == {id} and productId NOT IN (SELECT o.productId FROM prodorder o)");
            if(deleted == 0) {
                Console.WriteLine("Cannot delete items in an order");
            }
            return true;
        }
        // Method for updating the product
        // requires id of the product, column name and new value
        // Authored by Azim
        public void UpdateProduct(int id, string what, string value)
        {
            _db.Insert($"Update product set '{what}' = '{value}' where productId == {id};");
        }
        // This method gets a single Product from databse
        // requires id of the product
        // Authored by Azim
        public Product GetProduct(int id)  => _products.SingleOrDefault(prod => prod.id == id);

    }
}
