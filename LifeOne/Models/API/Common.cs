using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Script.Serialization;
using static LifeOne.Models.ShoppingRequest;

namespace LifeOne.Models.API
{
    public class Common
    {
        public static string HITAPI(string APIurl, string body)
        {
            var result = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(APIurl);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

            StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    body = body

                });

                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }
        public DataSet SaveRequestResponse(int OpCode, string Fk_MemId, string Request, string Response, string Type, string OrderId, string PaymentId, string Status)
        {
            try
            {
                SqlParameter[] para = {
                                      new SqlParameter("@Fk_MemId", Fk_MemId),
                                      new SqlParameter("@Request", Request),
                                      new SqlParameter("@Response", Response),
                                      new SqlParameter("@Type", Type),
                                      new SqlParameter("@OrderId", OrderId),
                                      new SqlParameter("@PaymentId", PaymentId),
                                      new SqlParameter("@Status", Status),
                                      new SqlParameter("@OpCode", OpCode),
                                  };
                DataSet ds = Connection.ExecuteQuery("SaveResponseLog", para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void SendEmailByAPI(string To, string Cc, string Bcc, string Subject, string msg, Stream stream, string filename)
        {
            string mailbody = "";
            mailbody = @"<html><body><table width='100%' style='text-align:left'>";
            mailbody += @"<tr><td> " + msg + " </td></tr>";
            mailbody += @"<tr><td> </td></tr>";
            mailbody += @"<tr><td> </td></tr>";
            mailbody += @"<tr><td>Regards,</td></tr>";
            mailbody += @"<tr><td>CashBag Team</td></tr>";
            mailbody += @"</table></body></html>";
            #region Begin New
            //var fromAddress = new MailAddress("support@quaeretech.com", "Optymystix");
            var fromAddress = new MailAddress("do-not-reply@LifeOne.com", "LifeOne");
            var toAddress = new MailAddress(To, "");
            //  string[] ToMuliId = ToMailId.Split(';');
            var smtp = new SmtpClient
            {
                Host = "email-smtp.ap-southeast-1.amazonaws.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("AKIAW4X2ZNUKGKHTIPUI", "BLrQYP/GvQBIRRO0ZREIzjeGZbd2pRH3Y7ghiWfpxV3n")
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {

                IsBodyHtml = true,
                Subject = Subject,
                Body = mailbody
            })

            {
                if (stream != null)
                {
                    string fileName = Path.GetFileName(filename);
                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(filename);
                    //message.Attachments.Add(new Attachment(stream, fileName));
                    message.Attachments.Add(attachment);
                }
                if (!string.IsNullOrEmpty(Cc))
                {
                    foreach (string email in Cc.Split(';'))
                    {
                        message.CC.Add(new MailAddress(email));
                    }
                }
                if (!string.IsNullOrEmpty(Bcc))
                {
                    foreach (string email in Bcc.Split(';'))
                    {
                        message.Bcc.Add(new MailAddress(email));
                    }
                }
                smtp.Send(message);
            }
            #endregion
        }
        public class CashFreeCreditentials
        {
            public static string xclientid = "194233de3be4eab99189651b1c332491";
            public static string xclientsecret = "34bcf21a58ceb2da14661b03a60c3749ad574579";
            public static string xapiversion = "2022-01-01";
        }
        public class APIURL
        {
            public static string CreateOrderCashFreeTesting = "https://sandbox.cashfree.com/pg/orders";
            public static string CreateOrderCashFree = "https://sandbox.cashfree.com/pg/orders";
            public static string CreateOrderForShipping = "https://apiv2.shiprocket.in/v1/external/orders/create/adhoc";
            public static string Authorization = "https://apiv2.shiprocket.in/v1/external/auth/login";


            #region Piyush
            public static string CreateOrderRazorpay = "https://localhost:44380/api/Razorpay/CreateRazorPayOrder";
            #endregion
        }
        public static void SendEmailByAPICommon(string To, string Cc, string Bcc, string Subject, string msg, string filename)
        {
            string mailbody = "";
            mailbody = @"<html> <body style ='padding:0;margin:0auto;'> <center><div> <table cellpadding = '0' cellspacing = '0' style = 'border: 0; background:#fff url(bg-top-auto-reply.png); background-repeat:repeat-x; width:100%; font-family:Arial;' >";
            mailbody += @" <tr><td style='padding:50px 0;' align = 'center'><table cellpadding='0' cellspacing='0' style ='border:0;width:580px; border-radius:5px;background:#fff;box-shadow: 0.07px 3.999px 7.44px 0.56px rgba(0, 0, 0, 0.31); overflow:hidden;' >";
            mailbody += @"<tr> <td><table cellpadding = '0' cellspacing ='0' style ='border:0;background:#fff;padding:20px;'><tr><td align='center'><br><br><br><img src ='http://LifeOne.com/Content/assests/images/logo.png' alt = '' style='width: 150px;'><br><br><br>";
            mailbody += @"</td></tr><tr><td><br><p> Greetings for the day. Hope you are doing well.</p><p> Thank you for contacting LifeOne, we will connect with you soon.</p><p>We are working on your query and will revert back to you within 72 working hours.</p>";
            mailbody += @"<p>" + Subject + "</p><p>" + msg + "</p>";
            mailbody += @"<p>This is an auto generated mail.Please do not reply to the same.</p><p>Customer Care Team.</p><p> 011 - 23510379, 1800120000044 <br> Email: support @LifeOne.co.in <br> Working Hours - 9:30 to 6:30 pm<br>(Except Sunday and on official holidays) </p><br><br><br></td> </tr></table></td></tr>";
            mailbody += @"<tr><td style='height:20px;background:#f8b000;'></td></tr><tr><td style= 'height:10px;background:#fe5e00;' ></td></tr></table></td></tr></table></div></center></body></html>";
            #region Begin New
            var fromAddress = new MailAddress("do-not-reply@LifeOne.com", "LifeOne");
            var toAddress = new MailAddress(To, To);
            //  string[] ToMuliId = ToMailId.Split(';');
            var smtp = new SmtpClient
            {
                Host = "email-smtp.ap-southeast-1.amazonaws.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("AKIAW4X2ZNUKGKHTIPUI", "BLrQYP/GvQBIRRO0ZREIzjeGZbd2pRH3Y7ghiWfpxV3n")
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {

                IsBodyHtml = true,
                Subject = Subject,
                Body = mailbody
            })

            {

                if (!string.IsNullOrEmpty(Cc))
                {
                    foreach (string email in Cc.Split(';'))
                    {
                        message.CC.Add(new MailAddress(email));
                    }
                }
                if (!string.IsNullOrEmpty(Bcc))
                {
                    foreach (string email in Bcc.Split(';'))
                    {
                        message.Bcc.Add(new MailAddress(email));
                    }
                }
                smtp.Send(message);
            }
            #endregion
        }

        public static void SendEmailByAPICommonVerification(string To, string Name, string LoginId, string Password)
        {
            string mailbody = "";
            mailbody = @"<html><body style='margin:0px; padding:0px;background:#e4f1f0;'><div style ='padding:20px 0;'><center><div style='max-width:380px;margin:0px auto; width:100%;'><table style='width:100%;' cellpadding='0' cellspacing= '0'>";
            mailbody += @" <tr><td style='text-align:center;padding-bottom:10px;'><img src='http://LifeOne.com/Content/assests/images/logo.png' alt ='logo' style='width:120px;'></td></tr></table></div>";
            mailbody += @"<div style='max-width:380px;margin:0px auto;font-family:Arial;font-size:14px;background:#f3f3f3;box-shadow:2px 6px 15px 0 rgba(69,65,78,.1);'><div ><table style= 'width:100%;' cellpadding = '0' cellspacing = '0'><tr><td style='text-align:center;'><img src ='http://LifeOne.com/Content/assests/images/banner.jpg' alt ='' style= 'width:100%;'></td ></tr>";
            mailbody += @"<tr><td style='text-align:center;'><h1 style='color:#f8b200; margin-bottom:0; font-size:30px;'>Congratulation!! </h1></td></tr><tr><td style='text-align:center;'><span style='height:2px;background:#fe5e00;width:30px;display:inline-block;margin:0 auto 3px;'></span><span style='height:2px;background:#fe5e00;width:100px;display:inline-block;margin:0 auto 3px;'></span><span style='height:2px;background:#fe5e00;width:30px;display:inline-block;margin:0 auto 3px;'></span></td></tr>";
            mailbody += @"<tr><td style='padding:15px 15px 10px 15px;text-align:center; color:#fe5e00;'><strong>Dear " + Name + "</strong></td></tr><tr><td style= 'line-height:24px;text-align:center;padding:5px 15px; color:#878787;'> You have successfully registered with LifeOne.</td></tr>";
            mailbody += @"<tr><td style='line-height:24px;text-align:center;padding:5px 15px;color:#878787;'> Your details given below:-<td></tr><tr><td style='line-height:24px;text-align:center;color:#fe5e00;padding:5px 15px;'><strong>Login Id -" + LoginId + "</strong></td ></tr>";
            mailbody += @"<tr><td style='line-height:24px;text-align:center;color:#fe5e00;padding:5px 15px;'><strong> Password - " + Password + " </strong></td></tr><tr><td style='line-height:24px;text-align:center;color:#fe5e00;padding:5px 15px;'><strong> Transaction Password - " + Password + " </strong></td></tr >";
            mailbody += @"<tr><td style ='line-height:24px;text-align:center;color:#878787;padding-bottom:20px;'>For login click here <strong><a href= 'http://LifeOne.com/' target='_blank' style='color:#fe5e00;text-decoration:none;'>LifeOne.com</a></strong></td></tr></table></div>";
            mailbody += @"<div style='background:#f09601;padding:1px 0 20px;'><ul style='list-style-type: none;text-align:center;padding:0;'><li style='display:inline-block;'><a href='#' target='_blank'><img src='http://LifeOne.com/Content/assests/images/fb-img.jpg' alt = '' style='width:50px;'></a></li>";
            mailbody += @"<li style='display:inline-block;'><a href='#' target='_blank'><img src='http://LifeOne.com/Content/assests/images/twitter-img.jpg' alt= '' style='width:50px;'></a></li><li style='display:inline-block;'><a href= '#' target='_blank'><img src='http://LifeOne.com/Content/assests/images/linkedin-img.jpg' alt='' style= 'width:50px;'></a></li>";
            mailbody += @"<li style='display:inline-block;'><a href='#' target= '_blank'><img src='http://LifeOne.com/Content/assests/images/insta-img.jpg' alt='' style='width:50px;'></a></li></ul>";
            mailbody += @"<ul style='list-style-type:none;text-align:center;padding:0;'><li style='display:inline-block;padding:0 10px;'><a href='#' target = '_blank' style='color:#fff; text-decoration:none;' >Privacy Policy</a></li><li style='display:inline-block; padding:0 10px;'><a href='#' target='_blank' style='color:#fff;text-decoration:none;'>Terms & Conditions</a></li></ul>";
            mailbody += @"<p style='font-size:14px;text-align:center;color:#fff;'>Copyright © 2020 LifeOne.All rights reserved.</p></div></div></center></div></body></html>";
            #region Begin New
            var fromAddress = new MailAddress("do-not-reply@LifeOne.com", "LifeOne");
            var toAddress = new MailAddress(To, To);
            //  string[] ToMuliId = ToMailId.Split(';');
            var smtp = new SmtpClient
            {
                Host = "email-smtp.ap-southeast-1.amazonaws.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("AKIAW4X2ZNUKGKHTIPUI", "BLrQYP/GvQBIRRO0ZREIzjeGZbd2pRH3Y7ghiWfpxV3n")
                //587 Port- / 

            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                IsBodyHtml = true,
                Subject = "Registration",
                Body = mailbody
            })
            {

                smtp.Send(message);
            }
            #endregion
        }


        public static void _SendEmailByAPICommonVerification(string To, string Name, string LoginId, string Password)
        {
            string mailbody = "";
            mailbody = @"<html><body style='margin:0px; padding:0px;background:#e4f1f0;'><div style ='padding:20px 0;'><center><div style='max-width:380px;margin:0px auto; width:100%;'><table style='width:100%;' cellpadding='0' cellspacing= '0'>";
            mailbody += @" <tr><td style='text-align:center;padding-bottom:10px;'><img src='http://familynbusiness.com/Content/assests/images/logo.png' alt ='logo' style='width:120px;'></td></tr></table></div>";
            mailbody += @"<div style='max-width:380px;margin:0px auto;font-family:Arial;font-size:14px;background:#f3f3f3;box-shadow:2px 6px 15px 0 rgba(69,65,78,.1);'><div ><table style= 'width:100%;' cellpadding = '0' cellspacing = '0'><tr><td style='text-align:center;'><img src ='http://familynbusiness.com/Content/assests/images/banner.jpg' alt ='' style= 'width:100%;'></td ></tr>";
            mailbody += @"<tr><td style='text-align:center;'><h1 style='color:#f8b200; margin-bottom:0; font-size:30px;'>Congratulation!! </h1></td></tr><tr><td style='text-align:center;'><span style='height:2px;background:#fe5e00;width:30px;display:inline-block;margin:0 auto 3px;'></span><span style='height:2px;background:#fe5e00;width:100px;display:inline-block;margin:0 auto 3px;'></span><span style='height:2px;background:#fe5e00;width:30px;display:inline-block;margin:0 auto 3px;'></span></td></tr>";
            mailbody += @"<tr><td style='padding:15px 15px 10px 15px;text-align:center; color:#fe5e00;'><strong>Dear " + Name + "</strong></td></tr><tr><td style= 'line-height:24px;text-align:center;padding:5px 15px; color:#878787;'>Your Franchise request has been approved. Please use the below details for login.</td></tr>";
            mailbody += @"<tr><td style='line-height:24px;text-align:center;padding:5px 15px;color:#878787;'> Your details given below:-<td></tr><tr><td style='line-height:24px;text-align:center;color:#fe5e00;padding:5px 15px;'><strong>Login Id -" + LoginId + "</strong></td ></tr>";
            mailbody += @"<tr><td style='line-height:24px;text-align:center;color:#fe5e00;padding:5px 15px;'><strong>Password -" + Password + "</strong></td ></tr>";

            mailbody += @"<tr><td style ='line-height:24px;text-align:center;color:#878787;padding-bottom:20px;'>For login click here <strong><a href= 'http://familynbusiness.com' target='_blank' style='color:#fe5e00;text-decoration:none;'>familynbusiness.com</a></strong></td></tr></table></div>";
            mailbody += @"<div style='background:#f09601;padding:1px 0 20px;'><ul style='list-style-type: none;text-align:center;padding:0;'><li style='display:inline-block;'><a href='#' target='_blank'><img src='http://familynbusiness.com/Content/assests/images/fb-img.jpg' alt = '' style='width:50px;'></a></li>";
            mailbody += @"<li style='display:inline-block;'><a href='#' target='_blank'><img src='http://familynbusiness.com/Content/assests/images/twitter-img.jpg' alt= '' style='width:50px;'></a></li><li style='display:inline-block;'><a href= '#' target='_blank'><img src='http://familynbusiness.com/Content/assests/images/linkedin-img.jpg' alt='' style= 'width:50px;'></a></li>";
            mailbody += @"<li style='display:inline-block;'><a href='#' target= '_blank'><img src='http://familynbusiness.com/Content/assests/images/insta-img.jpg' alt='' style='width:50px;'></a></li></ul>";
            mailbody += @"<ul style='list-style-type:none;text-align:center;padding:0;'><li style='display:inline-block;padding:0 10px;'><a href='#' target = '_blank' style='color:#fff; text-decoration:none;' >Privacy Policy</a></li><li style='display:inline-block; padding:0 10px;'><a href='#' target='_blank' style='color:#fff;text-decoration:none;'>Terms & Conditions</a></li></ul>";
            mailbody += @"<p style='font-size:14px;text-align:center;color:#fff;'>Copyright © 2020 LifeOne.All rights reserved.</p></div></div></center></div></body></html>";
            #region Begin New
            var fromAddress = new MailAddress("do-not-reply@LifeOne.com", "LifeOne");
            var toAddress = new MailAddress(To, To);
            //  string[] ToMuliId = ToMailId.Split(';');
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(fromAddress.Address, "Pune123@")
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                IsBodyHtml = true,
                Subject = "Franchise Request Approval",
                Body = mailbody
            })
            {
                smtp.Send(message);
            }
            #endregion
        }

        public static string SendMailUsingGmail1(string email, string type, string message)
        {
            try
            {

                string to = email.ToString();
                string from = "quaeretechnologies@gmail.com";
                string authPass = "Maverick5181@";
                string subject = type;
                string body = message;
                using (MailMessage mm = new MailMessage(from, to))
                {
                    mm.Subject = subject;
                    mm.Body = body;
                    //if (fuAttachment.HasFile)
                    //{
                    //    foreach (HttpPostedFile file in fuAttachment.PostedFiles)
                    //    {
                    //        string fileName = Path.GetFileName(file.FileName);
                    //        file.SaveAs(Server.MapPath("~/Content/Uploads/") + fileName);
                    //        mm.Attachments.Add(new Attachment(file.InputStream, fileName));
                    //    }
                    //}
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential(from, authPass);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                    return "Success";

                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }



        public static string SendEmailAirlineTemplate(string Name, int bookingId, string CheckInDate, string CheckOutDate, string HotelName, string CityName,
             string amount, string HotelCode, int adult, int child)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/HotelBooking.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Name}", Name);
            body = body.Replace("{bookingid}", bookingId.ToString());
            body = body.Replace("{checkindate}", CheckInDate);
            body = body.Replace("{checkoutdate}", CheckOutDate);
            body = body.Replace("{hotelname}", HotelName);
            body = body.Replace("{hotelcode}", HotelCode);
            body = body.Replace("{cityname}", CityName);
            body = body.Replace("{TotalAmount}", amount);
            body = body.Replace("{Adult}", adult.ToString()); ;
            body = body.Replace("{Child}", child.ToString());
            return body;
        }

        public static string SendMailHotelBooking(string Name, int bookingId, string CheckInDate, string CheckOutDate, string HotelName, string CityName,
            string CustmerEmail, string amount, string HotelCode, int adult, int child)
        {
            string body = SendEmailAirlineTemplate(Name, bookingId, CheckInDate, CheckOutDate, HotelName, CityName, amount, HotelCode, adult, child);
            string result = SendMailUsingGmail1(CustmerEmail, "Hotel Booking Details", body);
            return result;
        }
        public static string GenerateRandom()
        {
            Random r = new Random();
            string s = "";
            for (int i = 0; i < 6; i++)
            {
                s = string.Concat(s, r.Next(10).ToString());
            }
            return s;
        }
        public static string ConvertToSystemDate(string InputDate, string InputFormat)
        {
            string[] DatePart = InputDate.Split(new string[] { "-", @"/" }, StringSplitOptions.None);

            string DateString;
            if (InputFormat == "dd-MMM-yyyy" || InputFormat == "dd/MMM/yyyy" || InputFormat == "dd/MM/yyyy" || InputFormat == "dd-MM-yyyy" || InputFormat == "DD/MM/YYYY" || InputFormat == "dd/mm/yyyy")
            {
                string Day = DatePart[0];
                string Month = DatePart[1];
                string Year = DatePart[2];

                if (Month.Length > 2)
                    DateString = InputDate;
                else
                    DateString = Year + "-" + Month + "-" + Day;
            }
            else if (InputFormat == "MM/dd/yyyy" || InputFormat == "MM-dd-yyyy")
            {
                DateString = InputDate;
            }
            else
            {
                throw new Exception("Invalid Date");
            }

            try
            {
                //Dt = DateTime.Parse(DateString);
                //return Dt.ToString("MM/dd/yyyy");
                return DateString;
            }
            catch
            {
                throw new Exception("Invalid Date");
            }
        }
    }


    public class CommonJsonReponse
    {
        public int totalCount { get; set; }
        public string response { get; set; }
        public string message { get; set; }
        public long TransId { get; set; }
        public string OrderStatus { get; set; }
        public long orderId { get; set; }
        public string OrderNo { get; set; }
    }

    public class DownLineJsonReponse
    {

        public string response { get; set; }
        public long totalCount { get; set; }
        public string message { get; set; }

    }
    public class _RechargeApiResponseModel
    {
        public string response { get; set; }
        public string message { get; set; }
        public bool Status { get; set; }
    }
 
}