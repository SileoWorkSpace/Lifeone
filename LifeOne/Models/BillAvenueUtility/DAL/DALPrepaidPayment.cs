using Dapper;
using LifeOne.Models.BillAvenueUtility.Entity.PrepaidRecharge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LifeOne.Models.Common;


namespace LifeOne.Models.BillAvenueUtility.DAL
{
    public class DALPrepaidPayment
    {
        public static int SavePrepaidPatmentRequest(MExtBillPayResponse _objDetails, MPrepaidPayRequestAPP _objrequestdetails)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@RequestId", _objrequestdetails.RequestId);
                queryParameters.Add("@ProcId", 4);
                queryParameters.Add("@ErrorMsg", "Success");
                int _iresult = DBHelperDapper.DAAdd("BASaveAllTransactionDetails", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int SavePrepaidPatmentRequestError(MExtBillPayResponse _objDetails, MPrepaidPayRequestAPP _objrequestdetails)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@RequestId", _objrequestdetails.RequestId);
                queryParameters.Add("@ProcId",1);
                queryParameters.Add("@ErrorMsg", _objDetails.errorInfo.error.errorCode + ' ' + _objDetails.errorInfo.error.errorMessage);
                int _iresult = DBHelperDapper.DAAdd("BASaveAllTransactionDetails", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }  
}