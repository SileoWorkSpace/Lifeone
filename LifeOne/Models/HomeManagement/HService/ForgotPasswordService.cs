using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.HomeManagement.HEntity;
using LifeOne.Models.HomeManagement.HDAL;
using LifeOne.Models.API;

namespace LifeOne.Models.HomeManagement.HService
{
    public class ForgotPasswordService
    {
        public static MForgotPassword SendOTP(MForgotPassword model)
        {
            MForgotPassword obj = DALForgotPassword.SendOTP(model);
            return obj;
        }

        public static MForgotPassword SendOTPForEmployee(MForgotPassword model)
        {
            MForgotPassword obj = DALForgotPassword.SendOTPForEmployee(model);
            return obj;
        }
        public static ResultSet MatchOTP(MForgotPassword model)
        {
            ResultSet obj = DALForgotPassword.MatchOTP(model);
            return obj;
        }
        public static ResultSet MatchOTPForEmployee(MForgotPassword model)
        {
            ResultSet obj = DALForgotPassword.MatchOTPForEmployee(model);
            return obj;
        }

        public static ResultSet ForgotPassword(MForgotPassword model)
        {
            ResultSet obj = DALForgotPassword.ForgotPassword(model);
            return obj;
        }
        public static ResultSet SetNewPassword(string UserId,string Password, string ENCPassword)
        {
            ResultSet obj = DALForgotPassword.SetNewPassword(UserId, Password,ENCPassword);
            return obj;
        }
    }
}