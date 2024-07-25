using LifeOne.Models.Common;
using LifeOne.Models.QUtility.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateEntity
{
    public class AssociatePurchaseDetails: MPaging
    {
    
        public long PK_RId { get; set; }
        public long Fk_MemId { get; set; }
        public string OrderNo { get; set; }
        public string Amount { get; set; }
        public int ItemQty { get; set; }
        public string PaymentType { get; set; }
        public string OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentStatus { get; set; }
        public string ApproveDate { get; set; }
        public string ProductType { get; set; }
        public string ProductName { get; set; }
        public int ReqQnty { get; set; }
        public int ApprovalQty { get; set; }
        public string PrdWiseRmrk { get; set; }
        public int PrdWiseStsId { get; set; }
        public decimal PointValue { get; set; }
        public string Status { get; set; }
        public string ShippedDate { get; set; }
        public string SupplyToName { get; set; }
        public string UserType { get; set; }
        public string ShippingStatus { get; set; }
        public List<AssociatePurchaseDetails> LstOrder { get; set; }
        public decimal DiscountAmt { get; set; }
        public decimal TotalPV { get; set; }
        public decimal CreditQuantity { get; set; }
        public decimal DebitQuantity { get; set; }
        public string TranDate { get; set; }
        public int TotalRecord { get; set; }
        public bool IsBox { get; set; }
        public int BoxQty { get; set; }
        public int ReqBoxQty { get; set; }
    }
}