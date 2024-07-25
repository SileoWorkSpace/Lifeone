using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MembersToMakePaymentModel
    {
        public int Fk_MemId { get; set; }
        public string LoginId { get; set; }
        public string Name { get; set; }
        public decimal SelfIncome { get; set; }
        public decimal TeamIncome { get; set; }
        public decimal AirOrbitIncome { get; set; }
        public decimal WaterOrbitIncome { get; set; }
        public decimal SpaceOrbitIncome { get; set; }
        public decimal RoyaltyIncome { get; set; }
        public decimal OrbitRoyaltyIncome { get; set; }
        public decimal LeadershipIncome { get; set; }
        public decimal ClubBonus { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal TDSAmount { get; set; }
        public decimal ProcessingFee { get; set; }
        public decimal NetAmount { get; set; }
        public string ClosingDate { get; set; }
        public List<MembersToMakePaymentModel> PayoutMembers { get; set; }
    }
}