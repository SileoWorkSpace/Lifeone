using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MAdminPlanIncome
    {
        
            public int Fk_PlanId { get; set; }
            public long Pk_PlanIncomeId { get; set; }
            public string PlanName  { get; set; }
            public long IncomeValue { get; set; }
            public int FK_ProductId { get; set; }
            public bool IsPercentage { get; set; }
            public int CreatedBy { get; set; }
            public int ProcId { get; set; }
            public string Status { get; set; }
            public bool IsAscDesc { get; set; }
            public int Page { get; set; }
            public int Size { get; set; }
            public bool IsActive { get; set; }
            public List<MAdminPlanIncome> objList { get; set; }

        }
        public class ResponsePlanIncomeModel
    {
            public int Flag { get; set; }
            public string Msg { get; set; }
        }
    }
