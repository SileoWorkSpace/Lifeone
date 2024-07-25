using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    //public class MOrderBillingAPI
    //{
    //    public string KaryOnLoginId { get; set; }
    //    public string InvoiceNo { get; set; }
    //    public string InvoiceDate { get; set; }
    //    public string DiscountAmount { get; set; }
    //    public string TaxableAmount { get; set; }
    //    public string GSTAmount { get; set; }
    //    public string TotalAmount { get; set; }
    //    public string TotalPv { get; set; }
    //    public string AirOrbit { get; set; }
    //    public string WaterOrbit { get; set; }
    //    public string SpaceOrbit { get; set; }
    //}


    //public class KaryonOrderSmryItemsAPIViewModel
    //{

    //    public string InvoiceNo { get; set; }
    //    public string ProductCode { get; set; }
    //    public decimal MRP { get; set; }
    //    public decimal DP { get; set; }
    //    public int ProductQty { get; set; }
    //    public decimal TaxableAmount { get; set; }
    //    public decimal GSTAmount { get; set; }
    //    public decimal TotalAmount { get; set; }
    //    public decimal TotalProductPV { get; set; }
    //}

    //public class KaryionOrderResponse
    //{
    //    public int Flag { get; set; }
    //    public string Message { get; set; }

    //}


    public class UpdateUserProfileViewModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string mobile { get; set; }
        public string pancard { get; set; }
        public string aadhar { get; set; }
        public string memberBankName { get; set; }
        public string memberBranch { get; set; }
        public string memberAccNo { get; set; }
        public string bankHolderName { get; set; }
        public string ifscCode { get; set; }
        public string LoginId { get; set; }
        public string PinCode { get; set; }
        public string Token { get; set; }
    }



    public class MSummaryAndOrderItemsInformation : MOrderBillingAPI
    {
        public string xmldata { get; set; }
        public List<KaryonOrderSmryItemsAPIViewModel> CartItems { get; set; }
    }

    public class ApproveDeclineKYCKaryon
    {
        public string LoginId { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string Token { get; set; }

    }


    public class StateMasterListModel
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
    }

    public class DistrictMasterListModel
    {
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
    }

    public class FranchiseBalanceDetailRequestModel
    {
        public long FranchiseId { get; set; }
        public long Fk_MemId { get; set; }
    }
    public class FranchiseBalanceDetailModel
    {
        public decimal CrAmount { get; set; }
        public decimal DrAmount { get; set; }
        public decimal BalAmount { get; set; }
        public string PinCode { get; set; }
        public bool Status { get; set; }
        public string DistrictName { get; set; }
    }

    public class FranchiseCrDetailsReq
    {
        public int Fk_MemId { get; set; }
        public int FK_ReqId { get; set; }
    }
    public class FranchiseCrDrDetails
    {
        public string ProductName { get; set; }
        public int CrQty { get; set; }
        public int DrQty { get; set; }
        public int BalQty { get; set; }
    }
    public class FranchiseCrDrDetailsResponse
    {
        public string response { get; set; }
        public string message { get; set; }
        public List<FranchiseCrDrDetails> FranchiseCrDrDetails { get; set; }
    }

    //public class FranchiseAllowRequestModel
    //{
    //    public long Fk_MemId { get; set; }
    //}

    //public class FranchiseAllowRequestDetailModel
    //{
    //    public string LoginId { get; set; }
    //    public string AssociateName { get; set; }
    //    public string TypeName { get; set; }
    //    public int StateId { get; set; }
    //    public string StateName { get; set; }
    //    public int DistrictId { get; set; }
    //    public string DistrictName { get; set; }
    //    public string CreateDate { get; set; }
    //    public int Flag { get; set; }
    //}
}

