using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MConsolidatedPayoutModel
    {
        public string Payout { get; set; }
        public decimal Amount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public List<MConsolidatedPayoutModel> ConsolidatedPayouts { get; set; }
    }
}