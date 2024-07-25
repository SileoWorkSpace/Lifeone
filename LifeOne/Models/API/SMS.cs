using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;

namespace LifeOne.Models.API
{
    public class SMS
    {
        static public void SendSMS(string Mobile, string Message)
        {
            try
            {
                string SMSAPI =ConfigurationManager.AppSettings["SMSAPI"].ToString();
                SMSAPI = SMSAPI.Replace("[AND]", "&");
                SMSAPI = SMSAPI.Replace("[MOBILE]", Mobile);
                SMSAPI = SMSAPI.Replace("[MESSAGE]", Message);
                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(SMSAPI));
                HttpWebResponse httpResponse = (HttpWebResponse)(httpReq.GetResponse());
            }
            catch (Exception ex)
            {
            }
        }
        static public void SendSMSTempOTP(string Mobile, string Message, string TempId)
        {
            try
            {
                string SMSAPI = ConfigurationManager.AppSettings["SMSAPI"].ToString();
                SMSAPI = SMSAPI.Replace("[AND]", "&");
                SMSAPI = SMSAPI.Replace("[MOBILE]", Mobile);
                SMSAPI = SMSAPI.Replace("[MESSAGE]", Message);
                SMSAPI = SMSAPI.Replace("[TEMPLATEID]", TempId);
                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(SMSAPI));
                HttpWebResponse httpResponse = (HttpWebResponse)(httpReq.GetResponse());
            }
            catch (Exception ex)
            {
            }
        }
        static public void SendSMSWitmtemp(string Mobile, string Message, string TempId)
        {
            try
            {
                string SMSAPI = ConfigurationManager.AppSettings["SMSAPI"].ToString();
                SMSAPI = SMSAPI.Replace("[AND]", "&");
                SMSAPI = SMSAPI.Replace("[MOBILE]", Mobile);
                SMSAPI = SMSAPI.Replace("[MESSAGE]", Message);
                SMSAPI = SMSAPI.Replace("[TEMPLATEID]", TempId);

                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(SMSAPI));
                HttpWebResponse httpResponse = (HttpWebResponse)(httpReq.GetResponse());
            }
            catch (Exception ex)
            {
            }
        }
        static public string OTPMemberMessage2(string MemberName, string OTP, string purpose)
        {

            string Message = ConfigurationManager.AppSettings["SMS-OTP-Member"].ToString();
            Message = Message.Replace("[MEMBER-NAME]", MemberName);
            Message = Message.Replace("[OTP]", OTP);
          // Message = Message.Replace("[PURPOSE]", purpose);
            return Message;
        }

        static public string PlotHolderRegistrationSMS(string MemberName, string LoginId, string Password)
        {

            string Message = "";
            Message = ConfigurationManager.AppSettings["SMS-PLOTHOLDER-REGISTRATION"].ToString();
            Message = Message.Replace("[MEMBER-NAME]", MemberName);
            Message = Message.Replace("[LOGIN-ID]", LoginId);
            Message = Message.Replace("[PASSWORD]", Password);
            Message = Message.Replace("[WEBSITE]", "lifeonewellness.com");
            return Message;
        }
        static public string ApproveFranchise(string MemberName,string PinCode)
        {

            string Message = "";
            Message = ConfigurationManager.AppSettings["SMS-Approve-Franchise"].ToString();
            Message = Message.Replace("[Member-Name]", MemberName);
            Message = Message.Replace("[pincode]", "("+PinCode+")");
            return Message;
        }
        static public string ApproveFranchiseDistrict(string MemberName, string PinCode)
        {

            string Message = "";
            Message = ConfigurationManager.AppSettings["SMS-Approve-Franchise-district"].ToString();
            Message = Message.Replace("[Member-Name]", MemberName);
            Message = Message.Replace("[District]", "(" + PinCode + ")");
            return Message;
        }


        static public string ApprovePaymentMessage(string MemberName, string Logindid, string password, string pincode)
        {

            string Message = "";
            Message = ConfigurationManager.AppSettings["SMS-Approve-Franchise-Payment"].ToString();
            Message = Message.Replace("[Member-Name]", MemberName);
            Message = Message.Replace("[Password]", password);
            Message = Message.Replace("[LoginId]", Logindid);
            Message = Message.Replace("[pincode]", pincode);
            return Message;
        }
    }
}