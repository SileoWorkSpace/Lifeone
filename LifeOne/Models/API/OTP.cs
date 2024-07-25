using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class OTP
    {
        public long Fk_memId { get; set; }
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
        public string Email { get; set; }
    }
}