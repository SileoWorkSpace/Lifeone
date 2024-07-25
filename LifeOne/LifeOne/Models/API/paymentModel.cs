using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models
{
    public class paymentModel
    {
        public string name { get; set; }
        public string currency { get; set; }
        public string price { get; set; }
        public string quantity { get; set; }
        public string sku { get; set; }
        public string tax { get; set; }
        public string shipping { get; set; }
        public string subtotal { get; set; }
        public string total { get; set; }
        public string invoice_number { get; set; }
        public string fk_memid { get; set; }
        public string payerid { get; set; }
        public string paymentstatus { get; set; }
        public string type { get; set; }
        public string fk_orderid { get; set; }
        public string paymentid { get; set; }
        public string intent { get; set; }
        public string payeremail { get; set; }
        public string payerfirstname { get; set; }
        public string payerlastname { get; set; }
        public string merchantid { get; set; }
        public string merchantemail { get; set; }
        public string transdesc { get; set; }
        public string transactionno { get; set; }
        public string transtype { get; set; }
    }

    public class VerdorDetails 
    {
        public string response { get; set; }
        public List<VerdorData> data { get; set; }
    }
    public class VerdorData
    { 
     
        public string seller_id { get; set; }  
        public string shop_title { get; set; }
        public string contact_number { get; set; }
        public int VendorId { get; set; }
        public string shop_logo { get; set; }         
        public string line1 { get; set; }
        public string line2 { get; set; }
        public string city { get; set; }
        public string pincode { get; set; }
        public string state { get; set; }
        public string country { get; set; }
      
    }
    public class shop_address
    {
     public string line1 { get; set; }
        public string line2 { get; set; }
        public string city { get; set; }
        public string pincode { get; set; }
        public string state { get; set; }
        public string country { get; set; }
    }
}