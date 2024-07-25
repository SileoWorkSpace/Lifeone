using Dapper;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.BillAvenueUtility.Entity;
using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateDAL
{
    public class AssociateAdvancePaymentDAL
    {
        public static List<AdvancePaymentModel> GetAllAdvancePaymentRequest(int? Fk_MemId)
        {
            try
            {
                string _qury = "ProAllAdvancePaymentRequest @FK_MID=" + Fk_MemId + "";
                List<AdvancePaymentModel> _iresult = DBHelperDapper.DAGetDetailsInList<AdvancePaymentModel>(_qury).ToList();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static AddUpdateResponseResponseModel SubmitAdvancePaymentRequest(AdvancePaymentModel req)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@FK_MID", req.FK_MID);
                queryParameters.Add("@RequestAmount", req.RequestAmt);  
                queryParameters.Add("@ReturnType", req.ReturnType);
                queryParameters.Add("@EMIOption", req.EMIOption);
                queryParameters.Add("@Month", req.Month);
                queryParameters.Add("@Amount", req.Amount??0);
                queryParameters.Add("@RecoveryDate", req.RecoveryDate);   
                queryParameters.Add("@Remark", req.Remark);
                AddUpdateResponseResponseModel _iresult = DBHelperDapper.DAAddAndReturnModel<AddUpdateResponseResponseModel>("ProAdvancePaymentRequest", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                return new AddUpdateResponseResponseModel { Status = false, Message = ex.Message };
            }
        }


        public static LoanEligibilityModel CheckLoanEligibility(int? Fk_MemId)
        {
            try
            {
                string _qury = "Proc_GetAvarageAmount @FK_Memid=" + Fk_MemId + "";
                LoanEligibilityModel _iresult = DBHelperDapper.DAGetDetails<LoanEligibilityModel>(_qury);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}