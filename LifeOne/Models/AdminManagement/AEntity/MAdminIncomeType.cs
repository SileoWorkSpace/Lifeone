using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MAdminIncomeTypeMaster
    {
        public int Pk_IncomeTypeId { get; set; }
        public long Fk_UtilityId { get; set; }
        public string IncomeTypeName { get; set; }
        public string UserPlanName { get; set; }
        public int CreatedBy { get; set; }
        public int ProcId { get; set; }
        public string Status { get; set; }
        public bool IsAscDesc { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public bool IsActive { get; set; }
        public List<MAdminIncomeTypeMaster> objList { get; set; }

    }
    public class ResponseIncomeTypeMasterModel
    {
        public int Flag { get; set; }
        public string Msg { get; set; }
    }

    public class Member_RefundAmountModel
    {
        public long FK_MemId { get; set; }
        public string LoginId { get; set; }
        public string MemberName { get; set; }
        public string UtilityName { get; set; }
        public long Fk_UtilityId { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public string Description { get; set; }
        public string TransNo { get; set; }
        public string TransDate { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public string Mobile { get; set; }
        public int Pk_UniqueId { get; set; }
        public string RefundId { get; set; }
        public string PaymentId { get; set; }
        public string code { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int? Page { get; set; }
        public int Size { get; set; }
        public int TotalRecord { get; set; }
        public int IsExport { get; set; }
        public Pager Pager { get; set; }

        public List<Member_RefundAmountModel> RefundAmountList { get; set; }
    }

    public class RazorPayRefundResponse
    {
       public string id { get; set; }
       public string entity { get; set; }
       public string payment_id { get; set; }
       public string status { get; set; }
       public string Msg { get; set; }
    
       public string amount { get; set; }
       public string code { get; set; }

    }
}