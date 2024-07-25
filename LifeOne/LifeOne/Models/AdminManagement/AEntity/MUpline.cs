using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.Common;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MUpline
    {
        public string LoginId { get; set; }
        public string FirstName { get; set; }
        public string UID { get; set; }
        public string PerformanceLevel { get; set; }
        public double SelfBusiness { get; set; }
        public double TeamBusiness { get; set; }
        public int UIDCount { get; set; }
        public int BIDCount { get; set; }
        public int DirectCount { get; set; }
        public int TotalCount { get; set; }
        public Pager Pager { get; set; }
        public long Size { get; set; }
        public int? Page { get; set; }
        public int TotalRecords { get; set; }
        public List<MUpline> MUplineList { get; set; }
    }
}