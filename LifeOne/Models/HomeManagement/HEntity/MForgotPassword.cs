using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.HomeManagement.HEntity
{
    public class MForgotPassword
    {
        public int ProdID { get; set; }
        public int Flag { get; set; }
        public string Msg { get; set; }
        public int Pk_MemId { get; set; }
        public string UserId { get; set; }
        public string MobileNo { get; set; }
        public string OTP { get; set; }
        public string MemberName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string NormalPass { get; set; }
        
    }


    public class MOTP
    {
        public string OTP { get; set; }
        public string MobileNo { get; set; }
    }
    public class MatchOTPForUserMobile
    {
        public int Flag { get; set; }
        public string response { get; set; }
        public string message { get; set; }
    }
    public class OTP
    {
        public long Pk_UserId { get; set; }
        public string AndroidId { get; set; }
        public string DeviceId { get; set; }
        public int ProcId { get; set; }
        public int Otpno { get; set; }
        public string Mobile { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
        public string Purpose { get; set; }
    }
    public class OTPResponse
    {
        public string response { get; set; }
        public string message { get; set; }
        public string isexpired { get; set; }

        public string OTP { get; set; }
    }

    public class DetailsForOtp
    {
        public int Flag { get; set; }
        public string Msg { get; set; }
        public string Mobile { get; set; }
        public string MemberName { get; set; }
        public string OTP { get; set; }
        public string Purpose { get; set; }
    }
}