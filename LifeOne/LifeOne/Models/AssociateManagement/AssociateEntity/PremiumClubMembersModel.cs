using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
namespace LifeOne.Models.AssociateManagement.AssociateEntity
{
    public class PremiumClubMembers
    {
        public string LoginId { get; set; }
        public string Mobile { get; set; }
        public string DisplayName { get; set; }
        public string PackageName { get; set; }
        public string DOJ { get; set; }
        public string DOA { get; set; }
        public string NoofSp { get; set; }
    }

    public class PremiumClubQualifier
    {
        public string LoginId { get; set; }
        public string Mobile { get; set; }
        public string DisplayName { get; set; }
        public string PackageName { get; set; }
        public string DOJ { get; set; }
        public string DOA { get; set; }
        public string DOLS { get; set; }
        public string DOQ { get; set; }

        public string AchievedSatus { get; set; }
        public string NoofSp { get; set; }


    }
    public class AssociatePayoutReport
    {

        public int? PayoutNo { get; set; }
        public decimal SGYearlyBonus { get; set; }
        public decimal LDBRepurchase { get; set; }
        public decimal HybridMatchingBonus { get; set; }
        public decimal DirectorClub { get; set; }
        public int PaidMemberId { get; set; }
        public decimal ChairManClub { get; set; }
        public decimal HouseFund { get; set; }
        public decimal ProcessingFee { get; set; }
        public decimal LARBonus { get; set; }
        public decimal TDSAmount { get; set; }
        public decimal NetAmount { get; set; }
        public int FK_MemId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string IncomeType { get; set; }
        public decimal GrossAmount { get; set; }
        public string LoginId { get; set; }
        public string DisplayName { get; set; }
        public string ClosingDate { get; set; }
     



    }

    public class AssociatePayoutTypeReport
    {

        public int FK_MemId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
       
        public decimal GrossAmount { get; set; }
        public string LoginId { get; set; }
        public string DisplayName { get; set; }
        public decimal BusinessAmount { get; set; }
        public decimal Amount { get; set; }
        public string ClosingDate { get; set; }
        public string IncomeType { get; set; }
        public string Date { get; set; }

        public int? PayoutNo { get; set; }


    }



    public class PremiumClubMembersModel
    {
       
        public static List<PremiumClubMembers> GetPremiumClubMembers()
        {

            string _qry = "GetPremiumClubMembers";
            List<PremiumClubMembers> _iresult = DBHelperDapper.DAGetDetailsInList<PremiumClubMembers>(_qry);
            return _iresult;
        }

        public static List<PremiumClubQualifier> GetPremiumClubQualifier()
        {

            string _qry = "GetPremiumClubAchievers";
            List<PremiumClubQualifier> _iresult = DBHelperDapper.DAGetDetailsInList<PremiumClubQualifier>(_qry);
            return _iresult;
        }


        public static List<AssociatePayoutReport> AssociateRepurchasePayoutReport(AssociatePayoutReport obj)
        {
            var sqlparams = new DynamicParameters();
            sqlparams.Add("@FK_MemID", obj.FK_MemId);
            sqlparams.Add("@PayoutNo", obj.PayoutNo);
            sqlparams.Add("@FromDate", obj.FromDate);
            sqlparams.Add("@ToDate", obj.ToDate);
            var data = DBHelperDapper.DAAddAndReturnModelList<AssociatePayoutReport>("RepurchasePayoutIncomeReport_API", sqlparams);
            return data;
        }


        public static List<AssociatePayoutTypeReport> AssociateRepurchasePayoutType(AssociatePayoutTypeReport obj)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@FK_MemID", obj.FK_MemId);
            queryParameters.Add("@PayoutNo", obj.PayoutNo);
            queryParameters.Add("@IncomeType", obj.IncomeType);
            queryParameters.Add("@FromDate", obj.FromDate);
            queryParameters.Add("@ToDate", obj.ToDate);
            List<AssociatePayoutTypeReport> _iresult = DBHelperDapper.DAAddAndReturnModelList<AssociatePayoutTypeReport>("RepurchasePayoutIncomeReportType_API", queryParameters);
            return _iresult;
        }

    }


}