using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.LifeOnee
{
    public class WalletModel
    {
        //Fk_MemberId,CreditAmount,DebitAmount,beneficiaryid,TransNo,TransDate,TransAmount,TransStatus,BeneficiaryName,RemittersMobileNo,Remarks,Narration,Session,ErrorMsg,AuthCode
        public string DATE { get; set; }
        public string SESSION { get; set; }
        public string ERROR { get; set; }
        public string RESULT { get; set; }
        public string Session { get; set; }
        public int Fk_MemberId { get; set; }
        public Decimal CreditAmount { get; set; }
        public Decimal DebitAmount { get; set; }
        public int beneficiaryid { get; set; }
        public String TransNo { get; set; }
        public string TransDate { get; set; }
        public Decimal TransAmount { get; set; }
        public String TransStatus { get; set; }
        public String BeneficiaryName { get; set; }
        public String RemittersMobileNo { get; set; }      
        public String ErrorMsg { get; set; }
        public String AuthCode { get; set; }
        public String AddInfo { get; set; }
        public string ValidateToken { get; set; }
        public string AMOUNT { get; set; }
        public long CompanyId { get; set; }
        public long Fk_MemId { get; set; }
        public string SessionNo { get; set; }
        public string NUMBER { get; set; }
        public string benId { get; set; }

    }


}
public class WalletResponse
{   
    public string Response { get; set; }
}

#region Fund Transfer Log(For Fund Transfer Log)
public class FundTransferLog : DBHelper
{
    public string FK_MemID { get; set; }

    public string Type { get; set; }

    public DataSet LogFundTransfer()
    {
        SqlParameter[] para =
        {
                new SqlParameter ("@Fk_MemId",FK_MemID),
                new SqlParameter ("@Type",Type)

        };
        DataSet dsresult = ExecuteQuery("FundTransferLog", para);
        return dsresult;
    }
}

public class FundTransferAPILog
{
    //TransAmount,TransNo,Transdate,BeneficiaryName,Remarks,TransStatus
    public string TransAmount { get; set; }
    public string TransNo { get; set; }
    public string Transdate { get; set; }
    public string BeneficiaryName { get; set; }
    public string Remarks { get; set; }
    public string TransStatus { get; set; }
}

public class FundTransferLogResponse
{
    public string Response { get; set; }
    public List<FundTransferAPILog> FundTransferLog { get; set; }
    public decimal MonthlyAmount { get; set; }
    public decimal TotalAmount { get; set; }
}
#endregion

//#region Payout Request
//public class PayoutRequest : DBHelper
//{
//    public string LoginId { get; set; }

//    public string RequestedAmount { get; set; }

//    public DataSet PayoutReq()
//    {
//        SqlParameter[] para =
//        {
//                new SqlParameter ("@LoginId",LoginId),
//                 new SqlParameter ("@RequestedAmount",RequestedAmount)

//            };
//        DataSet dsresult = ExecuteQuery("webMemberPayoutRequest", para);
//        return dsresult;
//    }
//}


//public class PayoutRequestResponse
//{
//    public string Response { get; set; }
//    public string Message { get; set; }
//}
//#endregion

//#region Get PayoutRequestDetails
//public class GetPayoutRequestDetails : DBHelper
//{
//    public string LoginId { get; set; }

//    public DataSet PayoutDetails()
//    {
//        SqlParameter[] para =
//        {
//                new SqlParameter ("@loginId",LoginId)

//            };
//        DataSet dsresult = ExecuteQuery("webGetPayoutRequestDetail", para);
//        return dsresult;
//    }
//}

//public class GetPayoutRequestDetailsList
//{
//    public string PK_RequestId { get; set; }
//    public string FK_MemId { get; set; }
//    public string LoginId { get; set; }
//    public string DisplayName { get; set; }
//    public string MemberAccNo { get; set; }
//    public string IFSCCode { get; set; }
//    public string MemberBankName { get; set; }
//    public string MemberBranch { get; set; }
//    public string RequestedAmount { get; set; }
//    public string ApprovalStatus { get; set; }
//    public string ApprovalDate { get; set; }
//    public string RequestedDate { get; set; }
//}

//public class PayoutRequestDetailsResponse
//{
//    public string Response { get; set; }
//    public List<GetPayoutRequestDetailsList> GetPayoutRequestDetails { get; set; }
//}
//#endregion

