using LifeOne.Models.FranchiseManagement.FEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class MFranchiseProductDetailsResponse
    {
        public string response { get; set; }
        public string message { get; set; }
        public MFranchiseorderRequest ProductResponse { get; set; }
    }
}