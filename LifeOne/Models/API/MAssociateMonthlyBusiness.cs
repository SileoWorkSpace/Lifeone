using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class MAssociateMonthlyBusiness
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int Fk_MemId { get; set; }
        public string LoginId { get; set; }
        public string DisplayName { get; set; }
        public decimal TotalBV { get; set; }
        public decimal SelfBV { get; set; }
        public decimal TeamBV { get; set; }
    }

    public class APIAssociateBusinessReport
    {
        public List<MAssociateMonthlyBusiness> AssociateBusinessReport { get; set; }
        public string Remark { get; set; }
        public string Code { get; set; }
        public string response { get; set; }

    }
}