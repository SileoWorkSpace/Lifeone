using Dapper;
using DocumentFormat.OpenXml.Spreadsheet;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.API;
using LifeOne.Models.API.DAL;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using DBHelper = LifeOne.Models.Common.DBHelper;

namespace LifeOne.Models
    {  
    public class Reports : MPaging
    {
       
        public string PK_PaidId { get; set; }
        public int Fk_CategoryId { get; set; }
        public string PayoutNo { get; set; }
        public bool Checked { get; set; }
        public string DOJ { get; set; }
        public string DOA { get; set; }
        public string TransNo { get; set; }
        public string DDChequedate { get; set; }
        public string CreateDate { get; set; }              
       
        public string CategoryName { get; set; }
        public string OfferedPrice { get; set; }
        public string ProductDescription { get; set; }
        public string BalanceQuantity { get; set; }
        public string MobileNo { get; set; }
        public string Landmark { get; set; }
        public string Address { get; set; }  
        public string State { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public string AddressType { get; set; }
        public bool IsDefault { get; set; }
     

        public string LoginId { get; set; }
        public string FranchiseId { get; set; }
        public string ProductName { get; set; }
        public string Name { get; set; }
        public string OrderStatus { get; set; }

        public string FK_OrderId { get; set; }
        public decimal GatewayAmount { get; set; }
        public decimal WalletAmount { get; set; }
        public string Narration { get; set; }
        public int DirectSponsoring { get; set; }
        public int ActiveMember { get; set; }
        public int NumberOfSales { get; set; }
        public int Fk_ProductId { get; set; }
        public decimal TotalSale { get; set; }
        public int TotalPurchase { get; set; }
        public string BonanzaName { get; set; }
        public int BVCount { get; set; }
        public decimal TotalBV { get; set; }
        public string FromBV { get; set; }
        public string ToBV { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalPv { get; set; }
        public decimal DrAmount { get; set; }
        public decimal CrAmount { get; set; }
        public decimal Balance { get; set; }
        public decimal BusinessAmount { get; set; }
        public decimal DisAmount { get; set; }
        public decimal BusinessBV { get; set; }
        public string PersonName { get; set; }
        public string PackageName { get; set; }
        public string OrderNo { get; set; }
        public string RemarkCancel { get; set; }
        public string ShippingId { get; set; }
        public string InvoiceNo { get; set; }
        public string TransactionDate { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string OrderDate { get; set; }
        public string SellById { get; set; }
        public string SellByName { get; set; }
        public string SellToId { get; set; }
        public string SellToName { get; set; }
        public string Contact { get; set; }
        
        public decimal TotalAmt { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal IGST { get; set; }
        public decimal TaxableAmount { get; set; }
        public decimal TaxAmt { get; set; }
        public decimal DirectIncome { get; set; }
        public decimal MatchingBonus { get; set; }
        public decimal LeadershipBonus { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal TDSAmount { get; set; }
        public decimal ProcesssingFee { get; set; }
        public decimal NetAmount { get; set; }
        public decimal BinaryDist { get; set; }
        public decimal MatchingAmt { get; set; }
        public decimal CreditQuantity { get; set; }
        public decimal DebitQuantity { get; set; }
       
        public string HSNNo { get; set; }
        public string Packing { get; set; }
        public string Qty { get; set; }
        public string BV { get; set; }
        public string Rate { get; set; }
      
        public string Tax { get; set; }
        public string TaxableAmt { get; set; }
        public string SGSTAmt { get; set; }
        public string CGSTAmt { get; set; }
        public string IGSTAmt { get; set; }
        public string TotalTaxAmt { get; set; }
        public string TotAmount { get; set; }

        public decimal CommissionPercentage { get; set; }
        public string IncomeType { get; set; }
        public string PaymentDate { get; set; }
        public string ChequeNo { get; set; }
        public string ChaqueDate { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string PaymentMode { get; set; }
        public string RecognitionName { get; set; }
        public string TargetPoint { get; set; }
        public string AchievedOn { get; set; }
        public string ImageURL { get; set; }
        public string Status { get; set; }
        
        
        public int Quantity { get; set; }
        public long Pk_ProductId { get; set; }
        public int PK_Id { get; set; }
        public int FK_MemId { get; set; }
        public string IsPublished { get; set; }
        public string Pk_OrderId { get; set; }

        public int Pk_PackageID { get; set; }

        public bool IsActive { get; set; }

        public string PackageAmount { get; set; }

        public string Price { get; set; }
       public string BP { get; set; }

        public string DP { get; set; }

       

        public string CappingPoints { get; set; }



        public string StateCode { get; set; }
       
        public string DisplayName { get; set; }
        public string ActivationDate { get; set; }
        public string InnvoiceNo { get; set; }

       
       
     
        public string CompanyName { get; set; }
        public string OfficeAddress { get; set; }
        public string OfficeEmailId { get; set; }
        public string OfficeContactNo { get; set; }
        public string CompanyGST { get; set; }
        public string Website { get; set; }
        public string Pancard { get; set; }
        public string custAddress { get; set; }
        public string custCity { get; set; }
        public string custState { get; set; }
        public string custPincode { get; set; }
        public string custGSTNo { get; set; }
        public int IsExport { get; set; }

        public string SearchParam { get; set; }
        public string Searchby { get; set; }
        public string DOB { get; set; }
        public string Email { get; set; }
        public string ProfilePic { get; set; }

        public string BVAmountInWords { get; set; }
        public string Pk_AddressId { get; set; }
        public string Current_stock { get; set; }
        public string Refund_stock { get; set; }
        public int AddedBy { get; set; }
        public string flag { get; set; }
        public string message { get; set; }
        public DataTable dtDetails { get; set; }

        public List<Reports> list { get; set; }
        public List<Reports> OrderItems { get; set; }
        public List<Reports> LstOrder { get; set; }
       
       
        public int OpCode { get; set; }
        public DataTable dtPaidActivation { get; set; }
        public DataTable dtFreeActivation { get; set; }
        public DataTable dtProductStockReport { get; set; }
        public DataTable dtDirectBusinessValues0 { get; set; }
        public DataTable dtDirectBusinessValues1 { get; set; }
        public DataTable dtGetBonanzaReward { get; set; }
        public DataTable dtGetActiveMembers { get; set; }
        public DataTable dtGetProductGstReport { get; set; }
        public DataTable dtGetShoppingOrderDetails { get; set; }
        public DataTable dtGetPremiumClubMembers { get; set; }
        public DataTable dtGetPremiumClubQualifier { get; set; }
        public DataTable dtGetFranchisePayoutReport { get; set; }
        public DataTable dtGetFranchsisePayoutDetails { get; set; }
        public DataTable dtGetRecognition { get; set; }
        public DataTable dtGetPayPayoutReport { get; set; }


        public DataTable dtaddressdetails { get; set; }
        public DataTable dtGetBonanzaRewardForAdmin { get; set; }
        public DataTable dtBusinessDetails { get; set; }
        public DataTable dtDirectBusiness { get; set; }
        public DataTable dtCartList { get; set; }
        //public DataTable DtDetails { get; set; }
        public DataTable dtImages { get; set; }
        public string paymentId { get; set; }
        public DataTable dtPaymentDetails { get; set; }
        public DataTable dtGetInvoiceGSTReport { get; set; }
        public string RazorPayAmount { get; set; }
        public string Key { get; set; }
        public DataTable dtGetConsolidatedGstReport { get;  set; }

        public DataTable dtGetStatewiseGstReport { get; set; }

        public DataTable dtCancelInvoicRequest { get; set; }
        public DataTable AllGetShoppingOrderDetails { get; set; }
        
        
        public DataTable dtAllGetShoppingOrder { get; set; }
        

        public DataTable dtLifeTimeReward { get;  set; }


        public DataTable dtPackageMaster { get; set; }
       
          public DataTable dtGetMembersRecognition { get; set; }
        public DataTable dtGetTallyGSTReport { get; set; }
        public string FinalAmount { get;  set; }
        public int FK_RcogId { get;  set; }
        public string Fk_PurchaseOrderId { get;  set; }
        public string Pk_SupplierId { get;  set; }
        public bool IsFixed { get;  set; }
        public bool IsDiscount { get;  set; }
        public decimal Percentage { get; set; }
        public string MinBv { get;  set; }
        public string ProductMRP { get;  set; }
        public string ProductBV { get;  set; }
        public string ProductDP { get;  set; }
        public string ProductQTY { get;  set; }
        public string Code { get;  set; }
        public string Msg { get;  set; }
        public string Pk_PackageProductId { get;  set; }
        public string Token { get;  set; }
        
       

        public static Reports GetReports(int? Page, Reports _data)
        {
            if (Page == null)
            {
                Page = 1;
            }
//_data.Size = SessionManager.Size;
            _data.Page = Page;
            _data.Size = SessionManager.Size;

            _data.LstOrder = DALReports.GetReports(_data);
            int totalRecords = 0;
            if (_data.LstOrder.Count > 0)
            {
                totalRecords = Convert.ToInt32(_data.LstOrder[0].TotalRecords);
                var pager = new Pager(totalRecords, _data.Page, _data.Size);
                _data.Pager = pager;
            }
            else
            {
                _data.LstOrder = new List<Reports>();
            }
            return _data;
        }

    
        public static Reports GetDirectBusiness(int? Page, Reports _data)
        {
            if (Page == null)
            {
                Page = 1;
            }
            _data.Page = Page;
            _data.Size = SessionManager.Size;

            _data.LstOrder = DALReports.GetDirectBusiness(_data);
            int totalRecords = 0;
            if (_data.LstOrder.Count > 0)
            {
                totalRecords = Convert.ToInt32(_data.LstOrder[0].TotalRecords);
                var pager = new Pager(totalRecords, _data.Page, _data.Size);
                _data.Pager = pager;
            }
            else
            {
                _data.LstOrder = new List<Reports>();
            }
            return _data;

        }
        public static Reports GetFranchiseSales(int? Page, Reports _data)
        {
            if (Page == null)
            {
                Page = 1;
            }
            _data.Page = Page;
            _data.Size = SessionManager.Size;

            _data.LstOrder = DALReports.GetFranchiseSales(_data);
            int totalRecords = 0;
            if (_data.LstOrder.Count > 0)
            {
                totalRecords = Convert.ToInt32(_data.LstOrder[0].TotalRecords);
                var pager = new Pager(totalRecords, _data.Page, _data.Size);
                _data.Pager = pager;
            }
            else
            {
                _data.LstOrder = new List<Reports>();
            }
            return _data;
        }

        public static Reports GetFranchiseWalletLedgerForAdmin(int? Page, Reports _data)
        {
            if (Page == null)
            {
                Page = 1;
            }
            _data.Page = Page;
            _data.Size = SessionManager.Size;

            _data.LstOrder = DALReports.GetFranchiseWalletLedgerForAdmin(_data);
            int totalRecords = 0;
            if (_data.LstOrder.Count > 0)
            {
                totalRecords = Convert.ToInt32(_data.LstOrder[0].TotalRecords);
                var pager = new Pager(totalRecords, _data.Page, _data.Size);
                _data.Pager = pager;
            }
            else
            {
                _data.LstOrder = new List<Reports>();
            }
            return _data;
        }

        public DataSet GetPackageSaleReport()
        {

            try
            {
                SqlParameter[] para = {
                                        new SqlParameter("@Pk_ProductId", Pk_ProductId)

                 };
                DataSet ds = DBHelper.ExecuteQuery("GetPackageSaleReport", para);
                return ds;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet GetBusinessStatistics()
        {

            try
            {
                SqlParameter[] para = {
                                        new SqlParameter("@Pk_ProductId", Pk_ProductId)

                 };
                DataSet ds = DBHelper.ExecuteQuery("GetBusinessStatistics", para);
                return ds;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static AdminStock GetFranchiseStockForAdmin(int? Page, AdminStock _data)
        {

            if (_data.Page == null || _data.Page==0)
            {
                _data.Page = 1;
            }
            _data.Size = SessionManager.Size;

            _data.LstOrder = DALReports.GetFranchiseStockForAdmin(_data);
              int totalRecords = 0;
            if (_data.LstOrder.Count > 0)
            {
                totalRecords = Convert.ToInt32(_data.LstOrder[0].TotalRecords);
                var pager = new Pager(totalRecords, _data.Page, _data.Size);
                _data.Pager = pager;
            }
            else
            {
                _data.LstOrder = new List<AdminStock>();                  
            }


            return _data;
        }


        public DataSet GetAllProducts()
        {
            SqlParameter[] para = {
                                        //new SqlParameter("@Pk_ProductId", Pk_ProductId)

            };
            DataSet ds = DBHelper.ExecuteQuery("GetProductDetails", para);
            return ds;
        }
        public DataSet GetAllFranchiseDetail()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@FranchiseId", null)

            };
            DataSet ds = DBHelper.ExecuteQuery("ProGetFranchiseDetailsByLoginId", para);
            return ds;
        }

        public DataSet GetBonanzaReward()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Fk_MemId",FK_MemId )

            };
            DataSet ds = DBHelper.ExecuteQuery("GetBonanzaReward", para);
            return ds;
        }


        public DataSet GetBonanzaRewardForAdmin()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId",LoginId ),
                                        new SqlParameter("@FromDate",FromDate ),
                                        new SqlParameter("@ToDate",ToDate ),
                                        new SqlParameter("@FromBV",FromBV ),
                                        new SqlParameter("@ToBV",ToBV ),
                                        new SqlParameter("@Page",Page ),
                                        new SqlParameter("@Size",Size ),
                                        new SqlParameter("@IsExport",IsExport ),

            };
            DataSet ds = DBHelper.ExecuteQuery("GetBonanzaRewardForAdmin", para);
            return ds;

        }
        public DataSet GetPackagewayActvationReport()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@FK_ProductID",Pk_PackageID),
                                        new SqlParameter("@LoginId",LoginId),
                                        new SqlParameter("@PayMentMode",PaymentMode=="0"?null:PaymentMode),                      
                                        new SqlParameter("@fromDate",FromDate),
                                        new SqlParameter("@toDate",ToDate),
                                        new SqlParameter("@Page",Page ),
                                        new SqlParameter("@Size",Size ),
                                        new SqlParameter("@IsExport",IsExport ),

            };
            DataSet ds = DBHelper.ExecuteQuery("GetPackageWayActivation", para);
            return ds;

        }

        public static Reports GetPackageGstReport(int? Page, Reports _data)
        {
            if (Page == null)
            {
                Page = 1;
            }
            _data.Page = Page;
            _data.Size = SessionManager.Size;

            _data.LstOrder = DALReports.GetPackageGstReport(_data);
            int totalRecords = 0;
            if (_data.LstOrder.Count > 0)
            {
                totalRecords = Convert.ToInt32(_data.LstOrder[0].TotalRecords);
                var pager = new Pager(totalRecords, _data.Page, _data.Size);
                _data.Pager = pager;
            }
            else
            {
                _data.LstOrder = new List<Reports>();
            }


            return _data;
        }
        public DataSet GetProductStockReport()
        {

            try
            {
                SqlParameter[] para = {
                                        new SqlParameter("@Fk_ProductId", Fk_ProductId),
                                        
                                        new SqlParameter("@Page", Page),
                                        new SqlParameter("@Size", Size),
                                        new SqlParameter("@IsExport", IsExport),

                 };
                DataSet ds = DBHelper.ExecuteQuery("GetProductStockReport", para);
                return ds;

            }
            catch (Exception ex) 
            {

                throw ex;
            }
        }

        public DataSet GetProductGstReport(Reports reports)
        {
            SqlParameter[] para = {
                                        new SqlParameter("@OpCode",3 ),
                                        new SqlParameter("@Page",Page),
                                        new SqlParameter("@Size",Size),
                                        new SqlParameter("@IsExport",IsExport)

            };
            DataSet ds = DBHelper.ExecuteQuery("GetGstReport", para);
            return ds;

        }

        public DataSet GetActiveMembers()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginID",LoginId ),

            };
            DataSet ds = DBHelper.ExecuteQuery("GetActiveMembersFranchise", para);

            return ds;

        }
        public DataSet GetBusinessSummaryForAssociate()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Fk_MemId",FK_MemId ),
                                        new SqlParameter("@FromDate",FromDate ),
                                        new SqlParameter("@ToDate",ToDate )

            };
            DataSet ds = DBHelper.ExecuteQuery("GetBusinessSummaryForAssociate", para);

            return ds;

        }
        public DataSet GetCartList()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Quantity", Quantity),
                                        new SqlParameter("@Fk_MemId", FK_MemId), 
                                        new SqlParameter("@Pk_ProductId", Pk_ProductId),
                                        new SqlParameter("@PK_Id", PK_Id),

                                        new SqlParameter("@OpCode", OpCode),

            };
            DataSet ds = DBHelper.ExecuteQuery("CartListDetails", para);
            return ds;
        }



        public DataSet GetAssociateAddress()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Pk_AddressId",Pk_AddressId),
                                        new SqlParameter("@Fk_MemId", FK_MemId),
                                        new SqlParameter("@Token", Token),

            };
            DataSet ds = DBHelper.ExecuteQuery("GetAddressDetails", para);
            return ds;
        }
        public DataSet GetWalletAmount()
        {
            SqlParameter[] para = {
                                        
                                        new SqlParameter("@MemberId", FK_MemId),

            };
            DataSet ds = DBHelper.ExecuteQuery("Proc_GetWalletBalance", para);
            return ds;
        }
        public DataSet AssociateAddressDelete()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Pk_AddressId", Pk_AddressId),
                                        new SqlParameter("@Fk_MemId", FK_MemId),

            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteAddress", para);
            return ds;
        }




        public DataSet AssociateAddAddress(Reports report)
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Name", Name),
                                        new SqlParameter("@Fk_MemId", FK_MemId),
                                        new SqlParameter("@MobileNo", MobileNo),
                                        new SqlParameter("@Landmark", Landmark),
                                        new SqlParameter("@Address", Address),
                                        new SqlParameter("@State", State),
                                        new SqlParameter("@City", City),
                                        new SqlParameter("@PinCode", PinCode),
                                        new SqlParameter("@AddressType", AddressType),
                                        new SqlParameter("@IsDefault",IsDefault),
                                        new SqlParameter("@Token",Token),

            };
            DataSet ds = DBHelper.ExecuteQuery("InsertAddress", para);
            return ds;
        }
        public DataSet AssociateAddressEdit(Reports report)
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Pk_AddressId", Pk_AddressId),
                                         new SqlParameter("@Name", Name),
                                        new SqlParameter("@Fk_MemId", FK_MemId),
                                        new SqlParameter("@MobileNo", MobileNo),
                                        new SqlParameter("@Landmark", Landmark),
                                        new SqlParameter("@Address", Address),
                                        new SqlParameter("@State", State),
                                        new SqlParameter("@City", City),
                                        new SqlParameter("@PinCode", PinCode),
                                        new SqlParameter("@AddressType", AddressType),
                                        new SqlParameter("@IsDefault",IsDefault ),
                                         new SqlParameter("@Token",Token)
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateAddress", para);
            return ds;
        }
        public DataSet GetShoppingOrderDetails()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@OpCode",4 ),
                                        new SqlParameter("@LoginId",LoginId),
                                        new SqlParameter("@FK_MemId",FK_MemId),
                                        new SqlParameter("@OrderNo",OrderNo),
                                        new SqlParameter("@Page",Page),
                                        new SqlParameter("@Size",Size),


            };
            DataSet ds = DBHelper.ExecuteQuery("PlaceShoppingOrder", para);
            return ds;

        }
        public DataSet GetCancelOrderDetails()
        {
            SqlParameter[] para = {                                      
                                        new SqlParameter("@LoginId",LoginId),
                                        new SqlParameter("@FK_MemId",FK_MemId),
                                        new SqlParameter("@OrderNo",OrderNo),
                                        new SqlParameter("@Page",Page),
                                        new SqlParameter("@Size",Size),
            };
            DataSet ds = DBHelper.ExecuteQuery("CancelOrderDetails", para);
            return ds;

        }
        public DataSet GetShoppingOrderDetailsForOrderNo()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@OpCode",5 ),
                                        new SqlParameter("@ShippingId",ShippingId),
                                        new SqlParameter("@OrderNo",OrderNo),


            };
            DataSet ds = DBHelper.ExecuteQuery("PlaceShoppingOrder", para);
            return ds;

        }
        public DataSet CancelShoppingOrder()
        {
            SqlParameter[] para = {

                                        new SqlParameter("@CancelledBy",FK_MemId),
                                        new SqlParameter("@OrderNo",OrderNo),
                                        new SqlParameter("@RemarkCancel",RemarkCancel),

            };
            DataSet ds = DBHelper.ExecuteQuery("CancelShoppingOrder", para);
            return ds;

        }
        public DataSet PlaceOrderForWeb()
        {

            SqlParameter[] para = {
                                        new SqlParameter("@FK_MemId", FK_MemId),
                                        new SqlParameter("@FK_AddressId",Pk_AddressId),
                                        new SqlParameter("@dtPaymentDetails", dtPaymentDetails),
                                        new SqlParameter("@paymentId", paymentId),
                                        new SqlParameter("@Token", Token)

            };
            DataSet ds = DBHelper.ExecuteQuery("PlaceShoppingOrderForWeb", para);
            return ds;
        }
        public DataSet GetPremiumClubMembers()
        {
            SqlParameter[] para = {
                                        
                                        new SqlParameter("@LoginId",LoginId),
                                        new SqlParameter("@Page",Page),
                                        new SqlParameter("@Size", Size),
                                        new SqlParameter("@IsExport", IsExport)

            };
            DataSet ds = DBHelper.ExecuteQuery("GetPremiumClubMembers",para);
            return ds;

        }
        public DataSet GetPremiumClubQualifier()
        {

            DataSet ds = DBHelper.ExecuteQuery("GetPremiumClubAchievers");
            
            return ds;

        }
        public DataSet GetConsolidatedGstReport( Reports reports)
            
        {
            SqlParameter[] para = {
                                        new SqlParameter("@FromDate",FromDate),
                                        new SqlParameter("@ToDate",ToDate),
                                        new SqlParameter("@Page",Page),
                                        new SqlParameter("@Size",Size),
                                        new SqlParameter("@IsExport",IsExport),
            };
            DataSet ds = DBHelper.ExecuteQuery("ConsolidatedGstReport", para);
           
            return ds;

        }

        public DataSet GetStatewiseGstReport(Reports reports)

        {
            SqlParameter[] para = {
                                        new SqlParameter("@FromDate",FromDate),
                                        new SqlParameter("@ToDate",ToDate), 
                                        new SqlParameter ("@Page" , Page),
                                        new SqlParameter("@Size" , Size),
                                        new SqlParameter("@IsExport" , IsExport),

            };

            DataSet ds = DBHelper.ExecuteQuery("StatewiseGstReport", para);
            return ds;

        }
        public DataSet GetInvoiceGSTReport(Reports reports)

        {
            SqlParameter[] para = {
                                        new SqlParameter("@FromDate",FromDate),
                                        new SqlParameter("@ToDate",ToDate),
                                        new SqlParameter("@Page",Page),
                                        new SqlParameter("@Size",Size),
                                        new SqlParameter("@IsExport",IsExport)
            };

            DataSet ds = DBHelper.ExecuteQuery("GetInvoiceGST", para);
            return ds;

        }

          public DataSet CancelInvoicRequest(Reports reports)

        {
            SqlParameter[] para = {
                                       new SqlParameter("@LoginId",LoginId),
                                        new SqlParameter("@FromDate",FromDate),
                                        new SqlParameter("@ToDate",ToDate),
                                        new SqlParameter("@InvoiceNo",InvoiceNo),
                                        new SqlParameter("@OrderNo",OrderNo),
                                        new SqlParameter("@Page" ,Page),
                                        new SqlParameter("@Size" ,Size),
                                        new SqlParameter("@IsExport" ,IsExport),

            };

            DataSet ds = DBHelper.ExecuteQuery("GetcancelInvoice", para);
            return ds;

        }
        public DataSet LifeTimeReward()

        {
            SqlParameter[] para = {
                                       new SqlParameter("@LoginId",LoginId),
                                       new SqlParameter("@Page",Page),
                                       new SqlParameter("@Size",Size),
                                       new SqlParameter("@IsExport",IsExport),
                                       

            };
                 
            DataSet ds = DBHelper.ExecuteQuery("GetLifeTimeReward", para);
            return ds;

        }
        public DataSet GetFranchisePayoutReport()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@LoginID",LoginId),
                                    new SqlParameter("@IsPublished",IsPublished),
                                    new SqlParameter("@PayoutNo",PayoutNo),
                                    new SqlParameter("@FromDate",FromDate),
                                    new SqlParameter("@ToDate",ToDate),
                                    new SqlParameter("@Page",Page),
                                    new SqlParameter("@Size",Size),
                                    new SqlParameter("@IsExport",IsExport),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetFranchisePayout", para);
            return ds;
        }

        public List<Reports> GetFranchsisePayoutDetails(Reports reports)
        {
            try
            {

                string _qury = string.Empty;
                List<Reports> _iresult = null;
                var queryParameters = new DynamicParameters();


                queryParameters.Add("@FK_FranchiseId", reports.FranchiseId);
                queryParameters.Add("@PayoutNo", reports.PayoutNo);

                _iresult = DBHelperDapper.DAAddAndReturnModelList<Reports>("FranchsisePayoutDetails", queryParameters);



                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet UpdateFranchisePayout()
        {
            SqlParameter[] para = {
                                     new SqlParameter("@PK_PaidId",PK_PaidId),
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateFranchisePayout", para);
            return ds;
        }


        public DataSet PackageMaster()

        {
            SqlParameter[] para = {
                                  new SqlParameter("@Pk_PackageId", Pk_PackageID),
                                  new SqlParameter("@OpCode", OpCode),
                                  new SqlParameter("@PackageName",PackageName),
                                  new SqlParameter("@PackageAmount",PackageAmount),
                                  new SqlParameter("@Price",Price),
                                  new SqlParameter("@CGST",CGST),
                                  new SqlParameter("@SGST",SGST),
                                  new SqlParameter("@IGST",IGST),
                                  new SqlParameter("@BP",BP),
                                  new SqlParameter("@DP",DP),
                                  new SqlParameter("@Rate",Rate),
                                  new SqlParameter("@CappingPoints",CappingPoints),
                                  new SqlParameter("@ShoppingAmount","0"),
                                  new SqlParameter("@IsFixed",IsFixed),
                                  new SqlParameter("@IsDiscount",IsDiscount),
                                  new SqlParameter("@Percentage",Percentage),
                                  new SqlParameter("@MinBV",string.IsNullOrEmpty(MinBv)?"0":MinBv),
                                  new SqlParameter("@AddedBy",AddedBy),

            };

            DataSet ds = DBHelper.ExecuteQuery("ManagePackageMaster", para);
            return ds;

        }
        public DataSet GetPayPayoutReport()
        {
            SqlParameter[] para = {
                                     new SqlParameter("@LoginID",LoginId),
                                    
                                    new SqlParameter("@PayoutNo",PayoutNo),
                                    new SqlParameter("@FromDate",FromDate),
                                    new SqlParameter("@ToDate",ToDate),
                                    new SqlParameter("@Page",Page),
                                    new SqlParameter("@Size",Size),
                                    new SqlParameter("@IsExport",IsExport),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetPayPayoutReport", para);
            return ds;
        }
      
    
        public  int UpdateFranchisePayPayout(Reports reports)
        {
            try
            {

                string _qury = string.Empty;
                
                var queryParameters = new DynamicParameters();


                queryParameters.Add("@PK_PaidId", reports.PK_PaidId);
                queryParameters.Add("@PaymentMode", reports.PaymentMode);
                queryParameters.Add("@ChequeNo", reports.ChequeNo);
                queryParameters.Add("@ChaqueDate", reports.ChaqueDate);
                queryParameters.Add("@BankName", reports.BankName);
                queryParameters.Add("@BranchName", reports.BranchName);
                int _iresult = DBHelperDapper.DAAdd("UpdateFranchisePayPayout", queryParameters);
                



                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetRecognition()
        {
            SqlParameter[] para = {
                                     new SqlParameter("@Fk_MemId",FK_MemId),

                                    
            };
            DataSet ds = DBHelper.ExecuteQuery("GetRecognition", para);
            return ds;
        }
        public DataSet BindRecognition()
        {
            
            DataSet ds = DBHelper.ExecuteQuery("BindRecognition");
            return ds;
        }
        public DataSet GetMembersRecognition()
        {
            SqlParameter[] para = {
                                     new SqlParameter("@LoginId",LoginId),
                                     new SqlParameter("@FK_RcogId",FK_RcogId),
                                     new SqlParameter("@FromDate",FromDate),
                                     new SqlParameter("@ToDate",ToDate),
                                     new SqlParameter("@Page",Page),
                                     new SqlParameter("@Size",Size),
                                     new SqlParameter("@IsExport",IsExport),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetMembersRecognition",para);
            return ds;
        }
        public DataSet AllGetShoppingOrderDataDetails()
        {
            SqlParameter[] para = {
                                     new SqlParameter("@Opcode",OpCode),
                                     new SqlParameter("@Pk_orderId",Pk_OrderId),
                                     new SqlParameter("@OrderNo",OrderNo),
                                     new SqlParameter("@FromDate",FromDate),
                                     new SqlParameter("@ToDate",ToDate),
                                     new SqlParameter("@LoginId",LoginId),



            };
            DataSet ds = DBHelper.ExecuteQuery("GetOrdersList", para);
            return ds;
        }
        public DataSet ShoppingOrderInvoice(string Pk_OrderId)
        {
            SqlParameter[] para = {
                                    
                                     new SqlParameter("@Pk_orderId",Pk_OrderId),
            };
            DataSet ds = DBHelper.ExecuteQuery("ShoppingOrdersDetailsInvoice", para);
            return ds;
        }
        public DataSet UpdateDeliveryDetaails()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@OpCode",6 ),
                                        new SqlParameter("@ShippingId",ShippingId),
                                        new SqlParameter("@OrderId",FK_OrderId),
                                        new SqlParameter("@OrderNo",OrderNo),


            };
            DataSet ds = DBHelper.ExecuteQuery("PlaceShoppingOrder", para);
            return ds;

        }
        public DataSet GetOrderitemDetails()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@OpCode",OpCode ),
                                        new SqlParameter("@FK_OrderId",FK_OrderId)


            };
            DataSet ds = DBHelper.ExecuteQuery("PlaceShoppingOrder", para);
            return ds;

        }

        public static string HITAPIAuthorization(string APIurl)
        {
            var result = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(APIurl);
            httpWebRequest.ContentType = "application/json";

            httpWebRequest.Method = "POST";
            //httpWebRequest.Headers.Add("Authorization", "Bearer " + "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOjE2NTU3NjksImlzcyI6Imh0dHBzOi8vYXBpdjIuc2hpcHJvY2tldC5pbi92MS9leHRlcm5hbC9hdXRoL2xvZ2luIiwiaWF0IjoxNjYzNTgwMTIwLCJleHAiOjE2NjQ0NDQxMjAsIm5iZiI6MTY2MzU4MDEyMCwianRpIjoiVG5HSXFQd1AyeVhvdXFUSCJ9.6YhkOjZlaO_xrQzDgCFjRQXrI1cIEPo1mftXHYZur4E");
            using (var streamWriter = new

            StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    email = "LifeoneWellness@gmail.com",
                    password = "LifeoneWellnessadmin@108"
                  

                });

                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }
        public static string HITAPIForCreateOrder(string APIurl, string order_id,string order_date,string billing_customer_name,string billing_last_name,string billing_address,string billing_address_2,string billing_city,string billing_pincode,string billing_state,string billing_country,string billing_email,string billing_phone,List<OrderItems> order_items,string sub_total,string length,string breadth,string height,string weight,string Bearer)
        {
            var result = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(APIurl);
            httpWebRequest.ContentType = "application/json";
            
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Authorization", "Bearer " + Bearer);
            using (var streamWriter = new

            StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    order_id = order_id,
                    order_date= order_date,
                    pickup_location= "Primary",
                    billing_customer_name= billing_customer_name,
                    billing_last_name= billing_last_name,
                    billing_address= billing_address,
                    billing_address_2= billing_address_2,
                    billing_city= billing_city,
                    billing_pincode= billing_pincode,
                    billing_state= billing_state,
                    billing_country= billing_country,
                    billing_email= billing_email,
                    billing_phone= billing_phone,
                    shipping_is_billing=true,
                    order_items= order_items,
                    payment_method="Prepaid",
                    sub_total= sub_total,
                    length = length,
                    breadth=breadth,
                    height=height,
                    weight=weight

                });

                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }


        public static string HITAPIAUcthenication(string APIurl, string email, string password)
        {
            var result = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(APIurl);
            httpWebRequest.ContentType = "application/json";

            httpWebRequest.Method = "POST";
           
            using (var streamWriter = new

            StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    email = email,
                    password = password
                   

                });

                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }

        public DataSet GetOpenOrderList()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@OpCode",OpCode ),
                                        new SqlParameter("@Pk_OrderId",Pk_OrderId ),
                                        new SqlParameter("@Page",Page ),
                                        new SqlParameter("@Size",Size ),
                                        new SqlParameter("@IsExport",IsExport),

            };
            DataSet ds = DBHelper.ExecuteQuery("GetOpenOrderList", para);
            return ds;

        }


        public DataSet GetTallyGSTReport()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@LoginId",LoginId),
                                    new SqlParameter("@InvoiceNo",InvoiceNo),
                                    new SqlParameter("@FromDate",FromDate),
                                    new SqlParameter("@ToDate",ToDate),
                                    new SqlParameter("@Page",Page),
                                    new SqlParameter("@Size",Size),
                                    new SqlParameter("@IsExport",IsExport)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetTallyGSTReport", para);
            return ds;
        }
        public DataSet GetActivationTallyGSTReport()
        {
            SqlParameter[] para = {

                                    new SqlParameter("@FromDate",FromDate),
                                    new SqlParameter("@ToDate",ToDate),
                                    new SqlParameter("@InvoiceNo",InvoiceNo),
                                    new SqlParameter("@Page",Page),
                                    new SqlParameter("@Size",Size),
                                    new SqlParameter("@IsExport",IsExport)
            };
            DataSet ds = DBHelper.ExecuteQuery("ActivationTallyGSTReport", para);
            return ds;
        }
        public DataSet GetFranchiseeKYCDetails(VerifyPanOrVoterid verifyPanOrVoterid)
        {
            SqlParameter[] para ={
                new SqlParameter("@LoginId",verifyPanOrVoterid.LoginId),
                new SqlParameter("@Mobile",verifyPanOrVoterid.Mobile),
                new SqlParameter("@PanCard",verifyPanOrVoterid.PanCard),
                new SqlParameter("@Type",verifyPanOrVoterid.Type),
                new SqlParameter("@Fk_MemId",verifyPanOrVoterid.Fk_MemId),
                new SqlParameter("@Page",verifyPanOrVoterid.Page),
                new SqlParameter("@Size",SessionManager.Size),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetFranchiseeKYCDetails",para);
            return ds;
        }
        public DataSet ApproveDeclineFranchiseeKYC(VerifyPanOrVoterid verifyPanOrVoterid)
        {
            SqlParameter[] para ={
                new SqlParameter("@IsPanApproved",verifyPanOrVoterid.IsPanApproved),
                new SqlParameter("@IsAadharApproved",verifyPanOrVoterid.IsAadharApproved),
                new SqlParameter("@IsAadharBackApproved",verifyPanOrVoterid.IsAadharBackApproved),
                new SqlParameter("@IsBankApproved",verifyPanOrVoterid.IsBankApproved),
                new SqlParameter("@Fk_MemId",verifyPanOrVoterid.Fk_MemId),
                new SqlParameter("@Status",verifyPanOrVoterid.Status),
                new SqlParameter("@Remark",verifyPanOrVoterid.Remark),
                new SqlParameter("@AddedBy",verifyPanOrVoterid.AddedBy),
            };
            DataSet ds = DBHelper.ExecuteQuery("ApproveDeclineFranchiseeKYC",para);
            return ds;
        }
        public DataSet UploadFranchiseeKYC(MAdminUploadKyc model)
        {
            SqlParameter[] para ={
                 new SqlParameter("@Fk_MemId",model.Fk_MemId),    
                 new SqlParameter("@PanCard", model.PanCardNo),
                 new SqlParameter("@PanImage", model.PanUrl),
                 new SqlParameter("@AadharNumber", model.AddressProofNo),
                 new SqlParameter("@AadharImage", model.AddressFrontUrl),
                 new SqlParameter("@AadharBackImage", model.AddressBackUrl),
                 new SqlParameter("@AccountNumber", model.BankACNo),
                 new SqlParameter("@BankImage",model.BankUrl),
                 new SqlParameter("@BankName",model.BankName),
                 new SqlParameter("@AccountHolder",model.AccountHolder),
                 new SqlParameter("@IfscCode",model.IfscCode),
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateFranchiseKYC",para);
            return ds;
        }
        public DataSet GetPurchaseOrders()
        {
            SqlParameter[] para ={
                 new SqlParameter("@OpCode",OpCode),    
                 new SqlParameter("@Fk_PurchaseOrderId",Fk_PurchaseOrderId),    
                 new SqlParameter("@OrderNo",OrderNo), 
                 new SqlParameter("@Pk_SupplierId",Pk_SupplierId=="0"?null:Pk_SupplierId),    
                 new SqlParameter("@Page",Page),    
                 new SqlParameter("@Size",Size),    
                 new SqlParameter("@IsExport",IsExport)  
            };
            DataSet ds = DBHelper.ExecuteQuery("GetPurchaseOrders", para);
            return ds;
        }

        public DataSet GetPurchaseOrderList()
        {
            SqlParameter[] para = {

                                    new SqlParameter("@Pk_Id",FK_OrderId),
                                   // new SqlParameter("@ToDate",ToDate),
                                    new SqlParameter("@Page",Page),
                                    new SqlParameter("@Size",Size),
                                    new SqlParameter("@IsExport",IsExport)
            };
            DataSet ds = DBHelper.ExecuteQuery("PurchaseOrderList", para);
            return ds;
        }
        public DataSet AssociateIdCard()
        {
            SqlParameter[] para = {
                new SqlParameter("@Fk_MemId",FK_MemId),
               
                
            };
            DataSet ds = DBHelper.ExecuteQuery("GetAssociateIdCard", para);
            return ds;
        }

        public DataSet Setrefundstock()
        {
            SqlParameter[] para = new SqlParameter[]
                {
                new SqlParameter("@LoginId",LoginId),
                new SqlParameter("@Current_stock",Current_stock),
                new SqlParameter("@Fk_ProductId",Fk_ProductId), 
                new SqlParameter("@Refund_stock",Refund_stock),
                new SqlParameter("@addedBy",AddedBy)
                };
            DataSet ds = DBHelper.ExecuteQuery("Sp_refundStock", para);
            return ds;

        }
        public DataSet ADDTempPackageProduct()
        {
            SqlParameter[] para = new SqlParameter[]
                {
                new SqlParameter("@Fk_ProductId",Pk_ProductId),
                new SqlParameter("@Fk_PackageId",Pk_PackageID),
                new SqlParameter("@MRP",ProductMRP),
                new SqlParameter("@BV",ProductBV),
                new SqlParameter("@DP",ProductDP),
                new SqlParameter("@Qty",ProductQTY),
                new SqlParameter("@Createdby",AddedBy)
                };
            DataSet ds = DBHelper.ExecuteQuery("AddTempPackageProduct", para);
            return ds;

        }
        public DataSet GetTempPackageProduct()
        {
            SqlParameter[] para = new SqlParameter[]
                {
              
                new SqlParameter("@Createdby",AddedBy)
                };
            DataSet ds = DBHelper.ExecuteQuery("GetTempPackageProduct", para);
            return ds;

        }
        public DataSet DeleteTempPackageProduct()
        {
            SqlParameter[] para = new SqlParameter[]
                {
              
                new SqlParameter("@Pk_PackageProductId",Pk_PackageProductId)
                };
            DataSet ds = DBHelper.ExecuteQuery("DeleteTempPackageProduct", para);
            return ds;

        }

        public DataSet DeleteTempPackageProductById()
        {
            SqlParameter[] para = new SqlParameter[]
                {

                new SqlParameter("@CreatedBy",AddedBy)
                };
            DataSet ds = DBHelper.ExecuteQuery("DeleteTempPackageProductById", para);
            return ds;

        }
        public DataSet EditTempPackageProduct()
        {
            SqlParameter[] para = new SqlParameter[]
                {

                new SqlParameter("@Createdby",AddedBy),
                new SqlParameter("@Pk_PackageId",Pk_PackageID),
                };
            DataSet ds = DBHelper.ExecuteQuery("EditTempPackageProduct", para);
            return ds;

        }
        public DataSet GetPackageProduct()
        {
            SqlParameter[] para = new SqlParameter[]
                {

                new SqlParameter("@Pk_PackageId",Pk_PackageID),
                new SqlParameter("@Createdby",AddedBy)
                };
            DataSet ds = DBHelper.ExecuteQuery("GetPackageProducts", para);
            return ds;

        }

        public DataSet GetGeustShoppingOrderDetails()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@OpCode",8),
                                        new SqlParameter("@LoginId",LoginId),
                                        new SqlParameter("@OrderNo",OrderNo),
                                        new SqlParameter("@Page",Page),
                                        new SqlParameter("@Size",Size),
            };
            DataSet ds = DBHelper.ExecuteQuery("PlaceShoppingOrder", para);
            return ds;

        }

        public DataSet PackageMaster_Combo()
        {
            SqlParameter[] para = {
            new SqlParameter("@Pk_PackageID", Pk_PackageID),
            new SqlParameter("@PackageName", PackageName),
            new SqlParameter("@PackageAmount", PackageAmount),
            new SqlParameter("@Price", Price),
            new SqlParameter("@CGST", CGST),
            new SqlParameter("@SGST", SGST),
            new SqlParameter("@IGST", IGST),
            new SqlParameter("@BP", BP),
            new SqlParameter("@DP", DP),
            new SqlParameter("@Rate", Rate),
            new SqlParameter("@CappingPoints", CappingPoints),
            new SqlParameter("@MinBv", MinBv),
            new SqlParameter("@AddedBy", AddedBy),
            new SqlParameter("@Opcode", OpCode)
            };
            DataSet ds = DBHelper.ExecuteQuery("ManagePackageMaster_Combo", para);
            return ds;
        }




    }

    public class AdminStock: MPaging
    {
        public string LoginId { get; set; }
        public long Pk_ProductId { get; set; }
        public string PersonName { get; set; }
        public string ProductName { get; set; }
        public string Searchby { get; set; }
        public string RecentOrderStatus { get; set; }
        public string OrderStatus { get; set; }
        public int CreditQuantity { get; set; }
        public int DebitQuantity { get; set; }
        public int Balance { get; set; }
        public int FranchiseId { get; set; }
        public bool Checked { get; set; }

        public List<AdminStock> LstOrder { get;  set; }
        public static AdminStock GetFranchiseStockForAdmin(int? Page, AdminStock _data)
        {

            if (_data.Page == null || _data.Page == 0)
            {
                _data.Page = 1;
            }
            _data.Size = SessionManager.Size;

            _data.LstOrder = DALReports.GetFranchiseStockForAdmin(_data);
            int totalRecords = 0;
            if (_data.LstOrder.Count > 0)
            {
                totalRecords = Convert.ToInt32(_data.LstOrder[0].TotalRecords);
                var pager = new Pager(totalRecords, _data.Page, _data.Size);
                _data.Pager = pager;
            }
            else
            {
                _data.LstOrder = new List<AdminStock>();
            }


            return _data;
        }
    }
    public class OrderItems
    {
        public string name { get; set; }
        public string sku { get; set; }
        public string units { get; set; }
        public string selling_price { get; set; }
        public string discount { get; set; }
        public string tax { get; set; }
        public string hsn { get; set; }
    }
    public class ShippingResponse
    {
        public string order_id { get; set; }
        public string shipment_id { get; set; }
        public string status { get; set; }
        public int status_code { get; set; }
        public int onboarding_completed_now { get; set; }
        public string awb_code { get; set; }
        public string courier_company_id { get; set; }
        public string courier_name { get; set; }
    }
    public class AuthorizationResponse
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public int company_id { get; set; }
        public string created_at { get; set; }
        public string token { get; set; }
    }

}