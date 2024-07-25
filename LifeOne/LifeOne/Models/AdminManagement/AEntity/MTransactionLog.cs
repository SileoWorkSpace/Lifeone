
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LifeOne.Models.Common;
namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MTransactionLog
    {
        public string LoginId { get; set; }
        public string DisplayName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public string TransactionId { get; set; }
        public string ActionType { get; set; }
        public string Session { get; set; }
        public string SessionAuthCode { get; set; }
        public string PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int ProcId { get; set; }
        public string Badge { get; set; }
        public int? Page { get; set; }
        public int Size { get; set; }
        public int TotalRecord { get; set; }
        public string SearchParam { get; set; }
        public string Searchby { get; set; }
        public Pager Pager { get; set; }
        public int IsExport { get; set; }
        public List<MTransactionLog> LstTransactionLog { get; set; }
        public List<SelectListItem> ObjList { get; set; }
        public string Utility { get; set; }
        public string AssociateLoginId { get; set; }
        public int Fk_MemId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}