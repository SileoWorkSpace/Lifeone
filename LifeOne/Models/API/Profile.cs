using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class Profile
    {
       
    }
    public class BankDetail
    {
        public long fk_memId { get; set; }
        public string bankAccName { get; set; }
        public string memberBankName { get; set; }
        public string memberBranch { get; set; }
        public string memberAccNo { get; set; }
        public string bankHolderName { get; set; }
        public string ifscCode { get; set; }       
        public string relationWithApplicant { get; set; }

        public string Pincode { get; set; }
    }
    public class returnResponse
    {
        public string response { get; set; }
        public string message { get; set; }
    }
    public class UserProfile
    {
        public string loginId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string city { get; set; }
        public string tehsil { get; set; }
        public string pinCode { get; set; }
        public string profilepic { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string dob { get; set; }
        public string fatherName { get; set; }
        public string nomineeName { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string marritalStatus { get; set; }
        public string relationWithApplicant { get; set; }
        public string panCardURL { get; set; }
        public string joiningDate { get; set; }
        public string state { get; set; }
        public string bankAccName { get; set; }
        public string memberBankName { get; set; }
        public string memberBranch { get; set; }
        public string memberAccNo { get; set; }
        public string bankHolderName { get; set; }
        public string bankDocumentURL { get; set; }
        public string bankDocumentNo { get; set; }
        public string ifscCode { get; set; }
        public string pancard { get; set; }
        public string aadhar { get; set; }
        public string addressFrontUrl { get; set; }
        public string addressBackUrl { get; set; }
        public string addressProofType { get; set; }
        public string addressProofNo { get; set; }
        public int ?bankStatus { get; set; }
        public int? panStatus { get; set; }
        public int ?addressStatus { get; set; }
        public long fk_MemId { get; set; }
        public string YearOfBirth { get; set; }

    }
    public class ReturnUserProfile
    {
        public string response { get; set; }
        public UserProfile result { get; set; }
    }
   
}