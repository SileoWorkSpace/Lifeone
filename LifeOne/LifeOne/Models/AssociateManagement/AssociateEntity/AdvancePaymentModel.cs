using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateEntity
{
    public class AdvancePaymentModel  
    {
        public int RequestId { get; set; }
        public long FK_MID { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public decimal? Amount { get; set; }
        public string RecoveryMode { get; set; }
        public decimal? RequestAmt { get; set; }
        public DateTime? RequestDate { get; set; }
        public string RequestDateStr { get; set; }
        public DateTime? RecoveryDate { get; set; }
        public string RecoveryDateStr { get; set; }
        public int Month { get; set; }
        public decimal? ApprovedAmount { get; set; }
        public string ApprovedRejectBy { get; set; }
        public string ApprovedRejectDateStr { get; set; }
        public string Remark { get; set; }
        public string NarrationByAdmin { get; set; }
        public string ReturnType { get; set; }
        public string EMIOption { get; set; }      
       
        public int TotalRecords { get; set; }
    }
    public class AdvancePaymentResponseModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public List<AdvancePaymentModel> Data { get; set; }
    }

    public class LoanEligibilityModel
    {
        public decimal LoanAmountEligibility { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
    public class AdvancePaymentReportModel:MPaging
    {
        public List<AdvancePaymentModel> DataList { get; set; }
    }


    public class AdvancePaymentApprovalDetails
    {
        public int RequestId { get; set; }
        public decimal Amount { get; set; }
        public int FK_MemId { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentRemark { get; set; }
        public string ChequeDDNo { get; set; }
        public string ChequeDDDate { get; set; }
        public string BankName { get; set; }
        public string PaymentSlip { get; set; }
        public string HdnPaymentSlip { get; set; }
         public string PaymentMode { get; set; }
        public string TxtNo { get; set; }
        public HttpPostedFileBase Doc { get; set; }
        
    }
    public class AdvancePaymenthistory
    {
        public long Pk_CPHId { get; set; }
        public long Fk_RFId { get; set; }
        public string TransactionNo { get; set; }
        public int PaymentStatus { get; set; }
        public string PayMode { get; set; }
        public string BankName { get; set; }
        public string ReciptImage { get; set; }
        public int PaymentTypeId { get; set; }
        public string PaymentDate { get; set; }
        public DateTime AdPaymentdate { get; set; }
        public string AdPaymentdateStr { get; set; }
        public decimal PaymentAmount { get; set; }
        public int CreatedBy { get; set; }
    }
}