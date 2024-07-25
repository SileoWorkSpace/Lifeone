using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.API.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class FormTypeRepository
    {
        public List<FormTypeModel> FormTypeList()
        {
            DataSet ds = DBHelper.ExecuteQuery("FormTypeGetAll");
            List<FormTypeModel> objlist = new List<FormTypeModel>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objlist.Add(new FormTypeModel
                    {
                        FormType = dr["FormType"].ToString(),
                        PK_FormTypeId = dr["PK_FormTypeId"].ToString(),

                    });
                }
            }
            return objlist;
        }


        public DataSet DeleteFormType(string Id, string DeletedBy)
        {
            SqlParameter[] Para =
                {
                new SqlParameter("@PK_FormTypeId",Id),
                new SqlParameter("@DeletedBy",DeletedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("FormTypeDelete", Para);
            return ds;
        }


        public DataSet SaveFormType(FormTypeModel obj)
        {
            SqlParameter[] Para =
                {
                new SqlParameter("@FormType",obj.FormType),
                new SqlParameter("@CreatedBy",obj.CreatedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("FormTypeSave", Para);
            return ds;
        }


        public FormTypeModel EditFormType(string id)
        {
            FormTypeModel lstobj = new FormTypeModel();
            SqlParameter[] Para =
                {
                new SqlParameter("@PK_FormTypeId",id)
            };
            FormTypeModel obj = new FormTypeModel();
            DataSet ds = DBHelper.ExecuteQuery("FormTypeGetAll", Para);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                lstobj.PK_FormTypeId = ds.Tables[0].Rows[0]["PK_FormTypeId"].ToString();
                lstobj.FormType = ds.Tables[0].Rows[0]["FormType"].ToString();


            }


            return lstobj;
        }


        public DataSet UpdateFormType(FormTypeModel obj)
        {
            SqlParameter[] Para =
             {

                new SqlParameter("@PK_FormTypeId",obj.PK_FormTypeId),
                new SqlParameter("@FormType",obj.FormType),
                new SqlParameter("@UpdatedBy",obj.UpdatedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("[FormTypeUpdate]", Para);
            return ds;
        }



        //------------------------------------Drop-Downs------------------------
        public static List<SelectListItem> BindddlSite()
        {

            DataSet ds = DBHelper.ExecuteQuery("SiteMasterGetall");
            List<SelectListItem> ListOfSite = new List<SelectListItem>();
            ListOfSite.Add(new SelectListItem { Text = "--Select Site--", Value = "" });
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ListOfSite.Add(new SelectListItem { Text = dr["SiteName"].ToString(), Value = dr["PK_SiteId"].ToString() });
                }
            }
            return ListOfSite;
        }


        public static List<SelectListItem> getddlpayment()
        {
            List<SelectListItem> ddllist = new List<SelectListItem>();
            ddllist.Add(new SelectListItem { Value = "0", Text = "<-Select Plan->" });
            DataSet ds = DBHelper.ExecuteQuery("ddlPaymentPlan");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ddllist.Add(new SelectListItem { Value = dr["PK_PlanId"].ToString(), Text = dr["PlanName"].ToString() });
                }
            }
            return ddllist;        
        }
    }
}