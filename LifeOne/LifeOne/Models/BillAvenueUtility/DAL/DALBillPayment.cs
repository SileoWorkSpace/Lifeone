
using LifeOne.Models.BillAvenueUtility.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.Common;
using System.Data;
using System.Data.SqlClient;

namespace LifeOne.Models.BillAvenueUtility.DAL
{
    public class DALBillPayment
    {

        public DataSet SaveBillPayRequest(MBillerPayRequestResponse _objDetails, MBillerPayRequestAPP _objrequestdetails)
        {
            string Bankinfo = "";
            foreach (var item in _objrequestdetails.info)
            {
                Bankinfo += item.infoName + ',' + item.infoValue + ' ';
            }

            SqlParameter[] para = {
                                        new SqlParameter("@RequestId", _objrequestdetails.RequestId),
                                        new SqlParameter("@BillerId", _objrequestdetails.BillerId),
                                        new SqlParameter("@IPAdress", _objrequestdetails.IP),
                                        new SqlParameter("@AndroidId", _objrequestdetails.AndroidId),
                                        new SqlParameter("@DeviceId", _objrequestdetails.DeviceId),
                                        new SqlParameter("@CompanyId", _objrequestdetails.ProjectId),
                                        new SqlParameter("@Fk_MemId", _objrequestdetails.Fk_MemID),
                                        new SqlParameter("@LatePaymentFee", _objrequestdetails.LatePaymentFee),
                                        new SqlParameter("@FixedCharges", _objrequestdetails.FixedCharges),
                                        new SqlParameter("@AdditionalCharges", _objrequestdetails.AdditionalCharges),
                                        new SqlParameter("@BillerAdhoc", _objrequestdetails.BillerAdhoc),
                                        new SqlParameter("@PaymentMode", _objrequestdetails.PaymentMode),
                                        new SqlParameter("@QuickPay", _objrequestdetails.QuickPay),
                                        new SqlParameter("@SplitPay", _objrequestdetails.SplitPay),
                                        new SqlParameter("@CompanyId", _objrequestdetails.ProjectId),
                                        new SqlParameter("@Fk_MemId", _objrequestdetails.Fk_MemID),
                                        new SqlParameter("@CreatedBy", _objrequestdetails.Fk_MemID),
                                        new SqlParameter("@CustomerMobile", _objrequestdetails.CustomerMobile),
                                        new SqlParameter("@ProcId", 3),
                                        new SqlParameter("@ResponseAmount", _objDetails.RespAmount ?? string.Empty),
                                        new SqlParameter("@RespBillDate", _objDetails.RespBillDate ?? string.Empty),
                                        new SqlParameter("@ResPBillPeriod", _objDetails.RespBillPeriod ?? string.Empty),
                                        new SqlParameter("@RespBillNumber", _objDetails.RespBillNumber ?? string.Empty),
                                        new SqlParameter("@RespCustomerName", _objDetails.RespCustomerName ?? string.Empty),
                                        new SqlParameter("@RespDueDate", _objDetails.RespDueDate ?? string.Empty),
                                        new SqlParameter("@BankInfo", Bankinfo ?? string.Empty)
                    };
            DataSet ds = DBHelper.ExecuteQuery(ProcedureName.BASaveAllTransactionDetails, para);
            return ds;


        }

        public DataSet SaveBillPayResponseError(MBillerPayRequestResponse _objDetails, MBillerPayRequestAPP _objrequestdetails)
        {
            string Bankinfo = "";
            SqlParameter[] para = {
                                        new SqlParameter("@RequestId", _objrequestdetails.RequestId),
                                        new SqlParameter("@BillerId", _objrequestdetails.BillerId),
                                        new SqlParameter("@IPAdress", _objrequestdetails.IP),
                                        new SqlParameter("@AndroidId", _objrequestdetails.AndroidId),
                                        new SqlParameter("@DeviceId", _objrequestdetails.DeviceId),
                                        new SqlParameter("@CompanyId", _objrequestdetails.ProjectId),
                                        new SqlParameter("@Fk_MemId", _objrequestdetails.Fk_MemID),
                                        new SqlParameter("@LatePaymentFee", _objrequestdetails.LatePaymentFee),
                                        new SqlParameter("@FixedCharges", _objrequestdetails.FixedCharges),
                                        new SqlParameter("@AdditionalCharges", _objrequestdetails.AdditionalCharges),
                                        new SqlParameter("@BillerAdhoc", _objrequestdetails.BillerAdhoc),
                                        new SqlParameter("@PaymentMode", _objrequestdetails.PaymentMode),
                                        new SqlParameter("@QuickPay", _objrequestdetails.QuickPay),
                                        new SqlParameter("@SplitPay", _objrequestdetails.SplitPay),
                                        new SqlParameter("@CompanyId", _objrequestdetails.ProjectId),
                                        new SqlParameter("@Fk_MemId", _objrequestdetails.Fk_MemID),
                                        new SqlParameter("@CreatedBy", _objrequestdetails.Fk_MemID),
                                        new SqlParameter("@CustomerMobile", _objrequestdetails.CustomerMobile),
                                        new SqlParameter("@ProcId", 2),
                                        new SqlParameter("@ResponseAmount", _objDetails.RespAmount ?? string.Empty),
                                        new SqlParameter("@RespBillDate", _objDetails.RespBillDate ?? string.Empty),
                                        new SqlParameter("@ResPBillPeriod", _objDetails.RespBillPeriod ?? string.Empty),
                                        new SqlParameter("@RespBillNumber", _objDetails.RespBillNumber ?? string.Empty),
                                        new SqlParameter("@RespCustomerName", _objDetails.RespCustomerName ?? string.Empty),
                                        new SqlParameter("@RespDueDate", _objDetails.RespDueDate ?? string.Empty),
                                        new SqlParameter("@BankInfo", Bankinfo ?? string.Empty)
                    };
            DataSet ds = DBHelper.ExecuteQuery(ProcedureName.BASaveAllTransactionDetails, para);
            return ds;
        }
    }
}