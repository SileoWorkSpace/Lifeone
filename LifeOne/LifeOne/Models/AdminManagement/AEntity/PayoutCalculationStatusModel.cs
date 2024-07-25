using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class PayoutCalculationStatusModel
    {
        public int PK_PlanId { get; set; }
        public string PlanName { get; set; }
        public bool IsCalculated { get; set; }
        public string CalculationDate { get; set; }
        public string Code { get; set; }
        public string Remark { get; set; }
        public int TotalRecord { get; set; }
        public int? page { get; set; }
        public Pager Pager { get; set; }
        public int size { get; set; }
        public List<PayoutCalculationStatusModel> PayoutCalculationStatuses { get; set; }
        public List<MembersToMakePaymentModel> MembersToMakePayments { get; set; }
    }
}