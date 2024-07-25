using Dapper;
using LifeOne.Models.AdminManagement.ADAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace LifeOne.Models.API
{
    public class FranchiseListForWeb
    {
        public long FranchiseTypeId { get; set; }
        public long PKFranchiseRegistrationId { get; set; }
        public long KeyId { get; set; }
        public string FranchiseName { get; set; }
        public string MobileNo { get; set; }
        public string PinCode { get; set; }
        public string AppliedDate { get; set; }
        public string status { get; set; }
        public long Fk_MemeId { get; set; }
        public decimal Amount { get; set; }
        public string TypeName { get; set; }
        public string StateName { get; set; }
        public string DistrictName { get; set; }
        public string TotalPaidAmount { get; set; }
        public string StatusForCompanion { get; set; }
        public int CompanionStatus { get; set; }


        public List<FranchiseListForWeb> GetFranchise(long Fk_memId)
        {
            try
            {
                string _qry = "Proc_FranchiseListForApp @FkMemId=" + Fk_memId + "";
                List<FranchiseListForWeb> _iresult = DBHelperDapper.DAGetDetailsInList<FranchiseListForWeb>(_qry);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FranchiseListRequestForPaymentHistory> GetFranchisePaymentHistory(long RequestId)
        {
            try
            {
                string _qry = "Proc_GetFranchisePaymentHistory @RequestId=" + RequestId + "";
                List<FranchiseListRequestForPaymentHistory> _iresult = DBHelperDapper.DAGetDetailsInList<FranchiseListRequestForPaymentHistory>(_qry);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class FranchiseListRequest
    {
        public long FK_MemId { get; set; }
        
    }

    public class FranchiseListRequestForPayment
    {
        public long RequestId { get; set; }
    }
    public class FranchiseListResponseApp
    {
    
        public string response { get; set; }
        public string message { get; set; }
        public List<FranchiseListForWeb> FranchiseListForWebLst { get; set; }
    }

    public class FranchiseListResponseAppHistory
    {
        public string response { get; set; }
        public string message { get; set; }
        public List<FranchiseListRequestForPaymentHistory> FranchiseListForWebLst { get; set; }
    }

    public class FranchiseListRequestForPaymentHistory
    {
        public string TransactionNo { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentDate { get; set; }
        public string RequestRemark { get; set; }
        public string isApproved { get; set; }
        public string ApprovedBy { get; set; }
        public string ApproveDate { get; set; }
        public string ApprovelRemark { get; set; }
        public string Amount { get; set; }
    }

}