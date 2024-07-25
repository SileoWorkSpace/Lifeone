using Dapper;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.Common;
using LifeOne.Models.QUtility.Entity;
using LifeOne.Models.QUtility.Service;
using LifeOne.Models.Manager;

using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace LifeOne.Models.API.DAL
{
    public class RechargerApiBusiness
    {
        Uri baseAddress = new Uri("http://api.payadda.in");
        const string userName = @"9763577799";
        const string password = @"19564434";
        HttpClient client;
        string Credentials = new AuthenticationHeaderValue(AuthenticationSchemes.Basic.ToString(), Convert.ToBase64String(Encoding.ASCII.GetBytes($"{userName}:{password}"))).ToString();
        public RechargerApiBusiness()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public WalletBalanceModel GetBalanceAsync()
        {
            WalletBalanceModel _response = new WalletBalanceModel();
            try
            {
                var client = new RestClient("http://api.payadda.in/wallet/balance");
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", Credentials);
                IRestResponse response = client.Execute(request);
                var result = response.Content;
                if (response.IsSuccessful)
                    return JsonConvert.DeserializeObject<WalletBalanceModel>(response.Content);
                else
                    return new WalletBalanceModel { ResponseCode = 1, StatusMessage = "Failed" };
            }
            catch
            {
                return new WalletBalanceModel { ResponseCode = -1, StatusMessage = "System encountered error #0001" };
            }
        }
        public RechargeTypeResponseModel BindRechargeType()
        {
            try
            {
                RechargeTypeResponseModel _response = new RechargeTypeResponseModel();
                var QuaryParameter = new DynamicParameters();
                _response.Data = AdminManagement.ADAL.DBHelperDapper.DAAddAndReturnModelList<RechargeTypeModel>("ProGetRechargeType", QuaryParameter);
                if (_response.Data != null && _response.Data.Count() > 0)
                {
                    _response.Flag = 1;
                    _response.Status = true;
                    _response.response = "successs";
                    _response.message = "successs";
                    return _response;
                }
                else
                {
                    _response.Flag = 0;
                    _response.Status = false;
                    _response.response = "successs";
                    _response.message = "successs";
                    return _response;
                }
            }
            catch (Exception e)
            {
                return new RechargeTypeResponseModel { Flag = 0, Status = false, response = "failed", message = e.Message };
            }
        }

        public RechargeOperatorResponseModel BindOperatorList(int TypeId)
        {
            try
            {
                RechargeOperatorResponseModel _response = new RechargeOperatorResponseModel();
                var QuaryParameter = new DynamicParameters();
                QuaryParameter.Add("@RechargeTypeId", TypeId);
                _response.Data = AdminManagement.ADAL.DBHelperDapper.DAAddAndReturnModelList<RechargeOperatorModel>("ProBindOperatorList", QuaryParameter);
                if (_response.Data != null && _response.Data.Count() > 0)
                {
                    _response.Flag = 1;
                    _response.Status = true;
                    _response.response = "successs";
                    _response.message = "successs";
                    return _response;
                }
                else
                {
                    _response.Flag = 0;
                    _response.Status = false;
                    _response.response = "successs";
                    _response.message = "successs";
                    return _response;
                }
            }
            catch (Exception e)
            {
                return new RechargeOperatorResponseModel { Flag = 0, Status = false, response = "failed", message = e.Message };
            }
        }

        public ResponseModel RechargeV2(RechargeModel req, RequestModel _obj, string _ActionType, string _RechargeType)
        {
            RechargeResponseModel _response = new RechargeResponseModel();
            try
            {
                DALCommon _objDALCommon = new DALCommon();
                double Balance = GetBalanceAsync().Wallets.Select(x => x.WalletBalance).FirstOrDefault();
                if (Balance >= req.TransactionAmount)
                {
                    WalletTransactionResponse objResTrans = _objDALCommon.DALSaveTransactionDetails(_obj, "Debit", _ActionType, _RechargeType);
                    req.AccessToken = "hjW68ytzuysBzNRnpWVtbGv";
                    req.TransactionPin = "86536980";
                    req.CustomerId = 11956;
                    req.CompanyId = 11;
                    req.ClientRequestId = DateTime.UtcNow.AddMinutes(330).ToString("yyyyMMddHHmmss"); //"INC001";
                    string requestdata = JsonConvert.SerializeObject(req);
                    var client = new RestClient("http://api.payadda.in/recharge/process");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Authorization", Credentials);
                    request.AddJsonBody(requestdata);
                    IRestResponse response = client.Execute(request);
                    var result = response.Content;
                    if (response.IsSuccessful)
                    {
                        _response = JsonConvert.DeserializeObject<RechargeResponseModel>(response.Content);
                        if (_response.StatusCode == 0 || _response.StatusCode == 2) //2 for under process
                       //if (_response.StatusCode == 0) //2 for under process
                        {
                            var QuaryParameter = new DynamicParameters();
                            QuaryParameter.Add("@UserId", req.UserId);
                            QuaryParameter.Add("@LoginId", req.LoginId);
                            QuaryParameter.Add("@RechargeTypeId", req.RechargeType);
                            QuaryParameter.Add("@OperatorId", req.OperatorId);
                            QuaryParameter.Add("@StatusCode", _response.StatusCode);
                            QuaryParameter.Add("@VendorStatusCode", _response.VendorStatusCode);
                            QuaryParameter.Add("@CustomerNumber", _response.CustomerNumber);
                            QuaryParameter.Add("@TransactionAmount", _response.TransactionAmount);
                            QuaryParameter.Add("@RunningBalance", _response.RunningBalance);
                            QuaryParameter.Add("@StatusMessage", _response.StatusMessage);
                            QuaryParameter.Add("@TransactionId", _response.TransactionId);
                            QuaryParameter.Add("@VendorTransactionId", _response.VendorTransactionId);
                            QuaryParameter.Add("@OperatorTransactionId", _response.OperatorTransactionId);
                            QuaryParameter.Add("@VendorStatusMessage", _response.VendorStatusMessage);
                            QuaryParameter.Add("@AdditionalOutputs", _response.AdditionalOutputs ?? string.Empty);
                            QuaryParameter.Add("@Status", "Success");
                            QuaryParameter.Add("@ClientRequestId", req.ClientRequestId);
                            QuaryParameter.Add("@IsSuccess", true);
                            QuaryParameter.Add("@Fk_WalletDetailsId", objResTrans.WalletDetailsId);
                            
                            _RechargeApiResponseModel _objresponse = AdminManagement.ADAL.DBHelperDapper.DAAddAndReturnModel<_RechargeApiResponseModel>("ProRechargeV2", QuaryParameter);                            
                            string _customTRNXSTATUS = "7";

                            if (_response.StatusCode == 2) //pending                            
                                _customTRNXSTATUS = "3";

                            //sometime OperatorTransactionId comes "NA" in that case we are passing transactionid..                            
                            if (_response.OperatorTransactionId.ToString()== "NA" || _response.OperatorTransactionId.ToString() == "")                            
                                _response.OperatorTransactionId= DateTime.UtcNow.AddMinutes(330).ToString("yyyyMMddHHmmss");
                            
                            if (_ActionType == "Prepaid Recharge")
                            {
                                    List<PrepaidResponse> _ApiResponseList = new List<PrepaidResponse> { new PrepaidResponse {
                                    DATE = DateTime.UtcNow.AddMinutes(330).ToString("dd.MM.yyyy HH:mm:ss"),
                                    SESSION ="-",
                                    ERROR = "0",    
                                    RESULT = "0",
                                    TRANSID =_response.OperatorTransactionId.ToString(),
                                    TRNXSTATUS = _customTRNXSTATUS,
                                    AUTHCODE = string.Empty,ERRMSG = string.Empty,Status = "Success",
                                    Message = "Recharge has been successful!",
                                    CompanyId = req.CompanyId
                                }
                              };
                                new QUtility.DAL.DALPrepaid().PrepaidRecharge(_obj, _ApiResponseList, objResTrans);
                                return new ResponseModel { body = DALCommon.CustomSerializeObjectWithEncryptString(_ApiResponseList) };
                            }
                            else if (_ActionType == "DTH Recharge")
                            {
                                List<MDTHReponse> _DTHResponse = new List<MDTHReponse> { new MDTHReponse { 
                                DATE = DateTime.UtcNow.AddMinutes(330).ToString("dd.MM.yyyy HH:mm:ss"),
                                SESSION = string.Empty, ERROR = "0", RESULT = "0",
                                TRANSID =  _response.OperatorTransactionId.ToString(), // objResTrans.TransactionId,
                                TRNXSTATUS =_customTRNXSTATUS,// 7 - change bcoz in case of pending recarege will have to pass 3 
                                AUTHCODE = string.Empty,
                                ERRMSG = string.Empty,
                                Status = "Success", Message = "Recharge has been successful!", CompanyId = req.CompanyId } };
                                new QUtility.DAL.DALDTH().DTHRecharge(_obj, _DTHResponse, objResTrans);
                                return new ResponseModel { body = DALCommon.CustomSerializeObjectWithEncryptString(_DTHResponse) };
                            }
                            else
                            {
                                return new ResponseModel { body = DALCommon.CustomSerializeObjectWithEncryptString(new List<PrepaidResponse> { new PrepaidResponse { Message = "Invalid request", Status = "failed", ERROR = "1", ERRMSG = "Invalid request", RESULT = "1" } }) };
                            }
                        }
                        else
                        {
                            _objDALCommon.DALSaveTransactionDetails(_obj, "Credit", _ActionType, _RechargeType);
                            return new ResponseModel { body = DALCommon.CustomSerializeObjectWithEncryptString(new List<PrepaidResponse> { new PrepaidResponse { Message = _response.StatusMessage, Status = "failed", RESULT = "1", ERROR = "1", ERRMSG = _response.StatusMessage } }) };
                        }
                    }
                    else
                    {
                        _objDALCommon.DALSaveTransactionDetails(_obj, "Credit", _ActionType, _RechargeType);
                        return new ResponseModel { body = DALCommon.CustomSerializeObjectWithEncryptString(new List<PrepaidResponse> { new PrepaidResponse { Message = "Invalid request", Status = "failed", ERROR = "1", ERRMSG = "Invalid request", RESULT = "1" } }) };
                    }
                }
                else
                {
                    return new ResponseModel { body = DALCommon.CustomSerializeObjectWithEncryptString(new List<PrepaidResponse> { new PrepaidResponse { Message = "Low balance", Status = "failed", ERRMSG = "Low balance", ERROR = "1", RESULT = "1" } }) };
                }
            }
            catch (Exception e)
            {
                return new ResponseModel { body = DALCommon.CustomSerializeObjectWithEncryptString(new List<PrepaidResponse> { new PrepaidResponse { Message = "System encountered error #0001", Status = "failed", ERRMSG = "System encountered error #0001", ERROR = "1", RESULT = "1" } }) };
            }
        }

        public ApiResponeModel Recharge(RechargeModel req, RequestModel _obj)
        {
            RechargeResponseModel _response = new RechargeResponseModel();
            try
            {
                DALCommon _objDALCommon = new DALCommon();
                double Balance = GetBalanceAsync().Wallets.Select(x => x.WalletBalance).FirstOrDefault();
                if (Balance >= req.TransactionAmount)
                {

                    WalletTransactionResponse objResTrans = _objDALCommon.DALSaveTransactionDetails(_obj, "Debit", "Prepaid Recharge", "PrepaidRecharge");
                    req.AccessToken = "hjW68ytzuysBzNRnpWVtbGv";
                    req.TransactionPin = "86536980";
                    req.CustomerId = 11956;
                    req.CompanyId = 11;
                    req.ClientRequestId = "INC001";
                    string requestdata = JsonConvert.SerializeObject(req);
                    var client = new RestClient("http://api.payadda.in/recharge/process");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Authorization", Credentials);
                    request.AddJsonBody(requestdata);
                    IRestResponse response = client.Execute(request);
                    var result = response.Content;
                    if (response.IsSuccessful)
                    {
                        _response = JsonConvert.DeserializeObject<RechargeResponseModel>(response.Content);
                        if (_response.StatusCode == 0)
                        {
                            List<PrepaidResponse> _ApiResponseList = new List<PrepaidResponse>();
                            PrepaidResponse _ApiResponse = new PrepaidResponse();
                            _ApiResponse.DATE = DateTime.Now.ToString("yyyyMMddHHmmss");
                            _ApiResponse.SESSION = string.Empty;
                            _ApiResponse.ERROR = "Success";
                            _ApiResponse.RESULT = "Success";
                            _ApiResponse.TRANSID = _response.OperatorTransactionId.ToString();
                            _ApiResponse.TRNXSTATUS = "Success";
                            _ApiResponse.AUTHCODE = string.Empty;
                            _ApiResponse.ERRMSG = string.Empty;
                            _ApiResponse.Status = "Success";
                            _ApiResponse.Message = "Recharge has been successful!";
                            _ApiResponse.CompanyId = req.CompanyId;
                            QUtility.DAL.DALPrepaid _objDalPrepaid = new QUtility.DAL.DALPrepaid();
                            _ApiResponseList.Add(_ApiResponse);
                            _objDalPrepaid.PrepaidRecharge(_obj, _ApiResponseList, objResTrans);


                            var QuaryParameter = new DynamicParameters();
                            QuaryParameter.Add("@UserId", req.UserId);
                            QuaryParameter.Add("@LoginId", req.LoginId);
                            QuaryParameter.Add("@RechargeTypeId", req.RechargeType);
                            QuaryParameter.Add("@OperatorId", req.OperatorId);
                            QuaryParameter.Add("@StatusCode", _response.StatusCode);
                            QuaryParameter.Add("@VendorStatusCode", _response.VendorStatusCode);
                            QuaryParameter.Add("@CustomerNumber", _response.CustomerNumber);
                            QuaryParameter.Add("@TransactionAmount", _response.TransactionAmount);
                            QuaryParameter.Add("@RunningBalance", _response.RunningBalance);
                            QuaryParameter.Add("@StatusMessage", _response.StatusMessage);
                            QuaryParameter.Add("@TransactionId", _response.TransactionId);
                            QuaryParameter.Add("@VendorTransactionId", _response.VendorTransactionId);
                            QuaryParameter.Add("@OperatorTransactionId", _response.OperatorTransactionId);
                            QuaryParameter.Add("@VendorStatusMessage", _response.VendorStatusMessage);
                            QuaryParameter.Add("@AdditionalOutputs", _response.AdditionalOutputs);
                            QuaryParameter.Add("@Status", "Success");
                            QuaryParameter.Add("@IsSuccess", true);
                            _RechargeApiResponseModel _objresponse = AdminManagement.ADAL.DBHelperDapper.DAAddAndReturnModel<_RechargeApiResponseModel>("ProRecharge", QuaryParameter);
                            return new ApiResponeModel { Status = true, response = "success", message = _response.StatusMessage };
                        }
                        else
                        {
                            _objDALCommon.DALSaveTransactionDetails(_obj, "Credit", "Prepaid Recharge", "PrepaidRecharge");
                            return new ApiResponeModel { Status = false, response = "failed", message = _response.StatusMessage };
                        }
                    }
                    else
                    {
                        _objDALCommon.DALSaveTransactionDetails(_obj, "Credit", "Prepaid Recharge", "PrepaidRecharge");
                        return new ApiResponeModel { Status = false, response = "failed", message = "Invalid request" };
                    }
                }
                else
                {
                    return new ApiResponeModel { Status = false, response = "failed", message = "Low balance" };
                }
            }
            catch (Exception e)
            {
                return new ApiResponeModel { Status = false, response = "failed", message = "System encountered error #0001" };
            }
        }

        public ApiRechargeReportModel GetRechargeReport(RechargeReportFilterModel req)
        {
            try
            {
                DateTime? FromDate = string.IsNullOrEmpty(req.FromDate) ? (DateTime?)null : Convert.ToDateTime(req.FromDate);
                DateTime? ToDate = string.IsNullOrEmpty(req.ToDate) ? (DateTime?)null : Convert.ToDateTime(req.ToDate);
                ApiRechargeReportModel _response = new ApiRechargeReportModel();
                var QuaryParameter = new DynamicParameters();
                QuaryParameter.Add("@FromDate", FromDate);
                QuaryParameter.Add("@ToDate", ToDate);
                QuaryParameter.Add("@LoginId", req.LoginId);
                QuaryParameter.Add("@FK_MemId", req.FK_MemId);
                QuaryParameter.Add("@TransactionId", req.TransactionId);
                QuaryParameter.Add("@Status", req.Status);
                QuaryParameter.Add("@Page", req.Page);
                QuaryParameter.Add("@Size", SessionManager.Size);
                List<RechargeReportModel> Data = AdminManagement.ADAL.DBHelperDapper.DAAddAndReturnModelList<RechargeReportModel>("ProGetRechargeHistory", QuaryParameter);
                if (Data != null && Data.Count() > 0)
                {
                    _response.Flag = 1;
                    _response.Status = true;
                    _response.response = "success";
                    _response.Message = "success";
                    _response.Data = Data;
                }
                else
                {
                    _response.Flag = 0;
                    _response.Status = false;
                    _response.response = "failed";
                    _response.Message = "Data not found!!!";
                }
                return _response;
            }
            catch (Exception e)
            {
                return new ApiRechargeReportModel { Flag = 0, Status = false, response = "failed", Message = e.Message };
            }
        }

    }
}