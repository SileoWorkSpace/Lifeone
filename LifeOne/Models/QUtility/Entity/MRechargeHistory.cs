using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.QUtility.Entity
{
    public class MRechargeHistory
    {

        public string Action { get; set; }
        public string FK_MemID { get; set; }
        public string Type { get; set; }

    }

    public class GetRecentList
    {
        public string MobileNo { get; set; }
        public string Operator { get; set; }
        public string PaymentDate { get; set; }
        public string Amount { get; set; }
        public string Error { get; set; }
        public string Result { get; set; }
        public string TransactionId { get; set; }
        public string TransStatus { get; set; }
    }

    public class RecentResponse
    {
        public string Response { get; set; }
        public List<GetRecentList> RecentActivity { get; set; }
    }
}