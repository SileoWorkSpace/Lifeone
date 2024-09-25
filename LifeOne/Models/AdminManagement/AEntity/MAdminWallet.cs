using LifeOne.Models.Common;
using LifeOne.Models.QUtility.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MAdminWallet
    {

        public int Pk_RequestId { get; set; }
        public string LoginId { get; set; }
        public string DisplayName { get; set; }
        public string TransactionNo { get; set; }
        public string Amount { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentDate { get; set; }
        public string RequestRemark { get; set; }
        public string ApprovelRemark { get; set; }
        public string CompanyName { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string BranchName { get; set; }
        public string IFSCCode { get; set; }
        public string SlipUpload { get; set; }
        public string isApproved { get; set; }
        public Nullable<bool> IsApprove { get; set; }
        public string RequestedDate { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Status { get; set; }
        public string PinCode { get; set; }        
        public string PaymentStatus { get; set; }
        public int? PaymentCount { get; set; }
        public int? Page { get; set; }
        public int Size { get; set; }
        public int IsExport { get; set; }
        public long TotalRecords { get; set; }        
        public Pager Pager { get; set; }
        public decimal RequestAmount { get; set; }
        public int CompanionStatus { get; set; }
        public string TransactionDate { get; set; }
        public string Credit { get; set; }
        public string Debit { get; set; }
        public string Narration { get; set; }
        public string Balance { get; set; }
        public List<MAdminWallet> objList { get; set; }




    }

    public class ApproveWalletModel
    {
        public int Id { get; set; }
        public int Pk_RequestId { get; set; }
        public string Remark { get; set; }
        public string StatusValue { get; set; }
        public decimal ActualAmount { get; set; }
    }

    public class VirtualCompanion
    {
        public int PK_CompanionId { get; set; }
        public string FranchiseName { get; set; }
        public string AssociateId { get; set; }
        public string AssociateName { get; set; }
        public int NoofShares { get; set; }
        public decimal Amount { get; set; }
        public string MobileNo { get; set; }
        public string AppliedForPinCode { get; set; }
        public string PaymentDate { get; set; }
        public string Status { get; set; }
        public bool IsApproved { get; set; }
        public bool isDeclined { get; set; }
        public string DeclineReason { get; set; }
        public int? Page { get; set; }
        public int Size { get; set; }
        public long TotalRecords { get; set; }
        public Pager Pager { get; set; }
        public string ApprovedDate { get; set; }
        public List<VirtualCompanion> virtualCompanions { get; set; }
    }


    public class ViretualCompanionPaymentRequest
    {
        public int CompanionId { get; set; }
        public string Type { get; set; }
        public string Remark { get; set; }
        public string Pincode { get; set; }
    }

    public class FranchiseDetailsByPincode
    {
        public int Flag { get; set; }
        public string Msg { get; set; }
        public string CompanyName { get; set; }
    }
}