using LifeOne.Models.QUtility.DAL.DInterface;
using LifeOne.Models.QUtility.Entity;
using LifeOne.Models.QUtility.Service;
using Newtonsoft.Json;
using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.QUtility.DAL
{
    public class DALPrepaid : DBHelper, DIPrepaid
    {
        
        public DataSet PrepaidRecharge(RequestModel _objRequestModel, List<PrepaidResponse> _objResponseModel,
            WalletTransactionResponse _wallettranResponse)
        {
            try
            {   
                PrepaidAPI _param = DALCommon.CustomDeserializeObjectWithDecryptString<PrepaidAPI>(_objRequestModel.body);
                DataSet ds = MObileProvider(_param.Provider);
                SqlParameter[] para =
                {
                    new SqlParameter ("@Fk_MemId",_param.FK_MemId),
                    new SqlParameter ("@Operator",_param.Provider),
                    new SqlParameter ("@Mobile",_param.NUMBER),
                    new SqlParameter ("@RechargeAmount",_param.AMOUNT),
                    new SqlParameter ("@RechargeDate",_objResponseModel[0].DATE),
                    new SqlParameter ("@PlanOffer",0),//_param.PlanOffer  discuss latr
                    new SqlParameter ("@Session",_objResponseModel[0].SESSION),
                    new SqlParameter ("@Error",_objResponseModel[0].ERROR),
                    new SqlParameter ("@Result",_objResponseModel[0].RESULT),
                    new SqlParameter ("@ErrorMsg",_objResponseModel[0].ERRMSG),
                    new SqlParameter ("@TransactionId",_objResponseModel[0].TRANSID),
                    new SqlParameter ("@TransactionStatus",_objResponseModel[0].TRNXSTATUS),
                    new SqlParameter ("@AuthCode",_objResponseModel[0].AUTHCODE),
                    new SqlParameter ("@GatewayNo",ds.Tables[0].Rows[0]["GatewayNo"].ToString()),
                    new SqlParameter ("@TokenNo",_param.ValidateToken),
                    new SqlParameter ("@WalletDetailsId",_wallettranResponse.WalletDetailsId),
                    new SqlParameter ("@BBPSApiIntegrationId",_wallettranResponse.BBPSApiIntegrationId),
                    new SqlParameter ("@alltransactionlogId",_wallettranResponse.alltransactionlogId)
                };
                DataSet dsresult = ExecuteQuery("NewPrepaidMobileRecharge", para);
                if (dsresult.Tables.Count > 0)
                 //Sending Email here
                 DALSendEmail.SendEmail(_param.FK_MemId, _objResponseModel[0].TRNXSTATUS, "Prepaid Recharge", "Bag", _objResponseModel[0].DATE, _param.NUMBER,
                _param.AMOUNT, _objResponseModel[0].TRANSID, _param.Provider);
                return dsresult;
            }
            catch (Exception ex )
            {
                throw ex;
            }
        }
        public DataSet MObileProvider(string Provider)
        {
            try
            {
                SqlParameter[] para =
                {
                new SqlParameter ("@Provider",Provider)
                };
                DataSet dsresult = ExecuteQuery("GetProviderDetails", para);
                return dsresult;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}