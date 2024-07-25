using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MFranchiseViewProfile
    {
        public int FranciseID { get; set; }
        public int Fk_UserTypeID { get; set; }
        public string LoginID { get; set; }
        public string Password { get; set; }
        public string NormalPassword { get; set; }
        public string PersonName { get; set; }
        public string EmailAddress { get; set; }
        public string Contact { get; set; }
        public string DateOfBirth { get; set; }
        public string ProfilePic { get; set; }
        public string FatherName { get; set; }
        public string P_Address { get; set; }
        public string P_State { get; set; }
        public string P_City { get; set; }
        public string Cr_Address { get; set; }
        public string Cr_State { get; set; }
        public string Cr_PinCode { get; set; }
        public string Cr_Tehsil { get; set; }
        public string AadharNumber { get; set; }
        public string PanNumber { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string IfscCode { get; set; }
        public string CompanyName { get; set; }
        public string IsBlocked { get; set; }
        public string JoiningDate { get; set; }
        public string P_PinCode { get; set; }
        public string Cr_City { get; set; }
        public int PKFranchiseRegistrationId { get; set; }
        public List<MFranchiseProfilePaymentDetail> FranchisePaymentList { get; set; }
        public List<MFranchiseProductDetail> FranchiseProductList { get; set; }

    }
    public class MFranchiseProfilePaymentDetail
    {
        public decimal Amount { get; set; }
        public string TransactionNo { get; set; }
        public string RequestRemark { get; set; }
        public string FranchisePaymentStatus { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentDate { get; set; }
        public string ApprovelRemark { get; set; }
        public string BranchName { get; set; }


    }
    public class MFranchiseProductDetail
    {
        public string ProductName { get; set; }
        public string AvailableQuantity { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImage { get; set; }
        public decimal MRP { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal PV { get; set; }
        public string ProductType { get; set; }


    }
}