using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class Login
    {
        
        public string LoginId { get; set; }        
        public string Password { get; set; }
        public string AndroidId { get; set; }
        public string DeviceId { get; set; }
        public string DeviceType { get; set; }
        public string AppVersion { get; set; }
        public string Update { get; set; }
    }
    public class LoginResponse
    {
        public LoginDetails result { get; set; }
        public string message { get; set; }
        public string response { get; set; }
        public int VersionCode { get; set; }

    }

    public class TokenResponseResponse
    {
        public int Flag { get; set; }
        public string message { get; set; }
        public string response { get; set; }
        public string Token { get; set; }

    }
    public class LoginDetails
    {
        public string loginId { get; set; }
        public string profilePic { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string mobile { get; set; }
        public string androidId { get; set; }
        public string deviceId { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string tehsil { get; set; }
        public string email { get; set; }
        public string pincode { get; set; }
        public long memberId { get; set; }
        public string lastLogin { get; set; }
        public int flag { get; set; }
        public string message { get; set; }
        public string token { get; set; }
        public string inviteCode { get; set; }
        public string Status { get; set; }
        public string bankaccount { get; set; }
        public string benIFSC { get; set; }
        public string bankname{ get; set; }
        public bool Iskyc { get; set; }
        public bool IsBusinessId { get; set; }
        public bool IsPasswordChange { get; set; }

        public bool IsRefferandEarn { get; set; }
        public string CampaignEndDate { get; set; }
        public bool IsYouTubeBannerAllow { get; set; }
        public string YoutubeUrl { get; set; }
        public string VerifyStatus { get; set; }

        public string KYCRemarks { get; set; }
        public bool IsRefferUser { get; set; }

        public bool IsUserPin { get; set; }
        public bool FranchiseListAllow { get; set; }
        public bool IsTestimonialAllow { get; set; }
        public int VersionCode { get; set; }
        public int DistrictFranchiseAllow { get; set; }

        public string PerformanceLevel { get; set; }
        public string Recognition { get; set; }
        public string RecognitionPic { get; set; }
        public int PerformanceLevelId { get; set; }

    }
    public class MChkEmailExist
    {
        public int flag { get; set; }
        public string response { get; set; }

    }
    public class ReferalMobile
    {
        public string RefMobile { get; set; }
        public string Email { get; set; }
    }

    public class ResponseReferalMobile
    {
        public int Flag { get; set; }
        public string response { get; set; }
        public string name { get; set; }

    }

    public class ReferalDetails
    {
        public string InviteCode { get; set; }
    }

    public class UserDetailsBYLoginId
    {
        public string response { get; set; }
        public UserProfile userprofile { get; set; }
    }




    public class MGetDetails
    {
        public string InviteCode { get; set; }
    }

    public class ForgotPassword
    {

        public string LoginId { get; set; }

        public string Mobile { get; set; }
    }

    public class ApiResponseForgetPassword
    {

        public string response { get; set; }
        public string message { get; set; }
        public int Flag { get; set; }
        public long fk_memId { get; set; }
    }
    public class ChangePassword
    {

        public string Mobile { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string UpdatedBy { get; set; }

        public string Formname { get; set; }

        public string Fk_MemId { get; set; }
        public string NormalPassword { get;  set; }
    }
    public class APIResponseChangePassword
    {
        public string response { get; set; }
        public int flag { get; set; }
    }
    public class PincodeRequest
    {
        public string stateName { get; set; }
        public string districtName { get; set; }
        public string pinCode { get; set; }
        public string tehsil { get; set; }

    }
    public class PincodeResponse
    {

        public List<Pincode> result { get; set; }
        public string response { get; set; }
    }
    public class Pincode
    {
        public string stateName { get; set; }
        public string districtName { get; set; }
        public string pinCode { get; set; }
        public string tehsil { get; set; }

    }
    public class UpdateProfileResponse
    {
        public string response { get; set; }
        public string message { get; set; }
    }
    public class Imagelist
    {
        public string fk_MemId { get; set; }

        public string action { get; set; }

        public string imageUrl { get; set; }
        public string type { get; set; }
        public string uniqueNo { get; set; }

        public string response { get; set; }
        public string message { get; set; }
    }
    public class clsInputSignOut
    {
        public long FK_MemId { get; set; }
        public string AndroidId { get; set; }
        public string DeviceId { get; set; }
        public string Token { get; set; }

    }


   
}