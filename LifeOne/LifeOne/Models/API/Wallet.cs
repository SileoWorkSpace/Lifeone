
using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
namespace LifeOne.Models.API
{
    public class Wallet
    {
        public decimal CashbackWalletAmount { get; set; }
        //public decimal DreamyWalletAmount { get; set; }
        public decimal WalletAmount { get; set; }
        public decimal CommisionWalletAmount { get; set; }
        public bool IsAllowCommission { get; set; }

        public string CashBackLastUpdatedDate { get; set; }
        public string WalletLastUpdatedDate { get; set; }
        public string HoldAmountLastUpdatedDAte { get; set; }
        public string UnClearAmountLastUpdatedDAte { get; set; }
        public string CommisionWalletUPdatedDate { get; set; }
        public decimal HoldAmount { get; set; }

        public decimal UnClearAmount { get; set; }


        public int? Fk_FranchiseId { get; set; }
        



    }

    public class FranchiseWallet
    {   
        public decimal WalletAmount { get; set; }     
        public int Fk_FranchiseId { get; set; }

        public string WalletLastUpdatedDate { get; set; }

        public bool IspaymentApprove { get; set; }
        

    }


    public class WalletJsonResponse : CommonJsonReponse
    {
        public Wallet result { get; set; }
    }


    public class FranchiseWalletJsonResponse
    {
        public string message { get; set; }
        public string response { get; set; }

        public FranchiseWallet result { get; set; }
    }


    public class WalletJsonRequest
    {
         public long PK_FranchiseId { get; set; }
        public string paymentdate { get; set; }
        public string paymentmode { get; set; }
        public string transactionId { get; set; }
        public decimal amount { get; set; }
        public string filepath { get; set; }
        public long memberId { get; set; }
        public string remark { get; set; }
        public long FranchiseId { get; set; }
        public int Pk_CompanionId { get; set; }


    }
    public class WalletJsonLedgerReponse : CommonJsonReponse
    {
        public List<WalletJsonRequest> result { get; set; }

    }
    public class WalletJsonCommonRequest
    {
        public long memberId { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public int page { get; set; }
        public int size { get; set; }
        public string walletType { get; set; }

    }
    public class WalletActionRequest
    {
        public string type { get; set; }
        public string transactiondate { get; set; }
        public string transactionId { get; set; }
        public decimal mainamount { get; set; }
        public decimal walletamount { get; set; }
        public decimal paymentgatewayamount { get; set; }
        public long memberId { get; set; }
    }


    public class WalletResponse
    {
        public string actionType { get; set; }
        public decimal runningAmount { get; set; }
        public string paymentdate { get; set; }
        public string transactionId { get; set; }
        public decimal drAmount { get; set; }
        public decimal crAmount { get; set; }
        public string transactionStatus { get; set; }
        public string remarks { get; set; }
        public decimal amount { get; set; }

    }
    public class WalletJsonReponse : CommonJsonReponse
    {
        public List<WalletResponse> result { get; set; }

    }
    public class AmountTransfer : DBHelper
    {
        public string fk_MemId { get; set; }
        public string Amount { get; set; }
        public string TransactionId { get; set; }
        public DataSet SaveCommissionAmountToWallet()
        {
            SqlParameter[] Para =
                {
                new SqlParameter("@Fk_MemId",fk_MemId),
                new SqlParameter("@TransferAmount",Amount),
                new SqlParameter("@TransactionId",TransactionId)
            };
            DataSet ds = DBHelper.ExecuteQuery("CommissionTransferToWallet", Para);
            return ds;
        }
    }
    public class AmountTransferResponse
    {
        public string response { get; set; }
        public string message { get; set; }
    }


    //Addded By Dainik Kumar

    public class CommisionRequest
    {
        public int Fk_MemId { get; set; }
        public string Type { get; set; }
    }
    public class CommissionDetailsResponse
    {
        public string response { get; set; }
        public decimal TotalCommision { get; set; }
        public string UpdatedOn { get; set; }
        public List<CommissionDetails> CommissionList { get; set; }
    }
    public class CommissionDetails
    {
        public string CommissionName { get; set; }
        public decimal CommissionAmount { get; set; }
        public List<SubCommissionDetails> SubCommissionList { get; set; }


    }
    public class SubCommissionDetails
    {
        public string SubCommissionName { get; set; }
        public decimal SubCommissionAmount { get; set; }
        public string IconUrl { get; set; }
        public decimal TotalPv { get; set; }
    }

    public class HoldUnclearBalanceResponse
    {
        public string response { get; set; }
        public decimal TotalHoldUnclear { get; set; }
        public string UpdatedOn { get; set; }
        public List<HoldUnclearBalanceDetails> lstHoldUnclear { get; set; }
    }
    public class HoldUnclearBalanceDetails
    {
        public string Title { get; set; }
        public string Reason { get; set; }
        public decimal Amount { get; set; }
        public string TransDate { get; set; }
    }
    public class AssociateDashoboardResponse
    {
        //public string Tittle { get; set; }
        //public string ImageUrl { get; set; }
        //public string response { get; set; }
        //public decimal CurrentPV { get; set; }
        //public decimal TargetPV { get; set; }
        //public decimal SelfBusiness { get; set; }
        //public decimal CurrentMonthSelfBusiness { get; set; }
        //public decimal TeamBusiness { get; set; }
        //public decimal CurrentMonthTeamBusiness { get; set; }
        //public int UIDs { get; set; }
        //public int CurrentMonthUIDs { get; set; }
        //public int BIDs { get; set; }
        //public int CurrentMonthBIDs { get; set; }


        public string response { get; set; }
        public List<DirectList> directLists { get;  set; }
        public List<IncomeDetails> IncomeList { get;  set; }
        public string Name { get;  set; }
        public int TotalAssociates { get;  set; }
        public int TotalInactive { get;  set; }
        public int TotalDirct { get;  set; }
        public decimal CurrentBusinessLeft { get;  set; }
        public decimal CurrentBusinessRight { get;  set; }
        public int TotalActive { get;  set; }
        public decimal TeamBusinessLeft { get;  set; }
        public decimal TeamBusinessRight { get;  set; }
        public decimal RepurchaseCurrentBusinessRight { get;  set; }
        public decimal RepurchaseCurrentBusinessLeft { get;  set; }
        public decimal TeamRepurchaseLeftBusiness { get;  set; }
        public decimal RepurchaseCarryforwordRight { get;  set; }
        public decimal TotalIncome { get;  set; }
        public decimal DeductionAmount { get;  set; }
        public decimal ShoppingWallet { get;  set; }
        public decimal UtilityWallet { get;  set; }
        public decimal CarryforwordLeft { get;  set; }
        public decimal CarryforwordRight { get;  set; }
        public decimal RepurchaseCarryforwordLeft { get; set; }
        public decimal TeamRepurchaseRightBusiness { get; set; }
    }
    public class UIDDashboard
    {
        public int TotalCustomer { get; set; }
        public decimal TotalShopping { get; set; }
        public int Bid { get; set; }
        public int UID { get; set; }
        public int CurrTotalCustomer { get; set; }
        public decimal CurrTotalShopping { get; set; }
        public int CurrBid { get; set; }
        public int CurrUID { get; set; }
        public string Name { get; set; }
        public string PerformanceImageurl { get; set; }
        public string PermonaceType { get; set; }
        public string response { get; set; }
        public List<DirectList> directLists { get; set; }
      
    }
    public class DirectList
    {
        public string Name { get; set; }
        public string LoginId { get; set; }
        public string JoiningDate { get; set; }
        public string ActiveStatus { get; set; }
        public string profilepic { get; set; }
    }
    public class IncomeDetails
    {
        public string Income { get; set; }
        public decimal Paid { get; set; }
        public decimal Unpaid { get; set; }
    }
    public class SalesDetails
    {
        public string MemberName { get; set; }
        public string LoginId { get; set; }
        public decimal TotalSales { get; set; }
    }
}