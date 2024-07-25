using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
  
    public class INRModel
    {
        public string response { get; set; }
        public List<INRDetails> lstINR { get; set; }
    }
    public class INRDetails
    {
        public string transactionId { get; set; }
        public string storeName { get; set; }
        public decimal saleAmount { get; set; }
        public string transactionDate { get; set; }
        public decimal payout { get; set; }
        public string status { get; set; }
        public string subId { get; set; }
        public int fK_MEMId { get; set; }
        public string imageurl { get; set; }
    }
}