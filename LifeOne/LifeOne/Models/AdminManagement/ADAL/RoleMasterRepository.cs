using System;
using LifeOne.Models.AdminManagement.AEntity;

using LifeOne.Models.API.DAL;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using static LifeOne.Models.AdminManagement.AEntity.QCRMStringConstants;

using LifeOne.Models.AdminManagement.AModels;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class RoleMasterRepository
    {
        public List<RoleMasterModel> RoleMasterList()
        {
            DataSet ds = DBHelper.ExecuteQuery("RoleMasterGetAll");
            List<RoleMasterModel> objlist = new List<RoleMasterModel>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objlist.Add(new RoleMasterModel
                    {
                        PK_RoleId = dr["PK_RoleId"].ToString(),
                        RoleName = dr["RoleName"].ToString(),

                    });
                }
            }
            return objlist;
        }

        public DataSet SaveRoleMaster(RoleMasterModel obj)
        {
            SqlParameter[] Para =
                {
                new SqlParameter("@RoleName",obj.RoleName),

                new SqlParameter("@CreatedBy",obj.CreatedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("RoleMasterSave", Para);
            return ds;
        }

        public DataSet UpdateRoleMaster(RoleMasterModel obj)
        {
            SqlParameter[] Para =
             {

                new SqlParameter("@PK_RoleId",obj.PK_RoleId),
                new SqlParameter("@RoleName",obj.RoleName),
                new SqlParameter("@UpdatedBy",obj.UpdatedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("[RoleMasterUpdate]", Para);
            return ds;
        }


        public DataSet DeleteRoleMaster(string Id, string DeletedBy)
        {
            SqlParameter[] Para =
                {
                new SqlParameter("@PK_RoleId",Id),
                new SqlParameter("@DeletedBy",DeletedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("RoleMasterDelete", Para);
            return ds;
        }
        public RoleMasterModel EditRoleMaster(string id)
        {
            RoleMasterModel lstobj = new RoleMasterModel();
            SqlParameter[] Para =
                {
                new SqlParameter("@PK_RoleId",id)
            };
            RoleMasterModel obj = new RoleMasterModel();
            DataSet ds = DBHelper.ExecuteQuery("RoleMasterGetAll", Para);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                lstobj.PK_RoleId = ds.Tables[0].Rows[0]["PK_RoleId"].ToString();
                lstobj.RoleName = ds.Tables[0].Rows[0]["RoleName"].ToString();
            }
            return lstobj;
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
    }
}