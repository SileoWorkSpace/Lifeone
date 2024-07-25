using LifeOne.Models.BillAvenues.Entity;
using LifeOne.Models.BillAvenueUtility.API;
using LifeOne.Models.BillAvenueUtility.DAL;
using LifeOne.Models.BillAvenueUtility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.BillAvenueUtility.Service
{
    public class BillerQuickPayRequestService
    {
        ~BillerQuickPayRequestService() { }
        ResponseModel _objResponseModel = new ResponseModel();
        public ResponseModel InvokeQuickBillPayRequestService(RequestModel _obj)
        {
            MBillerPayRequestResponse _responsedetails = null;
            try
            {
                //get Reponse From APP Side ..Futher, we will  be stored request on the database here..
                MBillerPayRequestAPPQuickPay _MBillerPayRequestAPP = DALCommon.CustomDeserializeObjectWithDecryptString<MBillerPayRequestAPPQuickPay>(_obj.body);
                //Now convert the Model into XML for BiilAvenue
                string _url = "QuickBILLPayRequest";
                //Calling API Here
                //_objResponseModel = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                _responsedetails = DALCommon.CustomDeserializeObjectWithDecryptString<MBillerPayRequestResponse>(_objResponseModel.body);
                if (_responsedetails.responseCode == "000" || _responsedetails.responseCode == "111 ") //000 Recharge successful   / 111 Recharge in process    
                    DALQuickBillPayment.SaveBillPayRequest(_responsedetails, _MBillerPayRequestAPP);
                else
                    DALQuickBillPayment.SaveBillPayResponseError(_responsedetails, _MBillerPayRequestAPP);

                _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MBillerPayRequestResponse>(_responsedetails); // on success we will be added records on
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                _responsedetails.responseCode = "0";
                _responsedetails.errorInfo.error.errorMessage = DALErrorManament.CommonErrorMsg + ex.ToString();
                return DALErrorManament.ErroMethod<MBillerPayRequestResponse>(_responsedetails); 
            }
        }
        //Generate XMl Here as per Bill Avenue API       
        //to do : currency wiil be chnaged here
        public static string CreateXmlForQuickBiller(MBillerPayRequestAPPQuickPay _BillPayModelAPP, out string _finalxml)
        {
            try
            {
                MBillerQuickPayRequest _mBillerPayRequest = new MBillerQuickPayRequest();
                _mBillerPayRequest.AgentId = _BillPayModelAPP.AgentId;
                _mBillerPayRequest.BillerId = _BillPayModelAPP.BillerId;
                _mBillerPayRequest.BillerAdhoc = _BillPayModelAPP.BillerAdhoc.ToString();

                AgentDeviceInfo agentDeviceInfoDetails = new AgentDeviceInfo();
                agentDeviceInfoDetails.Ip = _BillPayModelAPP.IP;
                agentDeviceInfoDetails.InitChannel = "MOB";  //will disscuss ltr
                                                             //  agentDeviceInfoDetails.Mac = _BillPayModelAPP.MAC;
                agentDeviceInfoDetails.imei = _BillPayModelAPP.imei;
                agentDeviceInfoDetails.app = _BillPayModelAPP.app;
                agentDeviceInfoDetails.os = _BillPayModelAPP.os;

                //--------------------------------------------------
                _mBillerPayRequest.AgentDeviceInfo = agentDeviceInfoDetails;

                // Bind Customer information
                CustomerInfo _customerInfo = new CustomerInfo();
                _customerInfo.CustomerMobile = _BillPayModelAPP.CustomerMobile;
                _customerInfo.CustomerEmail = _BillPayModelAPP.CustomerEmail;
                _customerInfo.CustomerAdhaar = _BillPayModelAPP.CustomerAdhaar;
                _customerInfo.CustomerPan = _BillPayModelAPP.CustomerPan;
                //---------------------------------------------------
                _mBillerPayRequest.CustomerInfo = _customerInfo;
                //---------------------------------------------------

                //Input Paramters will have to disscuss from Bill Avenue Team why we take IT
                inputParams inputParams = InputParmetrs(_BillPayModelAPP.inputParams);
                _mBillerPayRequest.InputParams = inputParams;

                //--------------------------------------------------------
                // Amount information
                MamountInfo mamountInfo = new MamountInfo();
                mamountInfo.Amount = _BillPayModelAPP.Amount;
                mamountInfo.Currency = _BillPayModelAPP.Currency; ; //356 may be master here
                mamountInfo.CustConvFee = _BillPayModelAPP.CustConvFee;
                mamountInfo.AmountTags = "";
                //------------------------------------------------------
                _mBillerPayRequest.AmountInfo = mamountInfo;
                //-----------------------------------------------------

                //payment mode information
                MpaymentMethod mpaymentMethod = new MpaymentMethod();
                mpaymentMethod.paymentMode = _BillPayModelAPP.PaymentMode;
                mpaymentMethod.quickPay = _BillPayModelAPP.QuickPay;
                mpaymentMethod.splitPay = _BillPayModelAPP.SplitPay;
                _mBillerPayRequest.PaymentMethod = mpaymentMethod;

                //payment infomation
                MpaymentInfo PaymentInfo = new MpaymentInfo();
                PaymentInfo.Info = _BillPayModelAPP.info;
                //----------------------------------------------
                _mBillerPayRequest.PaymentInfo = PaymentInfo;
                //--------------------------------------------------

                string _xmlresult = DALXmlSerializers.SerializeToString(_mBillerPayRequest);
                _finalxml = DALCommon.RemoveCompanyIdAndOtherParams(_xmlresult);
                return _finalxml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // its not a manadtry fields for BIllAvenue
        private static inputParams InputParmetrs(List<input> APIinputParams)
        {
            List<input> _objlist = new List<input>();
            _objlist = APIinputParams;
            inputParams inputParams = new inputParams();
            inputParams.input = _objlist;
            return inputParams;
        }
    }
}