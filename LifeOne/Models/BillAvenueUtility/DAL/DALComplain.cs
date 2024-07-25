using Dapper;
using LifeOne.Models.BillAvenueUtility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LifeOne.Models.Common;


namespace LifeOne.Models.BillAvenueUtility.DAL
{
    public class DALComplain
    {
        public static int SaveComplainRegRequest(MBillComplaintRegistrationResponse _objDetails, MBillComplaintRegistration _objrequestdetails)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@AgentId", _objrequestdetails.AgentId);
                queryParameters.Add("@ComplainType", _objrequestdetails.ComplaintType);
                queryParameters.Add("@ComplaintDesc", _objrequestdetails.ComplaintDesc);
                queryParameters.Add("@ComplaintDisposition", _objrequestdetails.ComplaintDisposition);
                queryParameters.Add("@BillerId", _objrequestdetails.BillerId);
                queryParameters.Add("@ParticipationType", _objrequestdetails.ParticipationType);
                queryParameters.Add("@ServReason", _objrequestdetails.ServReason);
                queryParameters.Add("@TxnRefId", _objrequestdetails.TxnRefId);
                queryParameters.Add("@BillerId", _objrequestdetails.BillerId);
                queryParameters.Add("@ComplaintAssigned", _objDetails.complaintAssigned);
                queryParameters.Add("@ComplaintId", _objDetails.complaintId);
                queryParameters.Add("@ResponseCode", _objDetails.responseCode);
                queryParameters.Add("@ResponseReason", _objDetails.responseReason);
                queryParameters.Add("@CompanyId", _objrequestdetails.CompanyId);
                queryParameters.Add("@Fk_MemId", _objrequestdetails.Fk_MemID);
                queryParameters.Add("@ProcId", 1);
                queryParameters.Add("@CreatedBy", 1);
                int _iresult = DBHelperDapper.DAAdd("BAComplainsReg", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int SaveComplainRegResponse(MBillComplaintRegistrationResponse _objDetails, MBillComplaintRegistration _objrequestdetails)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@ProcId", 2);
                queryParameters.Add("@CreatedBy", 1);
                queryParameters.Add("@AgentId", _objrequestdetails.AgentId);
                queryParameters.Add("@ComplainType", _objrequestdetails.ComplaintType);
                queryParameters.Add("@ComplaintDesc", _objrequestdetails.ComplaintDesc);
                queryParameters.Add("@ComplaintDisposition", _objrequestdetails.ComplaintDisposition);
                queryParameters.Add("@BillerId", _objrequestdetails.BillerId);
                queryParameters.Add("@ParticipationType", _objrequestdetails.ParticipationType);
                queryParameters.Add("@ServReason", _objrequestdetails.ServReason);
                queryParameters.Add("@TxnRefId", _objrequestdetails.TxnRefId);
                queryParameters.Add("@BillerId", _objrequestdetails.BillerId);
                queryParameters.Add("@ResponseCode", _objDetails.responseCode);
                queryParameters.Add("@ResponseReason", _objDetails.responseReason);
                queryParameters.Add("@ErrorMsg", _objDetails.errorInfo.error.errorCode + ' ' + _objDetails.errorInfo.error.errorMessage);
                int _iresult = DBHelperDapper.DAAdd("BAComplainsReg", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int SaveComplainTrackingRequest(MBillComplaintTrackingResponse _objDetails, MBillComplaintTracking _objrequestdetails)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@ComplaintType", _objrequestdetails.complaintType);
                queryParameters.Add("@ComplaintId", _objDetails.complaintId);
                queryParameters.Add("@complaintAssigned", _objDetails.complaintAssigned);
                queryParameters.Add("@ComplaintStatus", _objDetails.complaintStatus);
                queryParameters.Add("@ResponseCode", _objDetails.responseCode);
                queryParameters.Add("@ResponseReason", _objDetails.responseReason);
                queryParameters.Add("@CompanyId", _objrequestdetails.CompanyId);
                queryParameters.Add("@Fk_MemId", _objrequestdetails.Fk_MemID);
                queryParameters.Add("@ProcId", 1);
                queryParameters.Add("@CreatedBy", 1);
                int _iresult = DBHelperDapper.DAAdd("BAComplainTrackingDetails", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int SaveComplainTrackingResponse(MBillComplaintTrackingResponse _objDetails, MBillComplaintTracking _objrequestdetails)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@ProcId", 2);
                queryParameters.Add("@CreatedBy", 1);
                queryParameters.Add("@ComplaintType", _objrequestdetails.complaintType);
                queryParameters.Add("@ResponseCode", _objDetails.responseCode);
                queryParameters.Add("@ResponseReason", _objDetails.responseReason);
                queryParameters.Add("@ErrorMsg", _objDetails.errorInfo.error.errorCode + ' ' + _objDetails.errorInfo.error.errorMessage);
                int _iresult = DBHelperDapper.DAAdd("BAComplainTrackingDetails", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}