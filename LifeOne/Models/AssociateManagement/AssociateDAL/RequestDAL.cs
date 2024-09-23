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
        public AssosiateRequest RequestwalletPoints(AssosiateRequest obj)
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

        public List<AssosiateRequest> getRequest()
        {
            try
            {
                DataSet ds = DBHelper.ExecuteQuery("GetEwallet");
                List<AssosiateRequest> lst = new List<AssosiateRequest>();
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            AssosiateRequest assosiateRequest = new AssosiateRequest();
                            assosiateRequest.RequestId = dr["Pk_RequestId"].ToString();
                            assosiateRequest.Fk_MemId = Convert.ToInt32(dr["FK_MemID"].ToString());
                            assosiateRequest.Amount = Convert.ToDecimal(dr["Amount"].ToString());
                            assosiateRequest.PaymentMode = dr["PaymentMode"].ToString();
                            assosiateRequest.TransactionNo = dr["TransactionNo"].ToString();
                            assosiateRequest.Date = dr["Tra_Date"].ToString();
                            assosiateRequest.RequestDate = dr["Req_Date"].ToString();
                            assosiateRequest.LoginId = dr["LoginId"].ToString();
                            assosiateRequest.Status = dr["Status"].ToString();
                            lst.Add(assosiateRequest);
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

        public DataSet Approove(AssosiateRequest assosiateRequest)
        {
              
            try
            {
               
                SqlParameter[] para = {
                new SqlParameter("@Pk_RequestId",assosiateRequest.RequestId),
                new SqlParameter("@Login_id",assosiateRequest.LoginId),
                new SqlParameter("@CreditAmount",assosiateRequest.Amount),
                new SqlParameter("@OpCode",assosiateRequest.OpCode)
                };
                DataSet ds = DBHelper.ExecuteQuery("Request_UpdateStatus", para);
                return ds;
 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet Declined(AssosiateRequest assosiateRequest)
        {
             
            try
            {
                SqlParameter[] para = {
               new SqlParameter("@Pk_RequestId",assosiateRequest.RequestId),
                new SqlParameter("@OpCode",assosiateRequest.OpCode)
                };
                DataSet ds = DBHelper.ExecuteQuery("Request_UpdateStatus", para);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}