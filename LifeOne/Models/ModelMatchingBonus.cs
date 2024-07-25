using DocumentFormat.OpenXml.Wordprocessing;
using LifeOne.Models.API.DAL;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.WebParts;

namespace LifeOne.Models
{
    public class ModelMatchingBonus
    {
        public string PK_PayoutID { get; set; }
        public string FK_MemID { get; set; }
        [Required]
        [Display(Name = "Login ID")]
        public string LoginID { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Direct Income")]

        public string DirectBonus { get; set; }
        public string MatchingBonus { get; set; }
        public string Royalty1 { get; set; }
        public string Royalty2 { get; set; }
        public string RClubA { get; set; }
        public string RClubB { get; set; }
        public decimal StarBonus { get; set; }
        public string ProductBonus { get; set; }

        public string CashBack { get; set; }
        [Display(Name = "Gross Amount")]
        public string GrossAmount { get; set; }
        [Display(Name = "TDS")]
        public string TDSAmount { get; set; }
        [Display(Name = "Processing Charges")]
        public string ProcessingCharges { get; set; }
        [Display(Name = "Net Amount")]
        public string NetAmount { get; set; }
        [Display(Name = "Payout No")]
        public string PayoutNo { get; set; }
        [Display(Name = "From Date")]
        public string FromDate { get; set; }
        [Display(Name = "To Date")]
        public string ToDate { get; set; }
        [Display(Name = "Closing Date")]
        public string ClosingDate { get; set; }

        // Payout Transcation Details
        public string BusinessAmount { get; set; }
        public string CommissionPercentage { get; set; }
        public string Amount { get; set; }
        public string IncomeType { get; set; }
        public string CurrentDate { get; set; }
        public string PaymentMode { get; set; }
        public string ChequeNo { get; set; }
        public string DisplayName { get; set; }
        public string RequestedAmount { get; set; }
        public string ProcessingFee { get; set; }
        public string MemberAccNo { get; set; }
        public string MemberAccNo1 { get; set; }
        public string IFSCCode { get; set; }
        public string MemberBankName { get; set; }
        public string MemberBranch { get; set; }
        public string RequestedDate { get; set; }
        public string ApprovalStatus { get; set; }
        public string PaidStatus { get; set; }
        public string PK_RequestId { get; set; }
        //public string MatchingIncome { get; set; }

        public string FromName { get; set; }
        public string ToMemberName { get; set; }
        public string FromLoginId { get; set; }
        public string ToLoginId { get; set; }
        public int PlanId { get; set; }
        public string AdjustedAmount { get; set; }
        public string Advance { get; set; }
        public decimal PointRate { get; set; }
        public decimal MatchingPoint { get; set; }
        public decimal SelfBusiness { get; set; }
        public decimal PerformanceBonus { get; set; }
        public decimal PreviousLeft { get; set; }
        public decimal PreviousRight { get; set; }
        public decimal CurrentLeft { get; set; }
        public decimal CurrentRight { get; set; }
        public decimal BalanceLeft { get; set; }
        public decimal BalanceRight { get; set; }
        public List<ModelMatchingBonus> listMatchingBonus { get; set; }

        public DataSet GetPerformanceBonus()
        {
            SqlParameter[] para = new SqlParameter[] {
            
            new SqlParameter("@fk_memid", FK_MemID),
            new SqlParameter("@PayoutNo", PayoutNo)
            };

            DataSet ds = DBHelper.ExecuteQuery("get_payout", para);
            return ds;

        }
        public DataSet GetMachingBonus()
        {
            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@fk_memid", FK_MemID),
            new SqlParameter("@PayoutNo", PayoutNo)
            };

            DataSet ds = DBHelper.ExecuteQuery("get_payout", para);
            return ds;

        }

        
    }
} 