using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class MCancelOrder
    {
        public string OrderNo { get; set; }
    }

    public class FranchiseAllowRequestModel
    {
        public long Fk_MemId { get; set; }
    }

    public class FranchiseAllowRequestDetailModel
    {
        public string LoginId { get; set; }
        public string AssociateName { get; set; }
        public string TypeName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public string CreateDate { get; set; }
        public string Msg { get; set; }
        public int Flag { get; set; }
        public int FranchiseTypeId { get; set; }        
    }
}