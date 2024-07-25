using LifeOne.Models.QUtility.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using LifeOne.Models.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.QUtility.DAL
{
    public class DALDTH : DBHelper
    {
        public DataSet DTHRecharge(RequestModel _objRequestModel, List<MDTHReponse> _objResponseModel,   WalletTransactionResponse _wallettranResponse)
        {
            try
            {
                MDTH _param = DALCommon.CustomDeserializeObjectWithDecryptString<MDTH>(_objRequestModel.body);
                DataSet ds = MObileProvider(_param.Provider);
                SqlParameter[] para =
                {
                    new SqlParameter("@MobileNo",_param.Number),
                    new SqlParameter("@AccountType",_param.alltransactionlogId),
                    new SqlParameter("@Amount",_param.Amount),
                    new SqlParameter("@RechargeDate",_objResponseModel[0].DATE),
                    new SqlParameter("@OperatorType",_param.Provider),
                    new SqlParameter("@FK_MemberId",_param.FK_MemId ),
                    new SqlParameter("@Session",_objResponseModel[0].SESSION),
                    new SqlParameter("@Error",_objResponseModel[0].ERROR),
                    new SqlParameter("@Result",_objResponseModel[0].RESULT),
                    new SqlParameter("@ErrorMessage",_objResponseModel[0].ERRMSG),
                    new SqlParameter("@TransactionId",_objResponseModel[0].TRANSID),
                    new SqlParameter("@TransactionStatus",_objResponseModel[0].TRNXSTATUS),
                    new SqlParameter("@AuthCode",_objResponseModel[0].AUTHCODE),
                    new SqlParameter("@GatewayNo",ds.Tables[0].Rows[0]["GatewayNo"].ToString()),
                    new SqlParameter("@TokenNo",_param.ValidateToken),
                    new SqlParameter ("@WalletDetailsId",_wallettranResponse.WalletDetailsId),
                    new SqlParameter ("@BBPSApiIntegrationId",_wallettranResponse.BBPSApiIntegrationId),
                    new SqlParameter ("@alltransactionlogId",_wallettranResponse.alltransactionlogId)

                };
                DataSet dsresult = ExecuteQuery("NewDTHRecharge", para);
                if (dsresult.Tables.Count > 0)
                    //Sending Email here
                    DALSendEmail.SendEmail(_param.FK_MemId, _objResponseModel[0].TRNXSTATUS, "DTH Recharge", "Bag", _objResponseModel[0].DATE, _param.Number,
                   _param.Amount, _objResponseModel[0].TRANSID, _param.Provider);
                return dsresult;
            }
            catch (Exception)
            {
                throw;
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
                DataSet dsresult = ExecuteQuery("GetDTHDetails", para);
                return dsresult;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}