using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.FranchiseManagement.FEntity
{
    public class FranchiseBusinessReportModel
    {
        public string LoginId { get; set; }
        public string DisplayName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string MobileNo { get; set; }
        public int Fk_MemId { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal Pv { get; set; }
        public string Date { get; set; }
        public int Quantity { get; set; }
        public string InvoiceNo { get; set; }
    }
    public class BusinessSummaryProductDetail
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalPv { get; set; }
        public string Date { get; set; }

    }
}