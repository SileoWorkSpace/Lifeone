using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MAdminDashbord:Menu
    {
        public DataTable dtPaidPackage { get; set; }
        public DataTable dtFreePackage { get; set; }
        public int TotalAssociate { get; set; }
        public int TotalUID { get; set; }
        public int TotalBID { get; set; }
        public int PackageCount { get; set; }
        public int FranchiseOrderCount { get; set; }
        public int RepurchaseCount { get; set; }
        public int TodayRepurchaseCount { get; set; }
        public ActiveIncative ActiveIncative { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public RepurchaseStatus RepurchaseStatus { get; set; }
        public BonusStatus BonusStatus { get; set; }
        public FranchiseOrderStatus FranchiseOrderStatus { get; set; }

        public int TodaysJoining { get; set; }
        public decimal TodaysConsumption { get; set; }
        public decimal PayAddaBalance { get; set; }
        public List<MtotalRechargeAndAmountCreated> MtotalRechargeAndAmountCreated { get; set; }

        public ManageWalletPayment ManageWalletPayment { get; set; }
        



    }


    public class ActiveIncative
    {
        public int ActiveAssociate { get; set; }
        public int InActiveAssociate { get; set; }
    }
    public class OrderStatus
    {
        public int TotalPkg { get; set; }
        public int ActivePkg { get; set; }
        public int PendingPkg { get; set; }
    }
    public class FranchiseOrderStatus
    {
        public int ActiveOrder { get; set; }
        public int PendingOrder { get; set; }
        public int CancelOrder { get; set; }
        public int DispatchOrder { get; set; }
        public int DiliveredOrder { get; set; }
    }
    public class RepurchaseStatus
    {
        public int ActiveOrder { get; set; }
        public int PendingOrder { get; set; }
        public int CancelOrder { get; set; }
        public int DispatchOrder { get; set; }
        public int DiliveredOrder { get; set; }
    }

    


    public class BonusStatus
    {
        public int BonusFresh { get; set; }
        public int BonusRepurchase { get; set; }
        public int LevelBonus { get; set; }
        public decimal SelfIncome { get; set; }
        public decimal TeamIncome { get; set; }
        public decimal AirOrbitIncome { get; set; }
        public decimal WaterOrbitIncome { get; set; }
        public decimal SpaceOrbitIncome { get; set; }
        public decimal SmartRoyaltyIncome { get; set; }
        public decimal SmartOrbitRoyaltyIncome { get; set; }
        public decimal SmartLeadershipIncome { get; set; }
        public decimal GlobalAchieverClubBonus { get; set; }
        public int TodayRepurchaseCount { get; set; }
    }
    public class AdminDashboardChart
    {
        public string JoiningDate { get; set; }
        public long totalMembers { get; set; }
    }
    public class MemberChart
    {
        public string JoiningDate { get; set; }
        public string totalMembers { get; set; }
    }
    public class FranchiseAmountViewModel
    {
        public decimal? ProductStockAmt { get; set; }
        public decimal? ProductStockBv { get; set; }
        public decimal? AssociatSaleAmt { get; set; }
        public decimal? AssociateSaleBv { get; set; }
        public decimal? PendingOrderActivation { get; set; }
        public decimal? PendingRecivepo { get; set; }
        public decimal? WalletAmt { get; set; }
        public decimal? UnpaidIncome { get; set; }
        public decimal? PaidIncome { get; set; }
    }

    public class FranchiseOrderViewModel
    {
        public long TotalOrder { get; set; }
        public long ActiveOrder { get; set; }
        public long PendingOrder { get; set; }
    }
    public class FranchisePaidViewModel
    {
        public string InvoiceNo { get; set; }
        public string Name { get; set; }
        public decimal? PaidAmt { get; set; }
        public decimal? TotalPv { get; set; }
        public string Status { get; set; }

    }

    public class FranchiseDashboardViewModel
    {
        public FranchiseAmountViewModel FranchiseAmounts { get; set; }
        public FranchiseOrderViewModel FranchiseOrders { get; set; }
        public List<FranchisePaidViewModel> FranchisePaidList { get; set; }
    }

    public class MtotalRechargeAndAmountCreated
    {
        public string ActionType { get; set; }
        public decimal TotalAmount { get; set; }
        
    }


    public class ManageWalletPayment
    {
        public decimal CreditAmount { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal Balance { get; set; }
        public decimal RefundAmount { get; set; }
    }


    public class WeeklyMemberKYCModel
    {
        public string date { get; set; }
        public decimal New { get; set; }
        public decimal Kyc { get; set; }
    }
}