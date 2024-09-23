using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateEntity
{
    public class RewardsModel
    {
        public decimal RewardAmount { get; set; }
        public decimal TargetPV { get; set; }
        public string TargetPoint { get; set; }
        public decimal LeftBusiness { get; set; }
        public decimal RightBusiness { get; set; }
        public decimal CurrentBusiness { get; set; }
        public decimal BalanceBusiness { get; set; }
        public string BalanceLeft { get; set; }
        public string BalanceRight { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Recognition { get; set; }
        public string PaymentStatus { get; set; }
        public string Status { get; set; }
        public string AchievedOn { get; set; }
        public string Rank { get; set; }
        public string Remarks { get; set; }
        public string Rcolor { get; set; }
        public string ReferBy { get; set; }
        public bool IsVisible { get; set; }
        public string Action { get; set; }
        public string Designation { get; set; }
        public long FkMemId { get; set; }
        public string RewardImage { get; set; }             
        public int FkSetRewardId { get; set; }
        public string RewardName { get; set; }
        public string ToursName { get; set; }
        public string ToursImage { get; set; }
        public decimal TargetAmount { get; set; }
        public int PK_RId { get; set; }
        public string Msg { get; set; }
        public string LoginId { get; set; }
        public string RewardImg { get; set; }
        public string ChequeDDBankName { get; set; }
        public string BankBranch { get; set; }
        public string ChequeDDNo { get; set; }
        public string ChequeDDDate { get; set; }
        public string PaidAmount { get; set; }
        public string PaymentModeName { get; set; }
        public List<RewardsModel> Rewardslst { get; set; }

    }

    public class AffiliatedUtilityShopping
    {
        public string LoginId { get; set; }
        public string DisplayName { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal DebitAmount { get; set; }
        public string TransactionNo { get; set; }
        public string TransactionDate { get; set; }
        public string Remarks { get; set; }
        public string ActionType { get; set; }
        public string OrderNo { get; set; }
        public string Operator { get; set; }
        public string Narration { get; set; }
        public long fk_MemId { get; set; }


    }

}