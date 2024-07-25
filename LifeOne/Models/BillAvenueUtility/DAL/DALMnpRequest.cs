using Dapper;
using LifeOne.Models.BillAvenueUtility.Entity;
using LifeOne.Models.BillAvenueUtility.Entity.DMTServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.Common;


namespace LifeOne.Models.BillAvenueUtility.DAL
{
    public class DALMnpRequest
    {
         public static int SaveMNPRequest(MnpResponse _objDetails, MnpRequest _objrequestdetails)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@AgentId", _objrequestdetails.AgentId);
                queryParameters.Add("@MobileNo", _objrequestdetails.MobileNo);
                queryParameters.Add("@ResponseCode", _objDetails.ResponseCode);
                queryParameters.Add("@ResponseReason", _objDetails.ResponseReason);
                queryParameters.Add("@RefId", _objDetails.RefId);
                queryParameters.Add("@PreviousLocation", _objDetails.PreviousLocation);
                queryParameters.Add("@PreviousOperator", _objDetails.PreviousOperator);
                queryParameters.Add("@CurrentLocation", _objDetails.CurrentLocation);
                queryParameters.Add("@CurrentOperator", _objDetails.CurrentOperator);
                queryParameters.Add("@CurrentOptBillerId", _objDetails.CurrentOptBillerId);
                queryParameters.Add("@Ported", _objDetails.Ported);
                queryParameters.Add("@CompanyId", _objrequestdetails.CompanyId);
                queryParameters.Add("@Fk_MemId", _objrequestdetails.Fk_MemID);
                queryParameters.Add("@ProcId", 1);  //
                queryParameters.Add("@CreatedBy", _objrequestdetails.Fk_MemID);
                int _iresult = DBHelperDapper.DAAdd("BAMNPRequestSave", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int SaveMNPResponse(MnpResponse _objDetails, MnpRequest _objrequestdetails)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@ProcId", 2);
                queryParameters.Add("@CreatedBy", _objrequestdetails.Fk_MemID);
                queryParameters.Add("@AgentId", _objrequestdetails.AgentId);
                queryParameters.Add("@MobileNo", _objrequestdetails.MobileNo);
                queryParameters.Add("@ResponseCode", _objDetails.ResponseCode);
                queryParameters.Add("@ResponseReason", _objDetails.ResponseReason);
                queryParameters.Add("@ErrorMsg", _objDetails.errorInfo.error.errorCode+' '+ _objDetails.errorInfo.error.errorMessage);
                int _iresult = DBHelperDapper.DAAdd("BAMNPRequestSave", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}