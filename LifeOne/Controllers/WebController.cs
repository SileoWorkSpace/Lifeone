using AesEncryption;
using LifeOne.Filters;
using LifeOne.Models;
using LifeOne.Models.API;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Web.Http;
using System.Net;
using static LifeOne.Models.Jobs.OrdersJob;
using Razorpay.Api;
using System.Net.Http;
using System.Data.SqlClient;
using LifeOne.Models.API.DAL;
using LifeOne.Models.QUtility.Entity;
using System.Web;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.AssociateManagement.AssociateService;
using LifeOne.Models.Common;
using System.Linq;
using LifeOne.Models.AdminManagement.AService;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.FranchiseManagement.FEntity;
using LifeOne.Models.FranchiseManagement.FDAL;
using LifeOne.Models.FranchiseManagement.FService;
using static LifeOne.Models.ShoppingResponse;
using static LifeOne.Models.ShoppingRequest;
using static LifeOne.Models.API.Common;

namespace LifeOne.Controllers
{
    //[AuthorizeFilter]create
    //[ValidCustomer]cancel

    public class WebController : ApiController
    {
        CustomerDb objcusdb = new CustomerDb();
        string Aeskey = ConfigurationManager.AppSettings["Aeskey"].ToString();
        string RazorPayLiveKey = ConfigurationManager.AppSettings["RazorKey"].ToString();
        string RazorPayLocalKey = ConfigurationManager.AppSettings["RazorKeyLocal"].ToString();
        string RazorPayLocalSecret = ConfigurationManager.AppSettings["RazorSecretLocaL"].ToString();
        string RazorPayLiveSecret = ConfigurationManager.AppSettings["RazorSecret"].ToString();
        #region Profile 
        [HttpGet]
        public ResponseModel ViewProfile(string memberId)
        {
            ReturnUserProfile obj = new ReturnUserProfile();
            ResponseModel objApiResponse = new ResponseModel();

            memberId = memberId.Replace(" ", "+");
            memberId = Crypto.Decryption(Aeskey, memberId);
            string EncryptedText = "";

            try
            {
                var res = objcusdb.GetUserProfile(long.Parse(memberId));
                if (res != null)
                {
                    obj.response = "success";
                    obj.result = res;
                }
                else
                {
                    obj.response = "error ";
                }
            }
            catch (Exception ex)
            {
                obj.response = "error ";
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ReturnUserProfile));
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
        public ResponseModel UpdateMemberPersonalDetails(RequestModel model)
        {
            UserProfile obj = new UserProfile();
            ResponseModel objApiResponse = new ResponseModel();
            ResultSet res = new ResultSet();
            UpdateProfileResponse data = new UpdateProfileResponse();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                obj = JsonConvert.DeserializeObject<UserProfile>(dcdata);
                res = objcusdb.UpdateProfile(obj);
                if (res != null)
                {
                    data.response = "success";
                    data.message = res.Msg;
                }
                else
                {
                    data.response = "error";
                    data.message = res.Msg;
                }

            }
            catch (Exception ex)
            {
                data.response = "error";
                data.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(UpdateProfileResponse));
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
        public ResponseModel BankDetailsUpdate(RequestModel model)
        {
            BankDetail obj = new BankDetail();
            ResponseModel objApiResponse = new ResponseModel();
            ResultSet res = new ResultSet();
            returnResponse data = new returnResponse();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                obj = JsonConvert.DeserializeObject<BankDetail>(dcdata);
                res = objcusdb.UpdateBank(obj);
                if (res != null)
                {
                    if (res.Flag == 1)
                    {
                        data.response = "success";
                        data.message = res.Msg;
                    }
                    else
                    {
                        data.response = "error";
                        data.message = res.Msg;
                    }
                }
                else
                {
                    data.response = "error";
                    data.message = res.Msg;
                }
            }
            catch (Exception ex)
            {
                data.response = "error";
                data.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(returnResponse));
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

        [HttpGet]
        public ResponseModel GetCompanyBank()
        {

            ResponseModel objApiResponse = new ResponseModel();
            BankMasterResponse objres = new BankMasterResponse();
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            StreamReader sr;
            string EncryptedText = "";
            try
            {

                var res = objcusdb.GetDreamyBankDetail();
                if (res != null)
                {
                    objres.result = res;
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

            js = new DataContractJsonSerializer(typeof(BankMasterResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }
        public ResponseModel SignOutAPI(RequestModel model)
        {
            var authToken = Request.Headers
                    .Authorization.Parameter;
            clsInputSignOut objInputSignOut = new clsInputSignOut();
            ResponseModel objApiResponse = new ResponseModel();
            returnResponse objReturn = new returnResponse();
            string EncryptedText = "";

            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                objInputSignOut = JsonConvert.DeserializeObject<clsInputSignOut>(dcdata);

                objInputSignOut.Token = authToken;
                var res = objcusdb.SignOutAPI(objInputSignOut);
                if (res != null)
                {
                    if (res.Flag == 1)
                    {
                        objReturn.response = "success";
                        objReturn.message = res.Msg;
                    }
                    else
                    {
                        objReturn.response = "error";
                        objReturn.message = res.Msg;
                    }
                }
                else
                {
                    objReturn.response = "error";
                    objReturn.message = "error";
                }

            }
            catch (Exception ex)
            {
                objReturn.response = "error";
                objReturn.message = ex.Message;

            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(returnResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objReturn);
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
        public ResponseModel GetDirect(RequestModel model)
        {
            ResponseModel objApiResponse = new ResponseModel();
            Direct d = new Direct();
            ReturnResponseDirect objres = new ReturnResponseDirect();
            //ResponseModel returnResponse = new ResponseModel();            
            string dcdata = Crypto.Decryption(Aeskey, model.body);
            d = JsonConvert.DeserializeObject<Direct>(dcdata);
            objres.lstDirect = objcusdb.GetDirect(d.FK_MemId);
            string EncryptedText = "";

            if (objres.lstDirect != null && objres.lstDirect.Count > 0)
            {
                objres.response = "success";
                objres.message = "success";
            }
            else
            {
                objres.response = "Failed";
                objres.message = "Failed to retireve direct list";
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ReturnResponseDirect));
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
        public ResponseModel GetWallet(string MemberId)
        {
            WalletJsonResponse objres = new WalletJsonResponse();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";

            try
            {
                MemberId = MemberId.Replace(" ", "+");
                MemberId = Crypto.Decryption(Aeskey, MemberId);
                //GetAllPendingOrder(Convert.ToInt64(MemberId));
                var res = objcusdb.GetWalletBalance(long.Parse(MemberId));

                if (res != null)
                {
                    objres.result = res;
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
            js = new DataContractJsonSerializer(typeof(WalletJsonResponse));
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
        public ResponseModel GetFranchiseWalletbalance(string MemberId)
        {
            FranchiseWalletJsonResponse objres = new FranchiseWalletJsonResponse();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";

            try
            {
                MemberId = MemberId.Replace(" ", "+");
                MemberId = Crypto.Decryption(Aeskey, MemberId);
                var res = objcusdb.GetWalletBalanceFranchise(long.Parse(MemberId));
                if (res != null)
                {
                    objres.result = res;
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
            js = new DataContractJsonSerializer(typeof(FranchiseWalletJsonResponse));
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
        public ResponseModel WalletRequest(RequestModel model)
        {
            CommonJsonReponse objresponse = new CommonJsonReponse();
            ResponseModel returnResponse = new ResponseModel();
            Models.API.WalletJsonRequest modelRequest = new Models.API.WalletJsonRequest();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<Models.API.WalletJsonRequest>(dcdata);

                var res = objcusdb.WalletRequest(modelRequest);
                if (res != null)
                {
                    if (res.Flag == 1)
                    {
                        objresponse.response = "success";
                        objresponse.message = res.Msg;
                        objresponse.TransId = res.TransId;

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
            js = new DataContractJsonSerializer(typeof(CommonJsonReponse));
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

        [HttpPost]
        public ResponseModel DebitWallet(RequestModel model)
        {
            CommonJsonReponse objresponse = new CommonJsonReponse();
            ResponseModel returnResponse = new ResponseModel();
            Models.API.WalletActionRequest modelRequest = new Models.API.WalletActionRequest();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<Models.API.WalletActionRequest>(dcdata);
                var res = objcusdb.DebitWallet(modelRequest);
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
            js = new DataContractJsonSerializer(typeof(CommonJsonReponse));
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

        [HttpGet]
        public ResponseModel GetTeamStatus(string memberId, string page, string pagesize, string searchtype, string searchvalue)
        {
            TeamResponse obj = new TeamResponse();
            ResponseModel objApiResponse = new ResponseModel();

            memberId = memberId.Replace(" ", "+");
            memberId = Crypto.Decryption(Aeskey, memberId);
            page = page.Replace(" ", "+");
            page = Crypto.Decryption(Aeskey, page);
            pagesize = pagesize.Replace(" ", "+");
            pagesize = Crypto.Decryption(Aeskey, pagesize);
            searchtype = searchtype.Replace(" ", "+");
            searchtype = Crypto.Decryption(Aeskey, searchtype);
            searchvalue = searchvalue.Replace(" ", "+");
            searchvalue = Crypto.Decryption(Aeskey, searchvalue);
            string EncryptedText = "";

            try
            {

                var res = objcusdb.GetTeam(long.Parse(memberId), Convert.ToInt32(page), Convert.ToInt32(pagesize), searchtype, searchvalue);

                if (res != null)
                {

                    obj.response = "success";
                    obj.message = "success";
                    obj.totalCount = res[0].totalCount;
                    obj.TransId = res[0].totalCount;
                    obj.result = res;

                }
                else
                {
                    obj.response = "error ";
                    obj.message = "error";
                }

            }
            catch (Exception ex)
            {
                obj.response = "error ";
                obj.message = ex.Message;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(TeamResponse));
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

        [HttpGet]
        public ResponseModel GetBusinessDetails(string memberId)
        {
            GetBusinessDetailsResponse obj = new GetBusinessDetailsResponse();
            GetBusinessDetails data = new GetBusinessDetails();
            ResponseModel objApiResponse = new ResponseModel();
            memberId = memberId.Replace(" ", "+");
            memberId = Crypto.Decryption(Aeskey, memberId);
            string EncryptedText = "";
            try
            {
                var res = objcusdb.GetBusinessDetails(long.Parse(memberId));
                if (res != null)
                {
                    data = res;
                    obj.response = "success";
                    obj.message = "success";
                    obj.result = data;
                }
                else
                {
                    obj.response = "error ";
                    obj.message = "error";
                    obj.result = null;
                }

            }
            catch (Exception ex)
            {
                obj.response = "error ";
                obj.message = ex.Message;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(GetBusinessDetailsResponse));
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

        [HttpGet]
        public ResponseModel GetDownline(string memberId, string page, string pagesize, string searchtype, string searchvalue)
        {
            DownlineTeam obj = new DownlineTeam();
            List<DownlineTeamData> data = new List<DownlineTeamData>();
            ResponseModel objApiResponse = new ResponseModel();

            memberId = memberId.Replace(" ", "+");
            memberId = Crypto.Decryption(Aeskey, memberId);
            page = page.Replace(" ", "+");
            page = Crypto.Decryption(Aeskey, page);
            pagesize = pagesize.Replace(" ", "+");
            pagesize = Crypto.Decryption(Aeskey, pagesize);
            searchtype = searchtype.Replace(" ", "+");
            searchtype = Crypto.Decryption(Aeskey, searchtype);
            searchvalue = searchvalue.Replace(" ", "+");
            searchvalue = Crypto.Decryption(Aeskey, searchvalue);
            string EncryptedText = "";
            try
            {
                data = objcusdb.GetDownline(long.Parse(memberId), Convert.ToInt32(page), Convert.ToInt32(pagesize), searchtype, searchvalue);
                //var datacount = objcusdb.getTotalCount(long.Parse(memberId), searchtype, searchvalue);
                if (data != null)
                {

                    obj.response = "success";
                    obj.message = "success";
                    obj.totalCount = data[0].totalCount;
                    obj.result = data;

                }
                else
                {
                    obj.response = "error ";
                    obj.message = "error";
                }

            }
            catch (Exception ex)
            {
                obj.response = "error ";
                obj.message = ex.Message;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(DownlineTeam));
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
        public ResponseModel GetWalletRequestLedger(RequestModel model)
        {
            WalletJsonLedgerReponse objres = new WalletJsonLedgerReponse();
            WalletJsonCommonRequest modelRequest = new WalletJsonCommonRequest();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<Models.API.WalletJsonCommonRequest>(dcdata);
                var res = objcusdb.GetWalletRequestLedger(modelRequest);
                if (res != null)
                {
                    objres.result = res;
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
            js = new DataContractJsonSerializer(typeof(WalletJsonLedgerReponse));
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
        public ResponseModel GetWalletLedger(RequestModel model)
        {
            WalletJsonReponse objres = new WalletJsonReponse();
            WalletJsonCommonRequest modelRequest = new WalletJsonCommonRequest();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<Models.API.WalletJsonCommonRequest>(dcdata);
                var res = objcusdb.GetWalletLedger(modelRequest);
                if (res != null)
                {
                    objres.result = res;
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
            js = new DataContractJsonSerializer(typeof(WalletJsonReponse));
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
        public ResponseModel CreateOrder(RequestModel model)
        {
            CommonJsonReponse objres = new CommonJsonReponse();
            JsonOrderRequest modelRequest = new JsonOrderRequest();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<Models.API.JsonOrderRequest>(dcdata);
                var res = objcusdb.CreateOrder(modelRequest);
                if (res != null && res.Flag == 1)
                {

                    objres.response = "success";
                    objres.message = res.Msg;
                }
                else
                {
                    objres.response = "error";
                    objres.message = res.Msg;
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
            js = new DataContractJsonSerializer(typeof(CommonJsonReponse));
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
        public ResponseModel CreateOrderV3(RequestModel model)
        {
            CommonJsonReponse objres = new CommonJsonReponse();
            CardItemRequest modelRequest = new CardItemRequest();
            ResponseModel objApiResponse = new ResponseModel();
            StreamReader srd;
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<Models.API.CardItemRequest>(dcdata);

                string xml = "<result>";
                if (modelRequest != null && modelRequest.cartitems != null && modelRequest.cartitems.Count > 0)
                {
                    foreach (var item in modelRequest.cartitems)
                    {
                        //as per discussion   item.BP is item.PV
                        xml += "<data><productname>" + item.productname + "</productname><price>" + item.price + "</price><producttype>" + item.producttype + "</producttype><quatity>" + item.quatity + "</quatity><image>" + item.image + "</image><cashbackper>" + item.cashbackper + "</cashbackper><BV>" + item.BV + "</BV><DP>" + item.DP + "</DP><PV>" + item.BP + "</PV></data>";
                    }
                }
                xml += "</result>";
                modelRequest.xmldata = xml;
                modelRequest.ProcId = 1;
                var res = objcusdb.CreateOrderV3(modelRequest);
                if (res != null && res.Flag == 1)
                {
                    string responseText = null;
                    string url = "https://shop.LifeOne.com/index.php/rest/V1/guest-carts/" + modelRequest.token + "/order";

                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    WebRequest request = WebRequest.Create(url);
                    if (request != null)
                    {
                        request.Method = "PUT";
                        request.Timeout = 50000;
                        request.ContentType = "application/json";
                        string body = "{\n\"paymentMethod\":{\n\"method\": \"" + modelRequest.method + "\" }}\n";
                        using (Stream s1 = request.GetRequestStream())
                        {
                            using (StreamWriter sw = new StreamWriter(s1))
                                sw.Write(body);
                        }
                        Stream s = request.GetResponse().GetResponseStream();
                        srd = new StreamReader(s);
                        responseText = srd.ReadToEnd();
                        if (!String.IsNullOrEmpty(responseText))
                        {
                            modelRequest.ProcId = 2;
                            modelRequest.OrderNo = responseText.Replace('"', ' ').Trim();
                            modelRequest.orderId = res.TransId;
                            var result = objcusdb.CreateOrderV3(modelRequest);
                            if (result != null)
                            {
                                objres.response = "success";
                                objres.message = "success";
                                objres.orderId = res.TransId;
                                objres.OrderNo = responseText.Replace('"', ' ').Trim();
                            }
                        }
                    }
                }
                else
                {
                    objres.response = "error";
                    objres.message = res.Msg;
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
            js = new DataContractJsonSerializer(typeof(CommonJsonReponse));
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
        public ResponseModel CreateOrderV3_Karyon(RequestModel model)
        {
            CommonJsonReponse objres = new CommonJsonReponse();
            CardItemRequest modelRequest = new CardItemRequest();
            ResponseModel objApiResponse = new ResponseModel();
            StreamReader srd;
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<Models.API.CardItemRequest>(dcdata);

                string xml = "<result>";
                if (modelRequest != null && modelRequest.cartitems != null && modelRequest.cartitems.Count > 0)
                {
                    foreach (var item in modelRequest.cartitems)
                    {
                        xml += "<data><productname>" + item.productname + "</productname><price>" + item.price + "</price><producttype>" + item.producttype + "</producttype><quatity>" + item.quatity + "</quatity><image>" + item.image + "</image><cashbackper>" + item.cashbackper + "</cashbackper><BV>" + item.BV + "</BV><DP>" + item.DP + "</DP><PV>" + item.PV + "</PV><productcode>" + item.productcode + "</productcode><OptionTypeId>" + item.OptionTypeId + "</OptionTypeId><OptionId>" + item.OptionId + "</OptionId></data>";
                    }
                }
                xml += "</result>";
                modelRequest.xmldata = xml;
                modelRequest.ProcId = 1;
                var res = objcusdb.CreateOrderV3_Karyon(modelRequest);
                if (res != null && res.Flag == 1)
                {
                    string responseText = null;
                    // string url = "https://quaeretech.com/karyon/index.php/rest/V1/guest-carts/" + modelRequest.token + "/order";
                    string url = "https://karyon.organic/shop/index.php/rest/V1/guest-carts/" + modelRequest.token + "/order";

                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    WebRequest request = WebRequest.Create(url);
                    if (request != null)
                    {
                        request.Method = "PUT";
                        request.Timeout = 50000;
                        request.ContentType = "application/json";
                        string body = "{\n\"paymentMethod\":{\n\"method\": \"" + modelRequest.method + "\" }}\n";
                        using (Stream s1 = request.GetRequestStream())
                        {
                            using (StreamWriter sw = new StreamWriter(s1))
                                sw.Write(body);
                        }
                        Stream s = request.GetResponse().GetResponseStream();
                        srd = new StreamReader(s);
                        responseText = srd.ReadToEnd();
                        if (!String.IsNullOrEmpty(responseText))
                        {
                            modelRequest.ProcId = 2;
                            modelRequest.OrderNo = responseText.Replace('"', ' ').Trim();
                            modelRequest.orderId = res.TransId;
                            var result = objcusdb.CreateOrderV3_Karyon(modelRequest);
                            if (result != null)
                            {
                                objres.response = "success";
                                objres.message = "success";
                                objres.orderId = res.TransId;
                                objres.OrderNo = responseText.Replace('"', ' ').Trim();
                            }
                        }
                    }
                }
                else
                {
                    objres.response = "error";
                    objres.message = res.Msg;
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
            js = new DataContractJsonSerializer(typeof(CommonJsonReponse));
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
        public ResponseModel CreateOrderV3APP(RequestModel model)
        {
            CommonJsonReponse objres = new CommonJsonReponse();
            CardItemRequest modelRequest = new CardItemRequest();
            ResponseModel objApiResponse = new ResponseModel();
            StreamReader srd;
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<Models.API.CardItemRequest>(dcdata);

                string xml = "<result>";
                if (modelRequest != null && modelRequest.cartitems != null && modelRequest.cartitems.Count > 0)
                {
                    foreach (var item in modelRequest.cartitems)
                    {
                        xml += "<data><PrdId>" + item.PrdId + "</PrdId><productname>" + item.productname + "</productname><price>" + item.price + "</price><producttype>" + item.producttype + "</producttype><quatity>" + item.quatity + "</quatity><image>" + item.image + "</image><cashbackper>" + item.cashbackper + "</cashbackper><BV>" + item.BV + "</BV><DP>" + item.DP + "</DP><PV>" + item.PV + "</PV></data>";
                    }
                }
                xml += "</result>";
                modelRequest.xmldata = xml;
                modelRequest.ProcId = 1;
                var res = objcusdb.CreateOrderV3APP(modelRequest);
                if (res != null && res.Flag == 1)
                {
                    objres.response = "Success";
                    objres.message = res.Msg;

                }
                else
                {
                    objres.response = "error";
                    objres.message = res.Msg;
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
            js = new DataContractJsonSerializer(typeof(CommonJsonReponse));
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

        /*its working for Magento API */
        [HttpPost]
        public HttpResponseMessage SaveOrderM(CardItemRequest modelRequest)
        {
            try
            {
                string xml = "<result>";
                if (modelRequest != null && modelRequest.cartitems != null && modelRequest.cartitems.Count > 0)
                {
                    foreach (var item in modelRequest.cartitems)
                    {
                        xml += "<data><productname>" + item.productname + "</productname><price>" + item.price + "</price><producttype>" + item.producttype + "</producttype><quatity>" + item.quatity + "</quatity><image>" + item.image + "</image><cashbackper>" + item.cashbackper + "</cashbackper></data>";
                    }
                }
                xml += "</result>";
                modelRequest.xmldata = xml;
                var res = objcusdb.SaveOrderDetails(modelRequest);
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel PaypalToWallet(RequestModel model)
        {
            CommonJsonReponse objres = new CommonJsonReponse();
            PaypalToWallet modelRequest = new PaypalToWallet();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<Models.API.PaypalToWallet>(dcdata);
                var res = objcusdb.PaypalToWallet(modelRequest);
                if (res != null && res.Flag == 1)
                {

                    objres.response = "success";
                    objres.message = res.Msg;
                }
                else
                {
                    objres.response = "error";
                    objres.message = res.Msg;
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
            js = new DataContractJsonSerializer(typeof(CommonJsonReponse));
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
        public ResponseModel TodayReferal()
        {
            TodaysReferalResponse objres = new TodaysReferalResponse();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                var res = objcusdb.TodayReferal();
                if (res != null && res.Count > 0)
                {

                    objres.response = "success";
                    objres.Data = res;
                }
                else
                {
                    objres.response = "error";
                    objres.Data = res;
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
            js = new DataContractJsonSerializer(typeof(TodaysReferalResponse));
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
        public ResponseModel GatewayFailedTrans(RequestModel model)
        {
            CommonJsonReponse objres = new CommonJsonReponse();
            PaypalToWallet modelRequest = new PaypalToWallet();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<Models.API.PaypalToWallet>(dcdata);
                var res = objcusdb.GatewayFailedTrans(modelRequest);
                if (res != null && res.Flag == 1)
                {

                    objres.response = "success";
                    objres.message = res.Msg;
                }
                else
                {
                    objres.response = "error";
                    objres.message = res.Msg;
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
            js = new DataContractJsonSerializer(typeof(CommonJsonReponse));
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
        public ResponseModel CreateOrderV2(RequestModel model)
        {
            CommonJsonReponse objres = new CommonJsonReponse();
            CardItemRequest modelRequest = new CardItemRequest();
            ResponseModel objApiResponse = new ResponseModel();
            StreamReader srd;
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<Models.API.CardItemRequest>(dcdata);

                string xml = "<result>";
                if (modelRequest != null && modelRequest.cartitems != null && modelRequest.cartitems.Count > 0)
                {
                    foreach (var item in modelRequest.cartitems)
                    {
                        xml += "<data><productname>" + item.productname + "</productname><price>" + item.price + "</price><producttype>" + item.producttype + "</producttype><quatity>" + item.quatity + "</quatity><image>" + item.image + "</image><cashbackper>" + item.cashbackper + "</cashbackper></data>";
                    }
                }
                xml += "</result>";
                modelRequest.xmldata = xml;
                modelRequest.ProcId = 1;
                var res = objcusdb.CreateOrderV2(modelRequest);
                if (res != null && res.Flag == 1)
                {
                    string responseText = null;
                    string url = "https://LifeOne.com/index.php/rest/V1/guest-carts/" + modelRequest.token + "/order";
                    WebRequest request = WebRequest.Create(url);
                    if (request != null)
                    {

                        request.Method = "PUT";
                        request.Timeout = 50000;
                        request.ContentType = "application/json";
                        string body = "{\n\"paymentMethod\":{\n\"method\": \"" + modelRequest.method + "\" }}\n";
                        using (Stream s1 = request.GetRequestStream())
                        {
                            using (StreamWriter sw = new StreamWriter(s1))
                                sw.Write(body);
                        }

                        Stream s = request.GetResponse().GetResponseStream();
                        srd = new StreamReader(s);
                        responseText = srd.ReadToEnd();
                        if (!String.IsNullOrEmpty(responseText))
                        {
                            modelRequest.ProcId = 2;
                            modelRequest.OrderNo = responseText.Replace('"', ' ').Trim();
                            modelRequest.orderId = res.TransId;
                            var result = objcusdb.CreateOrderV2(modelRequest);
                            if (result != null)
                            {
                                objres.response = "success";
                                objres.message = "success";
                            }
                        }

                    }
                }
                else
                {
                    objres.response = "error";
                    objres.message = res.Msg;
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
            js = new DataContractJsonSerializer(typeof(CommonJsonReponse));
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
        public ResponseModel Dashboard(string Id)
        {
            ResponseModel objApiResponse = new ResponseModel();
            LifeOne.Models.API.DAL.Dashboard db = new LifeOne.Models.API.DAL.Dashboard();
            DashboardResponse obj = new DashboardResponse();
            List<RecentOrder> orderItem = new List<RecentOrder>();
            DashboardDetails dashboard = new DashboardDetails();
            Id = Id.Replace(" ", "+");
            string EncryptedText = "";
            Id = Crypto.Decryption(Aeskey, Id);
            try
            {
                DataSet data = db.DashboardData(Id);
                if (data != null)
                {
                    if (data.Tables[0] != null && data.Tables[0].Rows.Count > 0)
                    {
                        dashboard.AllBusiness = Convert.ToDecimal(data.Tables[0].Rows[0]["AllBusiness"]);
                        dashboard.TodayBusiness = Convert.ToDecimal(data.Tables[0].Rows[0]["TodayBusiness"]);
                        dashboard.TeamBusiness = Convert.ToDecimal(data.Tables[0].Rows[0]["TeamBusiness"]);
                        dashboard.Teamsize = Convert.ToInt64(data.Tables[0].Rows[0]["Teamsize"]);
                        dashboard.DreamyWallet = Convert.ToDecimal(data.Tables[0].Rows[0]["DreamyWallet"]);
                        dashboard.CashbackWallet = Convert.ToDecimal(data.Tables[0].Rows[0]["CashbackWallet"]);
                        dashboard.CommissionWallet = Convert.ToDecimal(data.Tables[0].Rows[0]["CommissionWallet"]);
                        dashboard.MagixMoves = Convert.ToDecimal(data.Tables[0].Rows[0]["MagixMoves"]);
                        dashboard.LifeTimeIncome = Convert.ToDecimal(data.Tables[0].Rows[0]["LifeTimeIncome"]);
                        dashboard.ReferralIncome = Convert.ToDecimal(data.Tables[0].Rows[0]["ReferralIncome"]);
                        dashboard.RepurchaseIncomeSelf = Convert.ToDecimal(data.Tables[0].Rows[0]["RepurchaseIncomeSelf"]);
                        dashboard.RepurchaseIncomeTeam = Convert.ToDecimal(data.Tables[0].Rows[0]["RepurchaseIncomeTeam"]);

                        if (data.Tables[1] != null && data.Tables[1].Rows.Count > 0)
                        {
                            for (int i = 0; i < data.Tables[1].Rows.Count; i++)
                            {
                                RecentOrder order = new RecentOrder();
                                order.ActionType = data.Tables[1].Rows[i]["ActionType"].ToString();
                                order.OrderId = data.Tables[1].Rows[i]["OrderId"].ToString();
                                order.OrderDate = data.Tables[1].Rows[i]["OrderDate"].ToString();
                                order.Amount = Convert.ToDecimal(data.Tables[1].Rows[i]["Amount"]);
                                order.Type = data.Tables[1].Rows[i]["Type"].ToString();
                                orderItem.Add(order);
                            }
                        }
                        obj.response = "success";
                        obj.dashboardDetails = dashboard;
                        obj.Order = orderItem;
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
                obj.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(DashboardResponse));
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

        [HttpGet]
        public ResponseModel DashboardV2(string Id)
        {
            ResponseModel objApiResponse = new ResponseModel();
            LifeOne.Models.API.DAL.Dashboard db = new LifeOne.Models.API.DAL.Dashboard();
            DashboardResponseV2 obj = new DashboardResponseV2();
            List<RecentOrder> orderItem = new List<RecentOrder>();
            DashboardDetailsV2 dashboard = new DashboardDetailsV2();
            Id = Id.Replace(" ", "+");
            string EncryptedText = "";
            Id = Crypto.Decryption(Aeskey, Id);
            try
            {
                DataSet data = db.DashboardDataV2(Id);
                if (data != null)
                {
                    if (data.Tables[0] != null && data.Tables[0].Rows.Count > 0)
                    {
                        dashboard.AllBusiness = Convert.ToDecimal(data.Tables[0].Rows[0]["AllBusiness"]);
                        dashboard.TodayBusiness = Convert.ToDecimal(data.Tables[0].Rows[0]["TodayBusiness"]);
                        dashboard.TeamBusiness = Convert.ToDecimal(data.Tables[0].Rows[0]["TeamBusiness"]);
                        dashboard.DirectCount = Convert.ToInt64(data.Tables[0].Rows[0]["DirectCount"]);
                        dashboard.Teamsize = Convert.ToInt64(data.Tables[0].Rows[0]["Teamsize"]);
                        dashboard.DreamyWallet = Convert.ToDecimal(data.Tables[0].Rows[0]["DreamyWallet"]);
                        dashboard.CashbackWallet = Convert.ToDecimal(data.Tables[0].Rows[0]["CashbackWallet"]);
                        dashboard.CommissionWallet = Convert.ToDecimal(data.Tables[0].Rows[0]["CommissionWallet"]);
                        dashboard.MagixMoves = Convert.ToDecimal(data.Tables[0].Rows[0]["MagixMoves"]);
                        dashboard.LifeTimeIncome = Convert.ToDecimal(data.Tables[0].Rows[0]["LifeTimeIncome"]);
                        dashboard.ReferralIncome = Convert.ToDecimal(data.Tables[0].Rows[0]["ReferralIncome"]);
                        dashboard.RepurchaseIncomeSelf = Convert.ToDecimal(data.Tables[0].Rows[0]["RepurchaseIncomeSelf"]);
                        dashboard.RepurchaseIncomeTeam = Convert.ToDecimal(data.Tables[0].Rows[0]["RepurchaseIncomeTeam"]);
                        dashboard.Gross = Convert.ToDecimal(data.Tables[0].Rows[0]["Gross"]);
                        dashboard.TDS = Convert.ToDecimal(data.Tables[0].Rows[0]["TDS"]);
                        dashboard.ServiceCharges = Convert.ToDecimal(data.Tables[0].Rows[0]["ServiceCharges"]);
                        dashboard.NetIncome = Convert.ToDecimal(data.Tables[0].Rows[0]["NetIncome"]);
                        dashboard.AllOrders = Convert.ToInt64(data.Tables[0].Rows[0]["AllOrders"]);
                        dashboard.TodayOrders = Convert.ToInt64(data.Tables[0].Rows[0]["TodayOrders"]);

                        if (data.Tables[1] != null && data.Tables[1].Rows.Count > 0)
                        {
                            for (int i = 0; i < data.Tables[1].Rows.Count; i++)
                            {
                                RecentOrder order = new RecentOrder();
                                order.ActionType = data.Tables[1].Rows[i]["ActionType"].ToString();
                                order.OrderId = data.Tables[1].Rows[i]["OrderId"].ToString();
                                order.OrderDate = data.Tables[1].Rows[i]["OrderDate"].ToString();
                                order.Amount = Convert.ToDecimal(data.Tables[1].Rows[i]["Amount"]);
                                order.Type = data.Tables[1].Rows[i]["Type"].ToString();
                                orderItem.Add(order);
                            }

                        }
                        obj.response = "success";
                        obj.dashboardDetails = dashboard;
                        obj.Order = orderItem;
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
                obj.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(DashboardResponseV2));
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
        [HttpGet]

        public ResponseModel GetKYCStatus(string MemberId)
        {
            GetKYCStatus objres = new GetKYCStatus();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                MemberId = MemberId.Replace(" ", "+");
                MemberId = Crypto.Decryption(Aeskey, MemberId);
                var res = objcusdb.GetKYCStstus(long.Parse(MemberId));
                if (res != null)
                {
                    objres.Response = "success";
                    objres.Status = res.Status;
                }
                else
                {
                    objres.Response = "error";
                }
            }
            catch (Exception ex)
            {
                objres.Response = "error";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(GetKYCStatus));
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
        public ResponseModel SupportType()
        {
            List<SupportType> list = new List<SupportType>();
            SupportData objres = new SupportData();

            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {

                var res = objcusdb.SupportType();
                if (res != null)
                {
                    list = res;
                    objres.response = "success";
                    objres.Item = list;
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
            js = new DataContractJsonSerializer(typeof(SupportData));
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

        public ResponseModel GetAllSupport(string MemberId)
        {
            SupportJsonJResponse objres = new SupportJsonJResponse();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                MemberId = MemberId.Replace(" ", "+");
                MemberId = Crypto.Decryption(Aeskey, MemberId);
                var res = objcusdb.GetAllSupportRequest(long.Parse(MemberId));
                if (res != null)
                {
                    objres.result = res;
                    if (res.Count != 0)
                        objres.response = "success";
                    else
                        objres.response = "No Record Found...";
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
                objres.message = "error";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(SupportJsonJResponse));
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
        public ResponseModel SupportRequest(RequestModel model)
        {
            SupportResponse objres = new SupportResponse();
            SupportRequest modelRequest = new SupportRequest();
            ResponseModel objApiResponse = new ResponseModel();
            StreamReader srd;
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<SupportRequest>(dcdata);

                var res = objcusdb.SupportRequest(modelRequest);
                if (res != null && res.Flag == 1)
                {
                    objres.response = "success";
                    objres.message = res.message;
                    objres.TicketNo = res.TicketNo;
                    objres.Flag = res.Flag;
                }
                else
                {
                    objres.response = "error";

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
            js = new DataContractJsonSerializer(typeof(SupportResponse));
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
        public ResponseModel MessgageHistory(string Fk_SupportId)
        {
            ResponseSupportHistory objres = new ResponseSupportHistory();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                Fk_SupportId = Fk_SupportId.Replace(" ", "+");
                Fk_SupportId = Crypto.Decryption(Aeskey, Fk_SupportId);
                var res = objcusdb.GetAllMessageHistory(long.Parse(Fk_SupportId));
                if (res != null)
                {
                    objres.result = res;
                    if (res.Count != 0)
                        objres.response = "success";
                    else
                        objres.response = "No Record Found...";
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
                objres.message = "error";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ResponseSupportHistory));
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
        public ResponseModel ReplyMessageHistory(RequestModel model)
        {
            SupportResponse objres = new SupportResponse();
            MSupportHistoryModel modelRequest = new MSupportHistoryModel();
            ResponseModel objApiResponse = new ResponseModel();
            StreamReader srd;
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<MSupportHistoryModel>(dcdata);

                var res = objcusdb.ReplyMessageHistory(modelRequest);
                if (res != null && res.Flag == 1)
                {
                    objres.response = "success";
                    objres.message = res.message;
                    objres.Flag = res.Flag;
                }
                else
                {
                    objres.response = "error";

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
            js = new DataContractJsonSerializer(typeof(SupportResponse));
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
        public ResponseModel CommissionTransaferToWallet(RequestModel model)
        {
            ResponseModel objApiResponse = new ResponseModel();
            AmountTransfer obj = new AmountTransfer();
            AmountTransferResponse objrsp = new AmountTransferResponse();
            DataContractJsonSerializer js;
            MemoryStream ms;
            StreamReader sr;
            string EncryptedText = "";
            string CustData = "";
            try
            {
                if (model != null)
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    obj = JsonConvert.DeserializeObject<AmountTransfer>(dcdata);
                    DataSet ds = obj.SaveCommissionAmountToWallet();
                    if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Code"].ToString() == "1")
                        {
                            objrsp.response = "success";
                            objrsp.message = ds.Tables[0].Rows[0]["Remark"].ToString();
                        }
                        else if (ds.Tables[0].Rows[0]["Code"].ToString() == "0")
                        {
                            objrsp.response = "error";
                            objrsp.message = ds.Tables[0].Rows[0]["Remark"].ToString();

                        }
                    }
                    else
                    {
                        objrsp.response = "error";
                        objrsp.message = "Unable To Process Request";
                    }
                }
            }
            catch (Exception ex)
            {
                objrsp.response = ex.Message;
            }
            js = new DataContractJsonSerializer(typeof(AmountTransferResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objrsp);
            ms.Position = 0;
            sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }

        #region Get All Pending Order by Fk_MemId and Update Status

        public void GetAllPendingOrder(long memberId)
        {
            CustomerDb db = new CustomerDb();
            List<GetToken> data = new List<GetToken>();
            OrdersResponse data2 = new OrdersResponse();

            ResultSet response = new ResultSet();
            data = db.GetAllToken(memberId);
            if (data != null && data.Count > 0)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    StreamReader srd;
                    string responseText = null;
                    string Decryptedtext = null;

                    string url = "https://shop.LifeOne.com/api/orderby_token.php?token=" + data[i].Token;
                    try
                    {
                        WebRequest request = WebRequest.Create(url);
                        if (request != null)
                        {
                            request.Method = "Get";
                            request.Timeout = 50000;
                            request.ContentType = "application/json";
                            Stream s = request.GetResponse().GetResponseStream();
                            srd = new StreamReader(s);
                            responseText = srd.ReadToEnd();
                            if (!String.IsNullOrEmpty(responseText))
                            {
                                var DData = JsonConvert.DeserializeObject<ResponseModel>(responseText);
                                Decryptedtext = Crypto.Decryption(Aeskey, DData.body);
                                data2 = JsonConvert.DeserializeObject<OrdersResponse>(Decryptedtext);
                                if (data2.response != "Error")
                                {
                                    if (data2.response == "Success")
                                    {
                                        response = db.UpdateOrderStatus(memberId, Convert.ToDecimal(data2.data[0].price), data[i].Token, 1, data2.data[0].order_id);
                                    }
                                    else
                                    {
                                        response = db.UpdateOrderStatus(memberId, 0, data[i].Token, 2, "");
                                    }
                                }
                                else
                                {
                                    response.Flag = 0;

                                }
                            }
                        }
                        else
                        {
                            response.Flag = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        response.Flag = 0;
                        response.Msg = ex.Message;
                    }
                }
            }
            else
            {
                response.Flag = 0;
            }

        }

        [HttpGet]
        public ResultSet GetAllpendingOrderId()
        {

            ResultSet obj = new ResultSet();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {

                var res = objcusdb.GetAllpendingOrderId();
                if (res != null && res.Count > 0)
                {

                    foreach (var item in res)
                    {
                        GetAllPendingOrder(item.Fk_MemId);
                    }

                    obj.Flag = 1;
                    obj.Msg = "success";
                }
                else
                {
                    obj.Flag = 0;
                    obj.Msg = "success";
                }
            }
            catch (Exception ex)
            {
                obj.Flag = 0;
                obj.Msg = "success";
            }

            return obj;
        }

        [HttpPost]
        public ResponseModel BillDeskPayment(RequestModel model)
        {
            CommonJsonReponse objres = new CommonJsonReponse();
            BillDeskPayment modelRequest = new BillDeskPayment();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<Models.API.BillDeskPayment>(dcdata);
                var res = objcusdb.BillDeskToWallet(modelRequest);
                if (res != null && res.Flag == 1)
                {

                    objres.response = "success";
                    objres.message = res.Msg;
                }
                else
                {
                    objres.response = "error";
                    objres.message = res.Msg;
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
            js = new DataContractJsonSerializer(typeof(CommonJsonReponse));
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

        #endregion
        [HttpGet]
        public ResponseModel GetOrderList(string Fk_Memid)
        {
            OrdersJsonJResponse objres = new OrdersJsonJResponse();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                Fk_Memid = Fk_Memid.Replace(" ", "+");
                Fk_Memid = Crypto.Decryption(Aeskey, Fk_Memid);
                var res = objcusdb.GetAllOrders(long.Parse(Fk_Memid), "", 1);
                if (res != null)
                {
                    objres.result = res;
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
            js = new DataContractJsonSerializer(typeof(OrdersJsonJResponse));
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
        public ResponseModel GetOrderList_Karyon(string Fk_Memid)
        {
            OrdersJsonJResponse objres = new OrdersJsonJResponse();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                Fk_Memid = Fk_Memid.Replace(" ", "+");
                Fk_Memid = Crypto.Decryption(Aeskey, Fk_Memid);
                var res = objcusdb.GetAllOrdersKaryon(long.Parse(Fk_Memid), "", 1);
                if (res != null)
                {
                    objres.result = res;
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
            js = new DataContractJsonSerializer(typeof(OrdersJsonJResponse));
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
        public ResponseModel GetDetailedOrderList(string OrderNo)
        {
            OrdersDetailsJsonJResponse objres = new OrdersDetailsJsonJResponse();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                OrderNo = OrderNo.Replace(" ", "+");
                OrderNo = Crypto.Decryption(Aeskey, OrderNo);
                var res = objcusdb.GetOrderDetails(0, OrderNo, 2);
                if (res != null)
                {
                    objres.result = res;
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
            js = new DataContractJsonSerializer(typeof(OrdersDetailsJsonJResponse));
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
        public ResponseModel GetDetailedOrderList_Karyon(string OrderNo)
        {
            OrdersDetailsJsonJResponse objres = new OrdersDetailsJsonJResponse();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                OrderNo = OrderNo.Replace(" ", "+");
                OrderNo = Crypto.Decryption(Aeskey, OrderNo);
                var res = objcusdb.GetOrderDetailsKaryon(0, OrderNo, 2);
                if (res != null)
                {
                    objres.result = res;
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
            js = new DataContractJsonSerializer(typeof(OrdersDetailsJsonJResponse));
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
        public ResponseModel UpdateAndAddPaymentStatus_V2(RequestModel model)
        {
            ResponseModel objApiResponse = new ResponseModel();
            AllPaymentDetailsRequest obj = new AllPaymentDetailsRequest();
            AllPaymentDetailsResponse objrps = new AllPaymentDetailsResponse();
            Razorpay.Api.Order objorder = null;
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            StreamReader sr;

            string EncryptedText = "";
            try
            {
                if (model != null)
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    obj = JsonConvert.DeserializeObject<AllPaymentDetailsRequest>(dcdata);

                    string OrderId = "", Status = "";
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    RazorpayClient client = null;
                    //client = new RazorpayClient("rzp_test_AFSSERe0Blac2S", "X6UBRaEzdUHdJFfXPQDmdLKb");
                    client = new RazorpayClient(RazorPayLiveKey, RazorPayLiveSecret);
                    //key and vlaue should be changed                    
                    if (obj.Action == 1)
                    {
                        Dictionary<string, object> options = new Dictionary<string, object>();
                        options.Add("amount", obj.Amount * 100);
                        options.Add("receipt", "");
                        options.Add("currency", "INR");
                        options.Add("payment_capture", 1);
                        objorder = client.Order.Create(options);
                        OrderId = objorder["id"].ToString();
                        objrps.OrderId = OrderId;
                        obj.OrderId = OrderId;
                    }
                    if (obj.Action == 2)
                    {
                        objorder = client.Order.Fetch(obj.OrderId);
                        Status = objorder["status"].ToString();
                        objrps.Status = Status;
                    }

                    var res = objcusdb.SaveUpdatePaymentDetails(obj);
                    if (res != null && res.flag == 1)
                    {

                        objrps = res;
                        objrps.OrderId = OrderId;
                        objrps.Status = Status;
                        objrps.Response = "Success";
                    }
                    else
                    {
                        objrps = res;
                        objrps.Response = "Error";
                    }
                }
                else
                {
                    objrps.Response = "error";
                }
            }
            catch (Exception ex)
            {
                objrps.Response = "error" + ex;
            }
            js = new DataContractJsonSerializer(typeof(AllPaymentDetailsResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objrps);
            ms.Position = 0;
            sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }

        public ResponseModel UpdateAndAddPaymentStatus_V2Test(RequestModel model)
        {
            ResponseModel objApiResponse = new ResponseModel();
            AllPaymentDetailsRequest obj = new AllPaymentDetailsRequest();
            AllPaymentDetailsResponse objrps = new AllPaymentDetailsResponse();
            Razorpay.Api.Order objorder = null;
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            StreamReader sr;

            string EncryptedText = "";
            try
            {
                if (model != null)
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    obj = JsonConvert.DeserializeObject<AllPaymentDetailsRequest>(dcdata);

                    string OrderId = "", Status = "";
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    RazorpayClient client = null;
                    //client = new RazorpayClient("rzp_test_AFSSERe0Blac2S", "X6UBRaEzdUHdJFfXPQDmdLKb");
                    client = new RazorpayClient(RazorPayLocalKey, RazorPayLocalSecret); //test API Here
                    //client = new RazorpayClient("rzp_live_EsXl7y5NVT088S", "aLOdpHJZKwjKwXREWXw9qDkK");
                    //key and vlaue should be changed                    
                    if (obj.Action == 1)
                    {
                        Dictionary<string, object> options = new Dictionary<string, object>();
                        options.Add("amount", obj.Amount * 100);
                        options.Add("receipt", "");
                        options.Add("currency", "INR");
                        options.Add("payment_capture", 1);
                        objorder = client.Order.Create(options);
                        OrderId = objorder["id"].ToString();
                        objrps.OrderId = OrderId;
                        obj.OrderId = OrderId;
                    }
                    if (obj.Action == 2)
                    {
                        objorder = client.Order.Fetch(obj.OrderId);
                        Status = objorder["status"].ToString();
                        objrps.Status = Status;
                    }

                    var res = objcusdb.SaveUpdatePaymentDetails(obj);
                    if (res != null && res.flag == 1)
                    {

                        objrps = res;
                        objrps.OrderId = OrderId;
                        objrps.Status = Status;
                        objrps.Response = "Success";
                    }
                    else
                    {
                        objrps = res;
                        objrps.Response = "Error";
                    }
                }
                else
                {
                    objrps.Response = "error";
                }
            }
            catch (Exception ex)
            {
                objrps.Response = "error" + ex;
            }
            js = new DataContractJsonSerializer(typeof(AllPaymentDetailsResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objrps);
            ms.Position = 0;
            sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }

        [HttpPost]
        public ApiResponseModel UserBusinessDashboard(RequestModel model)
        {

            ApiResponseModel objApiResponse = new ApiResponseModel();
            BusinessDashboardRequest objrqst = new BusinessDashboardRequest();
            BusinessDashbaord objrps = new BusinessDashbaord();
            List<LevelStatus> objlst = new List<LevelStatus>();
            string EncryptedText = "";
            try
            {

                string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, model.body);
                objrqst = JsonConvert.DeserializeObject<BusinessDashboardRequest>(dcdata);
                DataSet ds = objrqst.GetBusinessDashboard();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Code"].ToString() == "1")
                        {

                            objrps.SelfTeam = ds.Tables[0].Rows[0]["SelfTeam"].ToString();
                            objrps.TotalTeam = ds.Tables[0].Rows[0]["TotalTeam"].ToString();
                            objrps.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                            objrps.JoiningDate = ds.Tables[0].Rows[0]["JoiningDate"].ToString();
                            objrps.Name = ds.Tables[0].Rows[0]["Name"].ToString();

                            objrps.LevelStatusList = objlst;
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                foreach (DataRow dr in ds.Tables[1].Rows)
                                {
                                    objlst.Add(new LevelStatus
                                    {
                                        LevelName = dr["LevelName"].ToString(),
                                        LevelCount = dr["Level"].ToString(),
                                        TeamCount = dr["levelcount"].ToString()
                                    });
                                }
                            }
                            else
                            {
                                for (int i = 1; i < 12; i++)
                                {
                                    string _levelname = "Level" + i.ToString();
                                    objlst.Add(new LevelStatus
                                    {
                                        LevelName = _levelname,
                                        LevelCount = i.ToString(),
                                        TeamCount = "0"
                                    });
                                }
                            }
                            objrps.Response = "success";
                            objrps.Message = "success";
                        }
                        else
                        {
                            objrps.Response = "error";
                            objrps.Message = "error";
                        }
                    }

                }
                else
                {
                    objrps.Response = "error";
                    objrps.Message = "error";
                }


            }
            catch (Exception ex)
            {
                objrps.Response = "error";
                objrps.Message = ex.Message;

            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BusinessDashbaord));
            ms = new MemoryStream();
            js.WriteObject(ms, objrps);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }

        /*INR Trsanctions*/
        public ApiResponseModel GetINRDealsTransaction(string LoginId, string Page = "1", string StoreName = "")
        {

            Models.API.INRModel obj = new Models.API.INRModel();
            ApiResponseModel objApiResponse = new ApiResponseModel();
            string EncryptedText = "";
            if (!string.IsNullOrEmpty(LoginId.ToString()) && !string.IsNullOrEmpty(Page.ToString()))
            {
                try
                {
                    LoginId = LoginId.Replace(" ", "+");
                    LoginId = ApiEncrypt_Decrypt.DecryptString(Aeskey, LoginId);
                    Page = Page.Replace(" ", "+");
                    Page = ApiEncrypt_Decrypt.DecryptString(Aeskey, Page);
                    if (StoreName != "")
                    {
                        StoreName = StoreName.Replace(" ", "+");
                        StoreName = ApiEncrypt_Decrypt.DecryptString(Aeskey, StoreName);
                    }
                    try
                    {
                        List<INRDetails> INRList = new List<INRDetails>();
                        SqlParameter[] para ={

                                   new SqlParameter("@LoginId",Convert.ToInt64(LoginId)),
                                    new SqlParameter("@page",Convert.ToInt32(Page)),
                                    new SqlParameter("@StoreName",StoreName)

                       };
                        DataSet ds = LifeOne.Models.API.DAL.DBHelper.ExecuteQuery("GetINRDealsTransaction", para);
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            obj.response = "success";
                            foreach (DataRow item in ds.Tables[0].Rows)
                            {
                                INRList.Add(new INRDetails
                                {
                                    transactionId = item["transactionId"].ToString(),
                                    storeName = item["StoreName"].ToString(),
                                    saleAmount = Convert.ToDecimal(item["SaleAmount"].ToString()),
                                    transactionDate = item["transactionDate"].ToString(),
                                    payout = Convert.ToDecimal(item["Payout"].ToString()),
                                    status = item["Status"].ToString(),
                                    subId = item["SubId"].ToString(),
                                    imageurl = item["imageurl"].ToString(),
                                    fK_MEMId = int.Parse(item["FK_MEMId"].ToString())
                                });
                                if (INRList.Count > 0)
                                {
                                    obj.lstINR = INRList;
                                }
                            }
                        }
                        else
                        {
                            obj.response = "No record found";
                        }
                    }
                    catch (Exception ex)
                    {
                        obj.response = "error";

                    }
                }
                catch (Exception ex)
                {
                    obj.response = "error";
                }
            }
            else
            {
                obj.response = "error";
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(Models.API.INRModel));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }
        public ApiResponseModel GetINRDealsTransactionWithoutpaging(int LoginId = 0)
        {
            Models.API.INRModel obj = new Models.API.INRModel();
            ApiResponseModel objApiResponse = new ApiResponseModel();
            string EncryptedText = "";
            if (!string.IsNullOrEmpty(LoginId.ToString()))
            {
                try
                {
                    string Login = LoginId.ToString();
                    Login = ApiEncrypt_Decrypt.DecryptString(Aeskey, Login);
                    List<INRDetails> INRList = new List<INRDetails>();
                    SqlParameter[] para ={

                                   new SqlParameter("@LoginId",Login)


                    };
                    DataSet ds = LifeOne.Models.API.DAL.DBHelper.ExecuteQuery("[GetINRDealsTransactionWithOutPaging]", para);

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj.response = "success";

                        foreach (DataRow item in ds.Tables[0].Rows)
                        {
                            INRList.Add(new INRDetails
                            {
                                transactionId = item["transactionId"].ToString(),
                                storeName = item["StoreName"].ToString(),
                                saleAmount = Convert.ToDecimal(item["SaleAmount"].ToString()),
                                transactionDate = item["transactionDate"].ToString(),
                                payout = Convert.ToDecimal(item["Payout"].ToString()),
                                status = item["Status"].ToString(),
                                subId = item["SubId"].ToString(),
                                fK_MEMId = int.Parse(item["FK_MEMId"].ToString())
                            });
                            if (INRList.Count > 0)
                            {
                                obj.lstINR = INRList;
                            }
                        }

                    }

                    else
                    {
                        obj.response = "No record found";

                    }
                }
                catch (Exception ex)
                {
                    obj.response = "error";

                }
            }
            else
            {
                obj.response = "error";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(Models.API.INRModel));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;

        }

        //Added By Dainik Kumar
        public ApiResponseModel GetCommissionDetails(string Fk_MemId)
        {
            try
            {

                ApiResponseModel objApiResponse = new ApiResponseModel();
                List<CommissionDetails> orderItem = new List<CommissionDetails>();
                CommissionDetailsResponse obj1 = new CommissionDetailsResponse();
                Category db = new Category();
                string EncryptedText = "";
                try
                {
                    string MemId = Crypto.Decryption(Aeskey, Fk_MemId);
                    DataSet ds = db.CommissiomDetails(Convert.ToInt32(MemId));
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Code"].ToString() == "1")
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                CommissionDetails obj = new CommissionDetails();
                                List<SubCommissionDetails> Catlist = new List<SubCommissionDetails>();
                                obj.CommissionName = dr["CommissionName"].ToString();
                                obj.CommissionAmount = Convert.ToDecimal(dr["CommissionAmount"].ToString());
                                if (ds.Tables[1].Rows.Count > 0)
                                {
                                    foreach (DataRow Dr1 in ds.Tables[1].Rows)
                                    {
                                        if (dr["CommissionId"].ToString() == Dr1["CommissionId"].ToString())
                                        {
                                            SubCommissionDetails objCat = new SubCommissionDetails();
                                            //objCat. = Dr1["categoryId"].ToString();
                                            objCat.SubCommissionName = Dr1["SubCommissionName"].ToString();
                                            objCat.SubCommissionAmount = Convert.ToDecimal(Dr1["SubCommissionAmount"].ToString());
                                            objCat.IconUrl = Dr1["IconUrl"].ToString();
                                            objCat.TotalPv = Convert.ToDecimal(Dr1["TotalPv"].ToString());

                                            Catlist.Add(objCat);
                                        }
                                    }
                                    obj.SubCommissionList = Catlist;
                                    orderItem.Add(obj);
                                }
                            }
                            obj1.TotalCommision = Convert.ToDecimal(ds.Tables[0].Rows[0]["TotalCommision"].ToString());
                            obj1.UpdatedOn = ds.Tables[0].Rows[0]["UpdatedOn"].ToString();
                            obj1.response = "Success";
                            obj1.CommissionList = orderItem;
                        }
                        else
                        {
                            obj1.response = "Success";
                            obj1.CommissionList = orderItem;
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
                js = new DataContractJsonSerializer(typeof(CommissionDetailsResponse));
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
            catch (Exception)
            {

                throw;
            }
        }
        public ApiResponseModel GetHoldUnclearBalanceDetails(string Fk_MemId, string Type)
        {

            ApiResponseModel objApiResponse = new ApiResponseModel();
            HoldUnclearBalanceResponse obj1 = new HoldUnclearBalanceResponse();
            Category db = new Category();
            string EncryptedText = "";
            try
            {
                string MemId = Crypto.Decryption(Aeskey, Fk_MemId);
                DataSet ds = db.GetHoldUncleareBalance(Convert.ToInt32(MemId), Type);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Code"].ToString() == "1")
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            HoldUnclearBalanceResponse obj = new HoldUnclearBalanceResponse();
                            List<HoldUnclearBalanceDetails> Catlist = new List<HoldUnclearBalanceDetails>();
                            obj.TotalHoldUnclear = Convert.ToDecimal(dr["TotalHoldUnclearBalnce"].ToString());
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                foreach (DataRow Dr1 in ds.Tables[1].Rows)
                                {
                                    HoldUnclearBalanceDetails objCat = new HoldUnclearBalanceDetails();
                                    objCat.Reason = Dr1["Reason"].ToString();
                                    objCat.Title = Dr1["Title"].ToString();
                                    objCat.Amount = Convert.ToDecimal(Dr1["Amount"].ToString());
                                    objCat.TransDate = Dr1["TransDate"].ToString();
                                    Catlist.Add(objCat);
                                }
                                obj1.lstHoldUnclear = Catlist;
                            }
                        }
                        obj1.UpdatedOn = ds.Tables[0].Rows[0]["UpdatedOn"].ToString();
                        obj1.response = "Success";
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
            js = new DataContractJsonSerializer(typeof(HoldUnclearBalanceResponse));
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

        [HttpGet]
        public ApiResponseModel AssociateDashboardApi(string Fk_MemId)
        {
            ApiResponseModel objApiResponse = new ApiResponseModel();
            List<DirectList> orderItem = new List<DirectList>();
            AssociateDashoboardResponse obj1 = new AssociateDashoboardResponse();
            Category db = new Category();
            string EncryptedText = "";
            try
            {

                Fk_MemId = Fk_MemId.Replace(" ", "+");
                string MemId = Crypto.Decryption(Aeskey, Fk_MemId);
                DataSet ds = db.GetAssociateDashboardDetails(Convert.ToInt32(MemId));
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Code"].ToString() == "1")
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            DirectList obj = new DirectList();
                            List<DirectList> Catlist = new List<DirectList>();
                            SalesDetails objSales = new SalesDetails();
                            List<SalesDetails> ObjSalelist = new List<SalesDetails>();

                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                foreach (DataRow Dr1 in ds.Tables[1].Rows)
                                {
                                    DirectList objCat = new DirectList();
                                    objCat.profilepic = Dr1["profilepic"].ToString();
                                    objCat.Name = Dr1["Name"].ToString();
                                    objCat.JoiningDate = Dr1["JoiningDate"].ToString();
                                    objCat.LoginId = Dr1["LoginId"].ToString();
                                    objCat.ActiveStatus = Dr1["ActiveStatus"].ToString();
                                    Catlist.Add(objCat);
                                }
                                obj1.directLists = Catlist;
                            }
                            else
                            {
                                obj1.directLists = Catlist;
                            }

                        }
                        foreach (DataRow dr in ds.Tables[3].Rows)
                        {
                            IncomeDetails obj = new IncomeDetails();
                            List<IncomeDetails> IncomeList = new List<IncomeDetails>();


                            if (ds.Tables[3].Rows.Count > 0)
                            {
                                foreach (DataRow Dr1 in ds.Tables[3].Rows)
                                {
                                    IncomeDetails objCat = new IncomeDetails();
                                    objCat.Income = Dr1["Income"].ToString();
                                    objCat.Paid = decimal.Parse(Dr1["Paid"].ToString());
                                    objCat.Unpaid = decimal.Parse(Dr1["Unpaid"].ToString());

                                    IncomeList.Add(objCat);
                                }
                                obj1.IncomeList = IncomeList;
                            }
                            else
                            {
                                obj1.IncomeList = IncomeList;
                            }

                        }
                        obj1.Name = ds.Tables[0].Rows[0]["DisplayName"].ToString();
                        obj1.TotalDirct = int.Parse(ds.Tables[0].Rows[0]["TotalDirect"].ToString());
                        obj1.TotalAssociates = int.Parse(ds.Tables[0].Rows[0]["TotalTeam"].ToString());
                        obj1.TotalActive = int.Parse(ds.Tables[0].Rows[0]["TotalActive"].ToString());
                        obj1.TotalInactive = int.Parse(ds.Tables[0].Rows[0]["TotalInactive"].ToString());
                        obj1.CurrentBusinessLeft = decimal.Parse(ds.Tables[0].Rows[0]["CurrentBusinessLeft"].ToString());
                        obj1.CurrentBusinessRight = decimal.Parse(ds.Tables[0].Rows[0]["CurrentBusinessRight"].ToString());

                        obj1.CarryforwordLeft = decimal.Parse(ds.Tables[0].Rows[0]["CarryforwordLeft"].ToString());
                        obj1.CarryforwordRight = decimal.Parse(ds.Tables[0].Rows[0]["CarryforwordRight"].ToString());
                        obj1.TeamBusinessLeft = decimal.Parse(ds.Tables[0].Rows[0]["LeftBusiness"].ToString());
                        obj1.TeamBusinessRight = decimal.Parse(ds.Tables[0].Rows[0]["RightBusiness"].ToString());


                        obj1.RepurchaseCurrentBusinessRight = decimal.Parse(ds.Tables[0].Rows[0]["RepurchaseCurrentBusinessRight"].ToString());
                        obj1.RepurchaseCurrentBusinessLeft = decimal.Parse(ds.Tables[0].Rows[0]["RepurchaseCurrentBusinessLeft"].ToString());

                        obj1.TeamRepurchaseLeftBusiness = decimal.Parse(ds.Tables[0].Rows[0]["RepurchaseLeftBusiness"].ToString());
                        obj1.TeamRepurchaseRightBusiness = decimal.Parse(ds.Tables[0].Rows[0]["RepurchaseRightBusiness"].ToString());
                        obj1.RepurchaseCarryforwordLeft = decimal.Parse(ds.Tables[0].Rows[0]["RepurchaseCarryforwordLeft"].ToString());
                        obj1.RepurchaseCarryforwordRight = decimal.Parse(ds.Tables[0].Rows[0]["RepurchaseCarryforwordRight"].ToString());

                        obj1.TotalIncome = decimal.Parse(ds.Tables[0].Rows[0]["TotalIncome"].ToString());
                        obj1.DeductionAmount = decimal.Parse(ds.Tables[0].Rows[0]["DeductionAmount"].ToString());
                        obj1.ShoppingWallet = decimal.Parse(ds.Tables[0].Rows[0]["ShoppingWallet"].ToString());
                        obj1.UtilityWallet = decimal.Parse(ds.Tables[0].Rows[0]["UtilityWallet"].ToString());

                        obj1.response = "success";
                    }
                    else
                    {
                        obj1.response = "success";
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
            js = new DataContractJsonSerializer(typeof(AssociateDashoboardResponse));
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
        [HttpPost]
        public ApiResponseModel AssociateGeneologyTree(RequestModel model)
        {
            ApiResponseModel objApiResponse = new ApiResponseModel();
            GenealogyResponse obj1 = new GenealogyResponse();
            Genealogy Obj = new Genealogy();
            Category db = new Category();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                Obj = JsonConvert.DeserializeObject<Genealogy>(dcdata);
                DataSet ds = db.TreeView(Obj);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        List<GenealogyTree> Catlist = new List<GenealogyTree>();
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            GenealogyTree objCat = new GenealogyTree();
                            objCat.MemId = dr["MemId"].ToString();
                            objCat.ParentId = dr["ParentId"].ToString();
                            objCat.SponsorId = dr["SponsorId"].ToString();
                            objCat.SponsorLoginId = dr["SponsorLoginId"].ToString();
                            objCat.LoginId = dr["LoginId"].ToString();
                            objCat.MobileNo = dr["MobileNo"].ToString();
                            objCat.DisplayName = dr["DisplayName"].ToString();
                            objCat.TempPermanent = dr["TempPermanent"].ToString();
                            objCat.Leg = dr["Leg"].ToString();
                            objCat.MemberLevel = dr["MemberLevel"].ToString();
                            objCat.status = dr["status"].ToString();
                            objCat.IDStatus = dr["IDStatus"].ToString();
                            objCat.SpillById = dr["SpillById"].ToString();
                            objCat.JoiningDate = dr["JoiningDate"].ToString();
                            objCat.UpgradeDate = dr["UpgradeDate"].ToString();
                            objCat.AllLeg1 = dr["AllLeg1"].ToString();
                            objCat.AllLeg2 = dr["AllLeg2"].ToString();
                            objCat.PermanentLeg1 = dr["PermanentLeg1"].ToString();
                            objCat.PermanentLeg2 = dr["PermanentLeg2"].ToString();
                            objCat.LeftNonActive = dr["LeftNonActive"].ToString();
                            objCat.RightNonActive = dr["RightNonActive"].ToString();
                            objCat.BvLeft = dr["LeftBusiness"].ToString();
                            objCat.BvRight = dr["RightBusiness"].ToString();
                            objCat.TotalBv = dr["TotalBusiness"].ToString();
                            objCat.CurrLeft = dr["CurrLeft"].ToString();
                            objCat.CurrRight = dr["CurrRight"].ToString();
                            objCat.ParentLoginId = dr["ParentLoginId"].ToString();
                            Catlist.Add(objCat);
                        }
                        obj1.Genealogytreelist = Catlist;
                        obj1.Response = "Success";
                    }
                    else
                    {
                        obj1.Response = "Record Not Found";
                    }
                }
                else
                {
                    obj1.Response = "error";
                }
            }
            catch (Exception ex)
            {
                obj1.Response = "error";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(GenealogyResponse));
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

        [HttpGet]
        public ApiResponseModel PayoutMasterList()
        {
            ApiResponseModel objApiResponse = new ApiResponseModel();
            PayoutMasterListResponse obj1 = new PayoutMasterListResponse();
            Category db = new Category();
            string EncryptedText = "";
            try
            {

                DataSet ds = db.PayoutMasterList();
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        List<PayoutMasterList> Catlist = new List<PayoutMasterList>();
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            PayoutMasterList objCat = new PayoutMasterList();
                            objCat.PayoutNo = int.Parse(dr["PayoutNo"].ToString());
                            objCat.Payout = dr["Payout"].ToString();

                            Catlist.Add(objCat);
                        }
                        obj1.payoutMasterList = Catlist;
                        obj1.response = "Success";
                    }
                    else
                    {
                        obj1.response = "Record Not Found";
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
            js = new DataContractJsonSerializer(typeof(PayoutMasterListResponse));
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

        [HttpGet]
        public ApiResponseModel RepurchasePayoutMasterList()
        {
            ApiResponseModel objApiResponse = new ApiResponseModel();
            PayoutMasterListResponse obj1 = new PayoutMasterListResponse();
            Category db = new Category();
            string EncryptedText = "";
            try
            {

                DataSet ds = db.SelectRepurchasePayoutMaster();
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        List<PayoutMasterList> Catlist = new List<PayoutMasterList>();
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            PayoutMasterList objCat = new PayoutMasterList();
                            objCat.PayoutNo = int.Parse(dr["PayoutNo"].ToString());
                            objCat.Payout = dr["PayoutValue"].ToString();

                            Catlist.Add(objCat);
                        }
                        obj1.payoutMasterList = Catlist;
                        obj1.response = "Success";
                    }
                    else
                    {
                        obj1.response = "Record Not Found";
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
            js = new DataContractJsonSerializer(typeof(PayoutMasterListResponse));
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

        [HttpGet]
        public ApiResponseModel IncentiveDetails(string Fk_MemId)
        {
            ApiResponseModel objApiResponse = new ApiResponseModel();
            IncetiveResponse obj1 = new IncetiveResponse();
            Category db = new Category();
            string EncryptedText = "";
            try
            {
                string MemId = Crypto.Decryption(Aeskey, Fk_MemId);
                DataSet ds = db.GetIncentiveDetails(Convert.ToInt32(MemId));
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Code"].ToString() == "1")
                    {

                        List<MyIncentiveDetails> Catlist = new List<MyIncentiveDetails>();
                        foreach (DataRow Dr in ds.Tables[0].Rows)
                        {
                            MyIncentiveDetails objCat = new MyIncentiveDetails();
                            objCat.Month = Dr["Month"].ToString();
                            objCat.Year = Convert.ToInt32(Dr["Year"].ToString());
                            objCat.CurrentAmount = Convert.ToDecimal(Dr["CurrentAmount"].ToString());
                            objCat.PrevBalance = Convert.ToDecimal(Dr["PrevBalance"].ToString());
                            objCat.TotalAmount = Convert.ToDecimal(Dr["TotalAmount"].ToString());
                            objCat.CurrentPBV = Convert.ToDecimal(Dr["CurrentPBV"].ToString());
                            Catlist.Add(objCat);
                        }
                        obj1.lstIncetive = Catlist;
                        obj1.response = "Success";
                    }
                    else
                    {
                        obj1.response = "Record Not Found";
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
            js = new DataContractJsonSerializer(typeof(IncetiveResponse));
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
        [HttpPost]
        public ApiResponseModel PayoutReport(RequestModel model)
        {
            ApiResponseModel objApiResponse = new ApiResponseModel();
            PayoutResponse obj1 = new PayoutResponse();
            PayoutRequestAPI Obj = new PayoutRequestAPI();
            Category db = new Category();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                Obj = JsonConvert.DeserializeObject<PayoutRequestAPI>(dcdata);
                DataSet ds = db.PayoutReportAPI(Obj);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Code"].ToString() == "1")
                    {

                        List<PayoutDetails> Catlist = new List<PayoutDetails>();

                        foreach (DataRow Dr in ds.Tables[0].Rows)
                        {
                            PayoutDetails objCat = new PayoutDetails();
                            objCat.UserType = Dr["UserType"].ToString();
                            objCat.DisplayName = Dr["DisplayName"].ToString();
                            objCat.Amount = Convert.ToDecimal(Dr["NetAmount"].ToString());
                            objCat.IncomeDate = Dr["IncomeDate"].ToString();
                            objCat.ClosingDate = Dr["ClosingDate"].ToString();
                            objCat.PaymentDate = Dr["PaymentDate"].ToString();
                            objCat.MemberAccNo = Dr["MemberAccNo"].ToString();
                            objCat.MemberBankName = Dr["MemberBankName"].ToString();
                            objCat.IFSCCode = Dr["IFSCCode"].ToString();
                            objCat.LoginId = Dr["LoginId"].ToString();
                            objCat.PayoutNo = Convert.ToInt32(Dr["PayoutNo"].ToString());
                            objCat.GrossAmount = Convert.ToDecimal(Dr["GrossAmount"].ToString());
                            objCat.DirectIncome = Convert.ToDecimal(Dr["DirectIncome"].ToString());
                            objCat.LeadershipBonus = Convert.ToDecimal(Dr["LeadershipBonus"].ToString());
                            objCat.MatchingBonus = Convert.ToDecimal(Dr["MatchingBonus"].ToString());
                            objCat.NetAmount = Convert.ToDecimal(Dr["NetAmount"].ToString());
                            objCat.ProcessingFee = Convert.ToDecimal(Dr["ProcessingFee"].ToString());
                            objCat.TDSAmount = Convert.ToDecimal(Dr["TDSAmount"].ToString());
                            Catlist.Add(objCat);
                        }
                        obj1.lstPayout = Catlist;
                        obj1.response = "Success";
                    }
                    else
                    {
                        obj1.response = "Record Not Found";
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
            js = new DataContractJsonSerializer(typeof(PayoutResponse));
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
        [HttpGet]
        public ResponseModel GetDownlineAPI(string memberId, string page, string pagesize)
        {
            DownlineTeamAPI obj = new DownlineTeamAPI();
            List<DownlineAPI> data = new List<DownlineAPI>();
            ResponseModel objApiResponse = new ResponseModel();
            memberId = memberId.Replace(" ", "+");
            memberId = Crypto.Decryption(Aeskey, memberId);
            page = page.Replace(" ", "+");
            page = Crypto.Decryption(Aeskey, page);
            pagesize = pagesize.Replace(" ", "+");
            pagesize = Crypto.Decryption(Aeskey, pagesize);
            string EncryptedText = "";
            try
            {
                data = objcusdb.GetDownlineAPI(long.Parse(memberId), Convert.ToInt32(page), Convert.ToInt32(pagesize));
                //var datacount = objcusdb.getTotalCount(long.Parse(memberId), searchtype, searchvalue);
                if (data != null)
                {

                    obj.response = "success";
                    obj.result = data;

                }
                else
                {
                    obj.response = "error";
                }

            }
            catch (Exception ex)
            {
                obj.response = "error ";
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(DownlineTeamAPI));
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
        public ResponseModel GetDirectAPI(RequestModel model)
        {
            ResponseModel objApiResponse = new ResponseModel();
            DirectAPI d = new DirectAPI();
            ResponseDirectAPI objres = new ResponseDirectAPI();
            //ResponseModel returnResponse = new ResponseModel();            
            string dcdata = Crypto.Decryption(Aeskey, model.body);
            d = JsonConvert.DeserializeObject<DirectAPI>(dcdata);
            objres.lstDirect = objcusdb.GetDirectAPI(d.FK_MemId);
            string EncryptedText = "";

            if (objres.lstDirect != null && objres.lstDirect.Count > 0)
            {
                objres.response = "success";
                objres.message = "success";
            }
            else
            {
                objres.response = "Failed";
                objres.message = "Failed to retireve direct list";
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ResponseDirectAPI));
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
        public ResponseModel GetDirectAPI_V2(RequestModel model)
        {
            ResponseModel objApiResponse = new ResponseModel();
            MDirectV2 d = new MDirectV2();
            ResponseDirectAPIV2 objres = new ResponseDirectAPIV2();
            //ResponseModel returnResponse = new ResponseModel();            
            string dcdata = Crypto.Decryption(Aeskey, model.body);
            d = JsonConvert.DeserializeObject<MDirectV2>(dcdata);
            objres = objcusdb.GetDirectAPIV2(d.FK_MemId, d.Page, d.PageSize);
            string EncryptedText = "";

            if (objres.MemberuserInfo != null && objres.MemberuserInfo.Count > 0)
            {
                objres.response = "success";
                objres.message = "success";
            }
            else
            {
                objres.response = "Failed";
                objres.message = "Failed to retireve direct list";
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ResponseDirectAPIV2));
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
        public ResponseModel GetMemberKYCList(RequestModel model)
        {
            ResponseModel objApiResponse = new ResponseModel();
            CustomerDb db = new CustomerDb();
            MemberKYC obj = new MemberKYC();
            MemberKYC objres = new MemberKYC();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                obj = JsonConvert.DeserializeObject<MemberKYC>(dcdata);
                var res = db.GetMemberKYCList(obj);
                if (res != null)
                {
                    objres = res;
                    objres.response = "success";
                    objres.Message = "success";

                }
                else
                {
                    objres.response = "Fail";
                    objres.Message = "Data does not exists";

                }
            }
            catch (Exception ex)
            {
                objres.response = "Fail";
                objres.Message = "Something went wrong";

            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(MemberKYC));
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
        public ResponseModel GetMemberKYCListDownline(RequestModel model)
        {
            ResponseModel objApiResponse = new ResponseModel();
            CustomerDb db = new CustomerDb();
            MemberKYC obj = new MemberKYC();
            MemberKYC objres = new MemberKYC();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                obj = JsonConvert.DeserializeObject<MemberKYC>(dcdata);
                var res = db.GetMemberKYCListDownline(obj);
                if (res != null)
                {
                    objres = res;
                    objres.response = "success";
                    objres.Message = "success";
                }
                else
                {
                    objres.response = "Fail";
                    objres.Message = "Data does not exists";

                }
            }
            catch (Exception ex)
            {
                objres.response = "Fail";
                objres.Message = "Something went wrong";

            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(MemberKYC));
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

        //[HttpGet]  Changed by ramu verma 06072022
        //public ResponseModel GetAccountStatement(string Fk_MemId,string PayoutNo)
        [HttpPost]
        public ResponseModel GetAccountStatement(RequestModel model)
        {
            ResponseModel objApiResponse = new ResponseModel();
            CustomerDb db = new CustomerDb();
            AccountStateMentDetails obj = new AccountStateMentDetails();
            AccountStateMentResponse objres = new AccountStateMentResponse();
            string EncryptedText = "";
            try
            {
                //string Payout = Crypto.Decryption(Aeskey, PayoutNo);
                //string MemId = Crypto.Decryption(Aeskey, Fk_MemId);

                // objres.List = db.GetAccountSatateMent(long.Parse(MemId), long.Parse(Payout));

                string dcdata = Crypto.Decryption(Aeskey, model.body);
                obj = JsonConvert.DeserializeObject<AccountStateMentDetails>(dcdata);
                // objres.result = objcusdb.GetCropDetail(obj);
                objres.List = db.GetAccountSatateMent(obj);

                if (objres.List != null && objres.List.Count > 0)
                {
                    objres.response = "success";
                    objres.Message = "success";
                }
                else
                {
                    objres.response = "Fail";
                    objres.Message = "Data does not exists";

                }
            }
            catch (Exception ex)
            {
                objres.response = "Fail";
                objres.Message = "Something went wrong";

            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AccountStateMentResponse));
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
        public ResponseModel AssociateRepurchasePayoutStatement(RequestModel model)
        {
            ResponseModel objApiResponse = new ResponseModel();
            CustomerDb db = new CustomerDb();
            AccountStateMentDetails obj = new AccountStateMentDetails();
            AccountStateMentResponse objres = new AccountStateMentResponse();
            string EncryptedText = "";
            try
            {
                //string Payout = Crypto.Decryption(Aeskey, PayoutNo);
                //string MemId = Crypto.Decryption(Aeskey, Fk_MemId);

                // objres.List = db.GetAccountSatateMent(long.Parse(MemId), long.Parse(Payout));

                string dcdata = Crypto.Decryption(Aeskey, model.body);
                obj = JsonConvert.DeserializeObject<AccountStateMentDetails>(dcdata);
                // objres.result = objcusdb.GetCropDetail(obj);
                objres.List = db.AssociateRepurchasePayoutStatement(obj);

                if (objres.List != null && objres.List.Count > 0)
                {
                    objres.response = "success";
                    objres.Message = "success";

                }
                else
                {
                    objres.response = "Fail";
                    objres.Message = "Data does not exists";

                }
            }
            catch (Exception ex)
            {
                objres.response = "Fail";
                objres.Message = "Something went wrong";

            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AccountStateMentResponse));
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
        public ApiResponseModel PaymentAEPS(RequestModel model)
        {
            AEPSPayment ObjPaymentAEPS = new AEPSPayment();
            AEPSPaymentResponse ObjResponse = new AEPSPaymentResponse();
            ApiResponseModel objApiResponse = new ApiResponseModel();
            string EncryptedText = "";
            if (model != null)
            {
                try
                {
                    string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, model.body);
                    ObjPaymentAEPS = JsonConvert.DeserializeObject<AEPSPayment>(dcdata);
                    try
                    {

                        //DataSet ds = ObjPaymentAEPS.AEPSPaymentInsert();
                        //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        //{
                        //    if (ds.Tables[0].Rows[0]["Code"].ToString() == "1")
                        //    {
                        //        ObjResponse.message = ds.Tables[0].Rows[0]["Remark"].ToString();
                        //        ObjResponse.response = "Success";
                        //    }
                        //    else if (ds.Tables[0].Rows[0]["Code"].ToString() == "0")
                        //    {
                        //        ObjResponse.message = ds.Tables[0].Rows[0]["Remark"].ToString();
                        //        ObjResponse.response = "Error";
                        //    }
                        //    else
                        //    {
                        //        ObjResponse.message = "Could't Process";
                        //        ObjResponse.response = "Error";
                        //    }
                        //}


                        ObjResponse.message = "Sorry! Please try again later.";
                        ObjResponse.response = "Success";


                    }
                    catch (Exception ex)
                    {
                        ObjResponse.message = "Exception is" + ex;
                        ObjResponse.response = "Error";

                    }
                }
                catch (Exception ex)
                {
                    ObjResponse.message = "Exception is" + ex;
                    ObjResponse.response = "Error";
                }
            }
            else
            {
                ObjResponse.message = "Could't Process";
                ObjResponse.response = "Error";
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AEPSPaymentResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, ObjResponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;

        }

        [HttpGet]
        public ResponseModel GetLanguageList()
        {
            LanguageJsonJResponse objres = new LanguageJsonJResponse();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {

                var res = objcusdb.GetAllLanguage();
                if (res != null)
                {
                    objres.result = res;
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
            js = new DataContractJsonSerializer(typeof(LanguageJsonJResponse));
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

        #region Advance Payment 

        [HttpPost]
        public ApiResponseModel AdvancePaymentRequest(RequestModel model)
        {
            ApiResponseModel objApiResponse = new ApiResponseModel();
            AdvancePaymentModel _Obj = new AdvancePaymentModel();
            AddUpdateResponseResponseModel response = new AddUpdateResponseResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                _Obj = JsonConvert.DeserializeObject<AdvancePaymentModel>(dcdata);
                _Obj.RecoveryDate = DateTime.ParseExact(_Obj.RecoveryDateStr, "dd/MM/yyyy", null);
                AssociateAdvancePaymentService _Service = new AssociateAdvancePaymentService();
                response = _Service.SubmitAdvancePaymentRequest(_Obj);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = "error";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AddUpdateResponseResponseModel));
            ms = new MemoryStream();
            js.WriteObject(ms, response);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }

        [HttpPost]
        public ApiResponseModel GetAllAdvancePaymentRequest(RequestModel model)
        {
            ApiResponseModel objApiResponse = new ApiResponseModel();
            AdvancePaymentModel _Obj = new AdvancePaymentModel();
            AdvancePaymentResponseModel response = new AdvancePaymentResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                _Obj = JsonConvert.DeserializeObject<AdvancePaymentModel>(dcdata);
                AssociateAdvancePaymentService _Service = new AssociateAdvancePaymentService();
                List<AdvancePaymentModel> result = _Service.GetAllAdvancePaymentRequest((int)_Obj.FK_MID);
                if (result != null && result.Count > 0)
                    response = new AdvancePaymentResponseModel { Status = true, Data = result, Message = "Success" };
                else
                    response = new AdvancePaymentResponseModel { Status = false, Message = "Data not found!" };
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = "error";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AdvancePaymentResponseModel));
            ms = new MemoryStream();
            js.WriteObject(ms, response);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }

        [HttpPost]
        public ApiResponseModel CheckLoanEligibility(RequestModel model)
        {
            ApiResponseModel objApiResponse = new ApiResponseModel();
            AdvancePaymentModel _Obj = new AdvancePaymentModel();
            LoanEligibilityModel result = new LoanEligibilityModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                _Obj = JsonConvert.DeserializeObject<AdvancePaymentModel>(dcdata);
                AssociateAdvancePaymentService _Service = new AssociateAdvancePaymentService();
                result = _Service.CheckLoanEligibility((int)_Obj.FK_MID);
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "error";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(LoanEligibilityModel));
            ms = new MemoryStream();
            js.WriteObject(ms, result);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }
        #endregion

        [HttpGet]
        public ResponseModel GetCropList(string languageCode)
        {
            CropListResponse objres = new CropListResponse();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {

                var res = objcusdb.GetAllCrop(languageCode);
                if (res != null)
                {
                    objres.result = res;
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
            js = new DataContractJsonSerializer(typeof(CropListResponse));
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
        public ResponseModel CropdetailByLanguage(RequestModel model)
        {
            ResponseModel objApiResponse = new ResponseModel();
            CropDetialReq obj = new CropDetialReq();
            CropDetailbyLangunage3 objres = new CropDetailbyLangunage3();


            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                obj = JsonConvert.DeserializeObject<Models.API.CropDetialReq>(dcdata);
                objres.result = objcusdb.GetCropDetail(obj);
                if (objres.result != null)
                {
                    objres.result = objres.result;
                    objres.Response = "success";
                    objres.Message = "success";
                }
                else
                {
                    objres.result = null;
                    objres.Response = "error";
                    objres.Message = "error";
                }
            }
            catch (Exception ex)
            {
                objres.Response = "error";
                objres.Message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CropDetailbyLangunage3));
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
        public ResponseModel UpdateMemberLanguage(RequestModel model)
        {
            ResponseModel objApiResponse = new ResponseModel();
            MemberLanguage obj = new MemberLanguage();
            MemberLanguageResponse objres = new MemberLanguageResponse();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                obj = JsonConvert.DeserializeObject<Models.API.MemberLanguage>(dcdata);
                var res = objcusdb.UpdateMemberlanguage(obj.FK_MemId, obj.Language_Code);
                if (res != null)
                {
                    objres.result = res;
                    objres.Response = "success";
                    objres.Message = "success";
                }
                else
                {
                    objres.result = null;
                    objres.Response = "error";
                    objres.Message = "error";
                }
            }
            catch (Exception ex)
            {
                objres.Response = "error";
                objres.Message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(MemberLanguageResponse));
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
        public ResponseModel CreateOrderForKaryonCart(RequestModel model)
        {
            CommonJsonReponse objres = new CommonJsonReponse();
            MAssignDynamicProduct modelRequest = new MAssignDynamicProduct();
            ResponseModel objApiResponse = new ResponseModel();
            StreamReader srd;
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<Models.API.MAssignDynamicProduct>(dcdata);

                /*Call my Dynamic Product Assign Service here*/

                LifeOne.Models.API.ManageCartService _objservice = new LifeOne.Models.API.ManageCartService();
                DataTable _productDetails = _objservice.CreateCartProduct(modelRequest.Area, modelRequest.CropCode, modelRequest.language, modelRequest.Date);

                foreach (DataRow item in _productDetails.Rows)
                {
                    MAddtoCartMagento _objMShopping = new MAddtoCartMagento();
                    _objMShopping.sku = item["ProductSKU"].ToString(); //SKUID
                    _objMShopping.qty = Convert.ToInt32(item["PrdQty"]); //Qty Needed
                    _objMShopping.token = modelRequest.token;        //Token Will be the same as required 
                    _objMShopping.type = "simple";


                    List<CustomOption> _objlist = new List<CustomOption>();
                    CustomOption _objCustomOption = new CustomOption();
                    _objCustomOption.option_id = item["option_id"].ToString();
                    _objCustomOption.option_value = item["option_value"].ToString();
                    _objlist.Add(_objCustomOption);
                    _objMShopping.custom_options = _objlist;

                    string _body = JsonConvert.SerializeObject(_objMShopping);
                    /*Now call the Addtocart for magento API here*/
                    string _outputresult = AddtoCartOnTheMagentoPlatform("guest_addto.php", "POST", _body, null);
                    if (_outputresult != "0")
                    {
                        objres.response = "success";
                        objres.message = "success";
                    }
                    else
                    {
                        //objresponse.response = "Error";
                        //objresponse.message = _outputresult;
                        objres.response = "success";
                        objres.message = "success";
                    }
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
            js = new DataContractJsonSerializer(typeof(CommonJsonReponse));
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
        public string AddtoCartOnTheMagentoPlatform(string url, string Method, string body, string Token)
        {
            string responseText = "0";
            try
            {
                // string ICashCardBaseUrl = "https://quaeretech.com/karyon/api/";//ConfigurationManager.AppSettings["MShoppingAPI"].ToString();
                string ICashCardBaseUrl = "https://karyon.organic/shop/api/";
                string AeskeyM = ConfigurationManager.AppSettings["Aeskey"].ToString();
                string _modaldata = ApiEncrypt_Decrypt.EncryptString(AeskeyM, body);
                RequestModel requestModel = new RequestModel();
                requestModel.body = _modaldata;
                var client = new HttpClient();
                client.BaseAddress = new Uri(ICashCardBaseUrl);
                client.DefaultRequestHeaders.Add("contentType", "application/json");
                //HTTP POST
                var responseTask = client.PostAsJsonAsync(url, requestModel);
                responseTask.Wait();
                //updated on  11/015/2020
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;


                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    string response = result.Content.ReadAsStringAsync().Result;
                    ResponseModel respomodel = JsonConvert.DeserializeObject<ResponseModel>(response);
                    //Decryption
                    string _decryptdeserializeresponse = ApiEncrypt_Decrypt.DecryptString(AeskeyM, respomodel.body);
                    //final reponse
                    MagentoResponseModel finalresponse = JsonConvert.DeserializeObject<MagentoResponseModel>(_decryptdeserializeresponse);
                    if (finalresponse.response == "Success")
                    {
                        responseText = finalresponse.message;
                    }
                }
                return responseText;
            }
            catch (Exception e)
            {
                return responseText;
            }

        }

        //[HttpGet]
        //public HttpResponseMessage AddProductList()
        //{
        //    viewModel response = new viewModel();
        //    MemberLanguageResponse res = new MemberLanguageResponse();
        //    try
        //    {
        //        string url = "https://karyon.organic/shop/api/products.php";

        //        using (var client = new WebClient())
        //        {
        //            string s = client.DownloadString(url);
        //            response = JsonConvert.DeserializeObject<viewModel>(s);
        //            var t = response.product_list.FirstOrDefault(x => x.id == 2073);
        //        }

        //        string xml = "<result>";
        //        foreach (var item in response.product_list)
        //        {
        //            if (item.option.Count > 0 && item.option.FirstOrDefault().values.Count > 0)
        //            {
        //                foreach (var value in item.option.FirstOrDefault().values)
        //                {
        //                    xml += "<data>" +
        //                    "<MagentoProdId>" + item.id + "</MagentoProdId>" +
        //                    "<ProductSKU>" + item.sku + "</ProductSKU>" +
        //                    "<ProductName>" + item.name + "</ProductName>" +
        //                    "<ProductImage>" + item.gallery + "</ProductImage>" +
        //                    "<MRP>" + item.price + "</MRP>" +
        //                    "<SalePrice>" + item.price + "</SalePrice>" +
        //                    "<BV>" + item.bv + "</BV>" +
        //                    "<PV>" + 0 + "</PV>" +
        //                    "<DP>" + item.dp + "</DP>" +
        //                    "<Unit>" + (item.option.Count > 0 ? item.option.FirstOrDefault().title : null) + "</Unit>" +
        //                    "<OptionId>" + Convert.ToInt32(item.option[0].option_id) + "</OptionId>" +
        //                    "<OptionTypeId>" + Convert.ToInt32(value.option_type_id) + "</OptionTypeId>" +
        //                    "<Price>" + value.price + "</Price>" +
        //                    "<size>" + value.title + "</size>";
        //                    xml += "</data>";
        //                }
        //            }
        //        }
        //        xml += "</result>";
        //        res = objcusdb.AddProductList(xml);
        //    }
        //    catch (Exception ex)
        //    {
        //        res.flag = 0;
        //        res.Message = ex.Message;
        //    }
        //    return Request.CreateResponse(HttpStatusCode.OK, res);
        //}


        [HttpGet]
        public HttpResponseMessage AddProductList()
        {
            viewModel response = new viewModel();
            MemberLanguageResponse res = new MemberLanguageResponse();
            try
            {
                string url = "https://karyon.organic/shop/api/products.php";
                using (var client = new WebClient())
                {
                    string s = client.DownloadString(url);
                    response = JsonConvert.DeserializeObject<viewModel>(s);
                    var t = response.product_list.FirstOrDefault(x => x.id == 2073);
                }
                int count = 0;
                string xml = "<result>";
                foreach (var item in response.product_list)
                {
                    if (item.option.Count > 0 && item.option.FirstOrDefault().values.Count > 0)
                    {
                        count = 0;
                        foreach (var value in item.option.FirstOrDefault().values)
                        {
                            count = count + 1;
                            decimal size = 0.00m;
                            var vale = value.title.Split(' ');
                            if (!String.IsNullOrEmpty(vale[0]))
                                size = Convert.ToDecimal(vale[0]);
                            else
                                size = Convert.ToDecimal(vale[1]);

                            xml += "<data>" +
                            "<MagentoProdId>" + item.id + "</MagentoProdId>" +
                            "<ProductCode>" + item.product_code + "</ProductCode>" +
                            "<ProductSKU>" + item.sku + "</ProductSKU>" +
                            "<ProductName>" + item.name + "</ProductName>" +
                            "<ProductImage>" + item.gallery + "</ProductImage>" +
                            "<MRP>" + item.price + "</MRP>" +
                            "<SalePrice>" + item.price + "</SalePrice>" +
                            "<PV>" + 0 + "</PV>" +
                            "<Unit>" + (item.option.Count > 0 ? item.option.FirstOrDefault().title : null) + "</Unit>" +
                            "<OptionId>" + Convert.ToInt32(item.option[0].option_id) + "</OptionId>" +
                            "<OptionTypeId>" + Convert.ToInt32(value.option_type_id) + "</OptionTypeId>" +
                            "<Price>" + value.price + "</Price>" +
                            "<size>" + size + "</size>" +
                            "<CGST>" + item.gst / 2 + "</CGST>" +
                            "<SGST>" + item.gst / 2 + "</SGST>" +
                            "<IGST>" + item.gst + "</IGST>" +
                            "<Fk_OrbitType>" + item.Fk_OrbitType + "</Fk_OrbitType>";

                            if (count == 1)
                            {
                                xml += "<DP>" + item.dp + "</DP>" +
                                    "<BV>" + item.bv + "</BV>" +
                                    "<Bottles>" + item.bottles + "</Bottles>";
                            }
                            if (count == 2)
                            {
                                xml += "<DP>" + item.dp1 + "</DP>" +
                                    "<BV>" + item.bv1 + "</BV>" +
                                    "<Bottles>" + item.bottles1 + "</Bottles>";
                            }
                            if (count == 3)
                            {
                                xml += "<DP>" + item.dp2 + "</DP>" +
                                    "<BV>" + item.bv2 + "</BV>" +
                                    "<Bottles>" + item.bottles2 + "</Bottles>";
                            }
                            if (count == 4)
                            {
                                xml += "<DP>" + item.dp3 + "</DP>" +
                                    "<BV>" + item.bv4 + "</BV>" +
                                    "<Bottles>" + item.bottles4 + "</Bottles>";
                            }
                            xml += "</data>";
                        }
                    }
                }
                xml += "</result>";
                var a = response.product_list.FirstOrDefault(x => x.id == 2075);
                res = objcusdb.AddProductList(xml);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        [HttpPost]
        public HttpResponseMessage GetProductList(ProductViewModel model)
        {
            List<ProductViewModel> data = new List<ProductViewModel>();
            try
            {
                var res = objcusdb.GetProductList(model);
                if (res != null && res.Count > 0)
                {
                    data = res.GroupBy(x => x.ProductId).Select(x => new ProductViewModel
                    {
                        ProductId = x.FirstOrDefault().ProductId,
                        MagentoProdId = x.FirstOrDefault().MagentoProdId,
                        ProductName = x.FirstOrDefault().ProductName,
                        ProductImage = x.FirstOrDefault().ProductImage,
                        MRP = x.FirstOrDefault().MRP,
                        Units = x.FirstOrDefault().Units,
                        PV = x.FirstOrDefault().PV,
                        BV = x.FirstOrDefault().BV,
                        DP = x.FirstOrDefault().DP,
                        ProductUnitSizeList = x.Select(y => new ProductUnitSizeViewModel { KeyId = y.KeyId, Size = y.Size }).ToList()
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                data.FirstOrDefault().flag = 0;
                data.FirstOrDefault().Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        //[HttpPost]
        //public HttpResponseMessage GetProductList(ProductViewModel model)
        //{
        //    List<ProductViewModel> data = new List<ProductViewModel>();
        //    try
        //    {
        //        var res = objcusdb.GetProductList(model);
        //        if (res != null && res.Count > 0)
        //        {
        //            data = res.GroupBy(x => x.ProductId).Select(x => new ProductViewModel
        //            {
        //                ProductId = x.FirstOrDefault().ProductId,
        //                MagentoProdId = x.FirstOrDefault().MagentoProdId,
        //                ProductName = x.FirstOrDefault().ProductName,
        //                ProductImage = x.FirstOrDefault().ProductImage,
        //                MRP = x.FirstOrDefault().MRP,
        //                Units = x.FirstOrDefault().Units,
        //                PV = x.FirstOrDefault().PV,
        //                BV = x.FirstOrDefault().BV,
        //                DP = x.FirstOrDefault().DP,
        //                ProductUnitSizeList = x.Select(y => new ProductUnitSizeViewModel { KeyId = y.KeyId, Size = y.Size }).ToList()
        //            }).ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        data.FirstOrDefault().flag = 0;
        //        data.FirstOrDefault().Message = ex.Message;
        //    }
        //    return Request.CreateResponse(HttpStatusCode.OK, data);
        //}

        [HttpPost]
        public ResponseModel AesPayment(RequestModel model)
        {
            ResponseModel objApiResponse = new ResponseModel();
            AesPayment obj = new AesPayment();
            AesPaymentResponse objres = new AesPaymentResponse();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                obj = JsonConvert.DeserializeObject<Models.API.AesPayment>(dcdata);
                var res = objcusdb.AesPayment(obj);
                if (res != null)
                {
                    if (res.Flag == 1)
                    {
                        objres.Response = "success";
                        objres.Message = "success";
                    }
                    else
                    {
                        objres.Response = "error";
                        objres.Message = "error";
                    }
                }
                else
                {

                    objres.Response = "error";
                    objres.Message = "error";
                }
            }
            catch (Exception ex)
            {
                objres.Response = "error";
                objres.Message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AesPaymentResponse));
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
        public ResponseModel AesPayMentHistory(string MemberId)
        {
            AesPaymentHistoryResponse objres = new AesPaymentHistoryResponse();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                MemberId = MemberId.Replace(" ", "+");
                string MemId = Crypto.Decryption(Aeskey, MemberId);

                var res = objcusdb.GetAesHistory(Convert.ToInt64(MemId));
                if (res != null)
                {
                    objres.result = res;
                    objres.Response = "success";
                    objres.Message = "success";

                }
                else
                {
                    objres.Response = "error";
                    objres.Message = "error";
                }
            }
            catch (Exception ex)
            {
                objres.Response = "error";
                objres.Message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AesPaymentHistoryResponse));
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
        public ResponseModel ProductPurchaseHistory_Karyon(string MemberId, string LanguageId)
        {
            ProductPurchaseHistoryResponse objres = new ProductPurchaseHistoryResponse();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                MemberId = MemberId.Replace(" ", "+");
                LanguageId = LanguageId.Replace(" ", "+");
                string MemId = Crypto.Decryption(Aeskey, MemberId);
                string _LanguageIdCode = Crypto.Decryption(Aeskey, LanguageId);

                var res = objcusdb.GetProductPurchaseHistory_Karyon(Convert.ToInt64(MemId), _LanguageIdCode);
                if (res != null)
                {
                    objres.result = res;
                    objres.Response = "success";
                    objres.Message = "success";

                }
                else
                {
                    objres.Response = "error";
                    objres.Message = "error";
                }
            }
            catch (Exception ex)
            {
                objres.Response = "error";
                objres.Message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ProductPurchaseHistoryResponse));
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
        public ResponseModel ProductPurchaseHistoryDetail_Karyon(RequestModel model)
        {
            ResponseModel objApiResponse = new ResponseModel();
            ProductDetialReq obj = new ProductDetialReq();
            ProductDetailbyLangunage3 objres = new ProductDetailbyLangunage3();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                obj = JsonConvert.DeserializeObject<Models.API.ProductDetialReq>(dcdata);
                objres.result = objcusdb.GetProducHistorytDetail(obj);
                if (objres.result != null)
                {
                    objres.result = objres.result;
                    objres.Response = "success";
                    objres.Message = "success";
                }
                else
                {
                    objres.result = null;
                    objres.Response = "error";
                    objres.Message = "error";
                }
            }
            catch (Exception ex)
            {
                objres.Response = "error";
                objres.Message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ProductDetailbyLangunage3));
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
        public HttpResponseMessage AddCategoryList()
        {
            CategoryListViewModel response = new CategoryListViewModel();
            MemberLanguageResponse res = new MemberLanguageResponse();
            try
            {
                string url = "https://karyon.organic/shop/api/crop_category.php";

                using (var client = new WebClient())
                {
                    string s = client.DownloadString(url);
                    var tdata = JsonConvert.DeserializeObject<RequestModel>(s);
                    string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, tdata.body);
                    response = JsonConvert.DeserializeObject<CategoryListViewModel>(dcdata);
                }

                string xml = "<result>";

                foreach (var item in response.data)
                {
                    xml += "<data>" +
                        "<CropId>" + item.id + "</CropId>" +
                        "<CropName>" + item.name + "</CropName>";
                    xml += "</data>";
                }

                xml += "</result>";
                res = objcusdb.AddCategoryList(xml);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        [HttpGet]
        public HttpResponseMessage AddCropProducts()
        {
            CropProductListViewModel response = new CropProductListViewModel();
            MemberLanguageResponse res = new MemberLanguageResponse();
            try
            {
                string url = "https://karyon.organic/shop/api/cropnamebyid.php?id=1";
                using (var client = new WebClient())
                {
                    string s = client.DownloadString(url);
                    var tdata = JsonConvert.DeserializeObject<RequestModel>(s);
                    string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, tdata.body);
                    response = JsonConvert.DeserializeObject<CropProductListViewModel>(dcdata);
                }
                string xml = "<result>";
                foreach (var item in response.data)
                {
                    xml += "<data>" +
                    "<CategoryId>" + item.crop_id + "</CategoryId>" +
                    "<Crop_ProductId>" + item.id + "</Crop_ProductId>" +
                    "<Crop_ProductName>" + item.crop_name + "</Crop_ProductName>";
                    xml += "</data>";

                }
                xml += "</result>";
                res = objcusdb.AddCropProducts(xml);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        [HttpGet]
        public IHttpActionResult BindCropCategoryList()
        {
            try
            {
                AgricultureMasterService _Service = new AgricultureMasterService();
                CropCategoryResponse _DataList = new CropCategoryResponse();
                ResponseModel objApiResponse = new ResponseModel();
                string EncryptedText = "";
                _DataList.Data = _Service.BindCropCategoryList();
                if (_DataList.Data != null && _DataList.Data.Count() > 0)
                {
                    _DataList.response = "success";
                }
                else
                {
                    _DataList.response = "error";
                }
                string CustData = "";
                DataContractJsonSerializer js;
                MemoryStream ms;
                js = new DataContractJsonSerializer(typeof(CropCategoryResponse));
                ms = new MemoryStream();
                js.WriteObject(ms, _DataList);
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                CustData = sr.ReadToEnd();
                sr.Close();
                ms.Close();
                EncryptedText = Crypto.Encryption(Aeskey, CustData);
                objApiResponse.body = EncryptedText;
                return Ok(objApiResponse);
            }

            catch (Exception ex)
            {
                return Ok("null");
            }
        }
        [HttpPost]
        public IHttpActionResult BindCropProductByCategoyId(RequestModel model)
        {
            AgricultureMasterService _Service = new AgricultureMasterService();
            CropProductResponse _DataList = new CropProductResponse();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                CropProductModel _Model = JsonConvert.DeserializeObject<CropProductModel>(dcdata);
                _DataList.Data = _Service.BindCropProductByCategoyId(_Model.CategoryID);
                if (_DataList.Data != null && _DataList.Data.Count() > 0)
                    _DataList.response = "success";
                else
                    _DataList.response = "error";

            }
            catch (Exception ex)
            {
                _DataList.response = "error";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CropProductResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, _DataList);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return Ok(objApiResponse);
        }

        [HttpPost]
        public ResponseModel AddFomilynTour(RequestModel model)
        {
            CommonJsonReponse objres = new CommonJsonReponse();
            FamilyNTourViewModel modelRequest = new FamilyNTourViewModel();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<Models.API.FamilyNTourViewModel>(dcdata);
                var res = objcusdb.AddFomilynTour(modelRequest);
                if (res != null && res.Flag == 1)
                {

                    objres.response = "success";
                    objres.message = res.Msg;
                    objres.OrderNo = res.TourID;
                }
                else
                {
                    objres.response = "error";
                    objres.message = res.Msg;
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
            js = new DataContractJsonSerializer(typeof(CommonJsonReponse));
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
        public IHttpActionResult BindManageAppDashboardService()
        {
            try
            {
                LifeOne.Models.AdminManagement.AEntity.DynamicUtilityResponseModel _DataList = new LifeOne.Models.AdminManagement.AEntity.DynamicUtilityResponseModel();
                ResponseModel objApiResponse = new ResponseModel();
                string EncryptedText = "";
                _DataList = UtilityService.BindAllUtilityMaster();
                if (_DataList.Data != null && _DataList.Data.Count() > 0)
                {
                    _DataList.response = "success";
                }
                else
                {
                    _DataList.response = "error";
                }
                string CustData = "";
                DataContractJsonSerializer js;
                MemoryStream ms;
                js = new DataContractJsonSerializer(typeof(LifeOne.Models.AdminManagement.AEntity.DynamicUtilityResponseModel));
                ms = new MemoryStream();
                js.WriteObject(ms, _DataList);
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                CustData = sr.ReadToEnd();
                sr.Close();
                ms.Close();
                EncryptedText = Crypto.Encryption(Aeskey, CustData);
                objApiResponse.body = EncryptedText;
                return Ok(objApiResponse);
            }

            catch (Exception ex)
            {
                return Ok("null");
            }
        }

        /*admin can view his/her wallet balance*/
        [HttpPost]
        public List<BalanceResponseCyberplate> CheckBalance(CyberPlatPara para)
        {
            List<BalanceResponseCyberplate> list = new List<BalanceResponseCyberplate>();
            try
            {
                MCyberplatebalance _request2 = new MCyberplatebalance();

                var _check = _request2.ChkCompanyWalletBalanceService(); //_request2.Check(para);
                if (_check != null)
                {
                    list = _check;
                }

            }
            catch (Exception)
            {
                /// throw;
            }
            return list;
        }
        [HttpPost]
        public ResponseModel VeriFyPanOrVoterId(RequestModel model)
        {
            VerifyPanOrVoterId VerifyPanOrVoterId = new VerifyPanOrVoterId();
            VerifyPanOrVoterIdresponse objresponse = new VerifyPanOrVoterIdresponse();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                if (model != null)
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    VerifyPanOrVoterId = JsonConvert.DeserializeObject<VerifyPanOrVoterId>(dcdata);
                    var res = objcusdb.VeriFyPanOrVoterId(VerifyPanOrVoterId);

                    if (res != null)
                    {
                        if (res.Flag == 1)
                        {
                            objresponse.Flag = res.Flag;
                            objresponse.response = "success";
                            objresponse.Msg = res.Msg;
                        }
                        else
                        {
                            objresponse.Flag = res.Flag;
                            objresponse.response = "error";
                            objresponse.Msg = res.Msg;
                        }
                    }
                    else
                    {
                        objresponse.Flag = res.Flag;
                        objresponse.Msg = res.Msg;
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
            js = new DataContractJsonSerializer(typeof(VerifyPanOrVoterIdresponse));
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


        [HttpPost]
        public ResponseModel GenerateOrderIDForPayemnt(RequestModel model)
        {
            ResponseModel objApiResponse = new ResponseModel();
            AllPaymentDetailsRequest obj = new AllPaymentDetailsRequest();
            AllPaymentDetailsResponse objrps = new AllPaymentDetailsResponse();
            Razorpay.Api.Order objorder = null;
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            StreamReader sr;

            string EncryptedText = "";
            try
            {
                if (model != null)
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    obj = JsonConvert.DeserializeObject<AllPaymentDetailsRequest>(dcdata);

                    string OrderId = "", Status = "";
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    RazorpayClient client = null;
                    //client = new RazorpayClient("rzp_test_AFSSERe0Blac2S", "X6UBRaEzdUHdJFfXPQDmdLKb");
                    if (obj.Type == "LifeOne" || obj.Type == "Tour")
                    {
                        client = new RazorpayClient(RazorPayLiveKey, RazorPayLiveSecret);
                    }
                    else
                    {
                        client = new RazorpayClient(RazorPayLiveKey, RazorPayLiveSecret);
                    }

                    //key and vlaue should be changed                    
                    if (obj.Action == 1)
                    {
                        Dictionary<string, object> options = new Dictionary<string, object>();
                        options.Add("amount", obj.Amount * 100);
                        options.Add("receipt", "");
                        options.Add("currency", "INR");
                        options.Add("payment_capture", 1);
                        objorder = client.Order.Create(options);
                        OrderId = objorder["id"].ToString();
                        objrps.OrderId = OrderId;
                        obj.OrderId = OrderId;
                        objrps.Response = "Success";
                    }
                    else
                    {
                        objrps.OrderId = "";
                        objrps.Response = "Error";
                    }
                }
                else
                {
                    objrps.Response = "error";
                }
            }
            catch (Exception ex)
            {
                objrps.Response = "error" + ex;
            }
            js = new DataContractJsonSerializer(typeof(AllPaymentDetailsResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objrps);
            ms.Position = 0;
            sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }

        [HttpPost]
        public ResponseModel GenerateOrderIDForPayemnt_Test(RequestModel model)
        {
            ResponseModel objApiResponse = new ResponseModel();
            AllPaymentDetailsRequest obj = new AllPaymentDetailsRequest();
            AllPaymentDetailsResponse objrps = new AllPaymentDetailsResponse();
            Razorpay.Api.Order objorder = null;
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            StreamReader sr;

            string EncryptedText = "";
            try
            {
                if (model != null)
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    obj = JsonConvert.DeserializeObject<AllPaymentDetailsRequest>(dcdata);

                    string OrderId = "", Status = "";
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    RazorpayClient client = null;
                    //client = new RazorpayClient("rzp_test_AFSSERe0Blac2S", "X6UBRaEzdUHdJFfXPQDmdLKb");

                    if (obj.Type == "LifeOne" || obj.Type == "Tour")
                    {
                        client = new RazorpayClient("rzp_test_eMtOR5Pow97l9V", "Z09GZMKcCkG43gdD00OTm6kO"); //test API Here LifeOne
                    }
                    else
                    {
                        client = new RazorpayClient("rzp_test_9RHoDbOUpp1WNa", "qEXjFZhLmZ23yA8kQHxEUGPB"); //test API Here  Karyon
                    }

                    //key and vlaue should be changed                    
                    if (obj.Action == 1)
                    {
                        Dictionary<string, object> options = new Dictionary<string, object>();
                        options.Add("amount", obj.Amount * 100);
                        options.Add("receipt", "");
                        options.Add("currency", "INR");
                        options.Add("payment_capture", 1);
                        objorder = client.Order.Create(options);
                        OrderId = objorder["id"].ToString();
                        objrps.OrderId = OrderId;
                        obj.OrderId = OrderId;
                        objrps.Response = "Success";
                    }
                    else
                    {
                        objrps.OrderId = "";
                        objrps.Response = "Error";
                    }
                }
                else
                {
                    objrps.Response = "error";
                }
            }
            catch (Exception ex)
            {
                objrps.Response = "error" + ex;
            }
            js = new DataContractJsonSerializer(typeof(AllPaymentDetailsResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objrps);
            ms.Position = 0;
            sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }

        [HttpPost]
        public ResponseModel FranchisePaymentRequest(RequestModel model)
        {
            CommonJsonReponse objresponse = new CommonJsonReponse();
            ResponseModel returnResponse = new ResponseModel();
            Models.API.WalletJsonRequest modelRequest = new Models.API.WalletJsonRequest();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<Models.API.WalletJsonRequest>(dcdata);

                var res = objcusdb.FranchisePaymentRequest(modelRequest);
                if (res != null)
                {
                    if (res.Flag == 1)
                    {
                        objresponse.response = "success";
                        objresponse.message = res.Msg;
                        objresponse.TransId = res.TransId;

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
            js = new DataContractJsonSerializer(typeof(CommonJsonReponse));
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

        [HttpGet]
        public HttpResponseMessage GetRazorPayRefund(string Amount, string paymentId)
        {

            Models.AdminManagement.AEntity.RazorPayRefundResponse model = new Models.AdminManagement.AEntity.RazorPayRefundResponse();
            try
            {

                ServicePointManager.Expect100Continue = false;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                int amt = (int)(Convert.ToDecimal(Amount) * 100.00M);

                string key = ConfigurationManager.AppSettings["RazorKey"].ToString();
                string secret = ConfigurationManager.AppSettings["RazorSecret"].ToString();

                RazorpayClient client = new RazorpayClient(key, secret);

                Payment payment = client.Payment.Fetch(paymentId);

                Dictionary<string, object> input = new Dictionary<string, object>();
                input.Add("amount", amt);
                Refund refund = payment.Refund(input);
                model.entity = Convert.ToString(refund.Attributes["entity"]);
                model.id = Convert.ToString(refund.Attributes["id"]);
                model.payment_id = Convert.ToString(refund.Attributes["payment_id"]);
                model.amount = Convert.ToString(refund.Attributes["amount"]);
                model.code = "1";

            }
            catch (Exception ex)
            {
                model.code = "0";
                model.Msg = ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        #region Recharge Api (Maqsood)

        //[HttpGet]
        //public HttpResponseMessage GetBalance()
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK, new RechargerApiBusiness().GetBalanceAsync());
        //}

        [HttpGet]
        public ResponseModel GetBalance()
        {
            WalletBalanceResponseModel objresponse = new WalletBalanceResponseModel();
            ResponseModel _response = new ResponseModel();
            string EncryptedText = "";
            try
            {
                WalletBalanceModel _data = new RechargerApiBusiness().GetBalanceAsync();
                if (_data != null && _data.ResponseCode == 0)
                {
                    objresponse.Flag = 1;
                    objresponse.Status = true;
                    objresponse.response = "success";
                    objresponse.message = "Balance fetched successfully";
                    objresponse.WalletBalance = _data;
                }
                else
                {
                    objresponse.Flag = 0;
                    objresponse.Status = false;
                    objresponse.response = "failed";
                    objresponse.message = "Sorry we are unable to get your balance.";
                }
            }
            catch (Exception ex)
            {
                objresponse.Flag = 0;
                objresponse.Status = false;
                objresponse.response = "failed";
                objresponse.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(WalletBalanceResponseModel));
            ms = new MemoryStream();
            js.WriteObject(ms, objresponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            _response.body = EncryptedText;
            return _response;
        }
        [HttpPost]

        [HttpGet]
        public ResponseModel BindRechargeType()
        {
            RechargeTypeResponseModel objresponse = new RechargeTypeResponseModel();
            ResponseModel _response = new ResponseModel();
            string EncryptedText = "";
            try
            {
                objresponse = new RechargerApiBusiness().BindRechargeType();
            }
            catch (Exception ex)
            {
                objresponse.response = "error";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(RechargeTypeResponseModel));
            ms = new MemoryStream();
            js.WriteObject(ms, objresponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            _response.body = EncryptedText;
            return _response;
        }
        [HttpPost]
        public ResponseModel BindOperatorList(RequestModel model)
        {
            RechargeTypeModel req = new RechargeTypeModel();
            RechargeOperatorResponseModel objresponse = new RechargeOperatorResponseModel();
            ResponseModel _response = new ResponseModel();
            string EncryptedText = "";
            try
            {
                if (model != null)
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    req = JsonConvert.DeserializeObject<RechargeTypeModel>(dcdata);
                    var res = new RechargerApiBusiness().BindOperatorList(req.TypeId);
                    if (res != null)
                    {
                        if (res.Status)
                        {
                            objresponse.Flag = 1;
                            objresponse.response = "success";
                            objresponse.message = res.message;
                            objresponse.Data = res.Data;
                        }
                        else
                        {
                            objresponse.Flag = 0;
                            objresponse.response = "failed";
                            objresponse.message = res.message;
                        }
                    }
                    else
                    {
                        objresponse.Flag = 0;
                        objresponse.response = "failed";
                        objresponse.message = res.message;
                    }

                }
                else
                {
                    objresponse.Flag = 0;
                    objresponse.response = "failed";
                    objresponse.message = "error";
                }
            }
            catch (Exception ex)
            {
                objresponse.response = "error";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(RechargeOperatorResponseModel));
            ms = new MemoryStream();
            js.WriteObject(ms, objresponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            _response.body = EncryptedText;
            return _response;
        }

    
        #endregion

        #region Testimonials  Maqsood
        [HttpPost]
        public ResponseModel AddTestimonials(RequestModel model)
        {
            TestimonialsModel req = new TestimonialsModel();
            ApiResponeModel objresponse = new ApiResponeModel();
            ResponseModel _response = new ResponseModel();
            string EncryptedText = "";
            try
            {
                if (model != null)
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    req = JsonConvert.DeserializeObject<TestimonialsModel>(dcdata);
                    var res = req.AddTestimonials();
                    if (res != null)
                    {
                        if (res.Status)
                        {
                            objresponse.Status = true;
                            objresponse.Flag = 1;
                            objresponse.response = "success";
                            objresponse.message = res.Message;
                        }
                        else
                        {
                            objresponse.Status = false;
                            objresponse.Flag = 0;
                            objresponse.response = "failed";
                            objresponse.message = res.Message;
                        }
                    }
                    else
                    {
                        objresponse.Status = false;
                        objresponse.Flag = 0;
                        objresponse.response = "failed";
                        objresponse.message = res.Message;
                    }

                }
                else
                {
                    objresponse.Status = false;
                    objresponse.Flag = 0;
                    objresponse.response = "failed";
                    objresponse.message = "error";
                }
            }
            catch (Exception ex)
            {
                objresponse.Status = false;
                objresponse.Flag = 0;
                objresponse.response = "failed";
                objresponse.message = "System encountered error please try again later!!!";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ApiResponeModel));
            ms = new MemoryStream();
            js.WriteObject(ms, objresponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            _response.body = EncryptedText;
            return _response;
        }

        [HttpGet]
        public ResponseModel BindTestimonials()
        {
            TestimonialsResponseModel objresponse = new TestimonialsResponseModel();
            ResponseModel _response = new ResponseModel();
            string EncryptedText = "";
            try
            {
                var res = new TestimonialsModel().BindTestimonials();
                if (res != null)
                {
                    if (res.Status)
                    {
                        objresponse.Status = true;
                        objresponse.Flag = 1;
                        objresponse.response = "success";
                        objresponse.Message = res.Message;
                        objresponse.Data = res.Data;
                    }
                    else
                    {
                        objresponse.Status = false;
                        objresponse.Flag = 0;
                        objresponse.response = "failed";
                        objresponse.Message = res.Message;
                    }
                }
                else
                {
                    objresponse.Status = false;
                    objresponse.Flag = 0;
                    objresponse.response = "failed";
                    objresponse.Message = res.Message;
                }
            }
            catch (Exception ex)
            {
                objresponse.Status = false;
                objresponse.Flag = 0;
                objresponse.response = "failed";
                objresponse.Message = "System encountered error please try again later!!!";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(TestimonialsResponseModel));
            ms = new MemoryStream();
            js.WriteObject(ms, objresponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            _response.body = EncryptedText;
            return _response;
        }

        [HttpPost]
        public ResponseModel BindTestimonialsWithCategoryId(RequestModel model)
        {
            TestimonialsResponseModel objresponse = new TestimonialsResponseModel();
            ResponseModel _response = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                TestimonialsModel _objModel = JsonConvert.DeserializeObject<TestimonialsModel>(dcdata);
                var res = new TestimonialsModel().BindTestimonialsWithCat(_objModel.CropCategoryId);

                if (res != null)
                {
                    if (res.Status)
                    {
                        objresponse.Status = true;
                        objresponse.Flag = 1;
                        objresponse.response = "success";
                        objresponse.Message = res.Message;
                        objresponse.Data = res.Data;
                    }
                    else
                    {
                        objresponse.Status = false;
                        objresponse.Flag = 0;
                        objresponse.response = "failed";
                        objresponse.Message = res.Message;
                    }
                }
                else
                {
                    objresponse.Status = false;
                    objresponse.Flag = 0;
                    objresponse.response = "failed";
                    objresponse.Message = res.Message;
                }
            }
            catch (Exception ex)
            {
                objresponse.Status = false;
                objresponse.Flag = 0;
                objresponse.response = "failed";
                objresponse.Message = "System encountered error please try again later!!!";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(TestimonialsResponseModel));
            ms = new MemoryStream();
            js.WriteObject(ms, objresponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            _response.body = EncryptedText;
            return _response;
        }
        #endregion

        [HttpPost]
        public ResponseModel FranchiseRequestList(RequestModel model)
        {
            FranchiseListResponse objresponse = new FranchiseListResponse();
            ResponseModel returnResponse = new ResponseModel();
            FranchiseList modelRequest = new FranchiseList();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<FranchiseList>(dcdata);
                modelRequest.Type = "Android";
                objresponse.FranchiseListlst = AssociateTeamService.getFranchiseDetail(modelRequest);
                if (objresponse.FranchiseListlst.Count > 0)
                {

                    objresponse.response = "success";
                    objresponse.message = "success";
                    objresponse.FranchiseListlst = objresponse.FranchiseListlst;
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
            js = new DataContractJsonSerializer(typeof(FranchiseListResponse));
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

        [HttpPost]
        public ResponseModel AppNotification(RequestModel model)
        {
            AppNotification _response = new AppNotification();
            ResponseModel returnResponse = new ResponseModel();
            AppNotification modelRequest = new AppNotification();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<AppNotification>(dcdata);
                _response = ApiCommonService.AppNotification(modelRequest);
                _response.response = "success";
            }
            catch (Exception ex)
            {
                _response.response = "error";
                _response.Message = ex.Message;
                _response.Flag = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AppNotification));
            ms = new MemoryStream();
            js.WriteObject(ms, _response);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }

        [HttpPost]
        public ResponseModel UpdateNotification(RequestModel model)
        {
            AppNotification _response = new AppNotification();
            ResponseModel returnResponse = new ResponseModel();
            AppNotification modelRequest = new AppNotification();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<AppNotification>(dcdata);
                _response = ApiCommonService.UpdateNotification(modelRequest);
                _response.response = "success";
            }
            catch (Exception ex)
            {
                _response.response = "error";
                _response.Message = ex.Message;
                _response.Flag = 0;

            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AppNotification));
            ms = new MemoryStream();
            js.WriteObject(ms, _response);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }

        [HttpPost]
        public ResponseModel FranchiseRequestList_V2(RequestModel model)
        {
            FranchiseListResponse objresponse = new FranchiseListResponse();
            ResponseModel returnResponse = new ResponseModel();
            FranchiseList modelRequest = new FranchiseList();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<FranchiseList>(dcdata);
                modelRequest.Type = "Android";
                objresponse.FranchiseListlst = AssociateTeamService.getFranchiseDetailV2(modelRequest);
                if (objresponse.FranchiseListlst.Count > 0)
                {

                    objresponse.response = "success";
                    objresponse.message = "success";
                    objresponse.FranchiseListlst = objresponse.FranchiseListlst;
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
            js = new DataContractJsonSerializer(typeof(FranchiseListResponse));
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

        #region TourRequest by ovais
        public ResponseModel AddTourRequest(RequestModel model)
        {
            ResponseModel returnResponse = new ResponseModel();
            Models.AdminManagement.AEntity.TourDetailViewModel objresponse = new Models.AdminManagement.AEntity.TourDetailViewModel();
            Models.AdminManagement.AEntity.AddTourDetailViewModel modelRequest = new Models.AdminManagement.AEntity.AddTourDetailViewModel();

            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<Models.AdminManagement.AEntity.AddTourDetailViewModel>(dcdata);
                objresponse = DALTourDetail.AddTourRequest(modelRequest);

                if (objresponse.Flag > 0)
                {
                    objresponse.Remark = "success";
                    objresponse.Response = "success";
                }
            }
            catch (Exception ex)
            {
                objresponse.Remark = ex.Message;
                objresponse.Response = "failure";
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(Models.AdminManagement.AEntity.TourDetailViewModel));
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
        [HttpGet]
        public ResponseModel DestinationList()
        {
            ResponseModel returnResponse = new ResponseModel();
            TourDestinationResponse Obj = new TourDestinationResponse();
            List<TourDestination> objresponse = new List<TourDestination>();

            string EncryptedText = "";
            try
            {

                objresponse = DALTourDetail.TourDestinationList();
                if (objresponse.Count > 0)
                {
                    Obj.TourDestinationList = objresponse;
                    Obj.Remark = "success";
                    Obj.Flag = "1";
                    Obj.Response = "success";
                }

            }
            catch (Exception ex)
            {
                Obj.Remark = ex.Message;
                Obj.Response = "failure";
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(TourDestinationResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, Obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }

        [HttpPost]
        public ResponseModel APITourDetails(RequestModel model)
        {
            ResponseModel returnResponse = new ResponseModel();
            LifeOne.Models.AdminManagement.AEntity.TourResponse obj = new LifeOne.Models.AdminManagement.AEntity.TourResponse();
            LifeOne.Models.AdminManagement.AEntity.TourDetailViewModel model1 = new LifeOne.Models.AdminManagement.AEntity.TourDetailViewModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                var modelRequest = JsonConvert.DeserializeObject<Models.AdminManagement.AEntity.TourDetailViewModel>(dcdata);

                model1 = DALTourDetail.APITourDetail(modelRequest);
                var data = model1.TourDetailList.GroupBy(x => x.Tour_PKid).Select(x => new LifeOne.Models.AdminManagement.AEntity.APITour_PassengerDetailViewModel
                {
                    Tour_PKid = x.FirstOrDefault().Tour_PKid,
                    TotalPassenger = x.FirstOrDefault().TotalPassenger,
                    TotalDays = x.FirstOrDefault().TotalDays,
                    TotalNights = x.FirstOrDefault().TotalNights,
                    TourID = x.FirstOrDefault().TourID,
                    LoginId = x.FirstOrDefault().LoginId,
                    Name = x.FirstOrDefault().Name,
                    Mobile = x.FirstOrDefault().Mobile,
                    TourType = x.FirstOrDefault().TourType,
                    Source = x.FirstOrDefault().Source,
                    Destination = x.FirstOrDefault().Destination,
                    Your_Budget = x.FirstOrDefault().Your_Budget,
                    From_Dt = x.FirstOrDefault().From_Dt,
                    To_Dt = x.FirstOrDefault().To_Dt,
                    TourStatus = x.FirstOrDefault().TourStatus,
                    HotelCategory = x.FirstOrDefault().HotelCategory,
                    CompleteUpComing = x.FirstOrDefault().CompleteUpComing,
                    ReturnAirTour = x.FirstOrDefault().ReturnAirTour,
                    PaymentStatus = x.FirstOrDefault().PaymentStatus,
                    BookingAmount = x.FirstOrDefault().BookingAmount,
                    PassengerList = x.ToList().Select(y => new LifeOne.Models.AdminManagement.AEntity.PassengerDetailModel
                    {
                        Name = y.PassengerName,
                        Gender = y.Gender,
                        Age = y.Age,
                        PassengerType = y.PassengerType
                    }).ToList()
                }).ToList();
                obj.TourList = data;
                obj.Response = "success";

            }
            catch (Exception ex)
            {
                throw ex;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(LifeOne.Models.AdminManagement.AEntity.TourResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }

        [HttpPost]
        public ResponseModel TourOnlinePayment(ResponseModel model)
        {


            ResponseModel returnResponse = new ResponseModel();
            Models.AdminManagement.AEntity.TourOnlinePayment objresponse = new Models.AdminManagement.AEntity.TourOnlinePayment();
            Models.AdminManagement.AEntity.TourOnlinePayment modelRequest = new Models.AdminManagement.AEntity.TourOnlinePayment();

            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<Models.AdminManagement.AEntity.TourOnlinePayment>(dcdata);
                objresponse = DALTourDetail.TourOnlinePayment(modelRequest);

                if (objresponse.Response == "success")
                {
                    objresponse.Remark = "success";
                    objresponse.Response = "success";
                }

            }
            catch (Exception ex)
            {
                objresponse.Remark = ex.Message;
                objresponse.Response = "failure";
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(Models.AdminManagement.AEntity.TourOnlinePayment));
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
        [HttpPost]
        public ResponseModel TourOnlinePaymentStatus_V2(RequestModel model)
        {
            ResponseModel objApiResponse = new ResponseModel();
            AllPaymentDetailsRequest obj = new AllPaymentDetailsRequest();
            AllPaymentDetailsResponse objrps = new AllPaymentDetailsResponse();
            Razorpay.Api.Order objorder = null;
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            StreamReader sr;

            string EncryptedText = "";
            try
            {
                if (model != null)
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    obj = JsonConvert.DeserializeObject<AllPaymentDetailsRequest>(dcdata);

                    string OrderId = "", Status = "";
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    RazorpayClient client = null;
                    //client = new RazorpayClient("rzp_test_AFSSERe0Blac2S", "X6UBRaEzdUHdJFfXPQDmdLKb");
                    client = new RazorpayClient("rzp_live_nyCK5Vu6fq9JUq", "7snv6fZ93b3Z7h0xXdncSDX2");
                    //key and vlaue should be changed                    
                    if (obj.Action == 1)
                    {
                        Dictionary<string, object> options = new Dictionary<string, object>();
                        options.Add("amount", obj.Amount * 100);
                        options.Add("receipt", "");
                        options.Add("currency", "INR");
                        options.Add("payment_capture", 1);
                        objorder = client.Order.Create(options);
                        OrderId = objorder["id"].ToString();
                        objrps.OrderId = OrderId;
                        obj.OrderId = OrderId;
                    }
                    if (obj.Action == 2)
                    {
                        objorder = client.Order.Fetch(obj.OrderId);
                        Status = objorder["status"].ToString();
                        objrps.Status = Status;
                    }

                    var res = objcusdb.SaveUpdatePaymentDetails(obj);
                    if (res != null && res.flag == 1)
                    {

                        objrps = res;
                        objrps.OrderId = OrderId;
                        objrps.Status = Status;
                        objrps.Response = "Success";
                    }
                    else
                    {
                        objrps = res;
                        objrps.Response = "Error";
                    }
                }
                else
                {
                    objrps.Response = "error";
                }
            }
            catch (Exception ex)
            {
                objrps.Response = "error" + ex;
            }
            js = new DataContractJsonSerializer(typeof(AllPaymentDetailsResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objrps);
            ms.Position = 0;
            sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }

        #endregion
        // Franchise Order Request System
        [HttpPost]
        public ResponseModel GetkaryonProductList(RequestModel model)
        {
            MProductListAPIResponse objresponse = new MProductListAPIResponse();
            ResponseModel returnResponse = new ResponseModel();
            ProductViewModel modelRequest = new ProductViewModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<ProductViewModel>(dcdata);
                List<ProductViewModel> _objProductList = objcusdb.GetOrderTempKaryonApp(modelRequest);
                if (_objProductList.Count > 0)
                {
                    objresponse.response = "success";
                    objresponse.message = "success";
                    objresponse._ProductViewModellist = _objProductList;
                }
                else
                {
                    objresponse.response = "error";
                    objresponse.message = "Please try again later";
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
            js = new DataContractJsonSerializer(typeof(MProductListAPIResponse));
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


        [HttpPost]
        public ResponseModel GetProductCompanyList(RequestModel model)
        {
            MBindProductCompanyREponse objresponse = new MBindProductCompanyREponse();
            ResponseModel returnResponse = new ResponseModel();
            MBindCompanyList modelRequest = new MBindCompanyList();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<MBindCompanyList>(dcdata);
                modelRequest.ProdId = 1;
                List<MBindCompanyList> _objProductList = objcusdb.getProductCompanyList(modelRequest);
                if (_objProductList.Count > 0)
                {
                    objresponse.response = "success";
                    objresponse.message = "success";
                    objresponse._ProductViewModellist = _objProductList;
                }
                else
                {
                    objresponse.response = "error";
                    objresponse.message = "Please try again later";
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
            js = new DataContractJsonSerializer(typeof(MProductListAPIResponse));
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


        [HttpPost]
        public ResponseModel GetProductListCompanyWise(RequestModel model)
        {
            MProductListAPIResponse objresponse = new MProductListAPIResponse();
            ResponseModel returnResponse = new ResponseModel();
            ProductViewModel modelRequest = new ProductViewModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<ProductViewModel>(dcdata);
                modelRequest.ProdId = 2;
                List<ProductViewModel> _objProductList = objcusdb.getProductCompanyWise(modelRequest);
                if (_objProductList.Count > 0)
                {
                    objresponse.response = "success";
                    objresponse.message = "success";
                    objresponse._ProductViewModellist = _objProductList;
                }
                else
                {
                    objresponse.response = "error";
                    objresponse.message = "Please try again later";
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
            js = new DataContractJsonSerializer(typeof(MProductListAPIResponse));
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



        public ResponseModel AddTokaryonCart(RequestModel model)
        {
            MSimpleString obj = null;
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                LifeOne.Models.FranchiseManagement.FEntity.MFranchiseorderRequest modelRequest = JsonConvert.DeserializeObject<LifeOne.Models.FranchiseManagement.FEntity.MFranchiseorderRequest>(dcdata);
                obj = LifeOne.Models.FranchiseManagement.FDAL.DALFranchise.AddProduct(modelRequest);
                obj.Message = "Success";
            }
            catch (Exception ex)
            {
                obj.Code = 1;
                obj.Message = "Error";

            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(MSimpleString));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }

        [HttpPost]
        public ResponseModel GetkaryonOrderTemp(RequestModel model)
        {
            MSimpleString modelRequest = null;
            MOrderteampForKaryonResponse objresponse = new MOrderteampForKaryonResponse();
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<MSimpleString>(dcdata);
                List<LifeOne.Models.FranchiseManagement.FEntity.MFranchiseorderRequest> _objlst = LifeOne.Models.FranchiseManagement.FDAL.DALFranchise.GetOrderTemp(modelRequest);
                if (_objlst.Count > 0)
                {
                    objresponse.response = "success";
                    objresponse.message = "success";
                    objresponse._tempOrderDetails = _objlst;
                }
                else
                {
                    objresponse.response = "error";
                    objresponse.message = "no records found..";
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
            js = new DataContractJsonSerializer(typeof(MOrderteampForKaryonResponse));
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

        public ResponseModel FinalSubmitForOrder(RequestModel model)
        {
            MSimpleString obj = null;
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                LifeOne.Models.FranchiseManagement.FEntity.MFranchiseorderRequest modelRequest
                    = JsonConvert.DeserializeObject<LifeOne.Models.FranchiseManagement.FEntity.MFranchiseorderRequest>(dcdata);
                obj = LifeOne.Models.FranchiseManagement.FDAL.DALFranchise.SaveProductKaryon(modelRequest);
                obj.Message = "Success";
            }
            catch (Exception ex)
            {
                obj.Code = 1;
                obj.Message = "Error";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(MSimpleString));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }

        [HttpPost]
        public ResponseModel GetKaryonOrderDetails(RequestModel model)
        {
            MorderDetailsKaryonResponse objresponse = new MorderDetailsKaryonResponse();
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                LifeOne.Models.FranchiseManagement.FEntity.MOrder modelRequest = JsonConvert.DeserializeObject<LifeOne.Models.FranchiseManagement.FEntity.MOrder>(dcdata);
                List<LifeOne.Models.FranchiseManagement.FEntity.MOrder> _objOrderList = LifeOne.Models.FranchiseManagement.FDAL.DALFranchise.GetAllRequestdOrderList(modelRequest);
                if (_objOrderList.Count > 0)
                {

                    objresponse.response = "success";
                    objresponse.message = "success";
                    objresponse._tempOrderDetails = _objOrderList;
                }
                else
                {
                    objresponse.response = "error";
                    objresponse.message = "no records found..";
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
            js = new DataContractJsonSerializer(typeof(MorderDetailsKaryonResponse));
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
        [HttpPost]
        public ResponseModel DeleteTokaryonCart(RequestModel model)
        {
            MSimpleString obj = null;
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                LifeOne.Models.FranchiseManagement.FEntity.MFranchiseorderRequest modelRequest = JsonConvert.DeserializeObject<LifeOne.Models.FranchiseManagement.FEntity.MFranchiseorderRequest>(dcdata);
                obj = LifeOne.Models.FranchiseManagement.FDAL.DALFranchise.DeleteProductTemp(modelRequest);
                obj.Message = "Success";
            }
            catch (Exception ex)
            {
                obj.Code = 1;
                obj.Message = "Error";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(MSimpleString));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }
        [HttpPost]
        public ResponseModel GetKaryonOrderItemsDetails(RequestModel model)
        {
            MorderDetailsKaryonResponse objresponse = new MorderDetailsKaryonResponse();
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                LifeOne.Models.FranchiseManagement.FEntity.MOrder modelRequest = JsonConvert.DeserializeObject<LifeOne.Models.FranchiseManagement.FEntity.MOrder>(dcdata);
                List<LifeOne.Models.FranchiseManagement.FEntity.MOrder> _objOrderList = LifeOne.Models.FranchiseManagement.FDAL.DALFranchise.DALGetOrderRequestItemDetail(modelRequest.PK_RId);
                if (_objOrderList.Count > 0)
                {

                    objresponse.response = "success";
                    objresponse.message = "success";
                    objresponse._tempOrderDetails = _objOrderList;
                }
                else
                {
                    objresponse.Flag = 0;
                    objresponse.response = "error";
                    objresponse.message = "no records found..";
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
            js = new DataContractJsonSerializer(typeof(MorderDetailsKaryonResponse));
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

        [HttpPost]
        public ResponseModel GetKaryonOrderShippingInformation(RequestModel model)
        {
            MshippinginformationResponseAPP objresponse = new MshippinginformationResponseAPP();
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                LifeOne.Models.FranchiseManagement.FEntity.MOrder modelRequest = JsonConvert.DeserializeObject<LifeOne.Models.FranchiseManagement.FEntity.MOrder>(dcdata);
                List<LifeOne.Models.AdminManagement.AEntity.MAddShippingInformation> _objOrderList = LifeOne.Models.AdminManagement.ADAL.DALShippingInformation.DALGetSgippingInformationOnEdit(modelRequest.PK_RId.ToString());
                if (_objOrderList.Count > 0)
                {
                    objresponse.response = "success";
                    objresponse.message = "success";
                    objresponse.shippingdetails = _objOrderList;
                }
                else
                {
                    objresponse.response = "error";
                    objresponse.message = "no records found..";
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
            js = new DataContractJsonSerializer(typeof(MshippinginformationResponseAPP));
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

        [HttpGet]
        public ResponseModel CancelOrder(string OrderNo)
        {
            ResponseModel resmodel = new ResponseModel();
            DALRefundService refund = new DALRefundService();
            OrdersDetailsJsonJResponse objres = new OrdersDetailsJsonJResponse();
            ResultSet objApiResponse = new ResultSet();

            string EncryptedText = "";

            try
            {
                OrderNo = OrderNo.Replace(" ", "+");
                OrderNo = Crypto.Decryption(Aeskey, OrderNo);
                var res = objcusdb.GetOrderDetails(0, OrderNo, 2);

                if (res != null)
                {
                    objres.result = res;

                    if (objres.result[0].gatewayAmount > 0)
                    {
                        Models.AdminManagement.AEntity.MRazorPayRefundResponse model = new Models.AdminManagement.AEntity.MRazorPayRefundResponse();
                        model = refund.GetRazorPayRefund(objres.result[0].gatewayAmount, objres.result[0].TransactionNo);

                        if (model.code == "1")
                        {
                            objApiResponse = refund.GateWayRefundAmount(model.id, model.payment_id, objres.result[0].FK_MemId, Convert.ToInt32(objres.result[0].gatewayAmount), objres.result[0].OrderNo);

                            if (objApiResponse.Flag == 1)
                            {
                                objres.response = "success";
                                objres.message = model.Msg;

                                if (objres.result[0].WalletAmount > 0)
                                {
                                    objApiResponse = refund.WalletRefundAmount(objres.result[0].WalletAmount, objres.result[0].FK_MemId, objres.result[0].OrderNo, objres.result[0].TransactionNo);

                                    if (objApiResponse.Flag == 1)
                                    {
                                        objres.response = "success";
                                        objres.message = objApiResponse.Msg;
                                    }
                                    else
                                    {
                                        objres.response = "error";
                                        objres.message = objApiResponse.Msg;
                                    }
                                }

                                if (objres.result[0].cashbackAmount > 0)
                                {
                                    objApiResponse = refund.CashBackRefundAmount(objres.result[0].cashbackAmount, objres.result[0].FK_MemId, objres.result[0].OrderNo, objres.result[0].TransactionNo);

                                    if (objApiResponse.Flag == 1)
                                    {
                                        objres.response = "success";
                                        objres.message = objApiResponse.Msg;
                                    }
                                    else
                                    {
                                        objres.response = "error";
                                        objres.message = objApiResponse.Msg;
                                    }
                                }

                                if (objres.result[0].OrderNo != null)
                                {
                                    Models.AdminManagement.AEntity.MagentoRequestModel _obj = new Models.AdminManagement.AEntity.MagentoRequestModel();
                                    _obj.order_id = OrderNo;
                                    string _body = JsonConvert.SerializeObject(_obj);

                                    string _outputresult = refund.sendMShoppingCancelOrder("cancelOrder.php", "POST", _body);

                                    if (_outputresult != "0")
                                    {
                                        objres.message = _outputresult;
                                        objres.response = "success";
                                    }
                                    else
                                    {
                                        objres.response = "error";
                                    }
                                }
                            }
                            else
                            {
                                objres.response = "error";
                                objres.message = model.Msg;
                            }
                        }
                        else
                        {
                            objres.response = "error";
                            objres.message = model.Msg;
                        }
                    }
                    else
                    {
                        if (objres.result[0].WalletAmount > 0)
                        {
                            objApiResponse = refund.WalletRefundAmount(objres.result[0].WalletAmount, objres.result[0].FK_MemId, objres.result[0].OrderNo, objres.result[0].TransactionNo);

                            if (objApiResponse.Flag == 1)
                            {
                                objres.response = "success";
                                objres.message = objApiResponse.Msg;
                            }
                            else
                            {
                                objres.response = "error";
                                objres.message = objApiResponse.Msg;
                            }
                        }

                        if (objres.result[0].cashbackAmount > 0)
                        {
                            objApiResponse = refund.CashBackRefundAmount(objres.result[0].cashbackAmount, objres.result[0].FK_MemId, objres.result[0].OrderNo, objres.result[0].TransactionNo);

                            if (objApiResponse.Flag == 1)
                            {
                                objres.response = "success";
                                objres.message = objApiResponse.Msg;
                            }
                            else
                            {
                                objres.response = "error";
                                objres.message = objApiResponse.Msg;
                            }
                        }

                        if (objres.result[0].OrderNo != null)
                        {
                            Models.AdminManagement.AEntity.MagentoRequestModel _obj = new Models.AdminManagement.AEntity.MagentoRequestModel();
                            _obj.order_id = OrderNo;
                            string _body = JsonConvert.SerializeObject(_obj);

                            string _outputresult = refund.sendMShoppingCancelOrder("cancelOrder.php", "POST", _body);
                            if (_outputresult != "0")
                            {
                                objres.message = _outputresult;
                                objres.response = "success";
                            }
                            else
                            {
                                objres.response = "error";
                            }
                        }
                    }
                }
                else
                {
                    objres.response = "error";
                    objres.message = objApiResponse.Msg;
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
            js = new DataContractJsonSerializer(typeof(OrdersDetailsJsonJResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            resmodel.body = EncryptedText;
            return resmodel;
        }

        [Route("KaryonOrderSmryItemsAPI")]
        [HttpPost]
        public HttpResponseMessage KaryonOrderSmryItemsAPI(KaryonOrderSmryItemsAPIViewModel model)
        {
            if (!String.IsNullOrEmpty(model.InvoiceNo) && !String.IsNullOrEmpty(model.ProductCode) && model.ProductQty > 0 && model.MRP > 0 && model.DP > 0 && model.TaxableAmount > 0 && model.GSTAmount > 0 && model.TotalAmount > 0 && model.TotalProductPV > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, objcusdb.KaryonOrderSmryItemsAPI(model));
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new KaryionOrderResponse { Flag = 0, Message = "All Fields Are Required" });
            }
        }


        [HttpPost]
        public TokenResponseResponse GenerateTokenKaryOn(Models.API.Login model)
        {
            TokenResponseResponse objresponse = new TokenResponseResponse();
            try
            {
                string GetToken = LifeOne.Models.API.TokenManager.GenerateToken(model.LoginId);
                if (GetToken != "")
                {
                    objresponse.Flag = 1;
                    objresponse.response = "success";
                    objresponse.Token = GetToken;
                    objresponse.message = "Token Generated Successfully!";
                }
                else
                {
                    objresponse.Flag = 0;
                    objresponse.response = "error";
                    objresponse.message = "please try again later";
                }
            }
            catch (Exception ex)
            {
                objresponse.response = "error";
                objresponse.message = ex.Message;

            }
            return objresponse;
        }


        [HttpPost]
        public KaryionOrderResponse KaryonOrderBillingSummary(MOrderBillingAPI model)
        {
            KaryionOrderResponse modelRes = new KaryionOrderResponse();
            try
            {
                string tokenUsername = TokenManager.ValidateToken(model.Token);
                if (model.KaryOnLoginId.Equals(tokenUsername))
                {
                    if (!String.IsNullOrEmpty(model.KaryOnLoginId) && !String.IsNullOrEmpty(model.InvoiceNo) && !String.IsNullOrEmpty(model.InvoiceDate) && !String.IsNullOrEmpty(model.DiscountAmount) && !String.IsNullOrEmpty(model.TaxableAmount) && !String.IsNullOrEmpty(model.GSTAmount) && !String.IsNullOrEmpty(model.TotalAmount) && !String.IsNullOrEmpty(model.TotalPv) && !String.IsNullOrEmpty(model.AirOrbit) && !String.IsNullOrEmpty(model.WaterOrbit) && !String.IsNullOrEmpty(model.SpaceOrbit))
                    {
                        modelRes = objcusdb.KaryonOrderBillingSummary(model);
                    }
                    else
                        modelRes = new KaryionOrderResponse { Flag = 0, Message = "All Fields Are Required" };
                }
                else
                    modelRes = new KaryionOrderResponse { Flag = 0, Message = "Invalid Token!" };
            }
            catch (Exception ex)
            {
                modelRes = new KaryionOrderResponse { Flag = 0, Message = ex.Message };
            }

            return modelRes;
        }

        [HttpPost]
        public KaryionOrderResponse KaryonOrderBillingSummaryWithItems(MSummaryAndOrderItemsInformation modelRequest)
        {

            KaryionOrderResponse objres = new KaryionOrderResponse();
            try
            {

                string tokenUsername = TokenManager.ValidateToken(modelRequest.Token);
                if (modelRequest.KaryOnLoginId.Equals(tokenUsername))
                {
                    if (String.IsNullOrEmpty(modelRequest.KaryOnLoginId) || String.IsNullOrEmpty(modelRequest.InvoiceNo) || String.IsNullOrEmpty(modelRequest.InvoiceDate) || String.IsNullOrEmpty(modelRequest.DiscountAmount)
                    || String.IsNullOrEmpty(modelRequest.TaxableAmount) || String.IsNullOrEmpty(modelRequest.GSTAmount) || String.IsNullOrEmpty(modelRequest.TotalAmount) ||
                    String.IsNullOrEmpty(modelRequest.TotalPv) || String.IsNullOrEmpty(modelRequest.AirOrbit) ||
                    String.IsNullOrEmpty(modelRequest.WaterOrbit) || String.IsNullOrEmpty(modelRequest.SpaceOrbit))
                    {
                        objres.Flag = 0;
                        objres.Message = "All Fields Are Required";
                    }
                    string xml = "<result>";
                    if (modelRequest != null && modelRequest.CartItems != null && modelRequest.CartItems.Count > 0)
                    {
                        foreach (var item in modelRequest.CartItems)
                        {
                            xml += "<data><InvoiceNo>" + item.InvoiceNo + "</InvoiceNo><ProductCode>" + item.ProductCode + "</ProductCode><MRP>" + item.MRP + "</MRP><DP>" + item.DP + "</DP><ProductQty>" + item.ProductQty + "</ProductQty><TaxableAmount>" + item.TaxableAmount + "</TaxableAmount><GSTAmount>" + item.GSTAmount + "</GSTAmount><TotalAmount>" + item.TotalAmount + "</TotalAmount><TotalProductPV>" + item.TotalProductPV + "</TotalProductPV></data>";
                        }
                    }
                    xml += "</result>";
                    modelRequest.xmldata = xml;
                    objres = objcusdb.AddKaryonOrderSummaryDAL(modelRequest);
                }
                else
                    objres = new KaryionOrderResponse { Flag = 0, Message = "Invalid Token!" };
            }
            catch (Exception ex)
            {
                objres.Message = "Error";
            }
            return objres;
        }

        [HttpPost]
        public KaryionOrderResponse ApproveDeclineKYC(ApproveDeclineKYCKaryon modelRequest)
        {
            KaryionOrderResponse objres = new KaryionOrderResponse();
            try
            {
                string tokenUsername = TokenManager.ValidateToken(modelRequest.Token);
                if (modelRequest.LoginId.Equals(tokenUsername))
                {
                    objres = objcusdb.DALUPdateKYC(modelRequest);
                }
                else
                    objres = new KaryionOrderResponse { Flag = 0, Message = "Invalid Token!" };
            }
            catch (Exception ex)
            {
                objres.Message = "Error";
            }
            return objres;
        }

        [HttpPost]
        public KaryionOrderResponse UpdateUserProfile(UpdateUserProfileViewModel model)
        {
            KaryionOrderResponse modelRes = new KaryionOrderResponse();
            try
            {
                string tokenUsername = TokenManager.ValidateToken(model.Token);
                if (model.LoginId.Equals(tokenUsername))
                {
                    if (!String.IsNullOrEmpty(model.firstName) && !String.IsNullOrEmpty(model.lastName) && !String.IsNullOrEmpty(model.mobile) && !String.IsNullOrEmpty(model.pancard) && !String.IsNullOrEmpty(model.aadhar) && !String.IsNullOrEmpty(model.memberBankName) && !String.IsNullOrEmpty(model.memberBranch) && !String.IsNullOrEmpty(model.memberAccNo) && !String.IsNullOrEmpty(model.bankHolderName) && !String.IsNullOrEmpty(model.ifscCode) && !String.IsNullOrEmpty(model.LoginId) && !String.IsNullOrEmpty(model.PinCode))
                    {
                        modelRes = objcusdb.UpdateUserProfile(model);
                    }
                    else
                    {
                        modelRes = new KaryionOrderResponse { Flag = 0, Message = "All Fields Are Required" };
                    }
                }
                else
                    modelRes = new KaryionOrderResponse { Flag = 0, Message = "Invalid Token!" };
            }
            catch (Exception ex)
            {
                modelRes = new KaryionOrderResponse
                {
                    Flag = 0,
                    Message = ex.Message
                };
            }
            return modelRes;
        }

        [HttpPost]
        public ResponseModel FranchiseListById(RequestModel model)
        {
            FranchiseListResponseApp objresponse = new FranchiseListResponseApp();
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                FranchiseListRequest modelRequest = JsonConvert.DeserializeObject<FranchiseListRequest>(dcdata);
                objresponse.FranchiseListForWebLst = new FranchiseListForWeb().GetFranchise(modelRequest.FK_MemId);
                if (objresponse.FranchiseListForWebLst.Count > 0)
                {
                    objresponse.response = "success";
                    objresponse.message = "success";
                    objresponse.FranchiseListForWebLst = objresponse.FranchiseListForWebLst;
                }
                else
                {
                    objresponse.response = "error";
                    objresponse.message = "no records found..";
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
            js = new DataContractJsonSerializer(typeof(FranchiseListResponseApp));
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

        [HttpPost]
        public ResponseModel FranchisePayemntHistoryListById(RequestModel model)
        {
            FranchiseListResponseAppHistory objresponse = new FranchiseListResponseAppHistory();
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                FranchiseListRequestForPayment modelRequest = JsonConvert.DeserializeObject<FranchiseListRequestForPayment>(dcdata);
                objresponse.FranchiseListForWebLst = new FranchiseListForWeb().GetFranchisePaymentHistory(modelRequest.RequestId);
                if (objresponse.FranchiseListForWebLst.Count > 0)
                {
                    objresponse.response = "success";
                    objresponse.message = "success";
                    objresponse.FranchiseListForWebLst = objresponse.FranchiseListForWebLst;
                }
                else
                {
                    objresponse.response = "error";
                    objresponse.message = "no records found..";
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
            js = new DataContractJsonSerializer(typeof(FranchiseListResponseAppHistory));
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

        [HttpPost]
        public ResponseModel FranchisePaymentRequest_V1(RequestModel model)
        {
            CommonJsonReponse objresponse = new CommonJsonReponse();
            ResponseModel returnResponse = new ResponseModel();
            Models.API.WalletJsonRequest modelRequest = new Models.API.WalletJsonRequest();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<Models.API.WalletJsonRequest>(dcdata);

                var res = objcusdb.FranchisePaymentRequest_V1(modelRequest);
                if (res != null)
                {
                    if (res.Flag == 1)
                    {
                        objresponse.response = "success";
                        objresponse.message = res.Msg;
                        objresponse.TransId = res.TransId;

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
            js = new DataContractJsonSerializer(typeof(CommonJsonReponse));
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

        [HttpGet]
        public HttpResponseMessage StateMasterList()
        {
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            var dataList = objcusdb.StateMasterList();
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(List<StateMasterListModel>));
            ms = new MemoryStream();
            js.WriteObject(ms, dataList);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel { body = returnResponse.body });
        }

        [HttpPost]
        public HttpResponseMessage DistrictMasterList(RequestModel model)
        {
            DistrictMasterListModel _model = new DistrictMasterListModel();
            string dcdata = Crypto.Decryption(Aeskey, model.body);
            _model = JsonConvert.DeserializeObject<Models.API.DistrictMasterListModel>(dcdata);
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            var dataList = objcusdb.DistrictMasterList(_model);
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(List<DistrictMasterListModel>));
            ms = new MemoryStream();
            js.WriteObject(ms, dataList);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel { body = returnResponse.body });
        }

        [HttpPost]
        public HttpResponseMessage FranchiseBalanceDetail(RequestModel model)
        {
            FranchiseBalanceDetailRequestModel _model = new FranchiseBalanceDetailRequestModel();
            string dcdata = Crypto.Decryption(Aeskey, model.body);
            _model = JsonConvert.DeserializeObject<Models.API.FranchiseBalanceDetailRequestModel>(dcdata);
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            var data = objcusdb.FranchiseBalanceDetail(_model);
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FranchiseBalanceDetailModel));
            ms = new MemoryStream();
            js.WriteObject(ms, data);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel { body = returnResponse.body, });
        }

        [HttpPost]
        public ResponseModel FranchiseProductCrDrDetails(RequestModel model)
        {
            FranchiseCrDrDetailsResponse objresponse = new FranchiseCrDrDetailsResponse();
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                FranchiseCrDetailsReq modelRequest = JsonConvert.DeserializeObject<FranchiseCrDetailsReq>(dcdata);
                objresponse.FranchiseCrDrDetails = objcusdb.FranchiseCrDrDetails(modelRequest);
                if (objresponse.FranchiseCrDrDetails.Count > 0)
                {
                    objresponse.response = "success";
                    objresponse.message = "success";
                    objresponse.FranchiseCrDrDetails = objresponse.FranchiseCrDrDetails;
                }
                else
                {
                    objresponse.response = "error";
                    objresponse.message = "no records found..";
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
            js = new DataContractJsonSerializer(typeof(FranchiseCrDrDetailsResponse));
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

        [HttpPost]
        public ResponseModel FranchiseCancelOrder(RequestModel model)
        {
            CommonJsonReponse objresponse = new CommonJsonReponse();
            ResponseModel returnResponse = new ResponseModel();
            MCancelOrder modelRequest = new MCancelOrder();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<MCancelOrder>(dcdata);

                var res = objcusdb.FranchiseCancelOrder(modelRequest);
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
            js = new DataContractJsonSerializer(typeof(CommonJsonReponse));
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

        [HttpPost]
        public HttpResponseMessage FranchiseAllowRequestByID(RequestModel model)
        {
            FranchiseAllowRequestModel reqModel = new FranchiseAllowRequestModel();
            string dcdata = Crypto.Decryption(Aeskey, model.body);
            reqModel = JsonConvert.DeserializeObject<Models.API.FranchiseAllowRequestModel>(dcdata);
            string EncryptedText = "";
            ResponseModel returnResponse = new ResponseModel();
            var data = objcusdb.FranchiseAllowRequestByID(reqModel);
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FranchiseAllowRequestDetailModel));
            ms = new MemoryStream();
            js.WriteObject(ms, data);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel { body = returnResponse.body });
        }

        [HttpPost]
        public ApiResponseModel CheckStatusForCompanion(RequestModel model)
        {
            ApiResponseModel objApiResponse = new ApiResponseModel();
            CheckStatusForCompanionResponse obj1 = new CheckStatusForCompanionResponse();
            Category db = new Category();
            string EncryptedText = "";
            try
            {
                MCompanion reqModel = new MCompanion();
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                reqModel = JsonConvert.DeserializeObject<Models.API.MCompanion>(dcdata);
                DataSet ds = db.CheckStatusForCompanion(Convert.ToInt32(reqModel.Fk_RequestId));
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            List<KeyValuesDetails> Catlist = new List<KeyValuesDetails>();
                            DeclineFranchiseDetails objDetails = new DeclineFranchiseDetails();
                            if (ds.Tables[3].Rows.Count > 0)
                            {
                                DeclineFranchiseDetails _objSale = new DeclineFranchiseDetails();
                                _objSale.AssociateId = ds.Tables[3].Rows[0]["AssociateId"].ToString();
                                _objSale.AssociateName = ds.Tables[3].Rows[0]["AssociateName"].ToString();
                                _objSale.MobileNo = ds.Tables[3].Rows[0]["MobileNo"].ToString();
                                _objSale.AppliedForPinCode = ds.Tables[3].Rows[0]["AppliedForPinCode"].ToString();
                                _objSale.DeclineMsg = ds.Tables[3].Rows[0]["DeclineMsg"].ToString();
                                _objSale.PinCode = ds.Tables[3].Rows[0]["PinCode"].ToString();
                                _objSale.Village = ds.Tables[3].Rows[0]["Village"].ToString();
                                _objSale.Taluka = ds.Tables[3].Rows[0]["Taluka"].ToString();
                                _objSale.District = ds.Tables[3].Rows[0]["District"].ToString();
                                _objSale.AreaName = ds.Tables[3].Rows[0]["AreaName"].ToString();
                                _objSale.FranchiseName = ds.Tables[3].Rows[0]["FranchiseName"].ToString();
                                _objSale.Status = ds.Tables[3].Rows[0]["Status"].ToString();
                                _objSale.IsApproved = Convert.ToBoolean(ds.Tables[3].Rows[0]["IsApproved"].ToString());
                                _objSale.isDeclined = Convert.ToBoolean(ds.Tables[3].Rows[0]["isDeclined"].ToString());
                                _objSale.DeclineReason = ds.Tables[3].Rows[0]["DeclineReason"].ToString();
                                _objSale.CompanionStatus = Convert.ToInt32(ds.Tables[3].Rows[0]["CompanionStatus"].ToString());
                                _objSale.PK_CompanionId = Convert.ToInt32(ds.Tables[3].Rows[0]["PK_CompanionId"].ToString());
                                obj1.declineFranchiseDetails = _objSale;
                            }
                            if (ds.Tables[2].Rows.Count > 0)
                            {
                                foreach (DataRow Dr1 in ds.Tables[2].Rows)
                                {
                                    KeyValuesDetails objCat = new KeyValuesDetails();
                                    objCat.KeyValus = Dr1["KeyValus"].ToString();
                                    Catlist.Add(objCat);
                                }
                                obj1.keyValuesDetails = Catlist;
                            }
                            else
                            {
                                obj1.keyValuesDetails = Catlist;
                            }
                        }
                        obj1.Flag = Convert.ToInt32(ds.Tables[0].Rows[0]["Flag"].ToString());
                        obj1.Msg = ds.Tables[0].Rows[0]["Msg"].ToString();
                        obj1.LoginID = ds.Tables[0].Rows[0]["LoginID"].ToString();
                        obj1.CompanyName = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                        obj1.PersonName = ds.Tables[0].Rows[0]["PersonName"].ToString();
                        obj1.P_Address = ds.Tables[0].Rows[0]["P_Address"].ToString();
                        obj1.P_PinCode = ds.Tables[0].Rows[0]["P_PinCode"].ToString();
                        obj1.P_State = ds.Tables[0].Rows[0]["P_State"].ToString();
                        obj1.P_City = ds.Tables[0].Rows[0]["P_City"].ToString();
                        obj1.Status = ds.Tables[0].Rows[0]["Status"].ToString();
                        obj1.Heading = ds.Tables[1].Rows[0]["Heading"].ToString();
                        obj1.response = "success";
                    }
                    else
                    {
                        obj1.response = "success";
                        obj1.Flag = Convert.ToInt32(ds.Tables[0].Rows[0]["Flag"].ToString());
                        obj1.Msg = ds.Tables[0].Rows[0]["Msg"].ToString();
                    }
                }
                else
                {
                    obj1.response = "error";
                    obj1.Msg = "error";
                }
            }
            catch (Exception ex)
            {
                obj1.response = "error";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CheckStatusForCompanionResponse));
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

        [HttpPost]
        public ResponseModel CompanionPaymentRequest(RequestModel model)
        {
            CommonJsonReponse objresponse = new CommonJsonReponse();
            ResponseModel returnResponse = new ResponseModel();
            CompanionPaymentReq modelRequest = new CompanionPaymentReq();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<CompanionPaymentReq>(dcdata);
                var res = objcusdb.CompanionPaymentRequest(modelRequest);
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
                    objresponse.message = "Something went wrong";
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
            js = new DataContractJsonSerializer(typeof(CommonJsonReponse));
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
        [HttpPost]
        public ResponseModel VirtualCompanionPaymentRequest(RequestModel model)
        {
            CommonJsonReponse objresponse = new CommonJsonReponse();
            ResponseModel returnResponse = new ResponseModel();
            Models.API.WalletJsonRequest modelRequest = new Models.API.WalletJsonRequest();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<Models.API.WalletJsonRequest>(dcdata);

                var res = objcusdb.VirtualCompanionPaymentRequest(modelRequest);
                if (res != null)
                {
                    if (res.Flag == 1)
                    {
                        objresponse.response = "success";
                        objresponse.message = res.Msg;
                        objresponse.TransId = res.TransId;

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
            js = new DataContractJsonSerializer(typeof(CommonJsonReponse));
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

        [HttpPost]
        public ResponseModel VirtualCompanionPayemntHistory(RequestModel model)
        {
            VirtualCompanionListResponseAppHistory objresponse = new VirtualCompanionListResponseAppHistory();
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                CompanionPayMentHistoryReq modelRequest = JsonConvert.DeserializeObject<CompanionPayMentHistoryReq>(dcdata);
                objresponse.VirtualCompanionListForWebLst = objcusdb.CompanionPaymentHistory(modelRequest);
                if (objresponse.VirtualCompanionListForWebLst.Count > 0)
                {
                    objresponse.response = "success";
                    objresponse.message = "success";
                    objresponse.TotalPaidAmount = objresponse.VirtualCompanionListForWebLst[0].TotalPaidAmount;
                    objresponse.VirtualCompanionListForWebLst = objresponse.VirtualCompanionListForWebLst;
                }
                else
                {
                    objresponse.response = "success";
                    objresponse.message = "no records found..";
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
            js = new DataContractJsonSerializer(typeof(VirtualCompanionListResponseAppHistory));
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

        //API for PayAdda. enbales us to updateing pending status of recharge
        //recharge Callback function        
        //[Route("api/data/{recordId}")]     
        [HttpGet]
        //[AuthorizeIPAddressAttribute]
        public PayAddaResponse UpdateRechargePendingStatus(string Status, string ClientTransactionId, string TransactionId, string OperatorTransactionId)
        {
            PayAddaResponse objres = new PayAddaResponse();
            try
            {
                objres = objcusdb.DALUpdateRechargePendingStatus(Status, ClientTransactionId, TransactionId, OperatorTransactionId);
                if (objres != null)
                {
                    objres.Flag = objres.Flag;
                    objres.Msg = objres.Msg;
                }
                else
                {
                    objres.Flag = 0;
                    objres.Msg = "Error";
                }
            }
            catch (Exception ex)
            {
                objres.Flag = 0;
                objres.Msg = ex.Message;
            }
            return objres;
        }

        //Franchise Billng Details
        //SelectProductToGetProductDetails
        [HttpPost]
        public ResponseModel SelectProductToGetProductDetails(RequestModel model)
        {
            MFranchiseProductDetailsResponse objresponse = new MFranchiseProductDetailsResponse();
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                MFranchiseorderRequest modelRequest = JsonConvert.DeserializeObject<MFranchiseorderRequest>(dcdata);
                objresponse.ProductResponse = new DALFranchise().GetProductDetailsForAPI(modelRequest);
                if (objresponse.ProductResponse != null)
                {
                    objresponse.response = "success";
                    objresponse.message = "success";
                }
                else
                {
                    objresponse.response = "error";
                    objresponse.message = "no records found..";
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
            js = new DataContractJsonSerializer(typeof(MFranchiseProductDetailsResponse));
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

        //AddToCartForFranchiseBilling
        public ResponseModel AddToCartForFranchiseBilling(RequestModel model)
        {
            MSimpleString obj = null;
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                MFranchiseorderRequest modelRequest = JsonConvert.DeserializeObject<MFranchiseorderRequest>(dcdata);
                obj = FranchiseOrderService.AddFCustomerProductServiceAPI(modelRequest);
                obj.Message = "Success";
            }
            catch (Exception ex)
            {
                obj.Code = 1;
                obj.Message = "Error";

            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(MSimpleString));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }

        //Get Details from temp data
        [HttpPost]
        public ResponseModel GetFranchiseOrderBillingTemp(RequestModel model)
        {
            MSimpleString modelRequest = null;
            MOrderteampForKaryonResponse objresponse = new MOrderteampForKaryonResponse();
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<MSimpleString>(dcdata);
                List<MFranchiseorderRequest> _objlst = DALFranchise.GetFCustomerOrderTemp(modelRequest);
                if (_objlst.Count > 0)
                {
                    objresponse.response = "success";
                    objresponse.message = "success";
                    objresponse._tempOrderDetails = _objlst;
                }
                else
                {
                    objresponse.response = "error";
                    objresponse.message = "no records found..";
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
            js = new DataContractJsonSerializer(typeof(MOrderteampForKaryonResponse));
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

        //Final Submit of Franchise Order
        public ResponseModel FinalSubmitForFranchiseBill(RequestModel model)
        {
            MSimpleString obj = new MSimpleString();
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                LifeOne.Models.FranchiseManagement.FEntity.MFranchiseorderRequest modelRequest
                    = JsonConvert.DeserializeObject<LifeOne.Models.FranchiseManagement.FEntity.MFranchiseorderRequest>(dcdata);
                MSimpleString result = FranchiseOrderService.SaveFCustomerProductServiceAPI(modelRequest);
                obj.Message = result.Remark;
                obj.Code = result.Code;
            }
            catch (Exception ex)
            {
                obj.Code = 0;
                obj.Message = "Error";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(MSimpleString));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }

        //After Submit get the franchise billed orde information
        [HttpPost]
        public ResponseModel GetFranchiseBilledorderDetails(RequestModel model)
        {
            MorderDetailsKaryonResponse objresponse = new MorderDetailsKaryonResponse();
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                LifeOne.Models.FranchiseManagement.FEntity.MOrder modelRequest = JsonConvert.DeserializeObject<LifeOne.Models.FranchiseManagement.FEntity.MOrder>(dcdata);
                List<LifeOne.Models.FranchiseManagement.FEntity.MOrder> _objOrderList = LifeOne.Models.FranchiseManagement.FDAL.DALFranchise.GetAllSuppliedOrderList(modelRequest);
                if (_objOrderList.Count > 0)
                {
                    objresponse.response = "success";
                    objresponse.message = "success";
                    objresponse._tempOrderDetails = _objOrderList;
                }
                else
                {
                    objresponse.response = "error";
                    objresponse.message = "no records found..";
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
            js = new DataContractJsonSerializer(typeof(MorderDetailsKaryonResponse));
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
        [HttpPost]
        public ResponseModel GetFranciseOrderItems(RequestModel model)
        {
            MorderDetailsKaryonResponse objresponse = new MorderDetailsKaryonResponse();
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                LifeOne.Models.FranchiseManagement.FEntity.MOrder modelRequest = JsonConvert.DeserializeObject<LifeOne.Models.FranchiseManagement.FEntity.MOrder>(dcdata);

                List<MOrder> _objOrderList = FranchiseOrderService.ViewSupplyItemService(modelRequest.PK_RId);

                if (_objOrderList.Count > 0)
                {
                    objresponse.response = "success";
                    objresponse.message = "success";
                    objresponse._tempOrderDetails = _objOrderList;
                }
                else
                {
                    objresponse.response = "error";
                    objresponse.message = "no records found..";
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
            js = new DataContractJsonSerializer(typeof(MorderDetailsKaryonResponse));
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
        [HttpPost]
        public ResponseModel DeleteFromFranchiseCartItem(RequestModel model)
        {
            MSimpleString obj = null;
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                LifeOne.Models.FranchiseManagement.FEntity.MFranchiseorderRequest modelRequest = JsonConvert.DeserializeObject<LifeOne.Models.FranchiseManagement.FEntity.MFranchiseorderRequest>(dcdata);
                obj = LifeOne.Models.FranchiseManagement.FDAL.DALFranchise.DeleteProductTempAPI(modelRequest);
                obj.Message = "Success";
            }
            catch (Exception ex)
            {
                obj.Code = 1;
                obj.Message = "Error";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(MSimpleString));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }
        /*Customer details*/
        [HttpGet]
        public ResponseModel CustomerDetailsByMobile(string Mobile)
        {
            CustomerDetailsResponse obj = new CustomerDetailsResponse();
            ResponseModel objApiResponse = new ResponseModel();

            Mobile = Mobile.Replace(" ", "+");
            Mobile = Crypto.Decryption(Aeskey, Mobile);
            string EncryptedText = "";
            try
            {
                var res = objcusdb.GetCustomerDetailsByMobile(Mobile);
                if (res != null)
                {
                    if (res.Flag == 1)
                    {
                        obj = res;
                        obj.response = "success";

                    }
                    else
                    {
                        obj = res;
                        obj.response = "success";
                    }
                }
                else
                {
                    obj.response = "error ";
                    obj.Msg = "error";
                }
            }
            catch (Exception ex)
            {
                obj.response = "error ";
                obj.Msg = ex.Message.ToString();
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CustomerDetailsResponse));
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

        /*ProductListByFranchise*/
        [HttpPost]
        public ResponseModel ProductListByFranchise(RequestModel model)
        {
            ProductDetailByFranchiseResponse obj = new ProductDetailByFranchiseResponse();
            ResponseModel objApiResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                ProductReq modelRequest = JsonConvert.DeserializeObject<ProductReq>(dcdata);
                var res = objcusdb.GetProductFranchiseByFranchise(modelRequest);
                if (res != null)
                {
                    if (res.Count > 0)
                    {
                        obj.result = res;
                        obj.Msg = "success";
                        obj.response = "success";
                    }
                    else
                    {
                        obj.result = res;
                        obj.Msg = "data does not exists";
                        obj.response = "success";
                    }
                }
                else
                {
                    obj.response = "error ";

                }
            }
            catch (Exception ex)
            {
                obj.response = "error";
                obj.Msg = ex.Message.ToString();
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ProductDetailByFranchiseResponse));
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
        public ResponseModel ApiAssociateBusinessReport(RequestModel model)
        {

            ResponseModel objApiResponse = new ResponseModel();
            APIAssociateBusinessReport objresult = new APIAssociateBusinessReport();
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                MAssociateMonthlyBusiness modelRequest = JsonConvert.DeserializeObject<MAssociateMonthlyBusiness>(dcdata);
                var res = objcusdb.APIAssociateBusinessReport(modelRequest);
                if (res != null)
                {
                    if (res.Count > 0)
                    {
                        objresult.AssociateBusinessReport = res;
                        objresult.Remark = "success";
                        objresult.response = "success";
                    }
                    else
                    {

                        objresult.Remark = "data does not exists";
                        objresult.response = "success";
                    }
                }
                else
                {
                    objresult.response = "error ";

                }
            }
            catch (Exception ex)
            {
                objresult.response = "error";
                objresult.Remark = ex.Message.ToString();
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(APIAssociateBusinessReport));
            ms = new MemoryStream();
            js.WriteObject(ms, objresult);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }

        #region Franchise Related
        [HttpPost]
        public ResponseModel GetMemberInfoByBIDUID_V3MasterFranchise(RequestModel model)
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
                    objresponse = objcusdb.GetMembersDetailsByIdForFranchiseRequest_v3MasterFranchise(modelRequest);
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

        //[HttpPost]
        //public ResponseModel CreateOrderV3KaryOnAPPSingleProduct(RequestModel model)
        //{
        //    CommonJsonReponse objres = new CommonJsonReponse();
        //    CardItemRequest modelRequest = new CardItemRequest();
        //    ResponseModel objApiResponse = new ResponseModel();
        //    StreamReader srd;
        //    string EncryptedText = "";
        //    try
        //    {
        //        string dcdata = Crypto.Decryption(Aeskey, model.body);
        //        modelRequest = JsonConvert.DeserializeObject<Models.API.CardItemRequest>(dcdata);

        //        string xml = "<result>";
        //        if (modelRequest != null && modelRequest.cartitems != null && modelRequest.cartitems.Count > 0)
        //        {
        //            foreach (var item in modelRequest.cartitems)
        //            {
        //                xml += "<data><PrdId>" + item.PrdId + "</PrdId><productname>" + item.productname + "</productname><price>" + item.price + "</price><producttype>" + item.producttype + "</producttype><quatity>" + item.quatity + "</quatity><image>" + item.image + "</image><cashbackper>" + item.cashbackper + "</cashbackper><BV>" + item.BV + "</BV><DP>" + item.DP + "</DP><PV>" + item.PV + "</PV></data>";
        //            }
        //        }
        //        xml += "</result>";
        //        modelRequest.xmldata = xml;
        //        modelRequest.ProcId = 1;
        //        var res = objcusdb.CreateOrderV3APPKaryonSingleApp(modelRequest);
        //        if (res != null && res.Flag == 1)
        //        {
        //            objres.response = "Success";
        //            objres.message = res.Msg;
        //            objres.orderId = res.orderId;
        //        }
        //        else
        //        {
        //            objres.response = "error";
        //            objres.message = res.Msg;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        objres.response = "error";
        //        objres.message = ex.Message;
        //    }

        //    string CustData = "";
        //    DataContractJsonSerializer js;
        //    MemoryStream ms;
        //    js = new DataContractJsonSerializer(typeof(CommonJsonReponse));
        //    ms = new MemoryStream();
        //    js.WriteObject(ms, objres);
        //    ms.Position = 0;
        //    StreamReader sr = new StreamReader(ms);
        //    CustData = sr.ReadToEnd();
        //    sr.Close();
        //    ms.Close();
        //    EncryptedText = Crypto.Encryption(Aeskey, CustData);
        //    objApiResponse.body = EncryptedText;
        //    return objApiResponse;
        //}


        //[HttpPost]
        //public ResponseModel ValidateCodeAndUPateOrderStatus(RequestModel model)
        //{
        //    CommonJsonReponse objresponse = new CommonJsonReponse();
        //    ResponseModel returnResponse = new ResponseModel();
        //    Models.API.ValidateCodeandUpdateStatus modelRequest = new Models.API.ValidateCodeandUpdateStatus();
        //    string EncryptedText = "";
        //    try
        //    {
        //        string dcdata = Crypto.Decryption(Aeskey, model.body);
        //        modelRequest = JsonConvert.DeserializeObject<Models.API.ValidateCodeandUpdateStatus>(dcdata);
        //        var res = objcusdb.DAlValidateCodeAndUPateOrderStatus(modelRequest);
        //        if (res != null)
        //        {
        //            if (res.Flag == 1)
        //            {
        //                objresponse.response = "success";
        //                objresponse.message = res.Msg;

        //            }
        //            else
        //            {
        //                objresponse.response = "error";
        //                objresponse.message = res.Msg;
        //            }
        //        }
        //        else
        //        {
        //            objresponse.response = "error";
        //            objresponse.message = "";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        objresponse.response = "error";
        //        objresponse.message = ex.Message;

        //    }
        //    string CustData = "";
        //    DataContractJsonSerializer js;
        //    MemoryStream ms;
        //    js = new DataContractJsonSerializer(typeof(CommonJsonReponse));
        //    ms = new MemoryStream();
        //    js.WriteObject(ms, objresponse);
        //    ms.Position = 0;
        //    StreamReader sr = new StreamReader(ms);
        //    CustData = sr.ReadToEnd();
        //    sr.Close();
        //    ms.Close();
        //    EncryptedText = Crypto.Encryption(Aeskey, CustData);
        //    returnResponse.body = EncryptedText;
        //    return returnResponse;
        //}

        //[HttpPost]
        //public ResponseModel GetMasterKaryonOrderItemsDetailsSingle(RequestModel model)
        //{
        //    MorderDetailsKaryonResponse objresponse = new MorderDetailsKaryonResponse();
        //    ResponseModel returnResponse = new ResponseModel();
        //    string EncryptedText = "";
        //    try
        //    {
        //        string dcdata = Crypto.Decryption(Aeskey, model.body);
        //        FamilyN.Models.FranchiseManagement.FEntity.MOrder modelRequest = JsonConvert.DeserializeObject<FamilyN.Models.FranchiseManagement.FEntity.MOrder>(dcdata);
        //        List<FamilyN.Models.FranchiseManagement.FEntity.MOrder> _objOrderList = FamilyN.Models.FranchiseManagement.FDAL.DALFranchise.DALMasterGetOrderRequestItemDetailSingle(modelRequest.PK_RId);
        //        if (_objOrderList.Count > 0)
        //        {

        //            objresponse.response = "success";
        //            objresponse.message = "success";
        //            objresponse._tempOrderDetails = _objOrderList;
        //        }
        //        else
        //        {
        //            objresponse.Flag = 0;
        //            objresponse.response = "error";
        //            objresponse.message = "no records found..";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        objresponse.response = "error";
        //        objresponse.message = ex.Message;

        //    }
        //    string CustData = "";
        //    DataContractJsonSerializer js;
        //    MemoryStream ms;
        //    js = new DataContractJsonSerializer(typeof(MorderDetailsKaryonResponse));
        //    ms = new MemoryStream();
        //    js.WriteObject(ms, objresponse);
        //    ms.Position = 0;
        //    StreamReader sr = new StreamReader(ms);
        //    CustData = sr.ReadToEnd();
        //    sr.Close();
        //    ms.Close();
        //    EncryptedText = Crypto.Encryption(Aeskey, CustData);
        //    returnResponse.body = EncryptedText;
        //    return returnResponse;
        //}

        //[HttpPost]
        //public ResponseModel GetMasterFranchiseBilledorderDetails(RequestModel model)
        //{
        //    MorderDetailsKaryonResponse objresponse = new MorderDetailsKaryonResponse();
        //    ResponseModel returnResponse = new ResponseModel();
        //    string EncryptedText = "";
        //    try
        //    {
        //        string dcdata = Crypto.Decryption(Aeskey, model.body);
        //        FamilyN.Models.FranchiseManagement.FEntity.MOrder modelRequest = JsonConvert.DeserializeObject<FamilyN.Models.FranchiseManagement.FEntity.MOrder>(dcdata);
        //        List<FamilyN.Models.FranchiseManagement.FEntity.MOrder> _objOrderList = FamilyN.Models.FranchiseManagement.FDAL.DALFranchise.GetMasterAllSuppliedOrderList(modelRequest);
        //        if (_objOrderList.Count > 0)
        //        {
        //            objresponse.response = "success";
        //            objresponse.message = "success";
        //            objresponse._tempOrderDetails = _objOrderList;
        //        }
        //        else
        //        {
        //            objresponse.response = "error";
        //            objresponse.message = "no records found..";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        objresponse.response = "error";
        //        objresponse.message = ex.Message;

        //    }
        //    string CustData = "";
        //    DataContractJsonSerializer js;
        //    MemoryStream ms;
        //    js = new DataContractJsonSerializer(typeof(MorderDetailsKaryonResponse));
        //    ms = new MemoryStream();
        //    js.WriteObject(ms, objresponse);
        //    ms.Position = 0;
        //    StreamReader sr = new StreamReader(ms);
        //    CustData = sr.ReadToEnd();
        //    sr.Close();
        //    ms.Close();
        //    EncryptedText = Crypto.Encryption(Aeskey, CustData);
        //    returnResponse.body = EncryptedText;
        //    return returnResponse;
        //}


        //[HttpGet]
        //public ResponseModel GetCompanyBankKaryon()
        //{

        //    ResponseModel objApiResponse = new ResponseModel();
        //    BankMasterKaryonResponse objres = new BankMasterKaryonResponse();
        //    string CustData = "";
        //    DataContractJsonSerializer js;
        //    MemoryStream ms;
        //    StreamReader sr;
        //    string EncryptedText = "";
        //    try
        //    {

        //        var res = objcusdb.GetkaryonBankDetail();
        //        if (res != null)
        //        {
        //            objres.result = res;


        //            objres.farmerMinimumAmount = 500;
        //            objres.response = "success";
        //            objres.message = "success";

        //        }
        //        else
        //        {
        //            objres.response = "error";
        //            objres.message = "error";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        objres.response = "error";
        //        objres.message = ex.Message;
        //    }

        //    js = new DataContractJsonSerializer(typeof(BankMasterKaryonResponse));
        //    ms = new MemoryStream();
        //    js.WriteObject(ms, objres);
        //    ms.Position = 0;
        //    sr = new StreamReader(ms);
        //    CustData = sr.ReadToEnd();
        //    sr.Close();
        //    ms.Close();
        //    EncryptedText = Crypto.Encryption(Aeskey, CustData);
        //    objApiResponse.body = EncryptedText;
        //    return objApiResponse;
        //}


        //[HttpPost]
        //public ResponseModel FranchisePaymentRequestMatserAddmore_V1(RequestModel model)
        //{
        //    CommonJsonReponse objresponse = new CommonJsonReponse();
        //    ResponseModel returnResponse = new ResponseModel();
        //    Models.API.WalletJsonRequest modelRequest = new Models.API.WalletJsonRequest();
        //    string EncryptedText = "";
        //    try
        //    {
        //        string dcdata = Crypto.Decryption(Aeskey, model.body);
        //        modelRequest = JsonConvert.DeserializeObject<Models.API.WalletJsonRequest>(dcdata);
        //        var res = objcusdb.FranchisePaymentRequestMasterAddmoreIncome_V1(modelRequest);
        //        if (res != null)
        //        {
        //            if (res.Flag == 1)
        //            {
        //                objresponse.response = "success";
        //                objresponse.message = res.Msg;
        //                objresponse.TransId = res.TransId;

        //            }
        //            else
        //            {
        //                objresponse.response = "error";
        //                objresponse.message = res.Msg;
        //            }
        //        }
        //        else
        //        {
        //            objresponse.response = "error";
        //            objresponse.message = "";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        objresponse.response = "error";
        //        objresponse.message = ex.Message;

        //    }
        //    string CustData = "";
        //    DataContractJsonSerializer js;
        //    MemoryStream ms;
        //    js = new DataContractJsonSerializer(typeof(CommonJsonReponse));
        //    ms = new MemoryStream();
        //    js.WriteObject(ms, objresponse);
        //    ms.Position = 0;
        //    StreamReader sr = new StreamReader(ms);
        //    CustData = sr.ReadToEnd();
        //    sr.Close();
        //    ms.Close();
        //    EncryptedText = Crypto.Encryption(Aeskey, CustData);
        //    returnResponse.body = EncryptedText;
        //    return returnResponse;
        //}


        //[HttpPost]
        //public ResponseModel CreateOrderV3MasterIdAPP(RequestModel model)
        //{
        //    CommonJsonReponse objres = new CommonJsonReponse();
        //    CardItemRequest modelRequest = new CardItemRequest();
        //    ResponseModel objApiResponse = new ResponseModel();
        //    StreamReader srd;
        //    string EncryptedText = "";
        //    try
        //    {
        //        string dcdata = Crypto.Decryption(Aeskey, model.body);
        //        modelRequest = JsonConvert.DeserializeObject<Models.API.CardItemRequest>(dcdata);

        //        string xml = "<result>";
        //        if (modelRequest != null && modelRequest.cartitems != null && modelRequest.cartitems.Count > 0)
        //        {
        //            foreach (var item in modelRequest.cartitems)
        //            {
        //                xml += "<data><PrdId>" + item.PrdId + "</PrdId><productname>" + item.productname + "</productname><price>" + item.price + "</price><producttype>" + item.producttype + "</producttype><quatity>" + item.quatity + "</quatity><image>" + item.image + "</image><cashbackper>" + item.cashbackper + "</cashbackper><BV>" + item.BV + "</BV><DP>" + item.DP + "</DP><PV>" + item.PV + "</PV></data>";
        //            }
        //        }
        //        xml += "</result>";
        //        modelRequest.xmldata = xml;
        //        var res = objcusdb.CreateOrderV3APPMasterId(modelRequest);
        //        if (res != null && res.Flag == 1)
        //        {
        //            objres.response = "Success";
        //            objres.message = res.Msg;
        //            objres.Fk_MemId = res.Fk_MemId;
        //            objres.orderId = res.orderId;
        //        }
        //        else
        //        {
        //            objres.response = "error";
        //            objres.message = res.Msg;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        objres.response = "error";
        //        objres.message = ex.Message;
        //    }

        //    string CustData = "";
        //    DataContractJsonSerializer js;
        //    MemoryStream ms;
        //    js = new DataContractJsonSerializer(typeof(CommonJsonReponse));
        //    ms = new MemoryStream();
        //    js.WriteObject(ms, objres);
        //    ms.Position = 0;
        //    StreamReader sr = new StreamReader(ms);
        //    CustData = sr.ReadToEnd();
        //    sr.Close();
        //    ms.Close();
        //    EncryptedText = Crypto.Encryption(Aeskey, CustData);
        //    objApiResponse.body = EncryptedText;
        //    return objApiResponse;
        //}


        //[HttpPost]
        //public ResponseModel GetkaryonMasterProductList(RequestModel model)
        //{
        //    MProductListAPIResponse objresponse = new MProductListAPIResponse();
        //    ResponseModel returnResponse = new ResponseModel();
        //    ProductViewModel modelRequest = new ProductViewModel();
        //    string EncryptedText = "";
        //    try
        //    {
        //        string dcdata = Crypto.Decryption(Aeskey, model.body);
        //        modelRequest = JsonConvert.DeserializeObject<ProductViewModel>(dcdata);
        //        List<ProductViewModel> _objProductList = objcusdb.GetOrderTempMsterKaryonApp(modelRequest);
        //        if (_objProductList.Count > 0)
        //        {
        //            objresponse.response = "success";
        //            objresponse.message = "success";
        //            objresponse._ProductViewModellist = _objProductList;
        //        }
        //        else
        //        {
        //            objresponse.response = "error";
        //            objresponse.message = "Please try again later";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        objresponse.response = "error";
        //        objresponse.message = ex.Message;

        //    }
        //    string CustData = "";
        //    DataContractJsonSerializer js;
        //    MemoryStream ms;
        //    js = new DataContractJsonSerializer(typeof(MProductListAPIResponse));
        //    ms = new MemoryStream();
        //    js.WriteObject(ms, objresponse);
        //    ms.Position = 0;
        //    StreamReader sr = new StreamReader(ms);
        //    CustData = sr.ReadToEnd();
        //    sr.Close();
        //    ms.Close();
        //    EncryptedText = Crypto.Encryption(Aeskey, CustData);
        //    returnResponse.body = EncryptedText;
        //    return returnResponse;
        //}
        #endregion

        #region Shopping API
        [HttpPost]
        public ResponseModel GetCategory()
        {

            ResponseModel objApiResponse = new ResponseModel();
            CategoryResponse objres = new CategoryResponse();
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            StreamReader sr;
            string EncryptedText = "";
            try
            {

                var res = objcusdb.GetCategory();
                if (res != null)
                {
                    objres.result = res;
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

            js = new DataContractJsonSerializer(typeof(CategoryResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }

        [HttpPost]
        public ResponseModel GetProducts(RequestModel model)
        {
            ProductRequest modelRequest = new ProductRequest();
            ResponseModel objApiResponse = new ResponseModel();
            ProductResponse objres = new ProductResponse();
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            StreamReader sr;
            string EncryptedText = "";
            try
            {
                if (model != null)
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    modelRequest = JsonConvert.DeserializeObject<ProductRequest>(dcdata);
                }

                var res = objcusdb.GetProductDetails(modelRequest);
                if (res != null)
                {
                    objres.result = res;
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

            js = new DataContractJsonSerializer(typeof(ProductResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }

        [HttpPost]
        public ResponseModel GetAddress(RequestModel model)
        {
            AddressRequest modelRequest = new AddressRequest();
            ResponseModel objApiResponse = new ResponseModel();
            AddressResponse objres = new AddressResponse();
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            StreamReader sr;
            string EncryptedText = "";
            try
            {

                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<AddressRequest>(dcdata);
                var res = objcusdb.GetAddressDetails(modelRequest);
                if (res != null)
                {
                    objres.result = res;
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

            js = new DataContractJsonSerializer(typeof(AddressResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }

        [HttpPost]
        public ResponseModel AddAddress(RequestModel model)
        {
            AddressRequest modelRequest = new AddressRequest();
            ResponseModel objApiResponse = new ResponseModel();
            AddressResponse objres = new AddressResponse();
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            StreamReader sr;
            string EncryptedText = "";
            try
            {

                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<AddressRequest>(dcdata);
                var res = modelRequest.Pk_AddressId == 0 ? objcusdb.AddAddress(modelRequest) : objcusdb.UpdateAddress(modelRequest);
                if (res.Flag == 1)
                {

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

            js = new DataContractJsonSerializer(typeof(AddressResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }

        [HttpPost]
        public ResponseModel DeleteAddress(RequestModel model)
        {
            AddressRequest modelRequest = new AddressRequest();
            ResponseModel objApiResponse = new ResponseModel();
            AddressResponse objres = new AddressResponse();
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            StreamReader sr;
            string EncryptedText = "";
            try
            {

                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<AddressRequest>(dcdata);
                var res = objcusdb.DeleteAddress(modelRequest);
                if (res.Flag == 1)
                {

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

            js = new DataContractJsonSerializer(typeof(AddressResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }

        [HttpPost]
        public ResponseModel PlaceOrder(RequestModel model)
        {
            DataTable dtProductDetails = new DataTable();
            dtProductDetails.Columns.Add("Fk_ProductId");
            dtProductDetails.Columns.Add("Amount");
            dtProductDetails.Columns.Add("Quantity");

            DataTable dtPaymentDetails = new DataTable();
            dtPaymentDetails.Columns.Add("PaymentMode");
            dtPaymentDetails.Columns.Add("Amount");

            Razorpay.Api.Order objorder = null;

            PlaceShoppingOrderRequest modelRequest = new PlaceShoppingOrderRequest();
            ResponseModel objApiResponse = new ResponseModel();
            PlaceOrderResponse objres = new PlaceOrderResponse();
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            string orderId = "";
            StreamReader sr;
            string EncryptedText = "";
            try
            {

                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<PlaceShoppingOrderRequest>(dcdata);

                for (int i = 0; i <= modelRequest.ProductDetails.Count - 1; i++)
                {
                    dtProductDetails.Rows.Add(modelRequest.ProductDetails[i].Fk_ProductId, modelRequest.ProductDetails[i].Amount, modelRequest.ProductDetails[i].Quantity);
                }
                modelRequest.dtProductDetails = dtProductDetails;

                for (int i = 0; i <= modelRequest.PaymentDetails.Count - 1; i++)
                {
                    dtPaymentDetails.Rows.Add(modelRequest.PaymentDetails[i].PaymentMode, modelRequest.PaymentDetails[i].Amount);
                    if (modelRequest.Type == "Local" && modelRequest.PaymentDetails[i].PaymentMode == "Gateway")
                    {

                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        RazorpayClient client = null;
                        client = new RazorpayClient(RazorPayLocalKey, RazorPayLocalSecret);
                        Dictionary<string, object> options = new Dictionary<string, object>();
                        options.Add("amount", modelRequest.PaymentDetails[i].Amount * 100);
                        options.Add("receipt", "");
                        options.Add("currency", "INR");
                        options.Add("payment_capture", 1);
                        objorder = client.Order.Create(options);
                        orderId = objorder["id"].ToString();


                    }
                    if (modelRequest.Type == "Live" && modelRequest.PaymentDetails[i].PaymentMode == "Gateway")
                    {
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        RazorpayClient client = null;
                        client = new RazorpayClient(RazorPayLiveKey, RazorPayLiveSecret);
                        Dictionary<string, object> options = new Dictionary<string, object>();
                        options.Add("amount", modelRequest.PaymentDetails[i].Amount * 100);
                        options.Add("receipt", "");
                        options.Add("currency", "INR");
                        options.Add("payment_capture", 1);
                        objorder = client.Order.Create(options);
                        orderId = objorder["id"].ToString();

                    }

                }
                modelRequest.OpCode = 1;
                modelRequest.dtPaymentDetails = dtPaymentDetails;
                modelRequest.OrderIdRazorpay = orderId;
                var res = objcusdb.PlaceOrder(modelRequest);
                if (res.Flag == 1)
                {

                    objres.response = "success";
                    objres.message = "success";
                    objres.OrderId = orderId;
                    objres.FK_OrderId = res.TransId;

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

            js = new DataContractJsonSerializer(typeof(PlaceOrderResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }


        [HttpPost]
        public ResponseModel GetOrderStatus(RequestModel model)
        {
            CommonJsonReponse objres = new CommonJsonReponse();
            PlaceShoppingOrderRequest modelRequest = new PlaceShoppingOrderRequest();
            ResponseModel objApiResponse = new ResponseModel();
            StreamReader srd;
            string EncryptedText = "";
            try
            {
                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<PlaceShoppingOrderRequest>(dcdata);


                modelRequest.OpCode = 3;
                
                DataSet dataSet = modelRequest.PlaceOrder();
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    if (dataSet.Tables[0].Rows[0]["Code"].ToString() == "1")
                    {
                        objres.response = "success";
                        objres.message = "success";
                        objres.OrderStatus = dataSet.Tables[0].Rows[0]["OrderStatus"].ToString();
                    }
                    else
                    {
                        objres.response = "error";
                        objres.message = "error";
                    }
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
            js = new DataContractJsonSerializer(typeof(CommonJsonReponse));
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
        public ResponseModel GetOrderList(RequestModel model)
        {
            GetOrdersRequest modelRequest = new GetOrdersRequest();
            ResponseModel objApiResponse = new ResponseModel();
            GetOrderListResponse objres = new GetOrderListResponse();
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            StreamReader sr;
            string EncryptedText = "";
            try
            {

                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<GetOrdersRequest>(dcdata);
                modelRequest.OpCode = 4;
                var res = objcusdb.GetOrderList(modelRequest);
                if (res != null)
                {
                    objres.result = res;
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

            js = new DataContractJsonSerializer(typeof(GetOrderListResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }

        [HttpPost]
        public ResponseModel GetOrderDetails(RequestModel model)
        {
            string result = Reports.HITAPIAuthorization(APIURL.Authorization);

            AuthorizationResponse deserializedProduct = JsonConvert.DeserializeObject<AuthorizationResponse>(result);


            ShoppingOrderDetailsRequest modelRequest = new ShoppingOrderDetailsRequest();
            ResponseModel objApiResponse = new ResponseModel();
            ShoppingOrderDetailsResponse objres = new ShoppingOrderDetailsResponse();
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            StreamReader sr;
            string EncryptedText = "";
            try
            {

                string dcdata = Crypto.Decryption(Aeskey, model.body);
                modelRequest = JsonConvert.DeserializeObject<ShoppingOrderDetailsRequest>(dcdata);
                modelRequest.OpCode = 5;
                modelRequest.token = deserializedProduct.token;
                var res = objcusdb.ShoppingOrderDetails(modelRequest);
               
                if (res != null)
                {
                    objres.result = res;
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

            js = new DataContractJsonSerializer(typeof(ShoppingOrderDetailsResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }



        #endregion Shopping API



        [HttpGet]
        public ResponseModel PayoutIncomedetails(string Fk_MemId, string PlanId)
        {
            PayoutDetailsRequest modelRequest = new PayoutDetailsRequest();
            ResponseModel objApiResponse = new ResponseModel();
            PayoutDetailsResponse objres = new PayoutDetailsResponse();
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            StreamReader sr;
            string EncryptedText = "";
            try
            {


                modelRequest.Fk_MemId = Convert.ToInt64(Crypto.Decryption(Aeskey, Fk_MemId));
                modelRequest.Fk_PlanId = Convert.ToInt64(Crypto.Decryption(Aeskey, PlanId));
                var res = objcusdb.PayoutIncomeReportType(modelRequest);
                if (res != null)
                {
                    objres.result = res;
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

            js = new DataContractJsonSerializer(typeof(PayoutDetailsResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }


        #region Premium Club API
        [HttpPost]
        public ResponseModel GetPremiumClubMembers(RequestModel model)
        {
            PremiumClubMembers modelRequest = new PremiumClubMembers();
            ResponseModel objApiResponse = new ResponseModel();
            Premium_ClubMembers objres = new Premium_ClubMembers();
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            StreamReader sr;
            string EncryptedText = "";
            try
            {
                if (model != null)
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    modelRequest = JsonConvert.DeserializeObject<PremiumClubMembers>(dcdata);
                }

                var res = PremiumClubMembersModel.GetPremiumClubMembers();
                if (res != null)
                {
                    objres.result = res;
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

            js = new DataContractJsonSerializer(typeof(Premium_ClubMembers));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }

        [HttpPost]
        public ResponseModel GetPremiumClubQualifier(RequestModel model)
        {
            PremiumClubQualifier modelRequest = new PremiumClubQualifier();
            ResponseModel objApiResponse = new ResponseModel();
            Premium_ClubQualifier objres = new Premium_ClubQualifier();
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            StreamReader sr;
            string EncryptedText = "";
            try
            {
                if (model != null)
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    modelRequest = JsonConvert.DeserializeObject<PremiumClubQualifier>(dcdata);
                }

                var res = PremiumClubMembersModel.GetPremiumClubQualifier();
                if (res != null)
                {
                    objres.result = res;
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

            js = new DataContractJsonSerializer(typeof(Premium_ClubQualifier));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }

        #endregion  Premium Club API


        //[HttpPost]
        //public ResponseModel AssociateRepurchasePayoutReport(RequestModel model)
        //{
        //    AssociatePayoutReport modelRequest = new AssociatePayoutReport();
        //    //PremiumClubQualifier modelRequest = new PremiumClubQualifier();
        //    ResponseModel objApiResponse = new ResponseModel();
        //    APayoutReport objres = new APayoutReport();
        //    string CustData = "";
        //    DataContractJsonSerializer js;
        //    MemoryStream ms;
        //    StreamReader sr;
        //    string EncryptedText = "";
        //    try
        //    {
        //        if (model != null)
        //        {
        //            string dcdata = Crypto.Decryption(Aeskey, model.body);
        //            modelRequest = JsonConvert.DeserializeObject<AssociatePayoutReport>(dcdata);
        //        }

        //        var res = PremiumClubMembersModel.AssociateRepurchasePayoutReport(modelRequest);
        //        if (res != null)
        //        {
        //            objres.result = res;
        //            objres.response = "success";
        //            objres.message = "success";

        //        }
        //        else
        //        {
        //            objres.response = "error";
        //            objres.message = "error";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        objres.response = "error";
        //        objres.message = ex.Message;
        //    }

        //    js = new DataContractJsonSerializer(typeof(APayoutReport));
        //    ms = new MemoryStream();
        //    js.WriteObject(ms, objres);
        //    ms.Position = 0;
        //    sr = new StreamReader(ms);
        //    CustData = sr.ReadToEnd();
        //    sr.Close();
        //    ms.Close();
        //    EncryptedText = Crypto.Encryption(Aeskey, CustData);
        //    objApiResponse.body = EncryptedText;
        //    return objApiResponse;
        //}


        [HttpPost]
        public ResponseModel AssociateRepurchasePayoutReport(RequestModel model)
        {
            try
            {
                APayoutReport objres = new APayoutReport();
                ResponseModel returnResponse = new ResponseModel();
                AssociatePayoutReport modelRequest = new AssociatePayoutReport();
                string EncryptedText = "";
                try
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    modelRequest = JsonConvert.DeserializeObject<AssociatePayoutReport>(dcdata);
                    modelRequest.FromDate = null;
                    modelRequest.ToDate = null;
                    var res = PremiumClubMembersModel.AssociateRepurchasePayoutReport(modelRequest);
                    if (res != null)
                    {
                        objres.result = res;
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
                js = new DataContractJsonSerializer(typeof(APayoutReport));
                ms = new MemoryStream();
                js.WriteObject(ms, objres);
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
        public ResponseModel AssociateRepurchasePayoutType(RequestModel model)
        {
            try
            {
                APayoutTypeReport objres = new APayoutTypeReport();
                ResponseModel returnResponse = new ResponseModel();
                AssociatePayoutTypeReport modelRequest = new AssociatePayoutTypeReport();
                string EncryptedText = "";
                try
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    modelRequest = JsonConvert.DeserializeObject<AssociatePayoutTypeReport>(dcdata);

                    var res = PremiumClubMembersModel.AssociateRepurchasePayoutType(modelRequest);
                    if (res != null)
                    {
                        objres.result = res;
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
                js = new DataContractJsonSerializer(typeof(APayoutTypeReport));
                ms = new MemoryStream();
                js.WriteObject(ms, objres);
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
        public ResponseModel BusinessSummary(RequestModel model)
        {
            try
            {
                BusinessSummaryResponse objres = new BusinessSummaryResponse();
                ResponseModel returnResponse = new ResponseModel();
                BusinessSummaryRequest modelRequest = new BusinessSummaryRequest();
                string EncryptedText = "";
                try
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    modelRequest = JsonConvert.DeserializeObject<BusinessSummaryRequest>(dcdata);
                    List<DirectSponsoring> lst = new List<DirectSponsoring>();
                    modelRequest.FromDate = String.IsNullOrEmpty(modelRequest.FromDate) ? null : modelRequest.FromDate;
                    modelRequest.ToDate = String.IsNullOrEmpty(modelRequest.ToDate) ? null : modelRequest.ToDate;
                    DataSet dataSet = modelRequest.GetBusinessSummary();
                    objres.TotalSponsoring = dataSet.Tables[0].Rows[0]["TotalSponsoring"].ToString();
                    objres.ActiveSponsoring = dataSet.Tables[0].Rows[0]["ActiveSponsoring"].ToString();
                    objres.InactiveSponsoring = dataSet.Tables[0].Rows[0]["InactiveSponsoring"].ToString();
                    objres.DirectBv = dataSet.Tables[0].Rows[0]["DirectBv"].ToString();
                    objres.LeftBV = dataSet.Tables[0].Rows[0]["LeftBV"].ToString();
                    objres.DirectBusiness = dataSet.Tables[0].Rows[0]["DirectBusiness"].ToString();
                    objres.RightBV = dataSet.Tables[0].Rows[0]["RightBV"].ToString();
                    objres.MatchingBV = dataSet.Tables[0].Rows[0]["MatchingBV"].ToString();
                    objres.LeftBVRepurchase = dataSet.Tables[0].Rows[0]["LeftBVRepurchase"].ToString();
                    objres.RightBVRepurchase = dataSet.Tables[0].Rows[0]["RightBVRepurchase"].ToString();
                    objres.MatchingBVRepurchase = dataSet.Tables[0].Rows[0]["MatchingBVRepurchase"].ToString();

                    if (dataSet.Tables[1] != null)
                    {
                        if (dataSet.Tables[1].Rows.Count > 0)
                        {
                            for (int i = 0; i <= dataSet.Tables[1].Rows.Count - 1; i++)
                            {
                                DirectSponsoring directSponsoring = new DirectSponsoring();
                                directSponsoring.LoginId = dataSet.Tables[1].Rows[i]["LoginId"].ToString();
                                directSponsoring.Name = dataSet.Tables[1].Rows[i]["Name"].ToString();
                                directSponsoring.PackageName = dataSet.Tables[1].Rows[i]["PackageName"].ToString();
                                directSponsoring.FLoginId = dataSet.Tables[1].Rows[i]["FLoginId"].ToString();
                                directSponsoring.FranchiseName = dataSet.Tables[1].Rows[i]["FranchiseName"].ToString();
                                directSponsoring.TopupDate = dataSet.Tables[1].Rows[i]["TopupDate"].ToString();
                                directSponsoring.JoiningDate = dataSet.Tables[1].Rows[i]["JoiningDate"].ToString();
                                directSponsoring.PayMentMode = dataSet.Tables[1].Rows[i]["PayMentMode"].ToString();
                                directSponsoring.Amount = dataSet.Tables[1].Rows[i]["Amount"].ToString();
                                directSponsoring.BV = dataSet.Tables[1].Rows[i]["BV"].ToString();
                                lst.Add(directSponsoring);
                            }
                        }
                    }
                    objres.directSponsorings = lst;



                    objres.response = "success";
                    objres.message = "success";
                }
                catch (Exception ex)
                {
                    objres.response = "error";
                    objres.message = ex.Message;

                }
                string CustData = "";
                DataContractJsonSerializer js;
                MemoryStream ms;
                js = new DataContractJsonSerializer(typeof(BusinessSummaryResponse));
                ms = new MemoryStream();
                js.WriteObject(ms, objres);
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
        public ResponseModel CreateOrderCashFree(RequestModel requestModel)
        {
            DataTable dtProductDetails = new DataTable();
            dtProductDetails.Columns.Add("Fk_ProductId");
            dtProductDetails.Columns.Add("Amount");
            dtProductDetails.Columns.Add("Quantity");

            DataTable dtPaymentDetails = new DataTable();
            dtPaymentDetails.Columns.Add("PaymentMode");
            dtPaymentDetails.Columns.Add("Amount");

            CashFreeOrderResponse objres = new CashFreeOrderResponse();
            ResponseModel returnResponse = new ResponseModel();
            CreateOrderCashFreeRequest modelRequest = new CreateOrderCashFreeRequest();
            string EncryptedText = "";
            try
            {
                string OrderId = Common.GenerateRandom();
                string dcdata = Crypto.Decryption(Aeskey, requestModel.body);
                modelRequest = JsonConvert.DeserializeObject<CreateOrderCashFreeRequest>(dcdata);
                if (modelRequest.Type == "Wallet")
                {
                    modelRequest.OrderId = DateTime.Now.ToString("ddMMyyyyhhMMss") + "" + modelRequest.Fk_MemId.ToString();
                    DataSet dataSet = modelRequest.CreateOrder();
                    if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Code"].ToString() == "1")
                        {
                            objres.code = 1;
                            objres.message = "Order Created Successfully";
                            objres.OrderId = dataSet.Tables[0].Rows[0]["OrderId"].ToString();
                        }
                        else
                        {
                            objres.code = 0;
                            objres.message = "Problem In Connection";
                        }
                    }
                }
                else
                {
                    PlaceShoppingOrderRequest placeShoppingOrderRequest = new PlaceShoppingOrderRequest();
                    ResponseModel objApiResponse = new ResponseModel();
                    PlaceOrderResponse placeOrderResponse = new PlaceOrderResponse();
                    string dcdata1 = Crypto.Decryption(Aeskey, requestModel.body);
                    placeShoppingOrderRequest = JsonConvert.DeserializeObject<PlaceShoppingOrderRequest>(dcdata1);
                    for (int i = 0; i <= placeShoppingOrderRequest.ProductDetails.Count - 1; i++)
                    {
                        dtProductDetails.Rows.Add(placeShoppingOrderRequest.ProductDetails[i].Fk_ProductId, placeShoppingOrderRequest.ProductDetails[i].Amount, placeShoppingOrderRequest.ProductDetails[i].Quantity);
                    }
                    placeShoppingOrderRequest.dtProductDetails = dtProductDetails;

                    for (int i = 0; i <= placeShoppingOrderRequest.PaymentDetails.Count - 1; i++)
                    {
                        dtPaymentDetails.Rows.Add(placeShoppingOrderRequest.PaymentDetails[i].PaymentMode, placeShoppingOrderRequest.PaymentDetails[i].Amount);
                        

                    }
                    placeShoppingOrderRequest.OpCode = 1;
                    placeShoppingOrderRequest.dtPaymentDetails = dtPaymentDetails;
                    //placeShoppingOrderRequest.OrderIdRazorpay = OrderId;
                    DataSet dataSet = placeShoppingOrderRequest.PlaceOrder();
                    if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Code"].ToString() == "1")
                        {
                            objres.code = 1;
                            objres.message = "Order Created Successfully";
                            objres.OrderId = dataSet.Tables[0].Rows[0]["OrderId"].ToString();
                            objres.FK_OrderId = dataSet.Tables[0].Rows[0]["FK_OrderId"].ToString();
                        }
                        else
                        {
                            objres.code = 0;
                            objres.message = "Problem In Connection";
                        }
                    }


                }

            }
            catch (Exception ex)
            {

                objres.code = 0;
                objres.message = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CashFreeOrderResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;

        }
        [HttpPost]
        public ResponseModel GetRecogination(RequestModel model)
        {
            try
            {
                RecoginationResponse objres = new RecoginationResponse();
                ResponseModel returnResponse = new ResponseModel();
                RecoginationRegest modelRequest = new RecoginationRegest();
                string EncryptedText = "";
                try
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    modelRequest = JsonConvert.DeserializeObject<RecoginationRegest>(dcdata);
                    List<Recogination> lst = new List<Recogination>();
         
                    DataSet dataSet = modelRequest.GetRecognition();
                   

                    if (dataSet.Tables[0] != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                Recogination recogination = new Recogination();
                                recogination.RecognitionName = dataSet.Tables[0].Rows[i]["RecognitionName"].ToString();
                                recogination.TargetPoint = (dataSet.Tables[0].Rows[i]["TargetPoint"].ToString());
                                recogination.AchievedOn = dataSet.Tables[0].Rows[i]["AchievedOn"].ToString();
                                recogination.ImageURL = dataSet.Tables[0].Rows[i]["ImageURL"].ToString();
                                recogination.RecogStatus = dataSet.Tables[0].Rows[i]["RecogStatus"].ToString();
                               
                                lst.Add(recogination);
                            }
                        }
                    }
                    objres.recoginationLst = lst;



                    objres.response = "success";
                    objres.message = "success";
                }
                catch (Exception ex)
                {
                    objres.response = "error";
                    objres.message = ex.Message;

                }
                string CustData = "";
                DataContractJsonSerializer js;
                MemoryStream ms;
                js = new DataContractJsonSerializer(typeof(RecoginationResponse));
                ms = new MemoryStream();
                js.WriteObject(ms, objres);
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
        public ResponseModel Rewards(RequestModel model)
        {
            try
            {
                RewardResponse objres = new RewardResponse();
                ResponseModel returnResponse = new ResponseModel();
                RewardRegest modelRequest = new RewardRegest();
                string EncryptedText = "";
                try
                {
                    string dcdata = Crypto.Decryption(Aeskey, model.body);
                    modelRequest = JsonConvert.DeserializeObject<RewardRegest>(dcdata);
                    List<Reward> lst = new List<Reward>();

                    DataSet dataSet = modelRequest.Rewards();


                    if (dataSet.Tables[0] != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                Reward reward = new Reward();
                                reward.RewardName = dataSet.Tables[0].Rows[i]["RewardName"].ToString();
                                reward.RewardAmount = (dataSet.Tables[0].Rows[i]["RewardAmount"].ToString());
                                reward.LeftBusiness = dataSet.Tables[0].Rows[i]["LeftBusiness"].ToString();
                                reward.RightBusiness = dataSet.Tables[0].Rows[i]["RightBusiness"].ToString();
                                reward.BalanceRight = dataSet.Tables[0].Rows[i]["BalanceRight"].ToString();
                                reward.Status = dataSet.Tables[0].Rows[i]["RewardStatus"].ToString();
                                reward.TargetPoint = dataSet.Tables[0].Rows[i]["RawardTarget"].ToString();
                                reward.AchievedOn = dataSet.Tables[0].Rows[i]["QDate"].ToString();
                                reward.FkSetRewardId = dataSet.Tables[0].Rows[i]["PK_MemberRewardID"].ToString();

                                lst.Add(reward);
                            }
                        }
                    }
                    objres.rewardLst = lst;



                    objres.response = "success";
                    objres.message = "success";
                }
                catch (Exception ex)
                {
                    objres.response = "error";
                    objres.message = ex.Message;

                }
                string CustData = "";
                DataContractJsonSerializer js;
                MemoryStream ms;
                js = new DataContractJsonSerializer(typeof(RewardResponse));
                ms = new MemoryStream();
                js.WriteObject(ms, objres);
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


    }
}