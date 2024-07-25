using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AModels
{
    public class RoleMasterModel
    {
        public string PK_RoleId { get; set; }
        public string RoleName { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public List<RoleMasterModel> RoleMasterList { get; set; }
    }
}