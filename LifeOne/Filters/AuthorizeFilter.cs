using LifeOne.Models.API;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using AesEncryption;

namespace LifeOne.Filters
{
    public class AuthorizeFilter : AuthorizationFilterAttribute
    {
        Resultresponse objresult = new Resultresponse();
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization != null)
            {
                var authToken = actionContext.Request.Headers
                    .Authorization.Parameter;
               string tokenvalue=JWTToken.ValidateToken(authToken.ToString()) == null ? "" : JWTToken.ValidateToken(authToken.ToString());
               
                if (!string.IsNullOrEmpty(tokenvalue))
                {
                    if(actionContext.Request.Content.Headers.ContentType!=null || actionContext.Request.Method.Method.ToString().ToLower() == "get")
                    {
                        if (actionContext.Request.Method.Method.ToString().ToLower() == "get")
                        {

                        }
                        else
                        {
                            if (actionContext.Request.Content.Headers.ContentType.MediaType.ToString().ToLower() == "application/json"||actionContext.Request.Content.Headers.ContentType.MediaType.ToString().ToLower() == "multipart/form-data")
                            {

                            }
                            else
                            {
                                objresult.response = "error";
                                objresult.exception = "Invalid content type";
                                string result = EncryptResult.EncryptData(objresult);
                                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, new { body = result });
                            }
                        }
                    }
                    else
                    {
                        objresult.response = "error";
                        objresult.exception = "content type missing";
                        string result = EncryptResult.EncryptData(objresult);
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, new { body = result });
                    }
                }
                else
                {

                    objresult.response = "error";
                    objresult.exception = "token expired";
                    string result = EncryptResult.EncryptData(objresult);
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, new { body = result });

                }
            }
            else
            {

                objresult.response = "error";
                objresult.exception = "You are unauthorized to access this resource";
                string result = EncryptResult.EncryptData(objresult);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, new { body = result });


            }
        }
        //public static bool IsAuthorizedUser(string _Username, string _password)
        //{
        //    string password = ConfigurationManager.AppSettings.Get("CashbagPassword");
        //    string userName = ConfigurationManager.AppSettings.Get("CashbagUserName");
        //    return _Username == userName && _password == password;
        //}
    }
    [DataContract]
    class Resultresponse
    {      
        [DataMember]
        public string response { get; set; }
        [DataMember]
        public string exception { get; set; }
    }
    class EncryptResult
    {
        public static string EncryptData(Resultresponse model)
        {
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(Resultresponse));
            ms = new MemoryStream();
            js.WriteObject(ms, model);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(ConfigurationManager.AppSettings["Aeskey"].ToString(), CustData);
            //EncryptedText = ApiEncrypt_Decrypt.EncryptString(ConfigurationManager.AppSettings["Aeskey"].ToString(), CustData);
            return EncryptedText;
        }
    }
}