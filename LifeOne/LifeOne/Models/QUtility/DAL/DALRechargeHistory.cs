using LifeOne.Models.QUtility.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using LifeOne.Models.Common;
namespace LifeOne.Models.QUtility.DAL
{
    public class DALRechargeHistory : DBHelper
    {
        public static ResponseModel DALGetRecentChargeDetails(RequestModel model)
        {
            List<GetRecentList> objList = new List<GetRecentList>();
            RecentResponse obj = new RecentResponse();
            if (model != null)
            {
                try
                {
                    MRechargeHistory _objrecenthisory = DALCommon.CustomDeserializeObjectWithDecryptString<MRechargeHistory>(model.body);
                    DataSet dsresult = RecentActivity(_objrecenthisory);
                    try
                    {
                        if (dsresult != null && dsresult.Tables.Count > 0)
                        {
                            if (dsresult != null && dsresult.Tables.Count > 0 && dsresult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                            {
                                #region Get Address
                                foreach (DataRow row in (dsresult.Tables[0].Rows))
                                {
                                    obj.Response = "Success";
                                    obj.RecentActivity = objList;
                                }
                                #endregion
                                foreach (DataRow row in (dsresult.Tables[0].Rows))
                                {
                                    objList.Add(new GetRecentList
                                    {
                                        MobileNo = row["MobileNo"].ToString(),
                                        Operator = row["Operator"].ToString(),
                                        PaymentDate = row["PaymentDate"].ToString(),
                                        Amount = row["Amount"].ToString(),
                                        Error = row["Error"].ToString(),
                                        Result = row["Result"].ToString(),
                                        TransactionId = row["TransactionId"].ToString(),
                                        TransStatus = row["TransactionStatus"].ToString()
                                    });
                                }
                            }
                            else if (dsresult != null && dsresult.Tables.Count > 0 && dsresult.Tables[0].Rows[0]["Msg"].ToString() == "0")
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
                }
                catch (Exception ex)
                {
                    obj.Response = "Error";
                }
            }
            else
            {
                obj.Response = "Error";
            }
            ResponseModel _objResponseModel = new ResponseModel();
            _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString(obj);
            return _objResponseModel;
        }
        public static DataSet RecentActivity(MRechargeHistory _recenthotory)
        {
            try
            { 
                SqlParameter[] para =
                {
                new SqlParameter ("@Action",_recenthotory.Action),
                new SqlParameter ("@Fk_MemId",_recenthotory.FK_MemID),
                new SqlParameter ("@Type",_recenthotory.Type),

            };
                DataSet dsresult = ExecuteQuery("GetRecentDetails", para);
                return dsresult;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}