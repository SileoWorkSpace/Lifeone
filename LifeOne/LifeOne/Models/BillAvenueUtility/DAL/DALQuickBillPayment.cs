using Dapper;
using LifeOne.Models.BillAvenueUtility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.Common;


namespace LifeOne.Models.BillAvenueUtility.DAL
{
    public class DALQuickBillPayment
    {

        public static int SaveBillPayRequest(MBillerPayRequestResponse _objDetails, MBillerPayRequestAPPQuickPay _objrequestdetails)
        {
            string Bankinfo = "";
            foreach (var item in _objrequestdetails.info)
            {
                Bankinfo += item.infoName + ',' + item.infoValue + ' ';
            }
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@RequestId", _objrequestdetails.RequestId);
                queryParameters.Add("@BillerId", _objrequestdetails.BillerId);
                queryParameters.Add("@IPAdress", _objrequestdetails.IP);
                queryParameters.Add("@AndroidId", _objrequestdetails.AndroidId);
                queryParameters.Add("@DeviceId", _objrequestdetails.DeviceId);
                queryParameters.Add("@CompanyId", "1");
                queryParameters.Add("@Fk_MemId", _objrequestdetails.Fk_MemID);
                //queryParameters.Add("@LatePaymentFee", _objrequestdetails.LatePaymentFee);
                //queryParameters.Add("@FixedCharges", _objrequestdetails.FixedCharges);
                //queryParameters.Add("@AdditionalCharges", _objrequestdetails.AdditionalCharges);
                queryParameters.Add("@BillerAdhoc", _objrequestdetails.BillerAdhoc);
                queryParameters.Add("@PaymentMode", _objrequestdetails.PaymentMode);
                queryParameters.Add("@QuickPay", _objrequestdetails.QuickPay);
                queryParameters.Add("@SplitPay", _objrequestdetails.SplitPay);
                queryParameters.Add("@CompanyId", "1");
                queryParameters.Add("@Fk_MemId", _objrequestdetails.Fk_MemID);
                queryParameters.Add("@CreatedBy", _objrequestdetails.Fk_MemID);
                queryParameters.Add("@ProcId", 1);
                queryParameters.Add("@ResponseAmount", _objDetails.RespAmount ?? string.Empty);
                queryParameters.Add("@RespBillDate", _objDetails.RespBillDate ?? string.Empty);
                queryParameters.Add("@ResPBillPeriod", _objDetails.RespBillPeriod ?? string.Empty);
                queryParameters.Add("@RespBillNumber", _objDetails.RespBillNumber ?? string.Empty);
                queryParameters.Add("@RespCustomerName", _objDetails.RespCustomerName ?? string.Empty);
                queryParameters.Add("@RespDueDate", _objDetails.RespDueDate ?? string.Empty);
                queryParameters.Add("@BankInfo", Bankinfo ?? string.Empty);
                int _iresult = DBHelperDapper.DAAdd("BASaveAllTransactionDetails", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int SaveBillPayResponseError(MBillerPayRequestResponse _objDetails, MBillerPayRequestAPPQuickPay _objrequestdetails)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@RequestId", _objrequestdetails.RequestId);
                queryParameters.Add("@ProcId", 2);
                queryParameters.Add("@BillerId", _objrequestdetails.BillerId);
                queryParameters.Add("@IPAdress", _objrequestdetails.IP);
                queryParameters.Add("@AndroidId", _objrequestdetails.AndroidId);
                queryParameters.Add("@DeviceId", _objrequestdetails.DeviceId);
                queryParameters.Add("@CompanyId", "1");
                queryParameters.Add("@Fk_MemId", _objrequestdetails.Fk_MemID);
                //queryParameters.Add("@LatePaymentFee", _objrequestdetails.LatePaymentFee);
                //queryParameters.Add("@FixedCharges", _objrequestdetails.FixedCharges);
                //queryParameters.Add("@AdditionalCharges", _objrequestdetails.AdditionalCharges);
                queryParameters.Add("@BillerAdhoc", _objrequestdetails.BillerAdhoc);
                queryParameters.Add("@PaymentMode", _objrequestdetails.PaymentMode);
                queryParameters.Add("@QuickPay", _objrequestdetails.QuickPay);
                queryParameters.Add("@SplitPay", _objrequestdetails.SplitPay);
                queryParameters.Add("@CompanyId", "1");
                queryParameters.Add("@SplitPay", _objrequestdetails.Fk_MemID);
                queryParameters.Add("@ErrorMsg", _objDetails.errorInfo.error.errorCode + ' ' + _objDetails.errorInfo.error.errorMessage);
                int _iresult = DBHelperDapper.DAAdd("BASaveAllTransactionDetails", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}