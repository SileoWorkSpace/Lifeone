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
    public class DALVerifyPassword:DBHelper
    {


        public ResponseModel DALVerifyPasswordHere(RequestModel _obj)
        {
            MVerifyWalletPasswordResponse obj = new MVerifyWalletPasswordResponse();
            try
            {
                MVerifyWalletPassword objWalletFlag = DALCommon.CustomDeserializeObjectWithDecryptString<MVerifyWalletPassword>(_obj.body);
                DataSet ds = DALVerifyPasswordDetails(objWalletFlag);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.Response = "Success";                        
                        obj.Status = ds.Tables[0].Rows[0]["Status"].ToString();                       
                    }
                    else if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        obj.Response = "Error";
                        obj.Status = ds.Tables[0].Rows[0]["Status"].ToString();
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
            ResponseModel _objResponseModel = new ResponseModel();
            _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString(obj);
            return _objResponseModel;
        }


        public DataSet DALVerifyPasswordDetails(MVerifyWalletPassword _objparam)
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter ("@Fk_MemId",_objparam.Fk_MemId),
                    new SqlParameter ("@Amount",_objparam.Password)                    
                };
                DataSet dsresult = ExecuteQuery("CHeckWalletStatus", para);
                return dsresult;
            }
            catch (Exception ex )
            {
                throw ex;
            }
        }
    }
}