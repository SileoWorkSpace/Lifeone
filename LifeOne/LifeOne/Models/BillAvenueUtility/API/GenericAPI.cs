using LifeOne.Models.BillAvenueUtility.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using LifeOne.Models.BillAvenueUtility.DAL;
using System.Net;
using System.IO;
using CCA.Util;
using System.Data;

namespace LifeOne.Models.BillAvenueUtility.API
{

    public class GenericAPI
    {
        public static ResponseModel sendRequestToUtilityAPIPost(RequestModel _body, string _url)
        {
            try
            {
                ResponseModel _objResponseModel = null;
                string ICashCardBaseUrl = ConfigurationManager.AppSettings["MBaseUrlAPIForBillAvenue"].ToString();
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
        public static T sendRequestToUtilityAPIPostWithCompIdAndFkMemID<T>(String encRequest, String _Url,
       string _billerid, long _CompanyId, long _Fk_MemID, string _AndroidId, string _IPAdressAPP, string _DeviceId, string _APIName, string _requestID) where T : class

        {
            MTransRequest _mTransRequest = new MTransRequest();
            T _response = null;
            CCACrypto ccaCrypto = new CCACrypto();


            string instituteId = "";
            string accessCode = "";
            string ver = "";
            string _workingKey = "";
            string BAVRequestId = "";
            String sUrl = "";

            instituteId = BIllAvenueCredientials.BAVinstituteId;
            accessCode = BIllAvenueCredientials.BAVaccessCode;
            _workingKey = BIllAvenueCredientials.BAVworkingKey;
            ver = BIllAvenueCredientials.BAVver;
            BAVRequestId = BIllAvenueCredientials.BAVRequestId;
            sUrl = BIllAvenueCredientials.MBaseUrlAPIBill + _Url;
            int reqId = Int32.Parse(BAVRequestId);
           
            string requestId = GenerateRequestId.RandomString(reqId);

            string xmlreq = encRequest;
            encRequest = ccaCrypto.Encrypt(encRequest, _workingKey);
            WebRequest wReq = WebRequest.Create(sUrl);
            HttpWebRequest httpReq = (HttpWebRequest)wReq;
            httpReq.Method = "POST";
            httpReq.ContentType = "application/x-www-form-urlencoded";
            string vData = "accessCode=" + accessCode + "&encRequest=" + encRequest + "&requestId=" + _requestID + "&ver=" + ver + "&instituteId=" + instituteId;

            //ONLY TLS 1.2
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Stream sendStream = httpReq.GetRequestStream();
            StreamWriter strmWrtr = new StreamWriter(sendStream);
            strmWrtr.Write(vData);
            strmWrtr.Close();
            WebResponse wResp = null;
            StreamReader strmRdr = null;
            string sResult = "";
            Console.WriteLine("CP1");

            try
            {
                /*Save Request*/
                _mTransRequest.BillerId = _billerid;
                _mTransRequest.CompanyId = _CompanyId;
                _mTransRequest.FK_memId = _Fk_MemID;
                _mTransRequest.IPAdress = _IPAdressAPP;
                _mTransRequest.AndroidId = _AndroidId;
                _mTransRequest.DeviceId = _DeviceId;
                _mTransRequest.TransReqEncType = encRequest;
                _mTransRequest.TransReqXML = xmlreq;
                _mTransRequest.Url = sUrl;
                _mTransRequest.AccessCode = accessCode;
                _mTransRequest.EnvId = "0";
                _mTransRequest.InstituteId = instituteId;
                _mTransRequest.WorkingKey = _workingKey;
                _mTransRequest.RequestId = requestId;
                _mTransRequest.ProcId = 1; // for Add Request
                _mTransRequest.APIName = _APIName;
                DALTransRequestAndResponse dALTransRequestAndResponse = new DALTransRequestAndResponse();
                DataSet dataSet = dALTransRequestAndResponse.SaveTransRequest(_mTransRequest);
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {
                        _mTransRequest.ProcId = 2;  // Save Response
                        wResp = httpReq.GetResponse();
                        Stream respStrm = wResp.GetResponseStream();
                        strmRdr = new StreamReader(respStrm);
                        sResult = strmRdr.ReadToEnd();
                        Console.WriteLine("Result is: " + sResult);
                        string _Data = ccaCrypto.Decrypt(sResult, _workingKey);
                        _mTransRequest.TransReqXML = _Data;
                        //API Response 
                        _response = DALXmlSerializers.DeserializeFromString<T>(_Data);
                        SetPropertyValue(_response, "RequestId", requestId);

                        //Saved Response Heres
                         dataSet = dALTransRequestAndResponse.SaveTransRequest(_mTransRequest);
                    }
                }
            

            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error : " + Ex.Message);
                sResult = "error: " + Ex.Message;
            }
            //Console.ReadLine();
            return _response;
        }

        public static T BAVsendRequestToUtilityAPIPostWithCompIdAndFkMemIDNew<T>(String encRequest, String _Url,
          string _billerid, long _CompanyId, long _Fk_MemID, string _AndroidId, string _IPAdressAPP, string _DeviceId, string _APIName,string requestId) where T : class

        {
            string instituteId = "";
            string accessCode = "";
            string ver = "";
            string _workingKey = "";
            string BAVRequestId = "";
            String sUrl = "";

            instituteId = BIllAvenueCredientials.BAVinstituteId;
            accessCode = BIllAvenueCredientials.BAVaccessCode;
            _workingKey = BIllAvenueCredientials.BAVworkingKey;
            ver = BIllAvenueCredientials.BAVver;
            BAVRequestId = BIllAvenueCredientials.BAVRequestId;
            sUrl = BIllAvenueCredientials.MBaseUrlAPIBill + _Url;

            int reqId = Int32.Parse(BAVRequestId);
            MTransRequest _mTransRequest = new MTransRequest();
            T _response = null;
            CCACrypto ccaCrypto = new CCACrypto();





            
            requestId = (requestId + GenerateRequestId.GetYDDDHHMM().ToString());
            Console.WriteLine("RequestId:" + requestId);
            string xmlreq = encRequest;
            encRequest = ccaCrypto.Encrypt(encRequest, _workingKey);
            WebRequest wReq = WebRequest.Create(sUrl);
            HttpWebRequest httpReq = (HttpWebRequest)wReq;
            httpReq.Method = "POST";
            httpReq.ContentType = "application/x-www-form-urlencoded";
            string vData = "accessCode=" + accessCode + "&requestId=" + requestId + "&encRequest=" + encRequest + "&ver=" + ver + "&instituteId=" + instituteId;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Stream sendStream = httpReq.GetRequestStream();
            StreamWriter strmWrtr = new StreamWriter(sendStream);
            strmWrtr.Write(vData);
            strmWrtr.Close();
            WebResponse wResp = null;
            StreamReader strmRdr = null;
            string sResult = "";
            Console.WriteLine("CP1");
            DALTransRequestAndResponse dALTransRequestAndResponse = new DALTransRequestAndResponse();
            try
            {
                /*Save Request*/
                _mTransRequest.BillerId = _billerid;
                _mTransRequest.CompanyId = _CompanyId;
                _mTransRequest.FK_memId = _Fk_MemID;
                _mTransRequest.IPAdress = _IPAdressAPP;
                _mTransRequest.AndroidId = _AndroidId;
                _mTransRequest.DeviceId = _DeviceId;
                _mTransRequest.TransReqEncType = encRequest;
                _mTransRequest.TransReqXML = xmlreq;
                _mTransRequest.Url = sUrl;
                _mTransRequest.AccessCode = accessCode;
                _mTransRequest.EnvId = "0";
                _mTransRequest.InstituteId = instituteId;
                _mTransRequest.WorkingKey = _workingKey;
                _mTransRequest.RequestId = requestId;
                _mTransRequest.ProcId = 1; // for Add Request
                _mTransRequest.APIName = _APIName;

                DataSet dataSet = dALTransRequestAndResponse.SaveTransRequest(_mTransRequest);
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {
                        _mTransRequest.ProcId = 2;  // Save Response
                        wResp = httpReq.GetResponse();
                        Stream respStrm = wResp.GetResponseStream();
                        strmRdr = new StreamReader(respStrm);
                        sResult = strmRdr.ReadToEnd();
                        Console.WriteLine("Result is: " + sResult);
                        string _Data = ccaCrypto.Decrypt(sResult, _workingKey);
                        _mTransRequest.TransReqXML = _Data;
                        //API Response 
                        _response = DALXmlSerializers.DeserializeFromString<T>(_Data);
                        SetPropertyValue(_response, "RequestId", requestId);

                        //Saved Response Heres
                        dALTransRequestAndResponse.SaveTransResponse(_mTransRequest);

                    }
                }


            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error : " + Ex.Message);
                sResult = "error: " + Ex.Message;
            }
            //Console.ReadLine();
            return _response;
        }


        public static T BAVsendRequestToUtilityAPIPostWithCompIdAndFkMemID<T>(String encRequest, String _Url,
         string _billerid, long _CompanyId, long _Fk_MemID, string _AndroidId, string _IPAdressAPP, string _DeviceId, string _APIName) where T : class

        {
            string instituteId = "";
            string accessCode = "";
            string ver = "";
            string _workingKey = "";
            string BAVRequestId = "";
            String sUrl = "";

            instituteId = BIllAvenueCredientials.BAVinstituteId;
            accessCode = BIllAvenueCredientials.BAVaccessCode;
            _workingKey = BIllAvenueCredientials.BAVworkingKey;
            ver = BIllAvenueCredientials.BAVver;
            BAVRequestId = BIllAvenueCredientials.BAVRequestId;
            sUrl = BIllAvenueCredientials.MBaseUrlAPIBill + _Url;

            int reqId = Int32.Parse(BAVRequestId);
            MTransRequest _mTransRequest = new MTransRequest();
            T _response = null;
            CCACrypto ccaCrypto = new CCACrypto();


            String requestId = GenerateRequestId.RandomString(reqId);



            requestId = (requestId + GenerateRequestId.GetYDDDHHMM().ToString());
            Console.WriteLine("RequestId:" + requestId);
            string xmlreq = encRequest;
            encRequest = ccaCrypto.Encrypt(encRequest, _workingKey);
            WebRequest wReq = WebRequest.Create(sUrl);
            HttpWebRequest httpReq = (HttpWebRequest)wReq;
            httpReq.Method = "POST";
            httpReq.ContentType = "application/x-www-form-urlencoded";
            string vData = "accessCode=" + accessCode + "&requestId=" + requestId + "&encRequest=" + encRequest + "&ver=" + ver + "&instituteId=" + instituteId;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Stream sendStream = httpReq.GetRequestStream();
            StreamWriter strmWrtr = new StreamWriter(sendStream);
            strmWrtr.Write(vData);
            strmWrtr.Close();
            WebResponse wResp = null;
            StreamReader strmRdr = null;
            string sResult = "";
            Console.WriteLine("CP1");
            DALTransRequestAndResponse dALTransRequestAndResponse = new DALTransRequestAndResponse();
            try
            {
                /*Save Request*/
                _mTransRequest.BillerId = _billerid;
                _mTransRequest.CompanyId = _CompanyId;
                _mTransRequest.FK_memId = _Fk_MemID;
                _mTransRequest.IPAdress = _IPAdressAPP;
                _mTransRequest.AndroidId = _AndroidId;
                _mTransRequest.DeviceId = _DeviceId;
                _mTransRequest.TransReqEncType = encRequest;
                _mTransRequest.TransReqXML = xmlreq;
                _mTransRequest.Url = sUrl;
                _mTransRequest.AccessCode = accessCode;
                _mTransRequest.EnvId = "0";
                _mTransRequest.InstituteId = instituteId;
                _mTransRequest.WorkingKey = _workingKey;
                _mTransRequest.RequestId = requestId;
                _mTransRequest.ProcId = 1; // for Add Request
                _mTransRequest.APIName = _APIName;

                DataSet dataSet = dALTransRequestAndResponse.SaveTransRequest(_mTransRequest);
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {
                        _mTransRequest.ProcId = 2;  // Save Response
                        wResp = httpReq.GetResponse();
                        Stream respStrm = wResp.GetResponseStream();
                        strmRdr = new StreamReader(respStrm);
                        sResult = strmRdr.ReadToEnd();
                        Console.WriteLine("Result is: " + sResult);
                        string _Data = ccaCrypto.Decrypt(sResult, _workingKey);
                        _mTransRequest.TransReqXML = _Data;
                        //API Response 
                        _response = DALXmlSerializers.DeserializeFromString<T>(_Data);
                        SetPropertyValue(_response, "RequestId", requestId);

                        //Saved Response Heres
                        dALTransRequestAndResponse.SaveTransResponse(_mTransRequest);

                    }
                }


            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error : " + Ex.Message);
                sResult = "error: " + Ex.Message;
            }
            //Console.ReadLine();
            return _response;
        }

        public static T BAVsendRequestToUtilityAPIPostWithCompIdAndFkMemID<T>(String encRequest, String _Url,
      string _billerid, long _CompanyId, long _Fk_MemID, string _AndroidId, string _IPAdressAPP, string _DeviceId, string _APIName, string _requestID) where T : class
        {
            string instituteId = "";
            string accessCode = "";
            string ver = "";
            string _workingKey = "";
            string BAVRequestId = "";
            String sUrl = "";

            instituteId = BIllAvenueCredientials.BAVinstituteId;
            accessCode = BIllAvenueCredientials.BAVaccessCode;
            _workingKey = BIllAvenueCredientials.BAVworkingKey;
            ver = BIllAvenueCredientials.BAVver;
            BAVRequestId = BIllAvenueCredientials.BAVRequestId;
            sUrl = BIllAvenueCredientials.MBaseUrlAPIBill + _Url;

            int reqId = Int32.Parse(BAVRequestId);
            MTransRequest _mTransRequest = new MTransRequest();
            T _response = null;
            CCACrypto ccaCrypto = new CCACrypto();

            String requestId = GenerateRequestId.RandomString(reqId);					//Unique ID of Agent Institution 
            if (_requestID != "" && _requestID != null)
            {
                requestId = _requestID;
            }
            else
            {
                requestId = GenerateRequestId.RandomString(reqId);					//Unique ID of Agent Institution 
            }

            string xmlreq = encRequest;
            encRequest = ccaCrypto.Encrypt(encRequest, _workingKey);
            WebRequest wReq = WebRequest.Create(sUrl);
            HttpWebRequest httpReq = (HttpWebRequest)wReq;
            httpReq.Method = "POST";
            httpReq.ContentType = "application/x-www-form-urlencoded";
            string vData = "accessCode=" + accessCode + "&encRequest=" + encRequest + "&requestId=" + requestId + "&ver=" + ver + "&instituteId=" + instituteId;

            //ONLY TLS 1.2
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Stream sendStream = httpReq.GetRequestStream();
            StreamWriter strmWrtr = new StreamWriter(sendStream);
            strmWrtr.Write(vData);
            strmWrtr.Close();
            WebResponse wResp = null;
            StreamReader strmRdr = null;

            string sResult = "";
            Console.WriteLine("CP1");
            try
            {
                /*Save Request*/
                _mTransRequest.BillerId = _billerid;
                _mTransRequest.CompanyId = _CompanyId;
                _mTransRequest.FK_memId = _Fk_MemID;
                _mTransRequest.IPAdress = _IPAdressAPP;
                _mTransRequest.AndroidId = _AndroidId;
                _mTransRequest.DeviceId = _DeviceId;
                _mTransRequest.TransReqEncType = encRequest;
                _mTransRequest.TransReqXML = xmlreq;
                _mTransRequest.Url = sUrl;
                _mTransRequest.AccessCode = accessCode;
                _mTransRequest.EnvId = "0";
                _mTransRequest.InstituteId = instituteId;
                _mTransRequest.WorkingKey = _workingKey;
                _mTransRequest.RequestId = requestId;

                _mTransRequest.ProcId = 1; // for Add Request
                _mTransRequest.APIName = _APIName;
                DALTransRequestAndResponse dALTransRequestAndResponse = new DALTransRequestAndResponse();
                DataSet dataSet = dALTransRequestAndResponse.SaveTransRequest(_mTransRequest);
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {
                        _mTransRequest.ProcId = 2;  // Save Response
                        wResp = httpReq.GetResponse();
                        Stream respStrm = wResp.GetResponseStream();
                        strmRdr = new StreamReader(respStrm);
                        sResult = strmRdr.ReadToEnd();
                        Console.WriteLine("Result is: " + sResult);
                        string _Data = ccaCrypto.Decrypt(sResult, _workingKey);
                        _mTransRequest.TransReqXML = _Data;
                        //API Response 
                        _response = DALXmlSerializers.DeserializeFromString<T>(_Data);
                        SetPropertyValue(_response, "RequestId", requestId);
                        //Saved Response Heres
                        dALTransRequestAndResponse.SaveTransResponse(_mTransRequest);
                    }
                }

            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error : " + Ex.Message);
                sResult = "error: " + Ex.Message;
            }
            //Console.ReadLine();
            return _response;
        }


        private static bool SetPropertyValue(object obj, string property, object value)
        {
            var prop = obj.GetType().GetProperty(property, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(obj, value, null);
                return true;
            }
            return false;
        }





    }
}