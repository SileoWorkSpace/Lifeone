using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.FranchiseManagement.FEntity                       
{
    public class MOrder : MPaging
    {

        public long PK_RId { get; set; }
        public int IsExport { get; set; }
        public long Fk_MemId { get; set; }  
        public long PK_OrderId { get; set; }
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
        public string ProductImage { get; set; }


        
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int IsShipped { get; set; }

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
        public List<MOrder> LstOrder { get; set; }
        public decimal DiscountAmt { get; set; }
        public decimal TotalPV { get; set; }
        public decimal CreditQuantity { get; set; }
        public decimal DebitQuantity { get; set; }
        public string TranDate { get; set; }
        
        public bool IsBox { get; set; }
        public int BoxQty { get; set; }
        public int ReqBoxQty { get; set; }
        public double TotalAmt { get; set; }
        public string TransactionNo { get; set; }
        public string PayMode { get; set; }
        public string PaymentDate { get; set; }

        public string FK_RefTblOrderId { get; set; }
        public string ReciveStatus { get; set; }

        public string SellTo { get; set; }
        public int AvailableStock { get; set; }
        public long FK_PrdId { get; set; }
        public long Fk_RId { get; set; }
        public long FK_FId { get; set; }
        public long PK_ORId { get; set; }        
        public string ReciptImage { get; set; }       
        public string LoginID { get; set; }
        public string PKFranchiseRegistrationId { get; set; }
        public string Message { get; set; }
        public string Fk_InvId { get;  set; }
    }

    public class OrderProductDetailsModel
    {
        public int PK_OrderId { get; set; }
        public int FK_OrderId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
        public decimal Amount { get; set; }
        public string ProductType { get; set; }
      

    }
    



}