using LifeOne.Models.Common;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MPayoutReport : MPaging
    {
        public string LoginId { get; set; }


        public string DisplayName { get; set; }
        public string ClosingDate { get; set; }
        public string IncomeMonth { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal DirectIncome { get; set; }
        public decimal TeamBuildingBonus { get; set; }
        public decimal BinaryIncome { get; set; }
        public decimal SponsorRoyalty { get; set; }
        public decimal TDSAmount { get; set; }
        public decimal ProcessingFee { get; set; }
        public decimal NetAmount { get; set; }
        public int? PayoutNo { get; set; }
        public decimal LeadershipBonus { get; set; }
        public decimal MatchingBonus { get; set; }
        public decimal HybridMatchingBonus { get; set; }
        public decimal RepurchaseBonus { get; set; }
        public string PaidUnPaidStatus { get; set; }
        public decimal LDBRepurchase { get; set; }
        public decimal Advance { get; set; }
        public decimal AdjustedAmount { get; set; }
        public string BankName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int FK_MemId { get; set; }
        public string BankHolderName { get; set; }
        public string MemberAccNo { get; set; }
        public string IFSCCode { get; set; }
        public string MemberBankName { get; set; }
        public string MemberBranch { get; set; }
        public string statusIFSC { get; set; }
        public string IncomeType { get; set; }
        public string SearchParam { get; set; }
        public string Searchby { get; set; }
        public int IsExport { get; set; }
        public List<MPayoutReport> Objlist { get; set; }

        public DateTime? fdate { get; set; }
        public DateTime? tdate { get; set; }
        public string MemberLoginId { get; set; }


        public String Name { get; set; }
        public String FatherName { get; set; }
        public String MobileNo { get; set; }
        public string PayountNo { get; set; }
        public Decimal AdvanceAmt { get; set; }
        public Decimal AdvAdjustedAmt { get; set; }
        public Decimal GrossIncome { get; set; }
        public Decimal TDS { get; set; }
        public Decimal NetIncome { get; set; }
        public Boolean IsPaid { get; set; }
        public String PaidDate { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public string PaymentModeName { get; set; }
        public string PaymentModeNo { get; set; }
        public string PaymentModeDate { get; set; }
        public string PaymentModeId { get; set; }
        public string BranchName { get; set; }
        public int PaidMemberId { get; set; }
        public long UpdateBy { get; set; }
        public int Flag { get; set; }
        public string Remark { get; set; }
        public decimal PrevLeft { get; set; }
        public decimal PrevRight { get; set; }
        public decimal CurrLeft { get; set; }
        public decimal CurrRight { get; set; }
        public decimal BalLeft { get; set; }
        public decimal BalRight { get; set; }
        public decimal CarFund { get; set; }
        public decimal LDBBonus { get; set; }
        public decimal HouseFund { get; set; }
        public decimal DirectorClub { get; set; }
        public decimal ChairManClub { get; set; }
        public decimal LARBonus { get; set; }
        public decimal SGYearlyBonus { get; set; }
        public string FirstName { get; set; }
        public decimal BusinessAmount { get; set; }
        public decimal PerformanceBonus { get; set; }
        public decimal Amount { get; set; }
        public string Date { get; set; }
        public DataSet GetPayoutReport()
        {
            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@fk_memid", FK_MemId),
            new SqlParameter("@PayoutNo",PayoutNo)
            };

            DataSet ds = DBHelper.ExecuteQuery("get_payout", para);
            return ds;

        }


    }
    public class StatementDetails
    {
        public string LoginId { get; set; }
        public string DisplayName { get; set; }
        public decimal Amount { get; set; }
        public string IncomeType { get; set; }
        public decimal CommissionPercentage { get; set; }
        public string CurrentDate { get; set; }
        public string ClosingDate { get; set; }
        public int PayoutNo { get; set; }
        public decimal ToSlabPercent { get; set; }
        public string Remark { get; set; }
        public decimal BusinessAmount { get; set; }
        public int FK_FromId { get; set; }
        public int FK_ToId { get; set; }
        public int TotalRecords { get; set; }
        
    }
    public class StatementResponse : MPaging
    {
        public int Flag { get; set; }
        public string response { get; set; }
        public string Msg { get; set; }
        public int Fk_MemId { get; set; }
        public int PayoutNo { get; set; }
        public List<StatementDetails> lstStatement { get; set; }
    }

    public class PaymentModeListViewModel
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }

    public class FranchiseBusinessModel : MPaging
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string LoginId { get; set; }
        public string FranchiseName { get; set; }
        public string Contact { get; set; }
        public string City { get; set; }
        public string P_State { get; set; }
        public string FranchiseType { get; set; }
        public decimal BV { get; set; }
        public decimal Amount { get; set; }
        public int IsExport { get; set; }
        public List<FranchiseBusinessModel> Objlist { get; set; }
    }

}