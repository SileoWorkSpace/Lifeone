using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LifeOne.Models.HomeManagement.HEntity
{
    public class MLogin
    {
        [Required(ErrorMessage = "Please Enter LoginId")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }
        public string UserType { get; set; }        
        public string OTP { get; set; }
        public bool IsMultiAccount { get; set; }
        public string ProductId { get; set; }
        public string Quantity { get; set; }
        public string Status { get; set; }
    }
    public class LoginResponse
    {
        public string Name { get; set; }
        public string LoginId { get; set; }
        public long SponsorFk_MemId { get; set; }
        public string ProfilePic { get; set; }
        public string RecognitionPic { get; set; }
        public string PerformanceLevel { get; set; }
        public string UserType { get; set; }
        public string Mobile { get; set; }
        public long Fk_MemId { get; set; }
        public string response { get; set; }
        public string IsBusinessId { get; set; }
        public string TopUpLevel { get; set; }
        public bool IsPasswordChange { get; set; }
        public bool IsMultiAccount { get; set; }
        public string FK_ProductID { get; set; }
        public string Recognition { get; set; }
        public int TotalItems { get; set; }
    }
    public class MultiUserIdList
    {
        public string Mobile { get; set; }
        public int FK_MemId { get; set; }
        public string LoginId { get; set; }
        public string _LoginId { get; set; }
        public string Name { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsActive { get; set; }  
        public bool IsBusinessId { get; set; }
        public bool IsPasswordChange { get; set; }
        public List<MultiUserIdList> _UserList { get; set; }
    }
    public class MemberExistOrNotModel
    {
        public bool MemberExistOrNot { get; set; }
        public string CampaignStartDate { get; set; }
    }
}