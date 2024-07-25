using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AssociateManagement.AssociateDAL;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.BillAvenueUtility.Entity;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateService
{
    public class AssociateAdvancePaymentService
    {
        public List<AdvancePaymentModel> GetAllAdvancePaymentRequest(int AssociateFk_MemId = 0)
        {

            List<AdvancePaymentModel> result = new List<AdvancePaymentModel>();
            try
            {
                result = AssociateAdvancePaymentDAL.GetAllAdvancePaymentRequest(AssociateFk_MemId);
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
        public AddUpdateResponseResponseModel SubmitAdvancePaymentRequest(AdvancePaymentModel req)
        {
            try
            {
                if (req.EMIOption == "Amount")
                {
                    decimal v1 = req.RequestAmt ?? 0;
                    decimal v2 = req.Amount ?? 0;
                    decimal Res = Decimal.Truncate(Decimal.Divide(v1, v2));
                    decimal Remainder = Decimal.Remainder(v1, v2);
                    req.Month = (int)((int)Remainder > 0 ? (int)Res + 1 : (int)Res);
                }
                else if (req.EMIOption == "Month")
                {
                    req.Amount = Math.Round(((req.RequestAmt ?? 0) / req.Month), 2);
                }

                AddUpdateResponseResponseModel result = AssociateAdvancePaymentDAL.SubmitAdvancePaymentRequest(req);
                return result;
            }
            catch (Exception ex)
            {
                return new AddUpdateResponseResponseModel { Status = false, Message = ex.Message };
            }
        }


        public LoanEligibilityModel CheckLoanEligibility(int AssociateFk_MemId = 0)
        {

            LoanEligibilityModel result = new LoanEligibilityModel();
            try
            {
                result = AssociateAdvancePaymentDAL.CheckLoanEligibility(AssociateFk_MemId);
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public AdvancePaymenthistory ViewAdvancePaymenthistory(int ReqId = 0)
        {
            return AdvancePaymentDAL.ViewAdvancePaymenthistory(ReqId);
        }
    }
}