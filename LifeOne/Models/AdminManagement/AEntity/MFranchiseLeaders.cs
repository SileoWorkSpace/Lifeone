using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MFranchiseLeaders: PagingViewModel
    {
        public long Fk_MemId { get; set; }
        public string LoginId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public List<MFranchiseLeaders> FranchiseLeaderList { get; set; }

    }

    public class PagingViewModel
    {
        public string Msg { get; set; }
        public Pager Pager { get; set; }
        public int? Page { get; set; }
        public int Flag { get; set; }
        public int TotalRecord { get; set; }
    }
}