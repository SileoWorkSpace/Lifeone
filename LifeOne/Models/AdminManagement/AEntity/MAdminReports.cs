using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MAdminReports
    {
    }
    public class MAdminMemberBankDetails
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string LoginId { get; set; }
        public string Email { get; set; }
        public long Fk_Memid { get; set; }
        public string MemberAccNo { get; set; }
        public string MemberBankName { get; set; }
        public string IFSCCode { get; set; }
        public string MemberBranch { get; set; }
        public string BankHolderName { get; set; }
        public int ?Page { get; set; }
        public int Size { get; set; }
        public int TotalRecord { get; set; }
        public string SearchParam { get; set; }
        public string Searchby { get; set; }
        public Pager Pager { get; set; }
        
        public List<MAdminMemberBankDetails> Objlist { get; set; }


    }

    public class MAdminCommission
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string LoginId { get; set; }
        public string Email { get; set; }
        public long Fk_Memid { get; set; }
        public decimal Amount { get; set; }
        public string JoiningDate { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int? Page { get; set; }
        public int TotalRecord { get; set; }
        public string SearchParam { get; set; }
        public string Searchby { get; set; }
        public Pager Pager { get; set; }
        public int Size { get; set; }
        public List<MAdminCommission> Objlist { get; set; }

    }

    public class MAdminUtilityBiilpayment
    {
        public string Response { get; set; }
        public string Msg { get; set; }
        public string TransactionId { get; set; }
        public int Fk_MemId { get; set; }
        public string LoginId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public Nullable<decimal> TransactionAmount { get; set; }
        public string TransactionDate { get; set; }
        public string Remarks { get; set; }
        public string IsCalculated { get; set; }
        public string TransactionType { get; set; }

        public int ?Page { get; set; }
        public int Size { get; set; }
        public int ProcId { get; set; }
        public int TotalRecords { get; set; }
        public int IsExport { get; set; }
        public string ActionType { get; set; }
        public string OrderNo { get; set; }
        public string Session { get; set; }
        public string Operator { get; set; }
        public decimal CommissionAmount { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public string SearchParam { get; set; }
        public string Searchby { get; set; }
        public Pager Pager { get; set; }
        public List<MAdminUtilityBiilpayment> Objlist { get; set; }


    }

    public class MAdminMembers
    {
        public long FK_MemId { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string MemberName { get; set; }
        public string SponsorId { get; set; }
        public string Status { get; set; }
        public string SponsorName { get; set; }
        public string Email { get; set; }
        public string state { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public string Tehsil { get; set; }
        public string InviteCode { get; set; }
        public string InvitedBy { get; set; }
        public string joiningDate { get; set; }
        public string Mobile { get; set; }
        public string SponsorMobile { get; set; }
        public string isblocked { get; set; }
        public long ToatlRecord { get; set; }
        
        public int ?page { get; set; }
        public int size { get; set; }
        public int TotalReferal { get; set; }
        public string pageurl { get; set; }
        public string ProfilePic { get; set; }
        public string SearchParam { get; set; }
        public string Searchby { get; set; }
        public Pager Pager { get; set; }
        public List<MAdminMembers> Objlist { get;set;}

    }

    public class MAdminAllwalletdetails : MAdminMembers
    {

        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public decimal Bag { get; set; }
        public decimal Unclear { get; set; }
        public decimal Commission { get; set; }
        public int TotalRecord { get; set; }
        public string Response { get; set; }
        public string Msg { get; set; }       
        public decimal TotalAmountAddWallet { get; set; }
        public decimal TotalAmountUseWallet { get; set; }
        public List<MAdminAllwalletdetails> Objlist2 { get; set; }

       
    }

    public class MAdminRazorPaymentSearch : MAdminMembers
    {
        public string Response { get; set; }
        public string Msg { get; set; }
        public string Payid { get; set; }
        public int Pk_PaymentId { get; set; }
        public decimal Amount { get; set; }
        
        public string contact { get; set; }
        public string CreatedDate { get; set; }
        public string method { get; set; }        
        public List<MAdminRazorPaymentSearch> Objlist2 { get; set; }

    }

    public class MAdminMemberKYC
    {
        public string Name { get; set; }
        public string LoginId { get; set; }
        public string Mobile { get; set; }
        public bool IsAadharApproved { get; set; }
        public bool IsAadharReject { get; set; }
        public string AdhaarCardAttachBack { get; set; }
        public string AdhaarCardAttach { get; set; }
        public long TotalRecords { get; set; }
        public string JoiningDate { get; set; }
        public int ?Page { get; set; }
        public int size { get; set; }
        public string AdharCardNumber { get; set; }
        public List<MAdminMemberKYC> Objlist { get; set; }
        public string response { get; set; }
        public int Code { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public int TotalRecord { get; set; }
        public string date { get; set; }
        public string AdharCardRejectRemark { get; set; }
        public string SearchParam { get; set; }
        public string Searchby { get; set; }
        public Pager Pager { get; set; }
       
    }

    public class MAdminIncomeType
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string LoginId { get; set; }
        public decimal Amount { get; set; }
        public decimal CrAmount { get; set; }
        public decimal DrAmount { get; set; }
        public int ProcId { get; set; }
        public string Remark { get; set; }
        public decimal SaleAmount { get; set; }
        public int ?Page { get; set; }
        public int size { get; set; }
        public int TotalRecord { get; set; }
        public long Fk_memId { get; set; }
        public string TransactionId { get; set; }
        public string TransactionDate { get; set; }
        public string StoreName { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public decimal TotalAmount { get; set; }
        public string response { get; set; }
        public string CreatedDate { get; set; }        
        public List<MAdminIncomeType> Objlist { get; set; }
        public string SearchParam { get; set; }
        public string Searchby { get; set; }
        public Pager Pager { get; set; }
    }

    public class MAdminVendorDetails
    {
        public string LoginId { get; set; }
        public string VendorName { get; set; }
        public string VendorContactNo { get; set; }
        public string shop_title { get; set; }
        public string shop_logo { get; set; }
        public string shop_country { get; set; }
        public string Pincode { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string Address { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalAmount { get; set; }
        public int ?Page { get; set; }
        public int Fk_VendorId { get; set; }
        public int Size { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public List<MAdminVendorDetails> Objlist { get; set; }
        public string response { get; set; }
        public string OrderNo { get; set; }
        public string TransactionNo { get; set; }
        public string Status { get; set; }
        public string CreatedDate { get; set; }
        public string ProductName { get; set; }
        public int Quatity { get; set; }
        public string Producttype { get; set; }
        public string SearchParam { get; set; }
        public string Searchby { get; set; }
        public Pager Pager { get; set; }
        public long TotalRecord { get; set; }
    }

    public class MAdminAchieversDetails
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string LoginId { get; set; }
        public string AchievementDate { get; set; }
        public long Prize { get; set; }
        public long ToatlRecord { get; set; }
        public int ?Page { get; set; }
        public int Size { get; set; }
        public string Status { get; set; }
        public string InviteCode { get; set; }
        public List<MAdminAchieversDetails> Objlist { get; set; }
        public string SearchParam { get; set; }
        public string Searchby { get; set; }
        public Pager Pager { get; set; }
       
    }

    public class MAdminBlockUnblock
    {


        public long Fk_MemId { get; set; }
        public string Name { get; set; }
        public string LoginId { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Status { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsPermanentBlock { get; set; }
        public int CreatedBy { get; set; }
        public string response { get; set; }
        public long TotalRecords { get; set; }
        public long IsExport { get; set; }
        public string SearchParam { get; set; }
        public string Searchby { get; set; }
        public Pager Pager { get; set; }

        public List<MAdminBlockUnblock> Objlist { get; set; }
        public int ?Page { get; set; }
        public long Size { get; set; }
    }

    public class BlockUnblockResponse
    {
        public string Flag { get; set; }
        public string Msg { get; set; }
        public string TransId { get; set; }
    }

public class MAdminFranchiseSell:MPaging
    {
        public string Saledate { get; set; }
        public string InvoiceNo { get; set; }
        public string ProductName { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double PrdAmt { get; set; }
        public double TotalPv { get; set; }
        public double TotalAmt { get; set; }
        public double TotalPrdAmount { get; set; }
        public double TotalprdAmtSum { get; set; }
        public double PaidAmt { get; set; }
        public long ?fk_MemId { get; set; }
        public string Date { get; set; }
        public int ProcId { get; set; }
        public string PayMode { get; set; }
        public string PaymentStatus { get; set; }
        public int BoxQuantity { get; set; }
        public int ReqBoxQty { get; set; }
        public string BillingType { get; set; }
public List<MAdminFranchiseSell> objList { get; set; }

        
           
    }

    public class MAdminFranchisePurchaseHistory
    {

        public long? fk_MemId { get; set; }
        public string Date { get; set; }
        public int ProcId { get; set; }
        public List<MAdminFranchisePurchaseHistory> objList { get; set; }
        public string ProductType { get; set; }
        public string ProductName { get; set; }
        public string quantity { get; set; }
        public string MRP { get; set; }
        public string SalesPrice { get; set; }
        public string InvoiceNo { get; set; }
        public double PaidAmt { get; set; }
        public double TotalPv { get; set; }
        public int RequestedQty { get; set; }
        public int ApproveQty { get; set; }
        public double PrdAmount { get; set; }
        public double TotalAmt { get; set; }       
        public string paymentStatus { get; set; }
        public string RequestStatus { get; set; }

    }
}