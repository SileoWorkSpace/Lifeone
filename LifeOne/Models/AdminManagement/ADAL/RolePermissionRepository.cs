using System;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.API.DAL;
using System.Web.Script.Serialization;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using static LifeOne.Models.AdminManagement.AEntity.QCRMStringConstants;
using Dapper;


namespace LifeOne.Models.AdminManagement.ADAL
{
    public class RolePermissionRepository
    {
        public List<RolePermissionModel> RolePermissionList()
        {
            List<RolePermissionModel> objlist = RoleMasterRepository.BindRolePermissionList(null);
            return objlist;
        }


        public DataSet DeleteRolePermission(string Id, string DeletedBy)
        {
            SqlParameter[] Para =
                {
                new SqlParameter(RolePermissionConstant.FK_RoleId,Id),
                new SqlParameter(RolePermissionConstant.DeletedBy,DeletedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery(RolePermissionConstant.RolePermissionDelete, Para);
            return ds;
        }


        public DataSet SaveRolePermission(RolePermissionModel obj, bool isUpdate = false)
        {
            SqlParameter[] Para =
                {
                new SqlParameter(RolePermissionConstant.FK_RoleId,obj.FK_RoleId),
                new SqlParameter(RolePermissionConstant.FK_FormTypeId,obj.FK_FormTypeId),
                new SqlParameter(RolePermissionConstant.FK_FormMasterId,obj.FK_FormMasterId),
                new SqlParameter(RolePermissionConstant.FormSave,obj.FormSave),
                new SqlParameter(RolePermissionConstant.FormView,obj.FormView),
                new SqlParameter(RolePermissionConstant.FormUpdate,obj.FormUpdate),
                new SqlParameter(RolePermissionConstant.FormDelete,obj.FormDelete),
                new SqlParameter(RolePermissionConstant.CreatedBy,obj.CreatedBy),
                new SqlParameter(RolePermissionConstant.isUpdate,isUpdate)
            };
            DataSet ds = DBHelper.ExecuteQuery(RolePermissionConstant.RolePermissionSave, Para);
            return ds;
        }



        public static LifeOne.Models.Common.AddUpdateResponseResponseModel SetMenuPermission(int UserId, int RoleId, string EntyUser, string MenuList)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@UserId", UserId);
                queryParameters.Add("@RoleId", RoleId);
                queryParameters.Add("@EntyUser", EntyUser);
                queryParameters.Add("@MenuList", MenuList);
                LifeOne.Models.Common.AddUpdateResponseResponseModel _iresult = DBHelperDapper.DAAddAndReturnModel<LifeOne.Models.Common.AddUpdateResponseResponseModel>("ProcUserMenuPermission", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public RolePermissionModel EditRolePermission(string roleId)
        {
            var lstobj = new List<RolePermissionModel>();
            SqlParameter[] Para =
                {
                new SqlParameter("@FK_RoleId",roleId)
            };
            RolePermissionModel obj = new RolePermissionModel();
            DataSet ds = DBHelper.ExecuteQuery("RolePermissionGetAll", Para);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lstobj.Add(new RolePermissionModel()
                    {
                        RoleName = ds.Tables[0].Rows[i][RolePermissionConstant.RoleName].ToString(),
                        FK_FormTypeId = int.Parse(ds.Tables[0].Rows[i][RolePermissionConstant.FK_FormTypeId].ToString()),
                        PK_RolePermissionId = ds.Tables[0].Rows[i][RolePermissionConstant.PK_RolePermissionId].ToString(),
                        FK_FormMasterId = int.Parse(ds.Tables[0].Rows[i][RolePermissionConstant.FK_FormMasterId].ToString()),
                        FormView = bool.Parse(ds.Tables[0].Rows[i][RolePermissionConstant.FormView].ToString()),
                        FormSave = bool.Parse(ds.Tables[0].Rows[i][RolePermissionConstant.FormSave].ToString()),
                        FormUpdate = bool.Parse(ds.Tables[0].Rows[i][RolePermissionConstant.FormUpdate].ToString()),
                        FormDelete = bool.Parse(ds.Tables[0].Rows[i][RolePermissionConstant.FormDelete].ToString()),
                    });
                }
                obj.FK_RoleId = int.Parse(ds.Tables[0].Rows[0][RolePermissionConstant.FK_RoleId].ToString());
                obj.RolePermissionList = lstobj;
            }

            return obj;
        }


        public DataSet UpdateRolePermission(RolePermissionModel obj)
        {
            SqlParameter[] Para =
                {
                new SqlParameter(RolePermissionConstant.PK_RolePermissionId,obj.PK_RolePermissionId),
                new SqlParameter(RolePermissionConstant.FK_RoleId,obj.FK_RoleId),
                new SqlParameter(RolePermissionConstant.FK_FormTypeId,obj.FK_FormTypeId),
                new SqlParameter(RolePermissionConstant.FK_FormMasterId,obj.FK_FormMasterId),
                new SqlParameter(RolePermissionConstant.FormSave,obj.FormSave),
                new SqlParameter(RolePermissionConstant.FormView,obj.FormView),
                new SqlParameter(RolePermissionConstant.FormUpdate,obj.FormUpdate),
                new SqlParameter(RolePermissionConstant.FormDelete,obj.FormDelete),
                new SqlParameter(RolePermissionConstant.UpdatedBy,obj.CreatedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery(RolePermissionConstant.RolePermissionUpdate, Para);
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
        public static List<SelectListItem> BindddlFormType()
        {

            DataSet ds = DBHelper.ExecuteQuery("FormTypeGetall");
            List<SelectListItem> ListOfFromType = new List<SelectListItem>();
            ListOfFromType.Add(new SelectListItem { Text = "--Select Form Type--", Value = "" });
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ListOfFromType.Add(new SelectListItem { Text = dr["FormType"].ToString(), Value = dr["PK_FormTypeId"].ToString() });
                }
            }
            return ListOfFromType;

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
    }
}