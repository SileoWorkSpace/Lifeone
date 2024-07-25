using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{   
    public class vendorDetails
    {
        public string seller_id { get; set; }
        public string shop_title { get; set; }
        public string contact_number { get; set; }
        public object shop_logo { get; set; }
        public string pincode { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string line1 { get; set; }
        public string line2 { get; set; }
    }

    public class MVendorViewModel
    {
        public string response { get; set; }
        public List<vendorDetails> data { get; set; }
    }

}