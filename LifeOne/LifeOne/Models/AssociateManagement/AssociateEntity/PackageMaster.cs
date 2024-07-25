using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.API.DAL;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Models.AssociateManagement.AssociateEntity
{
    public class PackageMaster
    {
        public string Pk_PackageID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string MRP { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
        public string IGST { get; set; }
        public string CreatedBy { get; set; }
        public string Pk_ProductId { get; set; }
        public string Msg { get; set; }
        public string DP { get; set; }
        public string BV { get; set; }
        public string Fk_MemId { get; set; }
        public string IsFixed { get; set; }




    }

    public class BindPckageMaster
    {
        public static List<SelectListItem> BindPackageMaster()
        {
            List<SelectListItem> Package = new List<SelectListItem>();

            SqlParameter[] Para =
              {
               new SqlParameter("@Fk_memid",SessionManager.AssociateFk_MemId)
            };
            DataSet ds = DBHelper.ExecuteQuery("BindPackageMaster_API", Para);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Package.Add(new SelectListItem { Text = "--Select Package--", Value = "" });
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Package.Add(new SelectListItem { Text = dr["PackageName"].ToString(), Value = dr["Pk_PackageID"].ToString() });
                    }
                }
            }
            return Package;
        }
        public static List<SelectListItem> BindPackageMasterAdmin()
        {
            List<SelectListItem> Package = new List<SelectListItem>();

            SqlParameter[] Para =
              {
               new SqlParameter("@Fk_memid",SessionManager.AssociateFk_MemId)
            };
            DataSet ds = DBHelper.ExecuteQuery("BindPackageMasterForAdmin", Para);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Package.Add(new SelectListItem { Text = "--Select Package--", Value = "" });
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Package.Add(new SelectListItem { Text = dr["PackageName"].ToString(), Value = dr["Pk_PackageID"].ToString() });
                    }
                }
            }
            return Package;
        }
        public static List<SelectListItem> BindPackageMasterFranchise()
        {
            List<SelectListItem> Package = new List<SelectListItem>();

            SqlParameter[] Para =
              {
               new SqlParameter("@Fk_memid",SessionManager.AssociateFk_MemId)
            };
            DataSet ds = DBHelper.ExecuteQuery("BindPackageMasterForFranchise", Para);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Package.Add(new SelectListItem { Text = "--Select Package--", Value = "" });
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Package.Add(new SelectListItem { Text = dr["PackageName"].ToString(), Value = dr["Pk_PackageID"].ToString() });
                    }
                }
            }
            return Package;
        }

        public static List<SelectListItem> GetPackageMaster()
        {
            List<SelectListItem> Package = new List<SelectListItem>();
            
            DataSet ds = DBHelper.ExecuteQuery("BindPackageMaster");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Package.Add(new SelectListItem { Text = "--Select Package--", Value = "0" });
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Package.Add(new SelectListItem { Text = dr["PackageName"].ToString(), Value = dr["Pk_PackageID"].ToString() });
                    }
                }
            }
            return Package;
        }
    }

  
    public class UpgradePackage
    {
        public long Fk_MemId { get; set; }
        public int Fk_packageid { get; set; }
        public decimal Amount { get; set; }
        public string TransactionNo { get; set; }
        public string LoginId { get; set; }
        public string PaymentMode { get; set; }
        public string CardType { get; set; }

        public string DisplayName { get; set; }
        public string Result { get; set; }

        public string TempPermanent { get; set; }

        public static DataSet DalUpgradePackage(UpgradePackage upgradePackage)
        {
            SqlParameter[] Para =
                {
               new SqlParameter("@Fk_memid",upgradePackage.Fk_MemId),
                new SqlParameter("@Fk_packageid",upgradePackage.Fk_packageid),
                new SqlParameter("@Amount",upgradePackage.Amount),
                 new SqlParameter("@TransactionNo",upgradePackage.TransactionNo),
                 new SqlParameter("@PaymentMode",upgradePackage.PaymentMode),
                 new SqlParameter("@CardType",upgradePackage.CardType),
                  new SqlParameter("@LoginId",upgradePackage.LoginId)
            };
            DataSet ds = DBHelper.ExecuteQuery("Proc_UpdateMemberPackage", Para);
            return ds;
        }


        public DataSet GetMemberInfo()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId) };
            DataSet ds = DBHelper.ExecuteQuery("GetMemberDetailsFromLoginId", para);
            return ds;

        }
        public DataSet GetMemberTopup()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId) };
            DataSet ds = DBHelper.ExecuteQuery("GetMemberForTopup", para);
            return ds;

        }
        public static List<PackageDetails> GetpackageDetails(PackageMaster obj)
        {

            string _qry = "Getpackagedetailsbyid @pk_packageid='" + obj.Pk_PackageID + "'";
            List<PackageDetails> _iresult = DBHelperDapper.DAGetDetailsInList<PackageDetails>(_qry);
            return _iresult;
        }

        public static List<PackageMaster> GetAmountPackagebyId(PackageMaster obj)
        {

            string _qry = "GetAmountPackagebyId @Pk_ProductId='" + obj.Pk_PackageID + "',@Fk_MemId='" + obj.Fk_MemId + "'";
            List<PackageMaster> _iresult = DBHelperDapper.DAGetDetailsInList<PackageMaster>(_qry);
            return _iresult;
        }

    }
    public class PackageDetails
    {
        public string BV { get; set; }
        public string ProdAmount { get; set; }
        public string Fk_PackageId { get; set; }
        public string ProductId { get; set; }
        public string ProductQty { get; set; }
        public string Createdby { get; set; }
        public string CreatedDate { get; set; }
        public string Updatedby { get; set; }
        public string UpdatedDate { get; set; }
        public string Deletedby { get; set; }
        public string Deleteddate { get; set; }
        public string IsDeleted { get; set; }
        public string ProductName { get; set; }
        public string PV { get; set; }
    }


}