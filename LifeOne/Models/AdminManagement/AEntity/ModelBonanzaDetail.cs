using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class ModelBonanzaMaster
    {
        public string BonanzaName { get; set; }
        public int? Pk_BonanzaId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string Code { get; set; }
        public string Remark { get; set; }

    }

    public class ModelBonanzaDetail
    {
        public int? Pk_BonanzaDetailId { get; set; }
        public int? Fk_BonanzaMasterId { get; set; }
        public string BonanzaDetailName { get; set; }
        public string BonanzaDetailImage { get; set; }
        public decimal BusinessAmount { get; set; }
        public decimal BonanzaTarget { get; set; }
        public string BonanzaName { get; set; }
        public int? Pk_BonanzaId { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string Code { get; set; }
        public string Remark { get; set; }

    }


    public class ModelRewardMaster
    {
        public string RewardName { get; set; }
        public int? Pk_RewardId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string Code { get; set; }
        public string Remark { get; set; }

    }
    public class ModelRewardDetail
    {
        public int? Pk_RewardDetailId { get; set; }
        public int? Fk_RewardMasterId { get; set; }
        public string RewardDetailName { get; set; }
        public string RewardDetailImage { get; set; }
        public decimal BusinessAmount { get; set; }
        public string DayDuration { get; set; }
        public string Rank { get; set; }
        public decimal NoOfPairs { get; set; }
        public string RewardTarget { get; set; }
        public string RewardName { get; set; }
        public int? Pk_BonanzaId { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string Code { get; set; }
        public string Remark { get; set; }

    }

}