﻿using LifeOne.Models.QUtility.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using LifeOne.Models.Common;

namespace LifeOne.Models.QUtility.DAL
{
    public class DALGasBillPayment : DBHelper
    {
        public DataSet BBPSIntegration(RequestModel _objRequestModel, List<MGasBillPaymentResponse> _objResponseModel, WalletTransactionResponse objResTrans)
        {
            try
            {
                MGasBillPayment _param = DALCommon.CustomDeserializeObjectWithDecryptString<MGasBillPayment>(_objRequestModel.body);
                DataSet ds = GasSuplierDetails(_param.Provider);
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
                    new SqlParameter ("@WalletDetailsId",objResTrans.WalletDetailsId),
                    new SqlParameter ("@BBPSApiIntegrationId",objResTrans.BBPSApiIntegrationId),
                    new SqlParameter ("@alltransactionlogId",objResTrans.alltransactionlogId),
                    new SqlParameter ("@TokenNo",_param.ValidateToken)
                };
                DataSet dsresult = ExecuteQuery("NewBBPSApiIntegration", para);
                if (dsresult.Tables.Count > 0)
                //Sending Email here
                DALSendEmail.SendEmail(_param.FK_MemId, _objResponseModel[0].TRNXSTATUS, "Gas Bill Payment", "Bag", _objResponseModel[0].DATE, _param.NUMBER,
                _param.AMOUNT, _objResponseModel[0].TRANSID, _param.Provider);
                return dsresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GasSuplierDetails(string _provider)
        {
            try
            { 
                SqlParameter[] para =
                {
                new SqlParameter ("@Provider",_provider)
                };
                DataSet dsresult = ExecuteQuery("GetGasBillSupplierDetails", para);
                return dsresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}