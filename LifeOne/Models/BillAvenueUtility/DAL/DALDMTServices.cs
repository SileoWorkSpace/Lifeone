using Dapper;
using LifeOne.Models.BillAvenueUtility.Entity.DMTServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.Common;


namespace LifeOne.Models.BillAvenueUtility.DAL
{
    public class DALDMTServices
    {
        public static int SaveDMTTransServiceRequest(DmtTransactionResponse _objDetails, MDmtTransactionRequest _objrequestdetails)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@AgentId", _objrequestdetails.AgentId);
                queryParameters.Add("@InitChannel", _objrequestdetails.InitChannel);
                queryParameters.Add("@TxnId", _objrequestdetails.TxnId);
                queryParameters.Add("@RespDesc", _objDetails.RespDesc);
                queryParameters.Add("@ResponseCode", _objDetails.ResponseCode);
                queryParameters.Add("@ResponseReason", _objDetails.ResponseReason);
                queryParameters.Add("@CompanyId", _objrequestdetails.CompanyId);
                queryParameters.Add("@Fk_MemId", _objrequestdetails.Fk_MemID);
                queryParameters.Add("@ProcId", 1);
                int _iresult = DBHelperDapper.DAAdd("SaveBATransReqDetail", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int SaveDMTServiceResponse(DmtTransactionResponse _objDetails, MDmtTransactionRequest _objrequestdetails)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@ProcId", 2);
                queryParameters.Add("@AgentId", _objrequestdetails.AgentId);
                queryParameters.Add("@InitChannel", _objrequestdetails.InitChannel);
                queryParameters.Add("@TxnId", _objrequestdetails.TxnId);
                queryParameters.Add("@ResponseCode", _objDetails.ResponseCode);
                queryParameters.Add("@ResponseReason", _objDetails.ResponseReason);
                queryParameters.Add("@CompanyId", _objrequestdetails.CompanyId);
                queryParameters.Add("@Fk_MemId", _objrequestdetails.Fk_MemID);
                queryParameters.Add("@ErrorMsg", _objDetails.errorInfo.error.errorCode+' '+ _objDetails.errorInfo.error.errorMessage);
                int _iresult = DBHelperDapper.DAAdd("SaveBATransReqDetail", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Transaction Refund OTP

        public static int SaveDMTTransRefundOTPRequest(DmtTransactionResponse _objDetails, MDmtTransactionRequest _objrequestdetails)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@AgentId", _objrequestdetails.AgentId);
                queryParameters.Add("@RequestType", _objrequestdetails.RequestType);
                queryParameters.Add("@UniqueRefId", _objrequestdetails.UniqueRefId);
                queryParameters.Add("@Otp", _objrequestdetails.Otp);
                queryParameters.Add("@InitChannel", _objrequestdetails.InitChannel);
                queryParameters.Add("@TxnId", _objrequestdetails.TxnId);
                queryParameters.Add("@RespDesc", _objDetails.RespDesc);
                queryParameters.Add("@ResponseCode", _objDetails.ResponseCode);
                queryParameters.Add("@ResponseReason", _objDetails.ResponseReason);
                queryParameters.Add("@CompanyId", _objrequestdetails.CompanyId);
                queryParameters.Add("@FK_MemId", _objrequestdetails.Fk_MemID);
                queryParameters.Add("@ProcId", 1);
                int _iresult = DBHelperDapper.DAAdd("SaveBATransRefundOtp", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int SaveDMTTransRefundOTPResponse(DmtTransactionResponse _objDetails, MDmtTransactionRequest _objrequestdetails)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@ProcId", 2);
                queryParameters.Add("@AgentId", _objrequestdetails.AgentId);
                queryParameters.Add("@RequestType", _objrequestdetails.RequestType);
                queryParameters.Add("@UniqueRefId", _objrequestdetails.UniqueRefId);
                queryParameters.Add("@Otp", _objrequestdetails.Otp);
                queryParameters.Add("@InitChannel", _objrequestdetails.InitChannel);
                queryParameters.Add("@TxnId", _objrequestdetails.TxnId);
                queryParameters.Add("@ResponseCode", _objDetails.ResponseCode);
                queryParameters.Add("@ResponseReason", _objDetails.ResponseReason);
                queryParameters.Add("@CompanyId", _objrequestdetails.CompanyId);
                queryParameters.Add("@FK_MemId", _objrequestdetails.Fk_MemID);
                queryParameters.Add("@ErrorMsg", _objDetails.errorInfo.error.errorCode + ' ' + _objDetails.errorInfo.error.errorMessage);
                int _iresult = DBHelperDapper.DAAdd("SaveBATransRefundOtp", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int SaveDMTTransFundTransferRequest(DmtTransactionResponse _objDetails, MDmtTransactionRequest _objrequestdetails)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@AgentId", _objrequestdetails.AgentId);
                queryParameters.Add("@SenderMobileNo", _objrequestdetails.SenderMobileNo);
                queryParameters.Add("@RequestType", _objrequestdetails.RequestType);
                queryParameters.Add("@UniqueRefId", _objDetails.FundTransferDetails.FundDetail.UniqueRefId);
                queryParameters.Add("@InitChannel", _objrequestdetails.InitChannel);
                queryParameters.Add("@TxnId", _objrequestdetails.TxnId);
                queryParameters.Add("@txnAmount", _objrequestdetails.TxnAmount);
                queryParameters.Add("@RecipientId", _objrequestdetails.RecipientId);
                queryParameters.Add("@ConvFee", _objrequestdetails.ConvFee);
                queryParameters.Add("@TxnType", _objrequestdetails.TxnType);
                queryParameters.Add("@CustConvFee", _objDetails.FundTransferDetails.FundDetail.CustConvFee);
                queryParameters.Add("@RespDesc", _objDetails.RespDesc);
                queryParameters.Add("@BankTxnId", _objDetails.FundTransferDetails.FundDetail.BankTxnId);
                queryParameters.Add("@ImpsName", _objDetails.FundTransferDetails.FundDetail.ImpsName);
                queryParameters.Add("@TxnStatus", _objDetails.FundTransferDetails.FundDetail.TxnStatus);
                queryParameters.Add("@DmtTxnId", _objDetails.FundTransferDetails.FundDetail.DmtTxnId); 
                queryParameters.Add("@RefId", _objDetails.FundTransferDetails.FundDetail.RefId);
                queryParameters.Add("@ResponseCode", _objDetails.ResponseCode);
                queryParameters.Add("@ResponseReason", _objDetails.ResponseReason);
                queryParameters.Add("@CompanyId", _objrequestdetails.CompanyId);
                queryParameters.Add("@FK_MemId", _objrequestdetails.Fk_MemID);
                queryParameters.Add("@ProcId", 1);
                int _iresult = DBHelperDapper.DAAdd("SaveBADMTFundTransfer", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int SaveDMTTransFundTransferResponse(DmtTransactionResponse _objDetails, MDmtTransactionRequest _objrequestdetails)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@ProcId", 2);
                queryParameters.Add("@AgentId", _objrequestdetails.AgentId);
                queryParameters.Add("@SenderMobileNo", _objrequestdetails.SenderMobileNo);
                queryParameters.Add("@RequestType", _objrequestdetails.RequestType);
                queryParameters.Add("@UniqueRefId", _objDetails.FundTransferDetails.FundDetail.UniqueRefId);
                queryParameters.Add("@InitChannel", _objrequestdetails.InitChannel);
                queryParameters.Add("@TxnId", _objrequestdetails.TxnId);
                queryParameters.Add("@txnAmount", _objrequestdetails.TxnAmount);
                queryParameters.Add("@RecipientId", _objrequestdetails.RecipientId);
                queryParameters.Add("@ConvFee", _objrequestdetails.ConvFee);
                queryParameters.Add("@TxnType", _objrequestdetails.TxnType);
                queryParameters.Add("@ResponseCode", _objDetails.ResponseCode);
                queryParameters.Add("@ResponseReason", _objDetails.ResponseReason);
                queryParameters.Add("@CompanyId", _objrequestdetails.CompanyId);
                queryParameters.Add("@FK_MemId", _objrequestdetails.Fk_MemID);
                queryParameters.Add("@ErrorMsg", _objDetails.errorInfo.error.errorCode + ' ' + _objDetails.errorInfo.error.errorMessage);
                int _iresult = DBHelperDapper.DAAdd("SaveBADMTFundTransfer", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}