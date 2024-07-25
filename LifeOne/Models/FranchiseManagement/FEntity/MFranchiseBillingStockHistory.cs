using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.FranchiseManagement.FEntity
{
    public class MFranchiseBillingStockHistory
    {
        public int PK_KeyId { get; set; }
        public int CategoryId { get; set; }
        public decimal To { get; set; }
        public decimal Amount { get; set; }
        public long PrdId { get; set; }
        public string ProductType { get; set; }
        public int Fk_OrbitId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public decimal PointValue { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmt { get; set; }
        public decimal TotalItemAmt { get; set; }
        public decimal TotalPv { get; set; }
        public string PaymentRemark { get; set; }
        public string ChequeDDNo { get; set; }
        public string ChequeDDDate { get; set; }
        public string BankName { get; set; }
        public string PaymentSlip { get; set; }
        public string HdnPaymentSlip { get; set; }
        public long FranchiseId { get; set; }
        public string PaymentMode { get; set; }
        public decimal DiscountAmt { get; set; }
        public string CustomerMobile { get; set; }
        public int AvailableStock { get; set; }
        public string BoxType { get; set; }
        public bool IsBox { get; set; }
        public int BoxQty { get; set; }
        public double BoxPV { get; set; }
        public int FK_OrbitType { get; set; }
        public long CustomerId { get; set; }
        public decimal DueAmt { get; set; }
        public decimal PrdAmt { get; set; }
        public int Code { get; set; }
        public int ReqBoxQty { get; set; }
        public int ItemsQty { get; set; }
        public string Remark { get; set; }
        public string SellToName { get; set; }
        public string SellDate { get; set; }
        public string InvoiceNo { get; set; }
        public string OrderNo { get; set; }
        public int CreditQuantity { get; set; }
        public int DebitQuantity { get; set; }





        public List<MFranchiseBillingStockHistory> LstStock { get; set; }
    }
}