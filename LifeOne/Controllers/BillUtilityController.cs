using LifeOne.Models.BillAvenueUtility.Entity;
using LifeOne.Models.BillAvenueUtility.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace LifeOne.Controllers
{
    public class BillUtilityController : ApiController
    {

        //Biller - Bind Dashboard Here 
        [HttpPost]
        public ResponseModel BillerInfo(RequestModel _model)
        {
            try
            {
                ResponseModel _result = DALBillerDetail.GetCustomBillerDetail(_model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Biller - Bind Dashboard Here 
        //[HttpPost]
        //public ResponseModel BillerInfoDashboard(RequestModel _model)
        //{
        //    try
        //    {   
        //        BillerInfoService _objService = new BillerInfoService();
        //        ResponseModel _result = _objService.InvokeBindDashboardService(_model);
        //        return _result;                                                                                                                     
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpPost]
        //public ResponseModel BillerGetCirCleInformation(RequestModel _model)
        //{
        //    try
        //    {
        //        BillerInfoService _objService = new BillerInfoService();
        //        ResponseModel _result = _objService.InvokeBillerWiseCircleDetails(_model);
        //        return _result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


        //public ResponseModel VlidateAPIForBiller(RequestModel _model)
        //{
        //    try
        //    {
        //        BillerInfoService _objService = new BillerInfoService();
        //        ResponseModel _result = _objService.InvokeVlidateAPIForBiller(_model);
        //        return _result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //getting biller info from Billerid
        //Pass the billerid and fetch the biller information, after getting biller details we will be takeing   
        //decision what exactly biller is? wheather it is Bill or prepaid service    

        [HttpPost]
        public ResponseModel BillInfoDetailsByBillerId(RequestModel _model)
        {
            try
            {
                BillerInfoService _objService = new BillerInfoService();
                ResponseModel _result = _objService.GetBillerInfoService(_model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //[HttpPost]
        //public ResponseModel BillerInfoMaster(RequestModel _model)
        //{
        //    try
        //    {
        //        BillerMasterService _objService = new BillerMasterService();
        //        ResponseModel _result = _objService.InvokeBillInformationMasterService(_model);
        //        return _result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}



        [HttpPost]
        public ResponseModel BAWalletStatus(RequestModel _model)
        {
            try
            {
                BillerWalletStatus _objService = new BillerWalletStatus();
                ResponseModel _result = _objService.InvokeBillerWalletStatus(_model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        ///*BILLFETCH - enables you to get the latest bill of the customer*/
        //[HttpPost]
        //public ResponseModel BILLFETCH(RequestModel _model)
        //{
        //    try
        //    {
        //        BillerFetchService _objService = new BillerFetchService();
        //        ResponseModel _result = _objService.InvokeBillFetchService(_model);
        //        return _result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        [HttpPost]
        public ResponseModel BILLPayRequest(RequestModel _model)
        {
            try
            {
                BillerPayRequestService _objService = new BillerPayRequestService();
                ResponseModel _result = _objService.InvokeBillPayRequestService(_model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Quick Pay Bill Request
        [HttpPost]
        public ResponseModel QuickBILLPayRequest(RequestModel _model)
        {
            try
            {
                BillerQuickPayRequestService _objService = new BillerQuickPayRequestService();
                ResponseModel _result = _objService.InvokeQuickBillPayRequestService(_model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ResponseModel TRANSACTIONSTATUS(RequestModel _model)
        {
            try
            {
                BillerTransactionStatusService _objService = new BillerTransactionStatusService();
                ResponseModel _result = _objService.InvokeTransactionStatusService(_model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //[HttpPost]
        //public ResponseModel COMPLAINTREGISTRATION(RequestModel _model)
        //{
        //    try
        //    {
        //        BillerComplaintRegistration _objService = new BillerComplaintRegistration();
        //        ResponseModel _result = _objService.InvokeBillComplaintRegistrationService(_model);
        //        return _result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //[HttpPost]
        //public ResponseModel COMPLAINTTRACKING(RequestModel _model)
        //{
        //    try
        //    {
        //        BillerComplaintRegistration _objService = new BillerComplaintRegistration();
        //        ResponseModel _result = _objService.InvokeBillComplaintTrackingService(_model);
        //        return _result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}     
        //[HttpPost]
        //public ResponseModel DepositEnquiry(RequestModel _model)
        //{
        //    try
        //    {
        //        BillerDepositEnquiryServicce _objService = new BillerDepositEnquiryServicce();
        //        ResponseModel _result = _objService.InvokeDepositEnquiryService(_model);
        //        return _result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        /*Prepaid Recharge*/
        //[HttpPost]
        //public ResponseModel PREPAIDPLANS(RequestModel _model)
        //{
        //    try
        //    {
        //        PrepaidPlanService _objService = new PrepaidPlanService();
        //        ResponseModel _result = _objService.InvokePrepaidPlanDetailsService(_model);
        //        return _result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        [HttpPost]
        public ResponseModel PREPAIDPAYMENT(RequestModel _model)
        {
            try
            {
                PrepaidPaymentRequestService _objService = new PrepaidPaymentRequestService();
                ResponseModel _result = _objService.InvokePrepaidPaymentService(_model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[HttpPost]
        //public ResponseModel GetAllPrepaidCircle(RequestModel _model)
        //{
        //    try
        //    {
        //        PrepaidGetCircleSevice _objService = new PrepaidGetCircleSevice();
        //        ResponseModel _result = _objService.InvokeGetPrepaidCircleService(_model);
        //        return _result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //DMT Service Request
        //[HttpPost]
        //public ResponseModel DMTServiceRequest(RequestModel _model)
        //{
        //    try
        //    {
        //        DMTRequestService _objService = new DMTRequestService();
        //        ResponseModel _result = _objService.InvokeDMTRequestService(_model);
        //        return _result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //[HttpPost]
        //public ResponseModel DMTRequestSenderRegistration(RequestModel _model)
        //{
        //    try
        //    {
        //        DMTRequestService _objService = new DMTRequestService();
        //        ResponseModel _result = _objService.InvokeDMTSenderRegistration(_model);
        //        return _result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpPost]
        //public ResponseModel DMTVerifySender(RequestModel _model)
        //{
        //    try
        //    {
        //        DMTRequestService _objService = new DMTRequestService();
        //        ResponseModel _result = _objService.InvokeDMTVerifySender(_model);
        //        return _result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpPost]
        //public ResponseModel DMTResendOTP(RequestModel _model)
        //{
        //    try
        //    {
        //        DMTRequestService _objService = new DMTRequestService();
        //        ResponseModel _result = _objService.InvokeDMTResendOTP(_model);
        //        return _result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpPost]
        //public ResponseModel DMTAllRecipients(RequestModel _model)
        //{
        //    try
        //    {
        //        DMTRequestService _objService = new DMTRequestService();
        //        ResponseModel _result = _objService.InvokeDMTAllRecipients(_model);
        //        return _result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //[HttpPost]
        //public ResponseModel DMTGetRecipients(RequestModel _model)
        //{
        //    try
        //    {
        //        DMTRequestService _objService = new DMTRequestService();
        //        ResponseModel _result = _objService.InvokeDMTGetRecipients(_model);
        //        return _result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpPost]
        //public ResponseModel DMTAddRecipients(RequestModel _model)
        //{
        //    try
        //    {
        //        DMTRequestService _objService = new DMTRequestService();
        //        ResponseModel _result = _objService.InvokeDMTAddRecipients(_model);
        //        return _result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpPost]
        //public ResponseModel DMTDeleteRecipients(RequestModel _model)
        //{
        //    try
        //    {
        //        DMTRequestService _objService = new DMTRequestService();
        //        ResponseModel _result = _objService.InvokeDMTDeleteRecipients(_model);
        //        return _result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //[HttpPost]
        //public ResponseModel BankList(RequestModel _model)
        //{
        //    try
        //    {
        //        DMTRequestService _objService = new DMTRequestService();
        //        ResponseModel _result = _objService.InvokeDMTBankList(_model);
        //        return _result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


        //Transaction Request
        //[HttpPost]
        //public ResponseModel DMTTransactionRequest(RequestModel _model)
        //{
        //    try
        //    {
        //        DMTTransactionRequestService _objService = new DMTTransactionRequestService();
        //        ResponseModel _result = _objService.InvokeDMTTransactionRequestService(_model);
        //        return _result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        ////Transaction Refund OTP
        //[HttpPost]
        //public ResponseModel DMTTransactionRefundOTP(RequestModel _model)
        //{
        //    try
        //    {
        //        DMTTransactionRequestService _objService = new DMTTransactionRequestService();
        //        ResponseModel _result = _objService.InvokeDMTTransactionRefundOTPService(_model);
        //        return _result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        ////Fund Transfer
        //[HttpPost]
        //public ResponseModel DMT(RequestModel _model)
        //{
        //    try
        //    {
        //        DMTTransactionRequestService _objService = new DMTTransactionRequestService();
        //        ResponseModel _result = _objService.InvokeDMTFundTransfer(_model);
        //        return _result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        ////Fund Transfer greater Than 5000 Rs.
        //[HttpPost]
        //public ResponseModel DMTFundTransferMaximumValue(RequestModel _model)
        //{
        //    try
        //    {
        //        DMTTransactionRequestService _objService = new DMTTransactionRequestService();
        //        ResponseModel _result = _objService.InvokeDMTFundTransferMaximumValue(_model);
        //        return _result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        ////Fund Transfer Milti Transaction
        //public ResponseModel DMTMultiTransaction(RequestModel _model)
        //{
        //    try
        //    {
        //        DMTTransactionRequestService _objService = new DMTTransactionRequestService();
        //        ResponseModel _result = _objService.InvokeDMTMultiTransactionService(_model);
        //        return _result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        ////Get Cust Conv Fee
        //public ResponseModel DMTGetCustomerConvFee(RequestModel _model)
        //{
        //    try
        //    {
        //        DMTTransactionRequestService _objService = new DMTTransactionRequestService();
        //        ResponseModel _result = _objService.InvokeDMTGetCustomerConvFeeService(_model);
        //        return _result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //MNP
        public ResponseModel MNPRequest(RequestModel _model)
        {
            try
            {
                MNPService _objService = new MNPService();
                ResponseModel _result = _objService.InvokeMNPRequestService(_model);
                return _result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel TransacationHistory(RequestModel _model)
        {
            try
            {
                TransactionHistoryService _objService = new TransactionHistoryService();
                ResponseModel _result = _objService.TransactionHistory(_model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        ///  Bill Avenue API Integration //28/07/2022
        /// </summary>
        /// <param name="_model"></param>
        /// <returns></returns>
        /// 

        [HttpPost]
        public ResponseModel BillirIdFromCategory(RequestModel _model)
        {
            try
            {
                BillerInfoService _objService = new BillerInfoService();
                ResponseModel _result = _objService.GetBillirIdFromCategoryService(_model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel GetCustomBillerDetail(RequestModel _model)
        {
            try
            {
                ResponseModel _result = DALBillerDetail.GetCustomBillerDetail(_model);
                return _result;
            }
            catch (Exception)
            {                   
                throw;
            }
        }

        [HttpPost]
        public ResponseModel BILLFETCH(RequestModel _model)
        {
            try
            {
                BillerFetchService _objService = new BillerFetchService();
                ResponseModel _result = _objService.InvokeBillFetchService(_model);
                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
