using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class QCRMStringConstants
    {
        public static class RolePermissionConstant
        {
            public const string PK_RolePermissionId = "PK_RolePermissionId";
            public const string FK_UserTypeId = "FK_UserTypeId";
            public const string FK_RoleId = "FK_RoleId";
            public const string FK_FormTypeId = "FK_FormTypeId";
            public const string FK_FormMasterId = "FK_FormMasterId";
            public const string FormView = "FormView";
            public const string FormSave = "FormSave";
            public const string FormUpdate = "FormUpdate";
            public const string FormDelete = "FormDelete";
            public const string CreatedBy = "CreatedBy";
            public const string UpdatedBy = "UpdatedBy";
            // Stored Procedure
            public const string PK_RoleId = "PK_RoleId";
            public const string RoleName = "RoleName";
            public const string DeletedBy = "DeletedBy";
            public const string isUpdate = "isUpdate";
            public const string RolePermissionGetAll = "RolePermissionGetAll";
            public const string RolePermissionDelete = "RolePermissionDelete";
            public const string RolePermissionFormGetAll = "RolePermissionFormGetAll";
            public const string RolePermissionSave = "RolePermissionSave";
            public const string RolePermissionUpdate = "RolePermissionUpdate";
        }

        public static class RoleMasterConstant
        {
            public const string PK_RoleId = "PK_RoleId";
            public const string RoleName = "RoleName";

            // Stored Procedure
            public const string RoleMasterGetAll = "RoleMasterGetAll";
        }

        public static class FormTypeConstant
        {
            public const string FK_FormTypeId = "FK_FormTypeId";
            public const string FormType = "FormType";
        }

        public static class FormMasterConstant
        {
            public const string FK_FormId = "FK_FormId";
            public const string FormName = "FormName";
        }

        public static class FormPermissionConstant
        {
            public const string PK_PermissionId = "PK_PermissionId";
            public const string FK_UserTypeId = "FK_UserTypeId";
            public const string FK_UserId = "FK_UserId";

            public const string FK_MemId = "Fk_MemId";

            public const string FK_RoleId = "FK_RoleId";
            public const string FK_FormTypeId = "FK_FormTypeId";
            public const string FK_FormId = "FK_FormId";
            public const string Name = "Name";

            public const string FormView = "FormView";
            public const string FormSave = "FormSave";
            public const string FormUpdate = "FormUpdate";
            public const string FormDelete = "FormDelete";

            public const string FormPermissionGetAll = "FormPermissionGetAll";
            public const string FormPermissionGetByUserId = "FormPermissionGetByUserId";
            public const string FormPermissionSave = "FormPermissionSave";
            public const string FormPermissionUpdate = "FormPermissionUpdate";
            public const string FormPermissionDelete = "FormPermissionDelete";

            public const string DeletedBy = "DeletedBy";
            public const string MemberGetByMemId = "MemberGetByMemId";
            public const string AdminGetByMemId = "AdminByUserId";
            public const string CreatedBy = "CreatedBy";
            public const string isUpdate = "isUpdate";

        }
    }
}