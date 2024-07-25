using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MPerformaneLevel
    {
        public int PK_LevelId { get; set; }
        public string PerformanceLevel { get; set; }
        public decimal TargetPVFrom { get; set; }
        public decimal TargetPVTo { get; set; }
        public decimal LevelPercentage { get; set; }
        public string AdditionalCriteria { get; set; }
        public int UnderLevelID { get; set; }
        public int UnderLevelCount { get; set; }
        public decimal UnderLevelBusiness { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public int ProcId { get; set; }
        public List<MPerformaneLevel> objList { get; set; }
    }
    public class ResponsePerformaneLevelModel
    {
        public int Flag { get; set; }
        public string Msg { get; set; }
    }
}