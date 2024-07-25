using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class MGetMembersDetailsByIdForFranchiseRequest
    {
        public string LoginId { get; set; }


        public string PersonName { get; set; }
        public string EmailAddress { get; set; }
        public string Dateofbirth { get; set; }
        public string FatherName { get; set; }
        public int IsApplied { get; set; }
        public string ProfilePic { get; set; }
        public string Cr_PinCode { get; set; }
        public string Cr_Address { get; set; }
        public string Cr_City { get; set; }
        public string Cr_State { get; set; }
        public string Co_Tehsil { get; set; }
        public string Zone { get; set; }
        public string CustAadharNo { get; set; }
        public string CustPanNo { get; set; }
        public string District { get; set; }
        public string Mobile { get; set; }
        public string response { get; set; }
        public string message { get; set; }
        public int Status { get; set; }
        public bool IsFranchise { get; set; }
        public string AssociateName { get; set; }
        public string FranchiseName { get; set; }
        public string Date { get; set; }
        public string Address { get; set; }
        public bool Isdecline { get; set; }
        public bool IsApprove { get; set; }
        public int IsFranchisepaymentApprove { get; set; }
        public string FranchisePaymentRemark { get; set; }
        public string Remark { get; set; }
        public string DepositPaymentDate { get; set; }
        public int FransachiseId { get; set; }        

    }


    public class MBIDRequest
    {
        public string BID { get; set; }
    }

    public class MBIDRequest_V3
    {
        public string BID { get; set; }
        public int KeyId { get; set; }
    }


}