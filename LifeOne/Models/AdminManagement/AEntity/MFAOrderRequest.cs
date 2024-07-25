using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using LifeOne.Models.Common;


namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MFAOrderRequest
    {
        public long ReqId { get; set; }


        public string FName { get; set; }
        public string LoginID { get; set; }
        public int TotalItems { get; set; }
        public double TotalAmt { get; set; }
        public string ReqDate { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentStatus { get; set; }
        public string ApproveDate { get; set; }

        public string ShippingStstId { get; set; }
        public string ShippingDate { get; set; }

        public long ParentId { get; set; }
        public int ParentTypeId { get; set; }
        public int ToatlRecord { get; set; }
        public int IsExport { get; set; }

        public string Response { get; set; }
        public string Msg { get; set; }
        public Pager Pager { get; set; }
        public long Size { get; set; }
        public string SearchParam { get; set; }
        public int? Page { get; set; }
        public string Searchby { get; set; }
        public string OrderNo { get; set; }
        
        public string BankName { get; set; }
        public string TransactionId { get; set; }
        public string PayMode { get; set; }
        public string ReciptImage { get; set; }
        public string PaymentDate { get; set; }
        public string InvoiceNo { get; set; }        
        public int ProcId { get; set; }
        public string Contact { get; set; }
        public string CompanyName { get; set; }
        public List<MFAOrderRequest> MFAOrderRequestList { get; set; }
        public List<MFAPaymentStatus> MFAPaymentStatusList { get; set; }

    }
    public class MFAOrderRequestDetail {
        public long Fk_RId { get; set; }
        public long FK_PrdId { get; set; }
        public long FK_FId { get; set; }
        public string ProductType { get; set; }
        public string ProductName { get; set; }
        public int ReqQnty { get; set; }
        public int ApprovalQty { get; set; }
        public string PrdWiseRmrk { get; set; }
        public int PrdWiseStsId { get; set; }
        public int AdminStock { get; set; }
        public int FranchiseStock { get; set; }
        public string OrderNo { get; set; }
    }

    public class XmlModel {
        public string XmlData { get; set; }
    }

    public class MFAPaymentStatus {
        public long ReqId { get; set; }
        public string BankName { get; set; }
        public string TransactionId { get; set; }
        public string PayMode { get; set; }
        public string ReciptImage { get; set; }
        public string PaymentDate { get; set; }
        public string InvoiceNo { get; set; }
        public decimal TotalAmt { get; set; }

    }
    public class OrderConfirmationModel
    {
        public string CustomerId { get; set; }
        public string LoginId { get; set; }
        public string BillingPincode { get; set; }
        
        public string OrderNo { get; set; }
        public string OrderDate { get; set; }
        public string OrderType { get; set; }
        public string VendorName { get; set; }
        public string DeliveryDate { get; set; }
        public string OrderAmount { get; set; }
        public decimal BaseAmount { get; set; }
        public string ShippingCharges { get; set; }
        public string UserName { get; set; }
        public string DeliveryAddress { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string PinCode { get; set; }
        public string OrderStatus { get; set; }
        public string InvoiceNo { get; set; }
        public string GSTIN { get; set; }
        public string PAN { get; set; }
        public string CIN { get; set; }
        public string TotalProductQty { get; set; }
        public string TotalProductAmt { get; set; }
        public string TotalDiscount { get; set; }
        public string TotalTax { get; set; }
        public string TotalCGST { get; set; }
        public string TotalSGST { get; set; }
        public string Total { get; set; }
        public string BName { get; set; }
        public string SName { get; set; }
        public string BAddress { get; set; }
        public string SAddress { get; set; }
        public string VAddress { get; set; }
        public string VGSTNO { get; set; }
        public string VCity { get; set; }
        public string BMobileNo { get; set; }
        public string SMobileNo { get; set; }
        public string ProductName { get; set; }
        public string AmountInWords { get; set; }
        public string TransportMode { get; set; }
        public string State { get; set; }
        public string InvoiceDate { get; set; }
        public string Address { get; set; }
        public string GSTNo { get; set; }
        public string City { get; set; }
        public string CourierName { get; set; }
        public string FinalAmount { get; set; }
        public string CompanyName { get; set; }
        public string OfficeAddress { get; set; }
        public string OfficeEmailId { get; set; }
        public string OfficeContactNo { get; set; }
        public string Website { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyLogo { get; set; }




        public List<ItemsModel> OrderItems { get; set; }
        public string HSNNo { get;  set; }
        public string StateCode { get;  set; }
        public string BGSTNo { get;  set; }
        public string FreeAmount { get;  set; }
    }

    public class ItemsModel
    {
        public string ProductId { get; set; }
        public string OrderId { get; set; }
        public string OrderNo { get; set; }
        public string Image { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string ProductDetails { get; set; }
        public string ProductQty { get; set; }
        public string boxqty { get; set; }

        
        public string ProductAmt { get; set; }
        public string Discount { get; set; }
        public string Tax { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
        public string IGST { get; set; }
        public string CGSTAmount { get; set; }
        public string SGSTAmount { get; set; }
        public string IGSTAmount { get; set; }
        public string TotalProductAmt { get; set; }
        public string OrderItemId { get; set; }
        public string SizeName { get; set; }
        public string UnitName { get; set; }
        public string InvoiceNo { get; set; }
        public string Status { get; set; }
        public string ProductUrl { get; set; }
        public string VendorName { get; set; }
        public string OrderAmount { get; set; }
        public decimal BaseAmount { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string GSTNo { get; set; }
        public decimal TotalAmount { get; set; }
        public string BV { get; set; }
        public string TotalBV { get; set; }
        public decimal TexableAmount { get; set; }
        public decimal SubTotal { get; set; }
        public string AmountInWords { get; set; }
        public decimal PointValue { get; set;}
        public string HSNNo { get;  set; }
    }

    public class ActivatedCustomerTaxInvoice
    {
        public string StateCode { get; set; }
        public string LoginId { get; set; }
        public string DisplayName { get; set; }
        public string ActivationDate { get; set; }
        public string InnvoiceNo { get; set; }
       
        public string FranchiseCode { get; set; }
        public string FranchiseName { get; set; }
        public decimal Amount { get; set; }
        public decimal BV { get; set; }
        public string CompanyName { get; set; }
        public string OfficeAddress { get; set; }
        public string OfficeEmailId { get; set; }
        public string OfficeContactNo { get; set; }
        public string CompanyGST { get; set; }
        public string Website { get; set; }
        public string Pancard { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }

        public string custAddress { get; set; }
        public string custCity { get; set; }
        public string custState { get; set; }
        public string custPincode { get; set; }
        public string mobile { get; set; }
        public string custGSTNo { get; set; }
        public string CompanyLogo { get; set; }
        
        public List<InvoiceProduct> OrderItems { get; set; }
        public string HSNNo { get;  set; }
        public string BVAmountInWords { get;  set; }
    }
    public class InvoiceProduct
    {
        public string ProductName { get; set; }
        public string HSNNo { get; set; }
        public string Qty { get; set; }
        public string BV { get; set; }
        public string Rate { get; set; }
        public string SGST { get; set; }
        public string CGST { get; set; }
        public string IGST { get; set; }
        public string Tax { get; set; }
        public string TaxableAmt { get; set; }
        public string SGSTAmt { get; set; }
        public string CGSTAmt { get; set; }
        public string IGSTAmt { get; set; }
        public string TotalTaxAmt { get; set; }
        public string TotAmount { get; set; }
    }

}