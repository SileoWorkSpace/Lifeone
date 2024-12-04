using LifeOne.Models.QUtility.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.Common;



namespace LifeOne.Models.FranchiseManagement.FEntity
{
    public class MFranchiseorderRequest: MCommonProperties
    {
        public int PK_KeyId { get; set; }
        public int PK_Id { get; set; }
        public int OpCode { get; set; }
        public int CategoryId { get; set; }
        public decimal To { get; set; }
        public decimal Amount { get; set; }
        public string ProductType { get; set; }
        public int Fk_OrbitId { get; set; }
        public decimal PointValue { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmt { get; set; }
        public decimal TotalItemAmt { get; set; }
        public decimal TotalPv { get; set; }
        public string PaymentRemark { get; set; }
        public string ChequeDDNo { get; set; }
        public string PaymentDate { get; set; }
        public decimal DP { get; set; }
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
        public int Code { get; set; }
        public int ReqBoxQty { get; set; }
        public string Remark { get; set; }

        public long Fk_Memid { get; set; }
        public string XmlData { get; set; }
        public bool isOnlinePayDone { get; set; }
        public bool IsFree { get; set; }
        public string OrbitName { get; set; }
        public decimal PaidAmount { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPinCode { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerLoginId { get; set; }
        public string CustomerGstNo { get; set; }
        public string UpiNumber { get; set; }
        public string OfferQuantity { get; set; }
        public string DistrictId { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string StateId { get; set; }
        public string PinCode { get; set; }
        public string ShoppingType { get; set; }
    }


    public class AdminProductDetail
    {
        public string id { get; set; }
        public string PackageName { get; set; }
        public string CategoryName { get; set; }
        public string Price { get; set; }
        public string status { get; set; }
        public string BP { get; set; }
        public string DP { get; set; }
        public string gst { get; set; }
        public string hsn { get; set; }
        public string rate { get; set; }
        public string ProductName { get; set; }
        public string qty { get; set; }
        public string SubTotal { get; set; }







    }


}