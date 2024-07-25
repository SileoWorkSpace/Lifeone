using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class MCompanion
    {
        public int Fk_RequestId { get; set; }
    }

    public class CheckStatusForCompanionResponse
    {
        public int Flag { get; set; }
        public string Msg { get; set; }
        public string LoginID { get; set; }
        public string CompanyName { get; set; }
        public string PersonName { get; set; }
        public string P_Address { get; set; }
        public string P_PinCode { get; set; }
        public string P_State { get; set; }
        public string P_City { get; set; }
        public string Heading { get; set; }
        public string response { get; set; }
        public string Status { get; set; }
        public List<KeyValuesDetails> keyValuesDetails { get; set; }
        public DeclineFranchiseDetails declineFranchiseDetails { get; set; }
    }
    public class KeyValuesDetails
    {
        public string KeyValus { get; set; }
    }
    public class DeclineFranchiseDetails
    {
        public string AssociateId { get; set; }
        public string AssociateName { get; set; }
        public string MobileNo { get; set; }
        public string AppliedForPinCode { get; set; }
        public string DeclineMsg { get; set; }
        public string FranchiseName { get; set; }
        public string AreaName { get; set; }
        public string District { get; set; }
        public string Taluka { get; set; }
        public string Village { get; set; }
        public string PinCode { get; set; }
        public string Status { get; set; }
        public bool IsApproved { get; set; }
        public bool isDeclined { get; set; }
        public string DeclineReason { get; set; }
        public int CompanionStatus { get; set; }
        public int PK_CompanionId { get; set; }

    }
    public class CompanionPaymentReq
    {
        public long FranchiseId { get; set; }
        public long Fk_MemId { get; set; }
        public int NoOfShare { get; set; }
        public decimal Amount { get; set; }

    }
    public class CompanionPayMentHistoryReq
    {
        public int Pk_CompanionId { get; set; }
    }

    public class VirtualCompanionListResponseAppHistory
    {
        public string response { get; set; }
        public string message { get; set; }
        public decimal TotalPaidAmount { get; set; }
        public List<VirtualCompanionRequestForPaymentHistory> VirtualCompanionListForWebLst { get; set; }
    }

    public class VirtualCompanionRequestForPaymentHistory
    {
        public string TransactionNo { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentDate { get; set; }
        public string RequestRemark { get; set; }
        public string isApproved { get; set; }
        public string ApprovedBy { get; set; }
        public string ApproveDate { get; set; }
        public string ApprovelRemark { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalPaidAmount { get; set; }
    }
}