using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateEntity
{
    public class MInvoice
    {
        public string MemberName { get; set; }
        public string MobileNo { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public double PaidAmt { get; set; }
        public double TotalAmount { get; set; }
        public double cashbackAmount { get; set; }
        public string OrderNo { get; set; }
        public string OrderDate { get; set; }
        public double TotalPv { get; set; }
        public List<InvoiceOrderDetails> lst { get; set; }
    }
    public class InvoiceOrderDetails
    {
        public string ProductName { get; set; }
        public int Quatity { get; set; }
        public double PrdAmt { get; set; }
        public double Amount { get; set; }
        public double BV { get; set; }
        public double PV { get; set; }
        public double BoxPv { get; set; }
        public double TotalPv { get; set; }
        public double TotalAmt { get; set; }
        public double Qpv { get; set; }
    }
}