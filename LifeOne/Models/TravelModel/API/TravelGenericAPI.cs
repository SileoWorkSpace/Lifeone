using LifeOne.Models.TravelModel.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json;

namespace LifeOne.Models.TravelModel.API
{
    public class TravelGenericAPI
    {

        public static ResponseModel sendRequestToUtilityAPI(string _body, string _url)
        {
            try
            {
                ResponseModel _objResponseModel = null;
                string ICashCardBaseUrl = ConfigurationManager.AppSettings["MBaseUrlAPI"].ToString();
                var client = new HttpClient();
                client.BaseAddress = new Uri(ICashCardBaseUrl);
                client.DefaultRequestHeaders.Add("contentType", "application/json");
                //HTTP POST
                var responseTask = client.PostAsJsonAsync(_url, _body);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    string response = result.Content.ReadAsStringAsync().Result;
                    _objResponseModel = JsonConvert.DeserializeObject<ResponseModel>(response);
                }
                return _objResponseModel;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static ResponseModel sendRequestToUtilityAPIPost(RequestModel _body, string _url)
        {
            try
            {
                ResponseModel _objResponseModel = null;
                string ICashCardBaseUrl = ConfigurationManager.AppSettings["MBaseUrlAPI"].ToString();
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
                }
                return _objResponseModel;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static ResponseModel sendRequestToUtilityAPIPostFlight(RequestModel _body, string _url)
        {
            try
            {
                ResponseModel _objResponseModel = null; 
                string ICashCardBaseUrl = ConfigurationManager.AppSettings["MBaseUrlAPIFlight"].ToString();
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
                }
                return _objResponseModel;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}