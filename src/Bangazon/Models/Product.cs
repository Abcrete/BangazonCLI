using System;

namespace Bangazon.Models
{
  public class Product
   //SUMMARY
    /*This class is authored by Azim. 
      Product Class identifies the properties for the entries to the Product Tabel in database*/
  {
    public int ProductId { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public double price { get; set; }
    public int quantity { get; set; }
    public int customerId { get; set; }
    public int productTypeId { get; set; }
    public DateTime dateCreated { get; set; }
  }
}
