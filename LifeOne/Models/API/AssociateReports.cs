using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class AssociateReports
    {

    }
    public class IncetiveResponse
    {
        public string response { get; set; }
        public List<MyIncentiveDetails> lstIncetive { get; set; }
    }
    public class MyIncentiveDetails
    {
        public string Month { get; set; }
        public int Year { get; set; }
        public decimal CurrentAmount { get; set; }
        public decimal PrevBalance { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal CurrentPBV { get; set; }
    }


    public class PayoutRequestAPI
    {
        public long Fk_MemId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int PayoutNo { get; set; }
    }

    public class Genealogy
    {
        public string LoginId { get; set; }
        public string SearchLoginId { get; set; }

      
    }

    public class PayoutResponse
    {
        public string response { get; set; }
        public List<PayoutDetails> lstPayout { get; set; }

    }
    public class GenealogyResponse
    {

        public string Response { get; set; }

        public List<GenealogyTree> Genealogytreelist { get; set; }
    }
    public class PayoutMasterListResponse
    {

        public string response { get; set; }

        public List<PayoutMasterList> payoutMasterList { get; set; }
    }
    public class PayoutMasterList
    {
        public int PayoutNo { get; set; }    
        public string  Payout { get; set; }    
    }
    public class GenealogyTree
    {

        public string MemId { get; set; }

        public string ParentId { get; set; }

        public string SponsorId { get; set; }

        public string LoginId { get; set; }

        public string SponsorLoginId { get; set; }

        public string DisplayName { get; set; }

        public string TempPermanent { get; set; }

        public string Leg { get; set; }

        public string MemberLevel { get; set; }

        public string status { get; set; }

        public string IDStatus { get; set; }

        public string SpillById { get; set; }

        public string JoiningDate { get; set; }

        public string UpgradeDate { get; set; }

        public string AllLeg1 { get; set; }

        public string AllLeg2 { get; set; }

        public string PermanentLeg1 { get; set; }

        public string PermanentLeg2 { get; set; }

        public string LeftNonActive { get; set; }

        public string RightNonActive { get; set; }

        public string BvLeft { get; set; }

        public string BvRight { get; set; }

        public string TotalBv { get; set; }


        public string CurrLeft { get; set; }
        public string CurrRight { get; set; }
        public string ParentLoginId { get; set; }
        public string MobileNo { get; set; }



    }
    public class PayoutDetails
    {
        public string UserType { get; set; }
        public string DisplayName { get; set; }
        public string LoginId { get; set; }
        public decimal Amount { get; set; }
        public string MemberAccNo { get; set; }
        public string IFSCCode { get; set; }
        public string IncomeDate { get; set; }
        public string ClosingDate { get; set; }
        public string PaymentDate { get; set; }
        public int PayoutNo { get; set; }
        public string MemberBankName { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal DirectIncome { get; set; }
         public decimal LeadershipBonus { get; set; }
        public decimal MatchingBonus { get; set; }
    public decimal NetAmount { get; set; }
      public decimal ProcessingFee { get; set; }
  public decimal TDSAmount { get; set; }
    }


    public class DownlineTeamAPI
    {

        public string response { get; set; }
        public List<DownlineAPI> result { get; set; }
    }

    public class DownlineAPI
    {        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string LoginId { get; set; }
        public long MemberId { get; set; }
        public decimal LevelStaus { get; set; }
        public string Status { get; set; }
        public long TotalCount { get; set; }
        public long BIDCount { get; set; }
        public long UIDCount { get; set; }
        public decimal CurrPBV { get; set; }
        public decimal CurrGBV { get; set; }
        public decimal CurrTBV { get; set; }
        public decimal TotalCurrPBV { get; set; }
        public decimal TotalCurrGBV { get; set; }
        public decimal TotalCurrTBV { get; set; }
        public int TotalRecords { get; set; }
        public string Type { get; set; }
        public string PerformanceLevel { get; set; }
        public decimal SelfBusiness { get; set; }
        public decimal TeamBusiness { get; set; }
        public string MemberLoginId { get; set; }
        public string MemberName { get; set; }
        public bool IsPasswordChange { get; set; }
        public string SponsorLoginId { get; set; }
        public string SponsorName { get; set; }
        public string DateOfActivation { get; set; }
        public string DateOfjoining { get; set; }
        public string ParentLoginId { get; set; }
        public string ParentName { get; set; }

        public long FK_MemId { get; set; }
        public string Place { get; set; }
    }

    public class DirectAPI
    {
        public long FK_MemId { get; set; }
        public long FK_sponsorId { get; set; }
        public string MemberLoginId { get; set; }
        public string MemberName { get; set; }
        public string Status { get; set; }
        public decimal LevelStaus { get; set; }
        public long TotalCount { get; set; }
        public long BIDCount { get; set; }
        public long UIDCount { get; set; }
        public decimal CurrPBV { get; set; }
        public decimal CurrGBV { get; set; }
        public decimal CurrTBV { get; set; }
        public decimal TotalCurrPBV { get; set; }
        public decimal TotalCurrGBV { get; set; }
        public decimal TotalCurrTBV { get; set; }
        public string Type { get; set; }
        public int TotalRecords { get; set; }
        public string PerformanceLevel { get; set; }
        public decimal SelfBusiness { get; set; }
        public decimal TeamBusiness { get; set; }
        public bool IsPasswordChange { get; set; }
        public string SponsorLoginId { get; set; }
        public string DateOfActivation { get; set; }
        public string DateOfjoining { get; set; }
        public string ParentLoginId { get; set; }
       public string Place { get; set; }
        public string ParentName { get; set; }
        public string ActiveInactiveStatus { get; set; }

        public string ActivationDate { get; set; }
    }

    public class MDirectV2 : DirectAPI
    {

        public int TotalDownload { get; set; }
        public int Totalteam { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }


    }


    public class ResponseDirectAPI
    {
        public string response { get; set; }
        public string message { get; set; }
        public List<DirectAPI> lstDirect { get; set; }
    }


    public class ResponseDirectAPIV2
    {
        public string response { get; set; }
        public string message { get; set; }
        public List<MDirectV2> lstDirect { get; set; }
        public List<MemberuserInfor> MemberuserInfo { get; set; }
    }


    public class MemberuserInfor
    {
        public long Fk_MemId { get; set; }
        public string LoginId { get; set; }
        public string FullName { get; set; }
        public string profilepic { get; set; }
        public long MyDirect { get; set; }
        public long MyTeam { get; set; }
    }

    public class MemberKYC
    {
        public long Fk_MemId { get; set; }
        public string LoginId { get; set; }
        public string Name { get; set; }
        public string AdhaarCardAttach { get; set; }
        public string AdhaarCardAttachBack { get; set; }
        public string PanCardUrl { get; set; }
        public string AddressFrontUrl { get; set; }
        public string AddressBackUrl { get; set; }
        public string BankDocumentURL { get; set; }
        public string profilepic { get; set; }
        public string AadharStatus { get; set; }
        public string PanStatus { get; set; }
        public string BankStatus { get; set; }
        public string AddressStatus { get; set; }
        public string ProfileStatus { get; set; }
        public string date { get; set; }
        public string response { get; set; }
        public string Message { get; set; }
    }

    public class AccountStateMentDetails
    {
        public string Month { get; set; }
        public int Year { get; set; }
        public decimal NetAmount { get; set; }
        public int PayoutNo { get; set; }
        public long Fk_MemId { get; set; }
        public string LoginId { get; set; }
        public string DisplayName { get; set; }
        public long FK_FromId { get; set; }  
        public long FK_ToId { get; set; }
        public decimal BusinessAmount { get; set; }
        public decimal Amount { get; set; }
        public string IncomeType { get; set; }
        public decimal CommissionPercentage { get; set; }
        public string CurrentDate { get; set; }
        public string ClosingDate { get; set; }

        public bool Status { get; set; }
        //public string LastChanged { get; set; }
        public int FK_PlanId { get; set; }
        public string Remark { get; set; }
        public decimal ToSlabPercent { get; set; }
        public string AdditionalRemarks { get; set; }

    }
    public class AccountStateMentResponse
    {
        public string response { get; set; }
        public string Message { get; set; }
        public List<AccountStateMentDetails> List { get; set; }
    }


    public class DistributorDetails
    {
        public string Status { get; set; }
        public string LoginId { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string Fk_memId { get; set; }
        public string SearchLoginId { get; set; }


    }

}