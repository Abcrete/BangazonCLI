using System;

namespace Bangazon.Models
{
  public class PopularProduct
   //SUMMARY
    /*This class is authored by Azim and Aarti. 
      PopularProduct Class identifies the popular product properties */
  {
    public int id { get; set; }
    public string product { get; set; }
    public int orders { get; set; }
    public int purchasers { get; set; }
    public double revenue { get; set; }
    
  }
}
