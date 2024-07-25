using LifeOne.Models.QUtility.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;
using LifeOne.Models.Common;
namespace LifeOne.Models.QUtility.DAL
{
    public class DALWalletTransaction : DBHelper
    {

        //string Aeskey = ConfigurationManager.AppSettings["AeskeyUtility"].ToString();
        public DataSet Transactions(WalletTransaction objWalletTrans)
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter ("@Fk_MemId",objWalletTrans.Fk_MemId),
                    new SqlParameter ("@Amount",objWalletTrans.Amount),
                    new SqlParameter ("@Operator",objWalletTrans.Operator),
                    new SqlParameter ("@Number",objWalletTrans.Number),
                    new SqlParameter ("@ActionType",objWalletTrans.ActionType),
                    new SqlParameter ("@Action",objWalletTrans.Action),
                    new SqlParameter ("@TransactionId",objWalletTrans.TransactionNo),
                    new SqlParameter ("@RechargeType",objWalletTrans.RechargeType),
               };
                DataSet dsresult = ExecuteQuery("NewWalletTransactions", para);
                return dsresult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet Transactions_V2(WalletTransaction objWalletTrans)
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter ("@Fk_MemId",objWalletTrans.Fk_MemId),
                    new SqlParameter ("@Amount",objWalletTrans.Amount),
                    new SqlParameter ("@Operator",objWalletTrans.Operator),
                    new SqlParameter ("@Number",objWalletTrans.Number),
                    new SqlParameter ("@ActionType",objWalletTrans.ActionType),
                    new SqlParameter ("@Action",objWalletTrans.Action),
                    new SqlParameter ("@TransactionId",objWalletTrans.TransactionNo),
                    new SqlParameter ("@RechargeType",objWalletTrans.RechargeType),
               };
                DataSet dsresult = ExecuteQuery("NewWalletTransactions", para);
                return dsresult;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public WalletTransactionResponse WalletTransactions(WalletTransaction objWalletTrans)
        {
            try
            {
                WalletTransactionResponse obj = new WalletTransactionResponse();
                DALWalletTransaction _objdal = new DALWalletTransaction();
                DataSet ds = _objdal.Transactions(objWalletTrans);
                try
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["msg"].ToString() == "1")
                        {
                            obj.Response = "Success";
                            obj.WalletDetailsId = ds.Tables[0].Rows[0]["WalletDetailsId"].ToString();
                            obj.BBPSApiIntegrationId = ds.Tables[0].Rows[0]["BBPSApiIntegrationId"].ToString();
                            obj.alltransactionlogId = ds.Tables[0].Rows[0]["alltransactionlogId"].ToString();
                            obj.TransactionId = ds.Tables[0].Rows[0]["TransactionNo"].ToString();
                        }
                        else if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["msg"].ToString() == "0")
                        {
                            obj.Response = "Error";
                        }
                    }
                    else
                    {
                        obj.Response = "Error";
                    }
                }
                catch (Exception ex)
                {
                    obj.Response = "Error";
                }
                return obj;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public WalletTransactionResponse WalletTransactions_V2(WalletTransaction objWalletTrans)
        {
            try
            {
                WalletTransactionResponse obj = new WalletTransactionResponse();
                DALWalletTransaction _objdal = new DALWalletTransaction();
                DataSet ds = _objdal.Transactions(objWalletTrans);
                try
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["msg"].ToString() == "1")
                        {
                            obj.Response = "Success";
                            obj.WalletDetailsId = ds.Tables[0].Rows[0]["WalletDetailsId"].ToString();
                            obj.BBPSApiIntegrationId = ds.Tables[0].Rows[0]["BBPSApiIntegrationId"].ToString();
                            obj.alltransactionlogId = ds.Tables[0].Rows[0]["alltransactionlogId"].ToString();
                            obj.TransactionId = ds.Tables[0].Rows[0]["TransactionNo"].ToString();
                        }
                        else if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["msg"].ToString() == "0")
                        {
                            obj.Response = "Error";
                        }
                    }
                    else
                    {
                        obj.Response = "Error";
                    }
                }
                catch (Exception ex)
                {
                    obj.Response = "Error";
                }
                return obj;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ResponseModel CheckWalletransaction(RequestModel _obj)
        {
            GetWalletResponse obj = new GetWalletResponse();
            try
            {
                CheckWalletStatus objWalletFlag = DALCommon.CustomDeserializeObjectWithDecryptString<CheckWalletStatus>(_obj.body);
                DataSet ds = CheckWallet(objWalletFlag);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.Response = "Success";
                        obj.Status = "Success";
                        obj.Balance = ds.Tables[0].Rows[0]["Balance"].ToString();
                        obj.BalanceAmount= ds.Tables[0].Rows[0]["Balance"].ToString();
                        obj.Status = ds.Tables[0].Rows[0]["Status"].ToString();
                        obj.ValidateToken = "";
                        if (obj.Balance == "")
                        {
                            obj.Balance = "0.00";
                            obj.BalanceAmount = "0.00";
                            obj.Response = "Insufficient Balance";
                            obj.Status = "Error";
                            obj.ValidateToken = "";
                        }
                    }
                    else if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {                      
                        obj.Response = "Insufficient Balance";
                        obj.Status = "Error";
                        obj.Balance = "0.00";
                        obj.BalanceAmount = "0.00";
                        obj.ValidateToken = "";
                    }
                }
                else
                {
                    obj.Balance = "0.00";
                    obj.BalanceAmount = "0.00";
                    obj.Response = "Error";
                    obj.Status = "Error";
                    obj.ValidateToken = "";
                }
            }
            catch (Exception ex)
            {
                obj.Balance = "0.00";
                obj.BalanceAmount = "0.00";
                obj.Response = "Error";
                obj.Status = "Error";
                obj.ValidateToken = "";
            }
            ResponseModel _objResponseModel = new ResponseModel();
            _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString(obj);
            return _objResponseModel;
        }
        public DataSet CheckWallet(CheckWalletStatus _objparam)
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter ("@Fk_MemId",_objparam.Fk_MemId),
                    new SqlParameter ("@Amount",_objparam.Amount),
                    new SqlParameter ("@Number",_objparam.Number)
                };
                DataSet dsresult = ExecuteQuery("CHeckWalletStatus", para);
                return dsresult;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public DataSet CheckCommissionWallet()
        //{
        //    SqlParameter[] para =
        //    {
        //        new SqlParameter ("@Fk_MemId",Fk_MemId),
        //         new SqlParameter ("@Amount",Amount),
        //          new SqlParameter ("@Number",Number)
        //    };
        //    DataSet dsresult = ExecuteQuery("CHeckCommissionWalletStatus", para);
        //    return dsresult;
        //}

    }
}