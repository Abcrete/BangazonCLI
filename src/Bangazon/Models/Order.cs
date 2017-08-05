using System;

namespace Bangazon.Models
{
    /* Order Class represents the properties for the entries to the Order Tabel in database
       Authored by Jason Smith */
    public class Order
    {
        /*This is the primary key for the entry on the Order table.*/
        public int id {get; set;}
        /* Customer who the order belongs to */
        public Customer customer {get; set;}
        /* Payment for the order, if null or is incomplete */
        public PaymentType payment {get; set;}
        /* Date order was instantialized */
        public DateTime dateCreated {get; set;}
    }
}
