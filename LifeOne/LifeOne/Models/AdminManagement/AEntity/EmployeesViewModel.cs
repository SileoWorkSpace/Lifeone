using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class EmployeesViewModel: PagingViewModel
    {
        public Nullable<long> FK_MemId { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string CustAadharNo { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MemberName { get; set; }
        public string SponsorId { get; set; }
        public string Status { get; set; }
        public string SponsorName { get; set; }
        public string Email { get; set; }
        public string state { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public string Tehsil { get; set; }
        public string InviteCode { get; set; }
        public string InvitedBy { get; set; }
        public string joiningDate { get; set; }
        public string Mobile { get; set; }
        public string SponsorMobile { get; set; }
        public string ProfilePic { get; set; }        
        public string Searchby { get; set; }
        public bool IsBusinessId { get; set; }
        public string IsReferralBlock { get; set; }
        public string AndroidId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string DOB { get; set; }
        public List<EmployeesViewModel> EmployeeList { get; set; }
    }
}