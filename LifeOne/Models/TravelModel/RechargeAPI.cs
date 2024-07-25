using LifeOne.Models.BillAvenues.Entity;
using LifeOne.Models.BillAvenueUtility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace LifeOne.Models.TravelModel
{
    public class RechargeAPI
    {
        public class RechargeOperatorsOutput
        {
            public List<OperatorLists> OperatorLists { get; set; }

        }
        public class OperatorLists
        {
            public string OperatorCode { get; set; }
            public string OperatorDescritpion { get; set; }
            public string RechargeType { get; set; }
            public string Commission { get; set; }

        }
        public class Response
        {
            public string ResponseStatus { get; set; }
            public RechargeOperatorsOutput RechargeOperatorsOutput { get; set; }

            public AgentCreditBalanceOutput AgentCreditBalanceOutput { get; set; }
        }
        public class AgentCreditBalanceOutput
        {
            public string RemainingAmount { get; set; }
        }
        public class Rechargereponse
        {
            public string ResponseStatus { get; set; }
            public string UserTrackId { get; set; }
            public RechargeOutput RechargeOutput { get; set; }
            public string FailureRemarks { get; set; }

        }
        public class RechargeOutput
        {
            public string ReferenceNumber { get; set; }
            public string OperatorDescription { get; set; }
            public string MobileNumber { get; set; }
            public string Amount { get; set; }
            public string RechargeDateTime { get; set; }
            public string OperatorTransactionId { get; set; }
        }
        public class RechargeRequest
        {
            public string OperatorCode { get; set; }
            public string MobileNumber { get; set; }
            public string Amount { get; set; }
            public string FK_MemID { get; set; }
            public string CompanyId = "10003";
            public string TransType = "Recharge";
            public string BookingId { get; set; }

        }

        #region Prepaid Mobile Recharge
        public class GetRechargeReprintRequest
        {
            public string UserTrackId { get; set; }
        }

        public class GetRechargeReprintResponse
        {
            public string ResponseStatus { get; set; }
            public string UserTrackId { get; set; }
            public RechargeReprintOutput ReprintOutput { get; set; }
        }

        public class RechargeReprintOutput
        {
            public string ReferenceNumber { get; set; }
            public string OperatorDescription { get; set; }
            public string MobileNumber { get; set; }
            public string Amount { get; set; }
            public string RechargeDateTime { get; set; }
            public string OperatorTransactionId { get; set; }
        }
        public class TransactionResponse
        {
            public string ResponseStatus { get; set; }
            public string UserTrackId { get; set; }
            public MobileTransactionStatusOutput TransactionStatusOutput { get; set; }


        }
        public class MobileTransactionStatusOutput
        {
            public string TransactionStatus { get; set; }
            public TransactionDetails TransactionDetails { get; set; }
            public string Remarks { get; set; }
        }
        public class TransactionDetails
        {
            public string ReferenceNumber { get; set; }
            public string OperatorDescription { get; set; }
            public string MobileNumber { get; set; }
            public string Amount { get; set; }
            public string RechargeDateTime { get; set; }
            public string OperatorTransactionId { get; set; }
        }
        [Serializable]
        [XmlRoot("billPaymentRequest")]
        public class BillPaymentRequest : Common
        {
            public string agentId { get; set; }
            public bool billerAdhoc { get; set; }

            [XmlElement("agentDeviceInfo")]
            public agentDeviceInfo agentDeviceInfo { get; set; }
            [XmlElement("customerInfo")]
            public customerInfo customerInfo { get; set; }
            public string billerId { get; set; }
            [XmlElement("inputParams")]
            public inputParams inputParams { get; set; }

            [XmlElement("additionalInfo")]
            public additionalInfo additionalInfo { get; set; }

            [XmlElement("billerResponse")]
            public MbillerResponse _objbillerResponse { get; set; }

            [XmlElement("amountInfo")]
            public amountInfo amountInfo { get; set; }

            [XmlElement(ElementName = "paymentMethod")]
            public MpaymentMethod PaymentMethod { get; set; }
            [XmlElement(ElementName = "paymentInfo")]
            public MpaymentInfo PaymentInfo { get; set; }

        }
        public class BillPaymentInput
        {
            public string FK_MemID { get; set; }
            public string OperatorCode { get; set; }
            public string MobileNumber { get; set; }
            public string Amount { get; set; }
            public string OtherDetails { get; set; }
        }
        public class GetRechargeHistoryResponse
        {
            public string ResponseStatus { get; set; }
            public string FailureRemarks { get; set; }
            public List<Recharges> Recharges { get; set; }
        }
        public class Recharges
        {
            public string ReferenceNumber { get; set; }
            public string MobileNumber { get; set; }
            public string RechargeDateTime { get; set; }
            public string OperatorDescription { get; set; }
            public decimal Amount { get; set; }
            public string RechargeStatus { get; set; }
            public string OperatorTransactionId { get; set; }
        }
        public class GetRechargeHistory
        {
            public RechargeHistoryInput RechargeHistoryInput { get; set; }
        }
        public class RechargeHistoryInput
        {
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public string MobileNumber { get; set; }

            public string Fk_MemId { get; set; }

            public string Type { get; set; }

        }
        public class AccountStatementResponse
        {
            public string ResponseStatus { get; set; }
            public AccountStatementOutput AccountStatementOutput { get; set; }
        }
        public class AccountStatementOutput
        {
            public string TravelAgentID { get; set; }
            public string TravelAgentName { get; set; }
            public string TotalRemainingAmount { get; set; }
            public List<Transactions> Transactions { get; set; }



        }
        public class Transactions
        {
            public string DateTime { get; set; }
            public string Description { get; set; }
            public string ReferenceNumber { get; set; }
            public string CreditAmount { get; set; }
            public string DebitAmount { get; set; }
            public string RemainingAmount { get; set; }
            public string Remarks { get; set; }
            public string TerminalName { get; set; }
            public string ProductDescription { get; set; }
        }
        public class PaymentOperatorsResponse
        {
            public string ResponseStatus { get; set; }
            public BillPaymentOperatorsOutput BillPaymentOperatorsOutput { get; set; }

        }
        public class BillPaymentOperatorsOutput
        {
            public List<OperatorLists> OperatorLists { get; set; }
        }
        public class BillPaymentResponse
        {
            public string ResponseStatus { get; set; }
            public string UserTrackId { get; set; }
            public BillPaymentOutput BillPaymentOutput { get; set; }
            public string FailureRemarks { get; set; }
        }
        public class BillPaymentOutput
        {
            public string ReferenceNumber { get; set; }
            public string OperatorDescription { get; set; }
            public string MobileNumber { get; set; }
            public string Amount { get; set; }
            public string RechargeDateTime { get; set; }
            public string OperatorTransactionId { get; set; }
        }

        #endregion
    }
}