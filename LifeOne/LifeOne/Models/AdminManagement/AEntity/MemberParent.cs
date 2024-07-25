using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MemberParent: MCommonProperties
    {
        public int Flag { get; set; }
        public long FK_MemId { get; set; }
        public long SponsorId { get; set; }
        public long CuurSponserID { get; set; }
        public string LoginID { get; set; }
        public string MemberName { get; set; }
        public string SponsorName { get; set; }
        public string SponsorLoginID { get; set; }
        public string Remark { get; set; }
    }
}