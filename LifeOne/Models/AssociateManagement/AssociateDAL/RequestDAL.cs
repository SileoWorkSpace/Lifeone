using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using Org.BouncyCastle.Asn1.Ocsp;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using LifeOne.Models.API.DAL;
using DocumentFormat.OpenXml.Drawing;

namespace LifeOne.Models.AssociateManagement.AssociateDAL
{
    public class RequestDAL
    {
        public AssosiateRequest RequestKaryonPoints(AssosiateRequest obj)
        {
           
            try
            {
                SqlParameter[] para = {
    new SqlParameter("@LoginId", obj.LoginId ?? (object)DBNull.Value),
    new SqlParameter("@Name", string.IsNullOrEmpty(obj.Name) ? (object)DBNull.Value : obj.Name),
    new SqlParameter("@Amount", obj.Amount),
    new SqlParameter("@PaymentMode", string.IsNullOrEmpty(obj.PaymentMode) ? (object)DBNull.Value : obj.PaymentMode),
    new SqlParameter("@UPI_Number", string.IsNullOrEmpty(obj.ChequeDD_No) ? (object)DBNull.Value : obj.ChequeDD_No),
    new SqlParameter("@TransactionDate", obj.Convert_date ?? (object)DBNull.Value),
    new SqlParameter("@Fk_BankId", obj.BankId ?? (object)DBNull.Value), 
    new SqlParameter("@OpCode", obj.OpCode),
    new SqlParameter("@AddedBy", obj.AddedBy)
};
                DataSet ds = DBHelper.ExecuteQuery("SaveEwalletRequest", para);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        obj.Code = ds.Tables[0].Rows[0]["Flag"].ToString();
                        obj.Message = ds.Tables[0].Rows[0]["Message"].ToString();
                    }
                }
                return obj;


            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SelectListItem> GetBank(AssosiateRequest obj)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            try
            {
                SqlParameter[] para = { 
                
                new SqlParameter("@Pk_BankId", obj.Pk_BankId)
                };
                DataSet ds = DBHelper.ExecuteQuery("GetBank",para);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lst.Add(new SelectListItem { Text = "--Select Bank--", Value = "0" });
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            lst.Add(new SelectListItem { Text = dr["BankName"].ToString(), Value = dr["Pk_BankId"].ToString() });
                        }
                    }
                    
                }
                return lst;
            }
            
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}