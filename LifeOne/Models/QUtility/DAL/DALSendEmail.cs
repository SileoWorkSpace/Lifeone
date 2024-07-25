using LifeOne.Models.QUtility.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using LifeOne.Models.Common;
namespace LifeOne.Models.QUtility.DAL
{
    public class DALSendEmail : DBHelper
    {
        public static void SendEmail(string Fk_MemId, string TRNXSTATUS, string _Category, string _DebitedFromAccNo,
            string DATE, string NUMBER, string AMOUNT, string TRANSID, string Provider)
        {
            try
            {
                GetResponse objres = new GetResponse();
                var Email = "";
                var DebitedFromAccNo = _DebitedFromAccNo;// "Bag";
                var UpdatedBalance = "";
                var Category = _Category; //"Prepaid Mobile Recharge";            
                var FirstName = "";
                var TransactionStatus = "";
                objres = GetDataforEmailSend(Fk_MemId);
                if (objres != null)
                {
                    Email = objres.Email;
                    UpdatedBalance = objres.UpdatedBalance;
                    FirstName = objres.FirstName;
                    if (TRNXSTATUS == "7")
                    {
                        TransactionStatus = "Successful";
                    }
                    else if (TRNXSTATUS == "3")
                    {
                        TransactionStatus = "Pending";
                    }
                    else if (TRNXSTATUS == "")
                    {
                        TransactionStatus = "Failed";
                    }
                    else
                    {
                        TransactionStatus = "Failed";
                    }
                    //var result = SendOTPMail(DATE, Email, NUMBER, AMOUNT, TRANSID, TransactionStatus, DebitedFromAccNo, UpdatedBalance, Category, Provider, FirstName);
                   //  string body = PopulateRegisTemplate(DATE, Email, NUMBER, AMOUNT, TRANSID, TransactionStatus, DebitedFromAccNo, UpdatedBalance, Category, Provider, FirstName);
                 //   SendMailUsingGmail1(Email, "FastKarrt", body);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        private static string SendMailUsingGmail1(string email, string type, string message)
        {
            try
            {
                string to = email.ToString();
                string from = "fastkaart.office@gmail.com";
                string authPass = "Fk@251525";
                string subject = type;

                string body = message;
                using (MailMessage mm = new MailMessage(from, to))
                {
                    mm.Subject = subject;
                    mm.Body = body;
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential(from, authPass);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    //smtp.Port = 587;
                    smtp.Port = 587;
                    smtp.Send(mm);                   
                }
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        private static string PopulateRegisTemplate(string DATE, string Email, string NUMBER, string AMOUNT, string TRANSID,
            string TransactionStatus, string DebitedFromAccNo, string UpdatedBalance, string Category, string Provider, 
            string FirstName)
        {
            try
            {
                string body = string.Empty;
                string[] arr = DATE.Split(' ');
                string[] d = arr[0].Split('.');
                string newDate = d[1] + "." + d[0] + "." + d[2] + " " + arr[1];
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/Recharge.html")))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{Date}", newDate);
                body = body.Replace("{Email}", Email);
                body = body.Replace("{MobileNo}", NUMBER);
                body = body.Replace("{Amount}", AMOUNT);
                body = body.Replace("{TxnId}", TRANSID);
                body = body.Replace("{TxnStatus}", TransactionStatus);
                body = body.Replace("{DebitedFromAccNo}", DebitedFromAccNo);
                body = body.Replace("{UpdatedBalance}", UpdatedBalance);
                body = body.Replace("{Category}", Category);
                body = body.Replace("{Provider}", Provider);
                body = body.Replace("{Name}", FirstName);
                return body;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static GetResponse GetDataforEmailSend(string _fk_memid)
        {
            GetResponse obj = new GetResponse();

            DataSet ds = Getdata(_fk_memid);
            try
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["msg"].ToString() == "1")
                    {
                        //Fk_MemId FirstName   UpdatedBalance Email
                        obj.Response = "Success";
                        obj.Fk_MemId = ds.Tables[0].Rows[0]["Fk_MemId"].ToString();
                        obj.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                        obj.UpdatedBalance = ds.Tables[0].Rows[0]["UpdatedBalance"].ToString();
                        obj.Email = ds.Tables[0].Rows[0]["Email"].ToString();

                    }
                    else if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["msg"].ToString() == "0")
                    {
                        obj.Response = "Error";

                    }
                }
                else
                {
                    obj.Response = "Error";
                }
            }
            catch (Exception ex)
            {
                obj.Response = "Error";
            }
            return obj;
        }
        private static DataSet Getdata(string Fk_MemId)
        {
            try
            {
                SqlParameter[] para =
                {
                  new SqlParameter ("@Fk_MemId",Fk_MemId)
                };
                DataSet dsresult = ExecuteQuery("GetdataforEmail", para);
                return dsresult;
            }
            catch (Exception ex )
            {
                throw ex ;
            }
        }

    }
}