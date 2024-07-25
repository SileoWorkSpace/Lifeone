using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.BillAvenueUtility.Entity
{
    public class MTransactionHistory
    {
        public string CompanyId { get; set; }
        public string Fk_MemId { get; set; }
    }
    public class MTransactionHistoryResponse
    {
        public string BillerId { get; set; }
        public string BillerName { get; set; }
        public string BillerCategory { get; set; }
        public string BillerCoverage { get; set; }
        public string BillerAdhoc { get; set; }
        public string LatePaymentFee { get; set; }
        public string FixedCharges { get; set; }
        public string AdditionalCharges { get; set; }
        public string BillAmount { get; set; }
        public string BillDate { get; set; }
        public string BillNumber { get; set; }
        public string BillPeriod { get; set; }
        public string CustomerName { get; set; }
        public string MobileNo { get; set; }
        public string DueDate { get; set; }
        public string TxnDate { get; set; }
        public string TxnReferenceId { get; set; }
        public string TxnStatus { get; set; }
        public string BillerPaymentModes { get; set; }
        public string QuickPay { get; set; }
        public string SplitPay { get; set; }
        public string RespAmount { get; set; }
        public string RespBillDate { get; set; }
        public string RespBillPeriod { get; set; }
        public string RespBillNumber { get; set; }
        public string RespCustomerName { get; set; }
        public string RespDueDate { get; set; }
    }
}