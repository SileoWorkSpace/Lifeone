using System;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.API.DAL;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class FormMasterRepository
    {
        public List<FormMasterModel> FormMasterList()
        {
            DataSet ds = DBHelper.ExecuteQuery("FormMasterGetAll");
            List<FormMasterModel> objlist = new List<FormMasterModel>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objlist.Add(new FormMasterModel
                    {
                        FK_FormTypeId = dr["FormType"].ToString(),
                        FormName = dr["FormName"].ToString(),
                        PK_FormId = dr["PK_FormId"].ToString(),
                        FormTypeName = dr["FormType"].ToString()
                    });
                }
            }
            return objlist;
        }


        public DataSet DeleteFormMaster(string Id, string DeletedBy)
        {
            SqlParameter[] Para =
                {
                new SqlParameter("@PK_FormId",Id),
                new SqlParameter("@DeletedBy",DeletedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("FormMasterDelete", Para);
            return ds;
        }


        public DataSet SaveFormMaster(FormMasterModel obj)
        {
            SqlParameter[] Para =
                {
                new SqlParameter("@FormName",obj.FormName),
                new SqlParameter("@FK_FormTypeId",obj.FK_FormTypeId),
                new SqlParameter("@CreatedBy",obj.CreatedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("FormMasterSave", Para);
            return ds;
        }


        public FormMasterModel EditFormMaster(string id)
        {
            FormMasterModel lstobj = new FormMasterModel();
            SqlParameter[] Para =
                {
                new SqlParameter("@PK_FormId",id)
            };
            FormMasterModel obj = new FormMasterModel();
            DataSet ds = DBHelper.ExecuteQuery("FormMasterGetAll", Para);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                lstobj.PK_FormId = ds.Tables[0].Rows[0]["PK_FormId"].ToString();
                lstobj.FormName = ds.Tables[0].Rows[0]["FormName"].ToString();
                lstobj.FK_FormTypeId = ds.Tables[0].Rows[0]["FK_FormTypeId"].ToString();

            }
            return lstobj;
        }


        public DataSet UpdateFormMaster(FormMasterModel obj)
        {
            SqlParameter[] Para =
             {

                new SqlParameter("@PK_FormId",obj.PK_FormId),
                new SqlParameter("@FK_FormTypeId",obj.FK_FormTypeId),
                new SqlParameter("@FormName",obj.FormName),
                new SqlParameter("@UpdatedBy",obj.UpdatedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("[FormMasterUpdate]", Para);
            return ds;
        }

        // ----------------------------------------//To Bind FormType DropDown------------------------------------------------------------------------------
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
    }
}