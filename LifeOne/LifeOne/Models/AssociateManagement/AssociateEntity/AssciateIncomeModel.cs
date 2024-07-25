using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateEntity
{
    public class AssciateIncomeModel : MPaging
    {
        public string FK_MemID { get; set; }
        public string LoginID { get; set; }
        public string DisplayName { get; set; }
        public string BusinessAmount { get; set; }
        public string CommissionPercentage { get; set; }
        public string IncomeType { get; set; }
        public string PlanName { get; set; }
        public string NetAmount { get; set; }
        public string ProcessingFee { get; set; }
        public string TDSAmount { get; set; }
        public string GrossAmount { get; set; }
        public string ClosingDate { get; set; }

        public string PayoutNo { get; set; }
        public string PlanId { get; set; }
        public List<AssciateIncomeModel> AssciateIncomeList { get; set; }

        
    }


}

