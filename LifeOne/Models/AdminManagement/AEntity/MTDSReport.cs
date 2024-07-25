using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MTDSReport: MPaging
    {
        public long Fk_MemId { get; set; }
        public decimal TotalCommission { get; set; }
        public decimal TotalTDSDeduction { get; set; }
        public decimal TotalPaisTds { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal TdsAmount { get; set; }
        public string MemberName { get; set; }
        public string Pancard { get; set; }
        public string LoginId { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string ChallanNumber { get; set; }
        public string MonthNameAndYear { get; set; }

        public decimal NetAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal TDS { get; set; }
        public string chequeDate { get; set; }
        public string PayoutNo { get; set; }
        public string PayoutNo1 { get; set; }
        public string ClosingDate { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }

        public string PanNo { get; set; }
        public string FinancealMonth { get; set; }
        public string FinancealYear { get; set; }
        public string PaymentDate { get; set; }

        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string ToPaymentDate { get; set; }
        public string FromPaymentDate { get; set; }
//public int Size { get; set; }
        public string SearchParam { get; set; }
        public string Searchby { get; set; }
       // public string TotalRecords { get; set; }
        public int IsExport { get; set; }

        //public Pager Pager { get;set; }
        //public List<MTDSReport> TdsReport { get; set; }
        public List<MTDSReport> Objlist { get; set; }




    }
}