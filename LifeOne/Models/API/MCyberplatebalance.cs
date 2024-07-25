using LifeOne.Models.BillAvenueUtility.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace LifeOne.Models.API
{
    public class MCyberplatebalance
    {
        public List<BalanceResponseCyberplate> ChkCompanyWalletBalanceService()
        {
            try
            {
                string _url = "CheckBalancesAmount";   //BaseUrl+API URL 
                BalanceModel _objBalanceModel = new BalanceModel();
                _objBalanceModel.CompanyId = 10012; //LifeOne Id
                _objBalanceModel.Fk_MemId = 1;
                string _servbody = JsonConvert.SerializeObject(_objBalanceModel);

                RequestModel _obj = new RequestModel();
                _obj.body = ApiEncrypt_Decrypt.EncryptString("Utility@#@132XYZ", _servbody);


                BalanceResponseCyberplate modelBalanceResponse = new BalanceResponseCyberplate();
                modelBalanceResponse = sendRequestToUtilityAPIPostCyberplate(_obj, _url);
                List<BalanceResponseCyberplate> _BalanceResponse = new List<BalanceResponseCyberplate>();
                _BalanceResponse.Add(modelBalanceResponse);
                return _BalanceResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static BalanceResponseCyberplate sendRequestToUtilityAPIPostCyberplate(RequestModel _body, string _url)
        {
            try
            {

                BalanceResponseCyberplate BalanceResponse = null;
                ResponseModel _objResponseModel = null;
                string ICashCardBaseUrl = "https://rmtapi.rightmovetech.com/api/CyberPlateWebAPIV2/";
                var client = new HttpClient();
                client.BaseAddress = new Uri(ICashCardBaseUrl);
                //client.DefaultRequestHeaders.Add("contentType", "application/json");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //HTTP POST
                //var responseTask = client.PostAsJsonAsync(_url, _body);
                var postTask = client.PostAsJsonAsync<RequestModel>(_url, _body);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    string response = result.Content.ReadAsStringAsync().Result;
                    _objResponseModel = JsonConvert.DeserializeObject<ResponseModel>(response);
                    BalanceResponse = LifeOne.Models.Common.DALCommon.CustomDeserializeObjectWithDecryptString<BalanceResponseCyberplate>(_objResponseModel.body);
                }
                return BalanceResponse;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
    public class BalanceModel
    {
        public long CompanyId { get; set; }
        public long Fk_MemId { get; set; }
    }

    public class BalanceResponseCyberplate
    {

        public Decimal BalanceAmount { get; set; }
        public String Status { get; set; }
        public string ValidateToken { get; set; }
        public string DebitCardCharge { get; set; }
        public string CreditCardCharge { get; set; }
        public string DebitCardMinCharge { get; set; }
        public string DebitCardMaxCharge { get; set; }
        public string ChargeableAmount { get; set; }
        public long CompanyId { get; set; }
    }
    //public class RequestModel
    //{
    //    public string body { get; set; }
    //}

    public class CyberPlatPara
    {
        public string Response { get; set; }
        public string Request { get; set; }
        public string keyPath { get; set; }
        public string Message { get; set; }
        public string SessionNo { get; set; }
        public string SDCode { get; set; }
        public string APCode { get; set; }
        public string OPCode { get; set; }
        public string MobileNo { get; set; }
        public string Account { get; set; }
        public string Authenticator3 { get; set; }
        public string Amount { get; set; }
        public string CERTNo { get; set; }
    }

}