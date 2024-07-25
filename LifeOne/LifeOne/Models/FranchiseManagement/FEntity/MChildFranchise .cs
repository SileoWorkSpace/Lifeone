using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.FranchiseManagement.FEntity
{
    public class MChildFranchise
    {
        public string PKFranchiseRegistrationId { get; set; }
        public string FK_UserTypeID { get; set; }
        public string Fk_ParentID { get; set; }
        public string FranchiseType { get; set; }
        public string LoginID { get; set; }
        public string Password { get; set; }
        public string NormalPassword { get; set; }
        public string PersonName { get; set; }
        public string EmailAddress { get; set; }
        public string Contact { get; set; }
        public string Dateofbirth { get; set; }
        public string DisplayDOB { get; set; }
        public string FatherName { get; set; }
        public string P_Address { get; set; }
        public string P_PinCode { get; set; }
        public string P_State { get; set; }
        public string P_City { get; set; }
        public string P_Tehsil { get; set; }
        public string Cr_Address { get; set; }
        public string Cr_PinCode { get; set; }
        public string Cr_State { get; set; }
        public string Cr_City { get; set; }
        public string Cr_Tehsil { get; set; }
        public string AadharNumber { get; set; }
        public string AadharImage { get; set; }
        public string PanNumber { get; set; }
        public string PanImage { get; set; }
        public string AccountHolder { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string IfscCode { get; set; }
        public string CompanyName { get; set; }
        public string Director1 { get; set; }
        public string Director2 { get; set; }
        public string Director3 { get; set; }
        public string Co_Contact { get; set; }
        public string Co_EmailAddress { get; set; }
        public string Co_GstNumber { get; set; }
        public string Co_PanNumber { get; set; }
        public string Co_PanImage { get; set; }
        public string Co_Address { get; set; }
        public string Co_PinCode { get; set; }
        public string Co_State { get; set; }
        public string Co_City { get; set; }
        public string Co_Tehsil { get; set; }
        public string CreatedBy { get; set; }
        public bool Issame { get; set; }
        public string IsBlocked { get; set; }
        public List<MChildFranchise> FranchiseMasterList { get; set; }
    }

    public class ChildFranchiseResponseMaster
    {
        public string Flag { get; set; }
        public string Msg { get; set; }
        public string TransId { get; set; }
    }

    public class MCustomerWalletBalnce
    {
        public string Balance { get; set; }
    }
}