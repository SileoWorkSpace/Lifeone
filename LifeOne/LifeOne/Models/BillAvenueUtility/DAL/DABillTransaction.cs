using Dapper;
using LifeOne.Models.BillAvenues.Entity;
using LifeOne.Models.BillAvenueUtility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LifeOne.Models.Common;

namespace LifeOne.Models.BillAvenueUtility.DAL
{
    public class DABillTransaction
    {

        public static int SaveBillTransRequest(MBillTransactionStatusResponse _objDetails, MBillTransactionStatus _objrequestdetails)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@RequestId", _objrequestdetails.RequestId);
               
                queryParameters.Add("@ProcId", 4); // For adding on rows trasaction Success updation
             
                //queryParameters.Add("@BillPeriod", _objDetails.billerResponse.billPeriod);
           
                int _iresult = DBHelperDapper.DAAdd("BASaveAllTransactionDetails", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int SaveBillTransResponseError(MBillTransactionStatusResponse _objDetails,
            MBillTransactionStatus _objrequestdetails)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@RequestId", _objDetails.RequestId);
                queryParameters.Add("@BillerId", _objrequestdetails.BillerId);
                queryParameters.Add("@IPAdress", _objrequestdetails.IPAdressAPP);
                queryParameters.Add("@AndroidId", _objrequestdetails.AndroidId);
                queryParameters.Add("@DeviceId", _objrequestdetails.DeviceId);
                queryParameters.Add("@CompanyId", _objrequestdetails.CompanyId);
                queryParameters.Add("@Fk_MemId", _objrequestdetails.Fk_MemID);
                queryParameters.Add("@CreatedBy", _objrequestdetails.Fk_MemID);
                queryParameters.Add("@ProcId", 3); // for insertiong trasaction error updation
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