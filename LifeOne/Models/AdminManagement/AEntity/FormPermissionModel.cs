using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class FormPermissionModel
    {
        public string PK_PermissionId { get; set; }
        public int? FK_UserTypeId { get; set; }
        public int? FK_UserId { get; set; }
        public int? FK_RoleId { get; set; }
        public string RoleName { get; set; }
        public string EmployeeName { get; set; }
        public int? FK_FormTypeId { get; set; }
        public string FormType { get; set; }
        public int? FK_FormId { get; set; }
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

        public List<FormPermissionModel> FormPermissionList { get; set; }
        public List<RolePermissionModel> SelectedRoles { get; set; }
    }

    public class MenuPermissionModel
    {
        public int PK_FormTypeId { get; set; }
        public string FormType { get; set; }
        public int PK_FormId { get; set; }
        public string FormName { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
    }
    public class MainMenuModel
    {
        public int PK_FormTypeId { get; set; }
        public string FormType { get; set; }
        public string Icon { get; set; }
        public List<MenuModel> MenuList { get; set; }
    }
    public class MenuModel
    {
        public int PK_FormId { get; set; }
        public string FormName { get; set; }
        public string Url { get; set; }

    }
    public class RoleModel
    {
        public int RoleId { get; set; }
        public string Role { get; set; }
    }
}