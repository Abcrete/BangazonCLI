namespace Bangazon.Models
{
  public class Product
   //SUMMARY
    /*This class is authored by Azim. 
      Product Class identifies the properties for the entries to the Product tabel in database*/
  {
    public int id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public double price { get; set; }
    public int quantity { get; set; }
    public Customer customer { get; set; }
    public Product product { get; set; }
    public DateTime dateCreated { get; set; }
  }
}
