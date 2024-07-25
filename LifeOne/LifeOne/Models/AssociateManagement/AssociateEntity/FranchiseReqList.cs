using Dapper;
using LifeOne.Models.Common;
using LifeOne.Models.HomeManagement.HDAL;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateEntity
{
    public class FranchiseReqList
    {
        public string LoginId { get; set; }
        public string AssociateName { get; set; }
        public string TypeName { get; set; }
        public string StateName { get; set; }
        public string DistrictName { get; set; }
        public long ToatlRecord { get; set; }
        public int? Page { get; set; }
        public int KeyId { get; set; }
        public int FK_FranchiseTypeId { get; set; }
        public int CreatedBy { get; set; }
    }

    public class FranchiseReqListResponse
    {
        public string LoginId { get; set; }
        public string AssociateName { get; set; }
        public string TypeName { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public string StateName { get; set; }
        public string DistrictName { get; set; }
        public string CreateDate { get; set; }
        public long ToatlRecord { get; set; }
        public int? Page { get; set; }
        public int KeyId { get; set; }
        public int ProcID { get; set; }
        public int FK_FranchiseTypeId { get; set; }
        public int CreatedBy { get; set; }

        public string Response { get; set; }
        public string Msg { get; set; }
        public Pager Pager { get; set; }
        public bool Status { get; set; }
        public int Flag { get; set; }
        public long Size { get; set; }
        public string SearchParam { get; set; }        
        public string Searchby { get; set; }
        public List<FranchiseReqListResponse> FranchiseReqLists { get; set; }
    }
}