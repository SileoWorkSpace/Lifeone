using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class RolePermissionModel
    {
        public string PK_RolePermissionId { get; set; }
        public int? FK_UserTypeId { get; set; }
        public int? FK_UserId { get; set; }

        public int? FK_RoleId { get; set; }
        public string RoleName { get; set; }

        public int? FK_FormTypeId { get; set; }
        public string FormType { get; set; }

        public int? FK_FormMasterId { get; set; }
        public string FormName { get; set; }

        public bool FormView { get; set; }
        public bool FormSave { get; set; }
        public bool FormUpdate { get; set; }
        public bool FormDelete { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public List<RolePermissionModel> RolePermissionList { get; set; }
    }
}