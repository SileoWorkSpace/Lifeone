using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace LifeOne.Models.API
{
   
    public class BusinessSummaryProductDetail
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalPv { get; set; }

        public decimal AirOrbit { get; set; }
        public decimal WaterOrbit { get; set; }
        public decimal SpaceOrbit { get; set; }
        public string Date { get; set; }

    }
    public class AssociateBusinessSummaryModel
    {
        public string LoginId { get; set; }
        public int Fk_MemId { get; set; }
        public int TotalRecords { get; set; }
        public string DisplayName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Mobile { get; set; }
        public string InvoiceNo { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalPaidAmount { get; set; }
        public decimal TotalPV { get; set; }
        public bool IsBusinessId { get; set; }

    }
}