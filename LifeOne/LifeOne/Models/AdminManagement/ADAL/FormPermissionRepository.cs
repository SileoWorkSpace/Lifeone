using System;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.API.DAL;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using Dapper;
using static LifeOne.Models.AdminManagement.AEntity.QCRMStringConstants;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class FormPermissionRepository
    {
        public List<FormPermissionModel> FormPermissionList()
        {
            DataSet ds = DBHelper.ExecuteQuery(FormPermissionConstant.FormPermissionGetAll);
            List<FormPermissionModel> objlist = new List<FormPermissionModel>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objlist.Add(new FormPermissionModel
                    {
                        FK_UserId = dr[FormPermissionConstant.FK_UserId] != null && !string.IsNullOrEmpty(dr[FormPermissionConstant.FK_UserId].ToString()) ? int.Parse(dr[FormPermissionConstant.FK_UserId].ToString()) : (int?)null,
                        FK_RoleId = dr[FormPermissionConstant.FK_RoleId] != null && !string.IsNullOrEmpty(dr[FormPermissionConstant.FK_RoleId].ToString()) ? int.Parse(dr[FormPermissionConstant.FK_RoleId].ToString()) : (int?)null,
                        FK_FormTypeId = dr[FormPermissionConstant.FK_FormTypeId] != null && !string.IsNullOrEmpty(dr[FormPermissionConstant.FK_FormTypeId].ToString()) ? int.Parse(dr[FormPermissionConstant.FK_FormTypeId].ToString()) : (int?)null,
                        FK_FormId = dr[FormPermissionConstant.FK_FormId] != null && !string.IsNullOrEmpty(dr[FormPermissionConstant.FK_FormId].ToString()) ? int.Parse(dr[FormPermissionConstant.FK_FormId].ToString()) : (int?)null,
                        EmployeeName = dr[FormPermissionConstant.Name].ToString(),
                        RoleName = dr[RolePermissionConstant.RoleName].ToString()
                    });
                }
            }
            return objlist;
        }


        public DataSet DeleteRolePermission(string Id, string DeletedBy)
        {
            SqlParameter[] Para =
                {
                new SqlParameter(FormPermissionConstant.FK_UserId,Id),
                new SqlParameter(FormPermissionConstant.DeletedBy,DeletedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery(FormPermissionConstant.FormPermissionDelete, Para);
            return ds;
        }


        public DataSet SaveFormPermission(FormPermissionModel obj, bool isUpdate = false)
        {
            SqlParameter[] Para =
             {
                new SqlParameter(FormPermissionConstant.FK_RoleId,obj.FK_RoleId),
                new SqlParameter(FormPermissionConstant.FK_FormTypeId,obj.FK_FormTypeId),
                new SqlParameter(FormPermissionConstant.FK_FormId,obj.FK_FormId),
                new SqlParameter(FormPermissionConstant.FK_UserId,obj.FK_UserId),
                new SqlParameter(FormPermissionConstant.FormView,obj.FormView),
                new SqlParameter(FormPermissionConstant.FormSave,obj.FormSave),
                new SqlParameter(FormPermissionConstant.FormUpdate,obj.FormUpdate),
                new SqlParameter(FormPermissionConstant.FormDelete,obj.FormDelete),
                new SqlParameter(FormPermissionConstant.CreatedBy,obj.CreatedBy),
                new SqlParameter(FormPermissionConstant.isUpdate,isUpdate)
            };
            DataSet ds = DBHelper.ExecuteQuery(FormPermissionConstant.FormPermissionSave, Para);
            return ds;
        }


        public FormPermissionModel EditFormPermission(int userId)
        {
            var lstobj = new List<FormPermissionModel>();
            SqlParameter[] Para =
                {
                new SqlParameter("@FK_UserId",userId)
            };
            FormPermissionModel obj = new FormPermissionModel();
            DataSet ds = DBHelper.ExecuteQuery(FormPermissionConstant.FormPermissionGetByUserId, Para);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lstobj.Add(new FormPermissionModel()
                    {
                        RoleName = ds.Tables[0].Rows[i][RolePermissionConstant.RoleName].ToString(),
                        FK_FormTypeId = int.Parse(ds.Tables[0].Rows[i][FormPermissionConstant.FK_FormTypeId].ToString()),
                        PK_PermissionId = ds.Tables[0].Rows[i][FormPermissionConstant.PK_PermissionId].ToString(),
                        FK_FormId = int.Parse(ds.Tables[0].Rows[i][FormPermissionConstant.FK_FormId].ToString()),
                        FormView = bool.Parse(ds.Tables[0].Rows[i][FormPermissionConstant.FormView].ToString()),
                        FormSave = bool.Parse(ds.Tables[0].Rows[i][FormPermissionConstant.FormSave].ToString()),
                        FormUpdate = bool.Parse(ds.Tables[0].Rows[i][FormPermissionConstant.FormUpdate].ToString()),
                        FormDelete = bool.Parse(ds.Tables[0].Rows[i][FormPermissionConstant.FormDelete].ToString()),
                    });
                }
                obj.FK_RoleId = int.Parse(ds.Tables[0].Rows[0][RolePermissionConstant.FK_RoleId].ToString());
                obj.FormPermissionList = lstobj;
            }

            return obj;
        }


        public DataSet UpdateRolePermission(FormPermissionModel obj)
        {
            SqlParameter[] Para =
                {
                new SqlParameter(FormPermissionConstant.PK_PermissionId,obj.PK_PermissionId),
                new SqlParameter(FormPermissionConstant.FK_RoleId,obj.FK_RoleId),
                new SqlParameter(FormPermissionConstant.FK_FormTypeId,obj.FK_FormTypeId),
                new SqlParameter(FormPermissionConstant.FK_FormId,obj.FK_FormId),
                new SqlParameter(FormPermissionConstant.FK_UserId,obj.FK_UserId),
                new SqlParameter(FormPermissionConstant.FormSave,obj.FormSave),
                new SqlParameter(FormPermissionConstant.FormView,obj.FormView),
                new SqlParameter(FormPermissionConstant.FormUpdate,obj.FormUpdate),
                new SqlParameter(FormPermissionConstant.FormDelete,obj.FormDelete),
                new SqlParameter(FormPermissionConstant.CreatedBy,obj.CreatedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery(FormPermissionConstant.FormPermissionUpdate, Para);
            return ds;
        }


        public static List<SelectListItem> BindddlRoles()
        {

            DataSet ds = DBHelper.ExecuteQuery(RoleMasterConstant.RoleMasterGetAll);
            List<SelectListItem> ListOfRoles = new List<SelectListItem>();
            ListOfRoles.Add(new SelectListItem { Text = "--Select Roles--", Value = "" });
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ListOfRoles.Add(new SelectListItem { Text = dr[RoleMasterConstant.RoleName].ToString(), Value = dr[RoleMasterConstant.PK_RoleId].ToString() });
                }
            }
            return ListOfRoles;

        }


        public static List<SelectListItem> BindUserList()
        {
            DataSet ds = DBHelper.ExecuteQuery(FormPermissionConstant.AdminGetByMemId);
            List<SelectListItem> ListOfUsers = new List<SelectListItem>();
            ListOfUsers.Add(new SelectListItem() { Text = "--Select User--", Value = "" });
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ListOfUsers.Add(new SelectListItem { Text = dr[FormPermissionConstant.Name].ToString(), Value = dr[FormPermissionConstant.FK_MemId].ToString() });
                }
            }
            return ListOfUsers;
        }
        public static List<RolePermissionModel> BindRolePermissionFormList()
        {

            DataSet ds = DBHelper.ExecuteQuery(RolePermissionConstant.RolePermissionFormGetAll);
            List<RolePermissionModel> ListOfFormRoles = new List<RolePermissionModel>();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ListOfFormRoles.Add(new RolePermissionModel { FK_FormTypeId = int.Parse(dr[FormTypeConstant.FK_FormTypeId].ToString()), FormType = dr[FormTypeConstant.FormType].ToString(), FK_FormMasterId = int.Parse(dr[RolePermissionConstant.FK_FormMasterId].ToString()), FormName = dr[FormMasterConstant.FormName].ToString() });
                }
            }
            return ListOfFormRoles;

        }
        public static List<RolePermissionModel> BindRolePermissionList(int? roleId)
        {
            DataSet ds = DBHelper.ExecuteQuery(RolePermissionConstant.RolePermissionGetAll, new SqlParameter[] { new SqlParameter(RolePermissionConstant.FK_RoleId, roleId) });
            List<RolePermissionModel> objlist = new List<RolePermissionModel>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objlist.Add(new RolePermissionModel
                    {
                        PK_RolePermissionId = dr[RolePermissionConstant.PK_RolePermissionId].ToString(),
                        FK_UserTypeId = int.Parse(dr[RolePermissionConstant.FK_UserTypeId].ToString()),
                        FK_RoleId = int.Parse(dr[RolePermissionConstant.FK_RoleId].ToString()),
                        RoleName = dr[RoleMasterConstant.RoleName].ToString(),
                        FK_FormTypeId = int.Parse(dr[RolePermissionConstant.FK_FormTypeId].ToString()),
                        FK_FormMasterId = int.Parse(dr[RolePermissionConstant.FK_FormMasterId].ToString()),
                        FormSave = bool.Parse(dr[RolePermissionConstant.FormSave].ToString()),
                        FormUpdate = bool.Parse(dr[RolePermissionConstant.FormUpdate].ToString()),
                        FormDelete = bool.Parse(dr[RolePermissionConstant.FormDelete].ToString()),
                        FormView = bool.Parse(dr[RolePermissionConstant.FormView].ToString())
                    });
                }
            }
            return objlist;
        }



        public static List<RoleModel> BindRoleMaster()
        {
            try
            {
                var queryParameters = new DynamicParameters();
                List<RoleModel> _iresult = DBHelperDapper.DAAddAndReturnModelList<RoleModel>("ProBindRoleMasterForMenuPermission", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<MainMenuModel> BindAllMenu()
        {
            try
            {
                List<MainMenuModel> _ObjList = new List<MainMenuModel>();
                var queryParameters = new DynamicParameters();
                List<MenuPermissionModel> _iresult = DBHelperDapper.DAAddAndReturnModelList<MenuPermissionModel>("ProBindAllMenu", queryParameters);
                _ObjList = _iresult.GroupBy(x => x.PK_FormTypeId).Select(x => new MainMenuModel
                {
                    PK_FormTypeId = x.FirstOrDefault().PK_FormTypeId,
                    FormType = x.FirstOrDefault().FormType,
                    Icon = x.FirstOrDefault().Icon,
                    MenuList = x.Select(p => new MenuModel { FormName = p.FormName, PK_FormId = p.PK_FormId, Url = p.Url }).ToList()
                }).ToList();

                return _ObjList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<EmployeeModel> BindEmpRoleIdWise(int RoleId = 0)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@RoleId", RoleId);
                List<EmployeeModel> _iresult = DBHelperDapper.DAAddAndReturnModelList<EmployeeModel>("ProBindEmpRoleIdWise", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}