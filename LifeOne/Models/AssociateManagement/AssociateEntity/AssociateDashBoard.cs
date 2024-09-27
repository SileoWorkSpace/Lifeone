using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateEntity
{
    public class AssociateDashboardReq
    {
        public long Fk_MemId { get; set; }
    }
    public class AssociateDashBoard
    {
        public decimal TotalSelfBusiness { get; set; }
        public decimal TotalTeamBusiness { get; set; }
        public decimal CurrSelfBusiness { get; set; }
        public decimal CurrTeamBusiness { get; set; }
        public decimal CurrentUIdBusiness { get; set; }
        public int TotalBId { get; set; }
        public int TotalUID { get; set; }
        public int DirectBID { get; set; }
        public int DirectUID { get; set; }
        public decimal CurrentPV { get; set; }
        public decimal TargetPV { get; set; }
        public decimal TargetPercentage { get; set; }
        public decimal UIDBusiness { get; set; }
        public decimal QPV { get; set; }
        public string PerformanceImageurl { get; set; }
        public string PermonaceType { get; set; }
        public string targetLevel { get; set; }
        public decimal GPV { get; set; }
        public decimal TeamQPV { get; set; }
        public decimal CurrSQPV { get; set; }
        public decimal CurrGPV { get; set; }
        public decimal CurrUQPV { get; set; }
        public decimal BalancePV { get; set; }
        public decimal MaxLegBusiness { get; set; }
        public string MaxBusinessId { get; set; }
        public decimal AirOrbitPV { get; set; }
        public decimal WaterOrbitPV { get; set; }
        public decimal SpaceOrbitPV { get; set; }
        //new Field
        public decimal SelfBusiness { get; set; }
        public int TotalBid { get; set; }
        public int TotalUId { get; set; }
        public decimal TotalBusiness { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalProductSold { get; set; }
        public decimal BIDPercentage { get; set; }
        public decimal UiDPercentage { get; set; }
        public decimal BusinessPercentage { get; set; }
        public decimal IncomePercentage { get; set; }
        public decimal TeamBusiness { get; set; }
        public decimal TotalCustomerShoppingPV { get; set; }
        public decimal totalShoppingPV { get; set; }
        public decimal ShoppingIncurrMonthPV { get; set; }
        public decimal CustomerShopInINR { get; set; }
        public decimal TotalShoppingINR { get; set; }
        public decimal ShoppingCurrINR { get; set; }
        public string Revenue { get; set; }
        public string SalesProductWise { get; set; }
        public string SalesProductWiseData { get; set; }
        public int TotalCustomer { get; set; }
        public string sptext { get; set; }
        public decimal RestBusiness { get; set; }
        public List<NewsDetails> lstNews { get; set; }
        public List<AssociateDetails> lstAssociates { get; set; }
        public List<SalesData> lstsales { get; set; }
    }
    public class AssociateDetails
    {
        public string Name { get; set; }
        public string profilepic { get; set; }
        public string LoginId { get; set; }
        public string PermonaceType { get; set; }
        public string JoiningDate { get; set; }
        public string ActiveStatus { get; set; }
    }
    
    public class SalesData
    {
        public string ProductType { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class AssociateDashboardWeb
    {
        public string LoginId { get; set; }
        public string DisplayName { get; set; }
        public string Mobile { get; set; }
        public string SponsorName { get; set; }
        public string SponsorId { get; set; }
        public int TotalDirect { get; set; }
        public int TotalTeam { get; set; }
        public int TotalActive { get; set; }
        public string TopUpLevel { get; set; }
        public int TotalInactive { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TodaysIncome { get; set; }
        public decimal MatchingPoints { get; set; }
        public decimal CarryforwordLeft { get; set; }
        public decimal CarryforwordRight { get; set; }
        public decimal PaidLeg1 { get; set; }
        public decimal PaidLeg2 { get; set; }
        public decimal CurrentBusinessLeft { get; set; }
        public decimal CurrentBusinessRight { get; set; }
        public decimal LeftBusiness { get; set; }
        public decimal RightBusiness { get; set; }
        public decimal RepurchaseCarryforwordLeft { get; set; }
        public decimal RepurchaseCarryforwordRight { get; set; }
        public decimal RepurchasePaidLeg1 { get; set; }
        public decimal RepurchasePaidLeg2 { get; set; }
        public decimal RepurchaseCurrentBusinessLeft { get; set; }
        public decimal RepurchaseCurrentBusinessRight { get; set; }
        public decimal RepurchaseLeftBusiness { get; set; }
        public decimal RepurchaseRightBusiness { get; set; }
        public string MemberBankName { get; set; }
        public string MemberAccNo { get; set; }
        public string MemberBranch { get; set; }
        public string IFSCCode { get; set; }
        public string BankHolderName { get; set; }
        public string KycStatus { get; set; }
        public decimal PayoutWallet { get; set; }
        public decimal ShoppingWallet { get; set; }
        public decimal UtilityWallet { get; set; }
        public decimal EWallet { get; set; }
        public decimal DeductionAmount { get; set; }
        public decimal PreLeftBV { get; set; }
        public decimal PreRightBV { get; set; }
        public string ClosingDate { get; set; }
        public string ActiveStatus { get; set; }


        public List<AssociateDetails> lstAssociates { get; set; } 
        public List<PayoutStatement> payoutStatements { get; set; }
        public List<PayoutSummary> payoutSummaries { get; set; }
        public List<NewsDetails> lstNews { get; set; }
        public long FkMemId { get;  set; }
        public List<RewardsModel> Rewardslst { get;  set; }
    }

    public class PayoutStatement
    {
        public decimal GrossAmount { get; set; }
        public decimal TDSAmount { get; set; }
        public decimal ProcessingCharges { get; set; }
        public decimal NetAmount { get; set; }
        public string ClosingDate { get; set; }
    }

    public class PayoutSummary
    {
        public string Income { get; set; }
        public decimal Paid { get; set; }
        public string Unpaid { get; set; }
    }
    public class NewsDetails
    {
        public string NewsHeading { get; set; }
        public string News { get; set; }
        public string Date { get; set; }
    }
}