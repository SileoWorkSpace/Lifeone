﻿using LifeOne.Models.QUtility.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LifeOne.Models.Common;


namespace LifeOne.Models.QUtility.DAL
{
	public class DALWaterBillPayment : DBHelper
    {
        public DataSet BBPSIntegration(RequestModel _objRequestModel, List<MWaterBillPaymentResponse> _objResponseModel, WalletTransactionResponse objResTrans)
        {
            try
            {
                MBroadBand _param = DALCommon.CustomDeserializeObjectWithDecryptString<MBroadBand>(_objRequestModel.body);
                DataSet ds = MObileSupplier(_param.Provider);
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
                return dsresult;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet MObileSupplier(string _Provider)
        {
            try
            {
                SqlParameter[] para =
                {
                new SqlParameter ("@Provider",_Provider)
                };
                DataSet dsresult = ExecuteQuery("GetWaterBillSupplierDetails", para);
                return dsresult;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}