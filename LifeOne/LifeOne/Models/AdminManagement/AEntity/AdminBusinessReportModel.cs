using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class AdminBusinessReportModel
    {
        public string FranchiseName { get; set; }
        public string LoginId { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Mobile { get; set; }
        public string OrderNo { get; set; }
        public string Invoice { get; set; }
        public string FK_MemId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal TotalPV { get; set; }
        public string Date { get; set; }
        public int Quantity { get; set; }
        public int TotalRecord { get; set; }
        public string Contact { get; set; }
        public Pager  Pager { get; set; }
        public string InvoiceNo { get; set; }
        public string fk_MemId { get; set; }
        public List<AdminBusinessReportModel> AdminBusinessReportModellst { get; set; }

    }
    public class BusinessDetail
    {
        public string ProductName { get; set; }
        public int OrderQuantity { get; set; }
        public int AproveQuantity { get; set; }
        public string Date { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalPV { get; set; }


    }
}