using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class RechargeApiModel
    {
    }
    public class WalletApiModel
    {
        public string WalletType { get; set; }
        public double WalletBalance { get; set; }
    }
    public class WalletBalanceModel
    {
        public int ResponseCode { get; set; }
        public int CustomerID { get; set; }
        public List<WalletApiModel> Wallets { get; set; }
        public string StatusMessage { get; set; }
    }
    public class WalletBalanceResponseModel
    {
        public WalletBalanceModel WalletBalance { get; set; }
        public string response { get; set; }
        public string message { get; set; }
        public int Flag { get; set; }
        public bool Status { get; set; }
    }

    public class AdditionalInput
    {
        public string InputName { get; set; }
        public string InputValue { get; set; }
    }

    public class RechargeModel
    {
        public string UserId { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public int CustomerId { get; set; }
        public string AccessToken { get; set; }
        public string TransactionPin { get; set; }
        public int CompanyId { get; set; }
        public string CustomerNumber { get; set; }
        public int TransactionAmount { get; set; }
        public int OperatorId { get; set; }
        public int RechargeType { get; set; }
        public string ClientRequestId { get; set; }
        public List<AdditionalInput> AdditionalInputs { get; set; }
    }
    public class RechargeResponseModel
    {
        public int StatusCode { get; set; }
        public string VendorStatusCode { get; set; }
        public string CustomerNumber { get; set; }
        public double TransactionAmount { get; set; }
        public double RunningBalance { get; set; }
        public string StatusMessage { get; set; }
        public int TransactionId { get; set; }
        public string VendorTransactionId { get; set; }
        public string OperatorTransactionId { get; set; }
        public string VendorStatusMessage { get; set; }
        public object AdditionalOutputs { get; set; }
    }
    public class RechargeTypeResponseModel
    {
        public int Flag { get; set; }
        public bool Status { get; set; }
        public string response { get; set; }
        public string message { get; set; }
        public List<RechargeTypeModel> Data { get; set; }
    }
    public class RechargeTypeModel
    {
        public int SrNo { get; set; }
        public string Type { get; set; }
        public int TypeId { get; set; }
    }
    public class RechargeOperatorResponseModel
    {
        public int Flag { get; set; }
        public bool Status { get; set; }
        public string response { get; set; }
        public string message { get; set; }
        public List<RechargeOperatorModel> Data { get; set; }
    }
    public class RechargeOperatorModel
    {
        public int SrNo { get; set; }
        public int OperatorId { get; set; }
        public string OperatorName { get; set; }
        public string Type { get; set; }
        public int TypeId { get; set; }
        public string Icon { get; set; }
        public bool Status { get; set; }
    }
    public class ApiResponeModel
    {
        public int Flag { get; set; }
        public string response { get; set; }
        public string message { get; set; }
        public bool Status { get; set; }
    }
    public class RechargeReportFilterModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string LoginId { get; set; }
        public int FK_MemId { get; set; }
        public int TransactionId { get; set; }
        public string Status { get; set; }
        public int Page { get; set; }
    }
    public class ApiRechargeReportModel
    {
        public int Flag { get; set; }
        public bool Status { get; set; }
        public string response { get; set; }
        public string Message { get; set; }
        public List<RechargeReportModel> Data { get; set; }

    }
    public class RechargeReportModel
    {
        public int TransactionId { get; set; }
        public string StatusMessage { get; set; }
        public string OperatorTransactionId { get; set; }
        public string Status { get; set; }
        public decimal TransactionAmount { get; set; }
        public string RechargeType { get; set; }
        public string OperatorName { get; set; }
        public string Icon { get; set; }
        public int TotalRecords { get; set; }
    }
    public class RechargeApiModeModel
    {
        public int CompId { get; set; }
        public bool IsLifeOnee { get; set; }
        public bool IsPayAdda { get; set; }
        public bool Status { get; set; }

    }

    public class ApiCommonRechargeModel
    {

        public string ValidateToken { get; set; }
        public int CompanyId { get; set; }
        public int Fk_MemID { get; set; }
        public int EnvId { get; set; }
        public string AndroidId { get; set; }
        
        public string DeviceId { get; set; }
        public string AgentId { get; set; }
        public string BillerId { get; set; }
        public string MAC { get; set; }
        public string IP { get; set; }
        public string CustomerMobile { get; set; }
        public string Number { get; set; }
        public string Provider { get; set; }
        public string CustomerAdhaar { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPan { get; set; }
        public string BillAmount { get; set; }
        public string TRANSID { get; set; }
        public string Amount { get; set; }
        public string BillDate { get; set; }
        public string BillNumber { get; set; }
        public string BillPeriod { get; set; }
        public string CustomerName { get; set; }
        public string DueDate { get; set; }
        public string CustConvFee { get; set; }
        public string PaymentMode { get; set; }
        public string QuickPay { get; set; }
        public string SplitPay { get; set; }
        public string LatePaymentFee { get; set; }
        public string FixedCharges { get; set; }
        public string AdditionalCharges { get; set; }
        public string billerAdhoc { get; set; }
        public string Remarks { get; set; }
        public string Currency { get; set; }
        public string RequestId { get; set; }
        public string PaymentURL { get; set; }
        public string CheckURL { get; set; }
        public int GatewayNo { get; set; }
        public string PlanOffer { get; set; }
        public List<Info> info { get; set; }
        public string Body { get; set; }
    }
    public class Info
    {
        public string infoName { get; set; }
        public string infoValue { get; set; }
    }
}