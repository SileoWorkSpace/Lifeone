using Dapper;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class AdvancePaymentDAL
    {
        public static List<AdvancePaymentModel> GetAllAdvancePaymentRequest(CommonRequestModel req)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@FromDate", req.FromDate);
                queryParameters.Add("@ToDate", req.ToDate);
                queryParameters.Add("@LoginId", req.LoginId);
                queryParameters.Add("@Status", req.Status);
                queryParameters.Add("@Name", req.Name);
                queryParameters.Add("@Mobile", req.Mobile);
                queryParameters.Add("@Email", req.Email);
                queryParameters.Add("@PageNo", req.Page);
                queryParameters.Add("@PageSize", SessionManager.Size);
                List<AdvancePaymentModel> _iresult = DBHelperDapper.DAAddAndReturnModelList<AdvancePaymentModel>("ProGetAllAdvancePayment", queryParameters).ToList();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static AddUpdateResponseResponseModel ApproveRequest(string Action, string Narration, int ReqId = 0)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Action", Action);
                queryParameters.Add("@Narration", Narration);
                queryParameters.Add("@ReqId", ReqId);
                queryParameters.Add("@Fk_MemId", SessionManager.Fk_MemId);
                AddUpdateResponseResponseModel _iresult = DBHelperDapper.DAAddAndReturnModel<AddUpdateResponseResponseModel>("ProApproveAdvancePaymentRequest", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static MSimpleString SaveAdvancePaymentDetails(AdvancePaymentApprovalDetails _data)
        {
            try
            {
               
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Fk_RFId", _data.RequestId);
                queryParameters.Add("@TransactionNo ", _data.ChequeDDNo);
                queryParameters.Add("@PaymentStatus ", "Success");
                queryParameters.Add("@PayMode ", _data.PaymentMode);
                queryParameters.Add("@BankName ", _data.BankName );
                queryParameters.Add("@ReciptImage ", _data.PaymentSlip);
                queryParameters.Add("@PaymentTypeId ",1 );
                queryParameters.Add("@PaymentDate ", _data.ChequeDDDate);
                queryParameters.Add("@AdPaymentdate ", _data.PaymentDate);
                queryParameters.Add("@PaymentAmount ", _data.Amount);
                queryParameters.Add("@CreatedBy", SessionManager.Fk_MemId);
                MSimpleString _iresult = DBHelperDapper.DAAddAndReturnModel<MSimpleString>("Proc_InserAdvPaymentHistory", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static AdvancePaymenthistory ViewAdvancePaymenthistory(int Req = 0)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@Fk_RFId", Req);            
            AdvancePaymenthistory _iresult = DBHelperDapper.DAAddAndReturnModel<AdvancePaymenthistory>("Proc_GetAdvancePaymentDetail", queryParameters);
            return _iresult;
        }
        
    }
}