using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALAdminUtility
    {
        public static List<MAdminUntility> GetData(MAdminUntility obj)
        {
            try
            {
                //var queryParameters = new DynamicParameters();

                //queryParameters.Add("@Pk_UtilityId", obj.Pk_UtilityId);
                //queryParameters.Add("@UtilityName", obj.UtilityName);
                //queryParameters.Add("@CreatedBy", obj.CreatedBy);
                //queryParameters.Add("@ProcId", obj.ProcId);
                //queryParameters.Add("@DueDate", _objDetails.billerResponse.dueDate);

                string _qury = "Utility @ProcId=" + obj.ProcId;
                List<MAdminUntility> _iresult = DBHelperDapper.DAGetDetailsInList<MAdminUntility>(_qury);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static ResponseUtilityModel SaveUtility(MAdminUntility obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@Pk_UtilityId", obj.Pk_UtilityId);
                queryParameters.Add("@UtilityName", obj.UtilityName);
                queryParameters.Add("@CreatedBy", obj.CreatedBy);
                queryParameters.Add("@ProcId", obj.ProcId);
                //queryParameters.Add("@DueDate", _objDetails.billerResponse.dueDate);
                // string _qury = "Utility @ProcId=" + obj.ProcId;
                //string _qury = "Utility @ProcId=" + obj.ProcId + ",@Pk_UtilityId=" + obj.Pk_UtilityId + ", @UtilityName = " + obj.UtilityName + ", @CreatedBy = " + obj.CreatedBy + "";
                ResponseUtilityModel _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseUtilityModel>("Utility", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static DynamicUtilityResponseModel BindAllUtilityMaster()
        {
            DynamicUtilityResponseModel _response = new DynamicUtilityResponseModel();
            try
            {
                var queryParameters = new DynamicParameters();
                _response.Data = DBHelperDapper.DAAddAndReturnModelList<DynamicUtilityModel>("ProBindAllUtilityMaster", queryParameters);
                if (_response.Data != null && _response.Data.Count() > 0)
                {
                    _response.Status = true;
                    _response.Message = "success";
                }
                else
                {
                    _response.Status = false;
                    _response.Message = "Data not found!!!";
                }
                return _response;
            }
            catch (Exception ex)
            {
                _response.Status = false;
                _response.Message = "#Error";
                return _response;
            }
        }
        public static DynamicUtilityResponseModel ChangeUtilityMasterStatus(DynamicUtilityModel req)
        {
            DynamicUtilityResponseModel _response = new DynamicUtilityResponseModel();
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@UtilityId", req.Id);
                queryParameters.Add("@Status", req.Status);
                queryParameters.Add("@EntryUser", SessionManager.Fk_MemId);
                queryParameters.Add("@Remark", "");
                _response = DBHelperDapper.DAAddAndReturnModel<DynamicUtilityResponseModel>("ProChangeUtilityStatus", queryParameters);
                return _response;
            }
            catch (Exception ex)
            {
                _response.Status = false;
                _response.Message = "#Error";
                return _response;
            }
        }
        public static DynamicUtilityResponseModel ChangeCMDStartDateTime(DynamicUtilityModel req)
        {
            DynamicUtilityResponseModel _response = new DynamicUtilityResponseModel();
            try
            {
                DateTime CMDate = DateTime.Parse(req.CampaignStartDate);
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@UtilityId", req.Id);
                queryParameters.Add("@CMDStartDateTime", CMDate);
                queryParameters.Add("@EntryUser", SessionManager.Fk_MemId);
                queryParameters.Add("@Remark", "");
                _response = DBHelperDapper.DAAddAndReturnModel<DynamicUtilityResponseModel>("ProChangeCMDStartDateTime", queryParameters);
                return _response;
            }
            catch (Exception ex)
            {
                _response.Status = false;
                _response.Message = "#Error";
                return _response;
            }
        }
        public static List<Member_RefundAmountModel> RefundAmount(Member_RefundAmountModel _response)
        {

            var queryParameters = new DynamicParameters();
            queryParameters.Add("@LoginId", _response.LoginId ?? string.Empty);
            queryParameters.Add("@Name", _response.MemberName ?? string.Empty);
            queryParameters.Add("@Mobile", _response.Mobile ?? string.Empty);
            //queryParameters.Add("@Transactionid", _response.TransNo ?? string.Empty);
            queryParameters.Add("@FromDate", string.IsNullOrEmpty(_response.FromDate) ? null : Common.DALCommon.ConvertToSystemDate(_response.FromDate, "MM-dd-yyyy"));
            queryParameters.Add("@ToDate", string.IsNullOrEmpty(_response.ToDate) ? null : Common.DALCommon.ConvertToSystemDate(_response.ToDate, "MM-dd-yyyy"));
            queryParameters.Add("@page", _response.Page);
            queryParameters.Add("@size", _response.Size);
            queryParameters.Add("@IsExport", _response.IsExport);
            _response.RefundAmountList = DBHelperDapper.DAAddAndReturnModelList<Member_RefundAmountModel>("Proc_RefundAmount", queryParameters);
            return _response.RefundAmountList;
        }
        public static Member_RefundAmountModel AddRefundAmount(Member_RefundAmountModel obj)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@Pk_UniqueId", obj.Pk_UniqueId);
            queryParameters.Add("@RefundId", obj.RefundId);
            queryParameters.Add("@PaymentId",obj.PaymentId);
            queryParameters.Add("@Amount", obj.DebitAmount);
            queryParameters.Add("@MemId", obj.FK_MemId);
            queryParameters.Add("@CreatedBy", SessionManager.Fk_MemId);

            obj = DBHelperDapper.DAAddAndReturnModel<Member_RefundAmountModel>("Proc_WalletRefund", queryParameters);
            return obj;
        }

    }
}