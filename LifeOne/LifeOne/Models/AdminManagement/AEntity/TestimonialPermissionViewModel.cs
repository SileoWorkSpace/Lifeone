using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class TestimonialPermissionViewModel
    {
        public long FK_MemId { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
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
        public string isblocked { get; set; }
        public long ToatlRecord { get; set; }
        public int TotalReferal { get; set; }
        public string pageurl { get; set; }
        public string ProfilePic { get; set; }
        public string SearchParam { get; set; }
        public int? Page { get; set; }
        public string Searchby { get; set; }
        public Pager Pager { get; set; }
        public List<MMemberList> MemberListDetail { get; set; }
        public bool IsBusinessId { get; set; }
        public string IsReferralBlock { get; set; }
        public string AndroidId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string DeviceId { get; set; }
        public Nullable<bool> IsAllowTestimonial { get; set; }

        public string Response { get; set; }
        public string Msg { get; set; }
        public long Size { get; set; }
        public List<TestimonialPermissionViewModel> List { get; set; }
    }
}