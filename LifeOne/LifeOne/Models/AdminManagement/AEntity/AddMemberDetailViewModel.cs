using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class AddMemberDetailViewModel
    {
        public long FK_MemId { get; set; }
        public long Fk_SponsorId { get; set; }
        public int FK_PerformanceLevelID { get; set; }
        public string LoginId { get; set; }
        public string InviteCode { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FathersName { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string MemberAccNo { get; set; }
        public string MemberBankName { get; set; }
        public string MemberBranch { get; set; }
        public string BankAccName { get; set; }
        public string BankHolderName { get; set; }
        public string IFSCCode { get; set; }
        public string PanCard { get; set; }
        public string AddressProofNo { get; set; }
        public string TempPermanent { get; set; }
        public DateTime DOB { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime PermanentDate { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsBusinessId { get; set; }
        public bool Iskyc { get; set; }
        public bool IsFranchise { get; set; }
        public bool IsFamilyn { get; set; }
    }
}