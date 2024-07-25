using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MAchieverClubBonusMaster
    {
        public int PK_LevelId { get; set; }
        public string PerformanceLevel { get; set; }
        public int SeminarClub { get; set; }
        public int CarClub { get; set; }
        public decimal TravelClub { get; set; }
        public decimal ProvidentClub { get; set; }
        public decimal HouseClub { get; set; }
        public decimal BlueDiamondClub { get; set; }
        public decimal CrownAmbassadorClub { get; set; }
        public decimal PresidentClub { get; set; }
        public decimal LevelPercentage { get; set; }
        public string AdditionalCriteria { get; set; }
        public string UnderLevelID { get; set; }
        public int UnderLevelCount { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public int ProcId { get; set; }
        public List<MAchieverClubBonusMaster> objList { get; set; }
    }
    public class AchieverClubBonusMasterResponse
    {
        public int Flag { get; set; }
        public string Msg { get; set; }
    }
}