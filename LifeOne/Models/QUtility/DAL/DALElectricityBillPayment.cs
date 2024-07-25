using LifeOne.Models.QUtility.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using LifeOne.Models.Common;
using System.Web;

namespace LifeOne.Models.QUtility.DAL
{
    public class DALElectricityBillPayment : DBHelper
    {
        public DataSet BBPSIntegration(RequestModel _objRequestModel, List<MElectricityBillPaymentResponse> _objResponseModel, WalletTransactionResponse walletTransactionResponse)
        {
            try
            {
                MElectricityBillPayment _param = DALCommon.CustomDeserializeObjectWithDecryptString<MElectricityBillPayment>(_objRequestModel.body);
                DataSet ds = ElectricityProvider(_param.Provider);
                SqlParameter[] para =
                {
                    new SqlParameter ("@Fk_MemId",_param.FK_MemId),
                    new SqlParameter ("@Operator",_param.Provider),
                    new SqlParameter ("@Number",_param.NUMBER),
                    new SqlParameter ("@Amount",_param.AMOUNT),
                    new SqlParameter ("@PaymentDate",_objResponseModel[0].DATE),
                    new SqlParameter ("@TransactionId",_objResponseModel[0].TRANSID),
                    new SqlParameter ("@TransactionStatus",_objResponseModel[0].TRNXSTATUS),
                    new SqlParameter ("@AuthCode",_objResponseModel[0].AUTHCODE),
                    new SqlParameter ("@ReferenceNumber",_objResponseModel[0].REFERENCENO),
                    new SqlParameter ("@BillerConfirmationNumber",_objResponseModel[0].BILLERCONFIRMATIONNUMBER),
                    new SqlParameter ("@Session",_objResponseModel[0].SESSION),
                    new SqlParameter ("@Error",_objResponseModel[0].ERROR),
                    new SqlParameter ("@Result",_objResponseModel[0].RESULT),
                    new SqlParameter ("@ErrorMsg",_objResponseModel[0].ERRMSG),
                    new SqlParameter ("@Type",_param.Type),
                    new SqlParameter ("@GatewayNo",ds.Tables[0].Rows[0]["GatewayNo"].ToString()),

                    new SqlParameter ("@WalletDetailsId",walletTransactionResponse.WalletDetailsId),
                    new SqlParameter ("@BBPSApiIntegrationId",walletTransactionResponse.BBPSApiIntegrationId),
                    new SqlParameter ("@alltransactionlogId",walletTransactionResponse.alltransactionlogId),

                    new SqlParameter ("@TokenNo",_param.ValidateToken)
                };
                DataSet dsresult = ExecuteQuery("NewBBPSApiIntegration", para);
                if (dsresult.Tables.Count > 0)
                    //Sending Email here
                    DALSendEmail.SendEmail(_param.FK_MemId, _objResponseModel[0].TRNXSTATUS, "Electronic Bill Payment", "Bag", _objResponseModel[0].DATE, _param.NUMBER,
                   _param.AMOUNT, _objResponseModel[0].TRANSID, _param.Provider);
                return dsresult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet ElectricityProvider(string provider)
        {
            try
            {
                SqlParameter[] para =
                {
                 new SqlParameter ("@Provider",provider)
               };
                DataSet dsresult = ExecuteQuery("GetElectricBillPaymentDetails", para);
                return dsresult;
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}