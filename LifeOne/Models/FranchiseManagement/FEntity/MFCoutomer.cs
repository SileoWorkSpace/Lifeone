using LifeOne.Models.QUtility.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.Common;
using System.Web.Mvc;

namespace LifeOne.Models.FranchiseManagement.FEntity
{
    public class MFCoutomer: MSimpleString
    {
        public string CustomerMobile { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMail { get; set; }
        public string PerformanceLevel { get; set; }
        public int PerformanceLevelID { get; set; }
        public decimal TargetPVFrom { get; set; }
        public decimal TargetPVTo { get; set; }
        public string FK_ProductId { get; set; }
        public string PackageName { get; set; }
        public decimal BP { get; set; }
        public decimal PackageAmount { get; set; }
        public List<SelectListItem> PackageList { get; set; }
        public string LoginId { get; set; }
      

    }

}