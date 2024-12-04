
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LifeOne.Models.API.DAL;
using System.Configuration;
using AesEncryption;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Web.Security;
using System.Data;
using LifeOne.Models.API;
using LifeOne.Models.QUtility.Entity;
using LifeOne.Models.HomeManagement.HService;
//using LifeOne.Models.HomeManagement.HEntity;

namespace LifeOne.Controllers
{
    public class CustomerController : ApiController
    {
        CustomerDb objcusdb = new CustomerDb();
        string Aeskey = ConfigurationManager.AppSettings["Aeskey"].ToString();
        #region registration by referral code
        [HttpPost]
        public ResponseModel Registration(RequestModel model)
        {
            try
            {
                ResultSet res = new ResultSet();
                RegistrationResponse objresponse = new RegistrationResponse();
                ResponseModel returnResponse = new ResponseModel();
                Registration modelRequest = new Registration();
                string EncryptedText = "";
                try
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    modelRequest = JsonConvert.DeserializeObject<Registration>(dcdata);
                    string password = modelRequest.Password;
                    modelRequest.Normalpassword = modelRequest.Password;
                    modelRequest.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(modelRequest.Password, "md5");
                    res = objcusdb.RegistrationByReferral(modelRequest);
                    if (res != null)
                    {
                        if (res.Flag == 1)
                        {
                            objresponse.response = "success";
                            objresponse.message = "success";
                            objresponse.Iskyc = res.Iskyc;

                            string message = SMS.PlotHolderRegistrationSMS(modelRequest.FirstName + " " + modelRequest.LastName, modelRequest.MobileNo, modelRequest.Normalpassword);
                            SMS.SendSMS(modelRequest.MobileNo, message);

                            //if (modelRequest.Email != null && modelRequest.Email != "")
                            //{
                            //    LifeOne.Models.API.Common.SendEmailByAPICommonVerification(modelRequest.Email, modelRequest.FirstName + " " + modelRequest.LastName, modelRequest.MobileNo, modelRequest.Normalpassword);
                            //}
                        }
                        else
                        {
                            objresponse.response = "error";
                            objresponse.message = res.Msg;
                        }
                    }
                    else
                    {
                        objresponse.response = "error";
                        objresponse.message = "";
                    }
                }
                catch (Exception ex)
                {
                    objresponse.response = "error";
                    objresponse.message = ex.Message;

                }
                string CustData = "";
                DataContractJsonSerializer js;
                MemoryStream ms;
                js = new DataContractJsonSerializer(typeof(RegistrationResponse));
                ms = new MemoryStream();
                js.WriteObject(ms, objresponse);
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                CustData = sr.ReadToEnd();
                sr.Close();
                ms.Close();
                EncryptedText = Crypto.Encryption(Aeskey, CustData);
                returnResponse.body = EncryptedText;
                return returnResponse;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //1 add pin, 2 Verify Pin, 3 Get PIn detaisl  (Prodt id )
        public ResponseModel UserPinManagment(RequestModel model)
        {
            try
            {
                ResultSet res = new ResultSet();
                MPinManagementResponse objresponse = new MPinManagementResponse();
                ResponseModel returnResponse = new ResponseModel();
                MPinManagement modelRequest = new MPinManagement();
                string EncryptedText = "";
                try
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    modelRequest = JsonConvert.DeserializeObject<MPinManagement>(dcdata);
                    res = objcusdb.DALPinManagement(modelRequest);
                    if (res != null)
                    {
                        if (res.Flag == 1)
                        {
                            objresponse.response = "success";
                            objresponse.message = res.Msg;
                        }
                        else
                        {
                            objresponse.response = "error";
                            objresponse.message = res.Msg;
                        }
                    }
                    else
                    {
                        objresponse.response = "error";
                        objresponse.message = "";
                    }
                }
                catch (Exception ex)
                {
                    objresponse.response = "error";
                    objresponse.message = ex.Message;

                }
                string CustData = "";
                DataContractJsonSerializer js;
                MemoryStream ms;
                js = new DataContractJsonSerializer(typeof(MPinManagementResponse));
                ms = new MemoryStream();
                js.WriteObject(ms, objresponse);
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                CustData = sr.ReadToEnd();
                sr.Close();
                ms.Close();
                EncryptedText = Crypto.Encryption(Aeskey, CustData);
                returnResponse.body = EncryptedText;
                return returnResponse;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ResponseModel FamilynRegistration(RequestModel model)
        {
            try
            {
                ResultSet res = new ResultSet();
                RegistrationResponse objresponse = new RegistrationResponse();
                ResponseModel returnResponse = new ResponseModel();
                Registration modelRequest = new Registration();
                string EncryptedText = "";
                try
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    modelRequest = JsonConvert.DeserializeObject<Registration>(dcdata);
                    string password = modelRequest.Password;
                    modelRequest.Normalpassword = modelRequest.Password;
                    modelRequest.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(modelRequest.Password, "md5");
                    res = objcusdb.RegistrationByReferralKaryon(modelRequest);
                    if (res != null)
                    {
                        if (res.Flag == 1)
                        {
                            objresponse.response = "success";
                            objresponse.message = "success";
                            objresponse.Iskyc = res.Iskyc;
                            string message = SMS.PlotHolderRegistrationSMS(modelRequest.FirstName + " " + modelRequest.LastName, modelRequest.MobileNo, modelRequest.Normalpassword);
                            SMS.SendSMS(modelRequest.MobileNo, message);   //open it on the development server    
                            //  LifeOne.Models.API.Common.SendEmailByAPICommonVerification(modelRequest.Email, modelRequest.FirstName + " " + modelRequest.LastName, modelRequest.MobileNo, modelRequest.Normalpassword);
                        }
                        else
                        {
                            objresponse.response = "error";
                            objresponse.message = res.Msg;
                        }
                    }
                    else
                    {
                        objresponse.response = "error";
                        objresponse.message = "";
                    }
                }
                catch (Exception ex)
                {
                    objresponse.response = "error";
                    objresponse.message = ex.Message;

                }
                string CustData = "";
                DataContractJsonSerializer js;
                MemoryStream ms;
                js = new DataContractJsonSerializer(typeof(RegistrationResponse));
                ms = new MemoryStream();
                js.WriteObject(ms, objresponse);
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                CustData = sr.ReadToEnd();
                sr.Close();
                ms.Close();
                EncryptedText = Crypto.Encryption(Aeskey, CustData);
                returnResponse.body = EncryptedText;
                return returnResponse;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ResponseModel KYCAccountverify(RequestModel model)
        {
            try
            {
                ResultSet res = new ResultSet();
                RegistrationResponse objresponse = new RegistrationResponse();
                ResponseModel returnResponse = new ResponseModel();
                Registration modelRequest = new Registration();
                string EncryptedText = "";
                try
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    modelRequest = JsonConvert.DeserializeObject<Registration>(dcdata);
                    string password = modelRequest.Password;
                    modelRequest.Normalpassword = modelRequest.Password;
                    modelRequest.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(modelRequest.Password, "md5");
                    res = objcusdb.KYCAccountverify(modelRequest);
                    if (res != null)
                    {
                        if (res.Flag == 1)
                        {
                            objresponse.response = "success";
                            objresponse.message = res.Msg;
                            objresponse.Iskyc = res.Iskyc;
                        }
                        else
                        {
                            objresponse.response = "error";
                            objresponse.message = res.Msg;
                        }
                    }
                    else
                    {
                        objresponse.response = "error";
                        objresponse.message = "";
                    }
                }
                catch (Exception ex)
                {
                    objresponse.response = "error";
                    objresponse.message = ex.Message;

                }
                string CustData = "";
                DataContractJsonSerializer js;
                MemoryStream ms;
                js = new DataContractJsonSerializer(typeof(RegistrationResponse));
                ms = new MemoryStream();
                js.WriteObject(ms, objresponse);
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                CustData = sr.ReadToEnd();
                sr.Close();
                ms.Close();
                EncryptedText = Crypto.Encryption(Aeskey, CustData);
                returnResponse.body = EncryptedText;
                return returnResponse;
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpPost]
        public ResponseModel KYCAccountverifyV2(RequestModel model)
        {
            try
            {
                ResultSet res = new ResultSet();
                RegistrationResponse objresponse = new RegistrationResponse();
                ResponseModel returnResponse = new ResponseModel();
                Registration modelRequest = new Registration();
                string EncryptedText = "";
                try
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    modelRequest = JsonConvert.DeserializeObject<Registration>(dcdata);
                    string password = modelRequest.Password;
                    modelRequest.Normalpassword = modelRequest.Password;
                    modelRequest.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(modelRequest.Password, "md5");
                    res = objcusdb.KYCAccountverifyV2(modelRequest);
                    if (res != null)
                    {
                        if (res.Flag == 1)
                        {
                            objresponse.response = "success";
                            objresponse.message = res.Msg;
                            objresponse.Iskyc = res.Iskyc;
                        }
                        else
                        {
                            objresponse.response = "error";
                            objresponse.message = res.Msg;
                        }
                    }
                    else
                    {
                        objresponse.response = "error";
                        objresponse.message = "";
                    }
                }
                catch (Exception ex)
                {
                    objresponse.response = "error";
                    objresponse.message = ex.Message;

                }
                string CustData = "";
                DataContractJsonSerializer js;
                MemoryStream ms;
                js = new DataContractJsonSerializer(typeof(RegistrationResponse));
                ms = new MemoryStream();
                js.WriteObject(ms, objresponse);
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                CustData = sr.ReadToEnd();
                sr.Close();
                ms.Close();
                EncryptedText = Crypto.Encryption(Aeskey, CustData);
                returnResponse.body = EncryptedText;
                return returnResponse;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
        #region login
        [HttpPost]
        public ResponseModel Login(RequestModel model)
        {
            LoginResponse objresponse = new LoginResponse();
            ResponseModel returnResponse = new ResponseModel();
            Models.API.Login modelRequest = new Models.API.Login();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<Models.API.Login>(dcdata);
                modelRequest.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(modelRequest.Password, "md5");
                if (modelRequest.Update == "Yes")
                {
                    var res = objcusdb.GetLogin(modelRequest);
                    if (res != null)
                    {
                        objresponse.result = res;
                        if (res.flag == 1)
                        {
                            objresponse.response = "success";
                            //objresponse.result.token = JWTToken.GenerateToken(Crypto.Encryption(Aeskey, objresponse.result.androidId));
                            objresponse.message = res.message;
                        }
                        else
                        {
                            objresponse.response = "error";
                            objresponse.message = res.message;
                        }
                    }
                    else
                    {
                        objresponse.response = "error";
                        objresponse.message = "";
                    }

                    objresponse.VersionCode = res.VersionCode;
                }
                else
                {
                    objresponse.response = "error";
                    objresponse.message = "Please update your App";
                }
            }
            catch (Exception ex)
            {
                objresponse.response = "error";
                objresponse.message = ex.Message;

            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(LoginResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objresponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }

        #endregion
        #region otp
        [HttpPost]
        public ResponseModel OTPRequest(RequestModel model)
        {
            OTP modal = new OTP();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            OTPResponse data = new OTPResponse();
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modal = JsonConvert.DeserializeObject<OTP>(dcdata);
                if (modal.ProcId != 3)
                {

                    var res = objcusdb.GenerateOTP(modal);
                    if (res != null)
                    {
                        if (res.Flag == 1)
                        {
                            data.response = "Success";
                            data.message = res.Msg;
                            data.isexpired = "False";
                            data.OTP = res.OTP; //remove it from after testing 
                            string Message2 = SMS.OTPMemberMessage2(res.MemberName, res.OTP, res.Purpose);
                            string mobile = res.Mobile;
                            SMS.SendSMSTempOTP(mobile, Message2, ConfigurationManager.AppSettings["TEMP-SMS-OTP-Member"].ToString());
                            data.OTP = "";
                            //if (res.Email != null)
                            //{
                            //    LifeOne.Models.API.Common.SendEmailByAPICommon(res.Email, "", "", "OTP Request", Message2, "");
                            //}
                        }
                        else if (res.Flag == 2)
                        {
                            data.response = "error";
                            data.message = res.Msg;
                            data.isexpired = "True";
                        }
                        else if (res.Flag == 3)
                        {
                            data.response = "success";
                            data.message = res.Msg;
                            data.isexpired = "False";
                        }
                        else
                        {
                            data.response = "error";
                            data.message = res.Msg;
                            data.isexpired = "False";
                        }
                    }
                    else
                    {
                        data.response = "error";
                        data.message = "No Record";
                        data.isexpired = "False";
                    }

                }
                else
                {
                    string Message2 = modal.Message;
                    string mobile = modal.Mobile;
                    SMS.SendSMS(mobile, Message2);
                    data.response = "success";
                    data.message = "message send successfully";
                    data.isexpired = "";
                }

            }
            catch (Exception ex)
            {
                data.response = "Error";
                data.message = ex.Message;
                data.isexpired = "False";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(OTPResponse
                ));
            ms = new MemoryStream();
            js.WriteObject(ms, data);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }
        #endregion
        [HttpPost]
        public ResponseModel GetCustomerReferalmobile(RequestModel model)
        {

            ReferalMobile objreferal = new ReferalMobile();
            ResponseReferalMobile objresponse = new ResponseReferalMobile();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            if (model != null)
            {
                try
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    objreferal = JsonConvert.DeserializeObject<ReferalMobile>(dcdata);
                    var res = objcusdb.GetReferalMobile(objreferal);
                    if (res != null)
                    {
                        objresponse.response = "success";
                        objresponse.name = res.name;
                    }
                    else
                    {
                        objresponse.response = "error";
                    }
                }
                catch (Exception ex)
                {
                    objresponse.response = "error";
                }
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ResponseReferalMobile));
            ms = new MemoryStream();
            js.WriteObject(ms, objresponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }
        #region Get Referal Details
        [HttpPost]
        public ResponseModel GetReferalDetails(RequestModel model)
        {
            ReferalDetails objGetReferalDetails = new ReferalDetails();
            ResponseReferalMobile objresponse = new ResponseReferalMobile();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                if (model != null)
                {

                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    objGetReferalDetails = JsonConvert.DeserializeObject<ReferalDetails>(dcdata);
                    var res = objcusdb.GetReferaldetails(objGetReferalDetails);
                    if (res != null && res.Flag == 1)
                    {
                        objresponse.response = "success";
                        objresponse.name = res.name;
                    }
                    else
                    {
                        objresponse.response = "error";
                    }

                }
                else
                {
                    objresponse.response = "error";
                }
            }
            catch (Exception ex)
            {
                objresponse.response = "error";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ResponseReferalMobile));
            ms = new MemoryStream();
            js.WriteObject(ms, objresponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }
        #endregion

        #region Get Referal Details For Magento 
        [HttpPost]
        public ResponseModel GetReferalDetailsByLoginID(RequestModel model)
        {
            ReferalDetails objreferal = new ReferalDetails();
            UserDetailsBYLoginId objresponse = new UserDetailsBYLoginId();

            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                if (model != null)
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    objreferal = JsonConvert.DeserializeObject<ReferalDetails>(dcdata);
                    UserProfile res = objcusdb.Proc_GetProfileDetailByLoginId(objreferal.InviteCode);
                    if (res != null)
                    {
                        objresponse.response = "success";
                        objresponse.userprofile = res;
                    }
                    else
                    {
                        objresponse.response = "error";
                        objresponse.userprofile = null;
                    }
                }
                else
                {
                    objresponse.response = "error";
                }
            }
            catch (Exception ex)
            {
                objresponse.response = "error";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(UserDetailsBYLoginId));
            ms = new MemoryStream();
            js.WriteObject(ms, objresponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }
        #endregion

        #region ForgotPassword
        public ResponseModel ForgotPassword(RequestModel model)
        {

            ForgotPassword objforgot = new ForgotPassword();
            ResponseModel objApiResponse = new ResponseModel();
            ApiResponseForgetPassword objresponse = new ApiResponseForgetPassword();

            string EncryptedText = "";
            try
            {
                if (model != null)
                {
                    string dcdat = Crypto.Decryption(Aeskey, model.body);
                    objforgot = JsonConvert.DeserializeObject<ForgotPassword>(dcdat);
                    var res = objcusdb.AssociateForgotPassword(objforgot);

                    if (res != null)
                    {
                        if (res.Flag == 1)
                        {
                            objresponse.response = "success";
                            objresponse.message = "Please Wait...";
                            objresponse.fk_memId = res.fk_memId;
                        }
                        else
                        {
                            objresponse.message = res.message;
                            objresponse.response = "error";
                        }
                    }
                    else
                    {
                        objresponse.message = res.message;
                        objresponse.response = "error";
                    }

                }
                else
                {
                    objresponse.response = "error";
                }
            }
            catch (Exception ex)
            {
                objresponse.response = "error";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ApiResponseForgetPassword));
            ms = new MemoryStream();
            js.WriteObject(ms, objresponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }
        #endregion
        #region Change Password 
        public ResponseModel ChangePassword(RequestModel model)
        {
            ChangePassword objSName = new ChangePassword();
            ResponseModel objApiResponse = new ResponseModel();
            APIResponseChangePassword obj = new APIResponseChangePassword();

            string EncryptedText = "";
            try
            {
                if (model != null)
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    objSName = JsonConvert.DeserializeObject<ChangePassword>(dcdata);
                    objSName.NormalPassword = objSName.NewPassword;
                    objSName.OldPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(objSName.OldPassword, "md5");
                    objSName.NewPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(objSName.NewPassword, "md5");
                    if (string.IsNullOrEmpty(objSName.Fk_MemId))
                    {
                        objSName.Fk_MemId = "0";
                    }
                    if (string.IsNullOrEmpty(objSName.UpdatedBy))
                    {
                        objSName.UpdatedBy = "0";
                    }

                    var res = objcusdb.Changepassword(objSName);
                    if (res != null)
                    {
                        if (res.flag == 1)
                        {
                            obj.response = "success";
                        }
                        else if (res.flag == 2)
                        {
                            obj.response = "Your Old Transaction Password is Mismatched";
                        }
                        else
                        {
                            obj.response = "Your Old Login Password is Mismatched";
                        }
                    }
                    else
                    {
                        obj.response = "error";
                    }
                }
                else
                {
                    obj.response = "error";
                }
            }

            catch (Exception ex)
            {
                obj.response = "error";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(APIResponseChangePassword));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }
        #endregion

        [HttpPost]
        public ResponseModel GetCustomerEmail(RequestModel model)
        {

            ReferalMobile objreferal = new ReferalMobile();
            MChkEmailExist objresponse = new MChkEmailExist();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";

            if (model != null)
            {
                try
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    objreferal = JsonConvert.DeserializeObject<ReferalMobile>(dcdata);
                    objresponse = objcusdb.CheckEmail(objreferal);
                    if (objresponse != null)
                    {
                        objresponse.response = "success";
                    }
                    else
                    {
                        objresponse.response = "error";
                    }
                }
                catch (Exception ex)
                {
                    objresponse.response = "error";
                }
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(MChkEmailExist));
            ms = new MemoryStream();
            js.WriteObject(ms, objresponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }
        // It enables us to load all types of shopping, travel and utilities
        public ResponseModel GetDashboardDetail()
        {
            jsonDashboardResponse objres = new jsonDashboardResponse();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                var shopping = objcusdb.GetDashboarddetaildata(1);
                var travel = objcusdb.GetDashboarddetaildata(2);
                var utilities = objcusdb.GetDashboarddetaildata(3);

                if (shopping != null && shopping.Count > 0)
                {
                    objres.shopping = shopping;
                }
                if (travel != null && travel.Count > 0)
                {
                    objres.travel = travel;
                }
                if (utilities != null && utilities.Count > 0)
                {
                    objres.utilities = utilities;
                }

                if ((shopping != null && shopping.Count > 0) || (travel != null && travel.Count > 0) || (utilities != null && utilities.Count > 0))
                {
                    objres.response = "success";
                }
                else
                {
                    objres.response = "error";
                }
            }
            catch (Exception ex)
            {
                objres.response = "error";
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(jsonDashboardResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;

        }
        [HttpGet]
        public ResponseModel GetAreaDetailByPincode(string pincode)
        {
            PincodeResponse objres = new PincodeResponse();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                pincode = pincode.Replace(" ", "+");
                pincode = Crypto.Decryption(Aeskey, pincode);

                var res = objcusdb.GetAreaDetailByPincode(pincode);
                if (res != null && res.Count > 0)
                {
                    objres.result = res;
                    objres.response = "success";
                }
                else
                {
                    objres.response = "error";
                }
            }
            catch (Exception ex)
            {
                objres.response = "error";
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(PincodeResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;

        }

        public ApiResponseModel GetDashboardDetail_V2()
        {
            ApiResponseModel objApiResponse = new ApiResponseModel();
            List<DashboardCategoryMaster> orderItem = new List<DashboardCategoryMaster>();
            DashboardCategoryResponse obj1 = new DashboardCategoryResponse();
            Category db = new Category();
            string EncryptedText = "";
            try
            {
                DataSet ds = db.GetDashboarddetaildata_V2();
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Code"].ToString() == "1")
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            DashboardCategoryMaster obj = new DashboardCategoryMaster();
                            List<DashboardCategory> Catlist = new List<DashboardCategory>();

                            obj.CategoryId = dr["CategoryId"].ToString();
                            obj.CategoryName = dr["CategoryName"].ToString();
                            obj.imgurl = dr["imgurl"].ToString();
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                foreach (DataRow Dr1 in ds.Tables[1].Rows)
                                {
                                    if (dr["CategoryId"].ToString() == Dr1["categoryId"].ToString())
                                    {
                                        DashboardCategory objCat = new DashboardCategory();
                                        objCat.categoryId = Dr1["categoryId"].ToString();
                                        objCat.name = Dr1["name"].ToString();
                                        objCat.link = Dr1["link"].ToString();
                                        objCat.imageurl = Dr1["imageurl"].ToString();
                                        Catlist.Add(objCat);
                                    }
                                }
                                obj.CategoryList = Catlist;
                                orderItem.Add(obj);
                            }
                        }
                        obj1.response = "Success";
                        obj1.category = orderItem;
                    }
                    else
                    {
                        obj1.response = "error";
                    }
                }
                else
                {
                    obj1.response = "error";
                }
            }
            catch (Exception ex)
            {
                obj1.response = "error";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(DashboardCategoryResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, obj1);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }

        public ResponseModel SetNewPassword(RequestModel model)
        {
            ChangePassword objSName = new ChangePassword();
            ResponseModel objApiResponse = new ResponseModel();
            APIResponseChangePassword obj = new APIResponseChangePassword();
            string EncryptedText = "";
            try
            {
                if (model != null)
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    objSName = JsonConvert.DeserializeObject<ChangePassword>(dcdata);

                    string ENCPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(objSName.NewPassword, "md5");
                    LifeOne.Models.API.ResultSet res = ForgotPasswordService.SetNewPassword(objSName.Fk_MemId, objSName.NewPassword, ENCPassword);
                    if (res != null)
                    {
                        if (res.Flag == 1)
                        {
                            obj.response = res.Msg;
                        }
                        else
                        {
                            obj.response = res.Msg;
                        }
                    }
                    else
                    {
                        obj.response = "error";
                    }
                }
                else
                {
                    obj.response = "error";
                }
            }

            catch (Exception ex)
            {
                obj.response = "error";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(APIResponseChangePassword));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }

        [HttpPost]
        public ResponseModel GetSameMobileNoDetails(RequestModel model)
        {
            MSameMobilenoDetailsResponse objres = new MSameMobilenoDetailsResponse();
            MMobileNo modelRequest = new MMobileNo();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";

            string dcdata = Crypto.Decryption(Aeskey, model.body);
            modelRequest = JsonConvert.DeserializeObject<Models.API.MMobileNo>(dcdata);
            var res = objcusdb.GetSameMObileDataDetails(modelRequest);
            try
            {
                if (res != null && res.Count > 0)
                {
                    objres.result = res;
                    objres.response = "success";
                    objres.message = "success";
                    foreach (var m in res)
                    {
                        objres.MemberExistOrNot = m.MemberExistOrNot;
                        objres.CampaignStartDate = m.CampaignStartDate;
                        objres.IsRegistered = m.IsRegistered;
                        objres.VersionCode = m.VersionCode;
                    }
                }
                else
                {
                    objres.response = "error";
                    objres.message = "stop _ Please update your application from google playstore to start reffer, earn and registration process. _ please update";

                }
            }
            catch (Exception ex)
            {
                objres.response = "error";
                objres.message = ex.ToString();
            }


            //objres.VersionCode = 17;
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(MSameMobilenoDetailsResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;

        }

        [HttpPost]
        public ResponseModel AssociateFranchiseRequest(RequestModel model)
        {
            try
            {
                ResultSet res = new ResultSet();
                LifeOne.Models.HomeManagement.HEntity.FranchiseRegistrationResponse objresponse = new LifeOne.Models.HomeManagement.HEntity.FranchiseRegistrationResponse();
                ResponseModel returnResponse = new ResponseModel();
                LifeOne.Models.HomeManagement.HEntity.MemberDetailsForFranchiseModel modelRequest = new LifeOne.Models.HomeManagement.HEntity.MemberDetailsForFranchiseModel();
                string EncryptedText = "";
                try
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    modelRequest = JsonConvert.DeserializeObject<LifeOne.Models.HomeManagement.HEntity.MemberDetailsForFranchiseModel>(dcdata);

                    res = objcusdb.DAlAssociateFranchiseRequest(modelRequest);
                    if (res != null)
                    {
                        if (res.Flag == 1)
                        {
                            objresponse.response = "success";
                            objresponse.message = "success";
                            //string message = SMS.PlotHolderRegistrationSMS(modelRequest.FirstName + " " + modelRequest.LastName, modelRequest.MobileNo, modelRequest.Normalpassword);
                            //SMS.SendSMS(modelRequest.MobileNo, message);   //open it on the development server   
                            ////if (modelRequest.Email != null || modelRequest.Email != "")
                            ////{
                            ////    LifeOne.Models.API.Common.SendEmailByAPICommonVerification(modelRequest.Email, modelRequest.FirstName + " " + modelRequest.LastName, modelRequest.MobileNo, modelRequest.Normalpassword);
                            ////}
                        }
                        else
                        {
                            objresponse.response = "error";
                            objresponse.message = res.Msg;
                        }
                    }
                    else
                    {
                        objresponse.response = "error";
                        objresponse.message = "";
                    }
                }
                catch (Exception ex)
                {
                    objresponse.response = "error";
                    objresponse.message = ex.Message;

                }
                string CustData = "";
                DataContractJsonSerializer js;
                MemoryStream ms;
                js = new DataContractJsonSerializer(typeof(LifeOne.Models.HomeManagement.HEntity.MemberDetailsForFranchiseModel));
                ms = new MemoryStream();
                js.WriteObject(ms, objresponse);
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                CustData = sr.ReadToEnd();
                sr.Close();
                ms.Close();
                EncryptedText = Crypto.Encryption(Aeskey, CustData);
                returnResponse.body = EncryptedText;
                return returnResponse;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /*Pincode*/
        [HttpPost]
        public ResponseModel GetPincodeDetails(RequestModel model)
        {
            MPincodeDetailsResponse objres = new MPincodeDetailsResponse();
            MPincode modelRequest = new MPincode();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<Models.API.MPincode>(dcdata);
                var res = objcusdb.GetPincodeDetais(modelRequest);
                if (res != null)
                {
                    objres._objPiinList = res;
                    objres.response = "success";
                    objres.message = "success";
                }
                else
                {
                    objres.response = "error";
                    objres.message = "error";
                }
            }
            catch (Exception ex)
            {
                objres.response = "error";
                objres.message = ex.Message;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(MPincodeDetailsResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;

        }

        [HttpPost]
        public ResponseModel GetMemberInfoByBIDUID(RequestModel model)
        {
            try
            {

                MGetMembersDetailsByIdForFranchiseRequest objresponse = new MGetMembersDetailsByIdForFranchiseRequest();
                ResponseModel returnResponse = new ResponseModel();
                MBIDRequest modelRequest = new MBIDRequest();
                string EncryptedText = "";
                try
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    modelRequest = JsonConvert.DeserializeObject<MBIDRequest>(dcdata);

                    objresponse = objcusdb.GetMembersDetailsByIdForFranchiseRequest(modelRequest);
                    if (objresponse != null)
                    {
                        objresponse.response = "success";
                    }
                    else
                    {
                        objresponse.response = "error";
                        objresponse.message = "";
                    }
                }
                catch (Exception ex)
                {
                    objresponse.response = "error";
                    objresponse.message = ex.Message;

                }
                string CustData = "";
                DataContractJsonSerializer js;
                MemoryStream ms;
                js = new DataContractJsonSerializer(typeof(MGetMembersDetailsByIdForFranchiseRequest));
                ms = new MemoryStream();
                js.WriteObject(ms, objresponse);
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                CustData = sr.ReadToEnd();
                sr.Close();
                ms.Close();
                EncryptedText = Crypto.Encryption(Aeskey, CustData);
                returnResponse.body = EncryptedText;
                return returnResponse;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ResponseModel GetMemberInfoByBIDUID_V2(RequestModel model)
        {
            try
            { 
                MGetMembersDetailsByIdForFranchiseRequest objresponse = new MGetMembersDetailsByIdForFranchiseRequest();
                ResponseModel returnResponse = new ResponseModel();
                MBIDRequest modelRequest = new MBIDRequest();
                string EncryptedText = "";
                try
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    modelRequest = JsonConvert.DeserializeObject<MBIDRequest>(dcdata);

                    objresponse = objcusdb.GetMembersDetailsByIdForFranchiseRequest_v2(modelRequest);
                    if (objresponse != null)
                    {
                        objresponse.response = "success";
                    }
                    else
                    {
                        objresponse.response = "error";
                        objresponse.message = "";
                    }
                }
                catch (Exception ex)
                {
                    objresponse.response = "error";
                    objresponse.message = ex.Message;

                }
                string CustData = "";
                DataContractJsonSerializer js;
                MemoryStream ms;
                js = new DataContractJsonSerializer(typeof(MGetMembersDetailsByIdForFranchiseRequest));
                ms = new MemoryStream();
                js.WriteObject(ms, objresponse);
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                CustData = sr.ReadToEnd();
                sr.Close();
                ms.Close();
                EncryptedText = Crypto.Encryption(Aeskey, CustData);
                returnResponse.body = EncryptedText;
                return returnResponse;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ResponseModel SubmitFranchiseApplicationForm(RequestModel model)
        {
            LifeOne.Models.HomeManagement.HEntity.FranchiseRegistrationResponse objres = new LifeOne.Models.HomeManagement.HEntity.FranchiseRegistrationResponse();
            LifeOne.Models.HomeManagement.HEntity.FranchiseApplicationFormModel modelRequest = new LifeOne.Models.HomeManagement.HEntity.FranchiseApplicationFormModel();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<LifeOne.Models.HomeManagement.HEntity.FranchiseApplicationFormModel>(dcdata);
                LifeOne.Models.HomeManagement.HEntity.FranchiseRegistrationResponse res = CustomerRegService.SaveFranchiseApplicationForm(modelRequest); ;
                if (res != null && res.Flag == 1)
                {
                    objres.response = "success";
                    objres.message = res.Remark;
                }
                else
                {
                    objres.response = "error";
                    objres.message = res.Remark;
                }
            }
            catch (Exception ex)
            {
                objres.response = "error";
                objres.message = ex.Message;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(LifeOne.Models.HomeManagement.HEntity.FranchiseRegistrationResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }
        [HttpPost]
        public ResponseModel GetMemberInfoByBIDUID_V3(RequestModel model)
        {
            try
            {
                MGetMembersDetailsByIdForFranchiseRequest objresponse = new MGetMembersDetailsByIdForFranchiseRequest();
                ResponseModel returnResponse = new ResponseModel();
                MBIDRequest_V3 modelRequest = new MBIDRequest_V3();
                string EncryptedText = "";
                try
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    modelRequest = JsonConvert.DeserializeObject<MBIDRequest_V3>(dcdata);

                    objresponse = objcusdb.GetMembersDetailsByIdForFranchiseRequest_v3(modelRequest);
                    if (objresponse != null)
                    {
                        objresponse.response = "success";
                    }
                    else
                    {
                        objresponse.response = "error";
                        objresponse.message = "";
                    }
                }
                catch (Exception ex)
                {
                    objresponse.response = "error";
                    objresponse.message = ex.Message;

                }
                string CustData = "";
                DataContractJsonSerializer js;
                MemoryStream ms;
                js = new DataContractJsonSerializer(typeof(MGetMembersDetailsByIdForFranchiseRequest));
                ms = new MemoryStream();
                js.WriteObject(ms, objresponse);
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                CustData = sr.ReadToEnd();
                sr.Close();
                ms.Close();
                EncryptedText = Crypto.Encryption(Aeskey, CustData);
                returnResponse.body = EncryptedText;
                return returnResponse;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ResponseModel SubmitFranchiseApplicationForm_V2(RequestModel model)
        {
            LifeOne.Models.HomeManagement.HEntity.FranchiseRegistrationResponse objres = new LifeOne.Models.HomeManagement.HEntity.FranchiseRegistrationResponse();
            LifeOne.Models.HomeManagement.HEntity.FranchiseApplicationFormModel modelRequest = new LifeOne.Models.HomeManagement.HEntity.FranchiseApplicationFormModel();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<LifeOne.Models.HomeManagement.HEntity.FranchiseApplicationFormModel>(dcdata);
                LifeOne.Models.HomeManagement.HEntity.FranchiseRegistrationResponse res = CustomerRegService.SaveFranchiseApplicationForm(modelRequest); ;
                if (res != null && res.Flag == 1)
                {
                    objres.response = "success";
                    objres.message = res.Remark;
                }
                else
                {
                    objres.response = "error";
                    objres.message = res.Remark;
                }
            }
            catch (Exception ex)
            {
                objres.response = "error";
                objres.message = ex.Message;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(LifeOne.Models.HomeManagement.HEntity.FranchiseRegistrationResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }
        public ResponseModel SubmitFranchiseApplicationFormByDistrict(RequestModel model)
        {
            LifeOne.Models.HomeManagement.HEntity.FranchiseRegistrationResponse objres = new LifeOne.Models.HomeManagement.HEntity.FranchiseRegistrationResponse();
            LifeOne.Models.HomeManagement.HEntity.FranchiseApplicationFormModel modelRequest = new LifeOne.Models.HomeManagement.HEntity.FranchiseApplicationFormModel();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<LifeOne.Models.HomeManagement.HEntity.FranchiseApplicationFormModel>(dcdata);
                LifeOne.Models.HomeManagement.HEntity.FranchiseRegistrationResponse res = CustomerRegService.SaveFranchiseApplicationForm_V1(modelRequest); ;
                if (res != null && res.Flag == 1)
                {
                    objres.response = "success";
                    objres.message = res.Remark;
                }
                else
                {
                    objres.response = "error";
                    objres.message = res.Remark;
                }
            }
            catch (Exception ex)
            {
                objres.response = "error";
                objres.message = ex.Message;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(LifeOne.Models.HomeManagement.HEntity.FranchiseRegistrationResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }

    }
}
