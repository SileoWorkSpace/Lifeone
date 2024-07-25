using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class MCustomerBilling
    {
      
    }
    public class CustomerDetailsResponse
    {
        public string response { get; set; }
        public int Flag { get; set; }
        public string Msg { get; set; }
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public string CustomerMail { get; set; }
        public long FK_MemId { get; set; }
    }
    public class ProductReq
    {
        public int FranchiseId { get; set; }
        public int ProcId { get; set; }
    }
    public class ProductDetailByFranchise
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }
    public class ProductDetailByFranchiseResponse
    {
        public string response { get; set; }
        public string Msg { get; set; }
        public List<ProductDetailByFranchise> result { get; set; }
    }
}