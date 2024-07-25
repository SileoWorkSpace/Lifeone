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
    public class DALTataSky : DBHelper
    {

        public DataSet TakaSkeyIntegration(RequestModel _objRequestModel, List<MTataSkyResponse> _objResponseModel,
            WalletTransactionResponse objResTrans)
        {
            try
            {
                MTataSky _param = DALCommon.CustomDeserializeObjectWithDecryptString<MTataSky>(_objRequestModel.body);
                SqlParameter[] para =
               {
                    new SqlParameter ("@Fk_MemId",_param.FK_MemId),
                    new SqlParameter ("@FName",_param.fName),
                    new SqlParameter ("@LName",_param.lName),
                    new SqlParameter ("@Number",_param.NUMBER),
                    new SqlParameter ("@Amount",_param.Amount),
                    new SqlParameter ("@Type",_param.Type),
                    new SqlParameter ("@ProductId",_param.ProductId),
                    new SqlParameter ("@RegionId",_param.RegionId),
                    new SqlParameter ("@BookingDate",_objResponseModel[0].DATE),
                    new SqlParameter ("@benAddressline1",_param.benAddressline1),
                    new SqlParameter ("@benAddressline2",_param.benAddressline2),
                    new SqlParameter ("@benCity",_param.benCity),
                    new SqlParameter ("@Pin",_param.Pin),
                    new SqlParameter ("@State",_param.State),
                    new SqlParameter ("@Session",_objResponseModel[0].SESSION),
                    new SqlParameter ("@Error",_objResponseModel[0].ERROR),
                    new SqlParameter ("@Result",_objResponseModel[0].RESULT),
                    new SqlParameter ("@ErrorMsg",_objResponseModel[0].ERRMSG),
                    new SqlParameter ("@TokenNo",_param.ValidateToken),

                    new SqlParameter ("@WalletDetailsId",objResTrans.WalletDetailsId),
                    new SqlParameter ("@BBPSApiIntegrationId",objResTrans.BBPSApiIntegrationId),
                    new SqlParameter ("@alltransactionlogId",objResTrans.alltransactionlogId)

                };
                DataSet dsresult = ExecuteQuery("NewTataSkyBooking", para);
                return dsresult;
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}