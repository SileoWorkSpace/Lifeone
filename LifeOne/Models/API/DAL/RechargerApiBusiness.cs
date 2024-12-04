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