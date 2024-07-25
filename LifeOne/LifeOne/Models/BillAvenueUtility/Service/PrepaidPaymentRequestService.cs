using LifeOne.Models.BillAvenues.Entity;
using LifeOne.Models.BillAvenueUtility.API;
using LifeOne.Models.BillAvenueUtility.DAL;
using LifeOne.Models.BillAvenueUtility.Entity;
using LifeOne.Models.BillAvenueUtility.Entity.PrepaidRecharge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LifeOne.Models.BillAvenueUtility.Entity.PrepaidRecharge.MPrepaidPaymentRequest;

namespace LifeOne.Models.BillAvenueUtility.Service
{
    public class PrepaidPaymentRequestService
    {

        ~PrepaidPaymentRequestService() { }
        ResponseModel _objResponseModel = new ResponseModel();
        public ResponseModel InvokePrepaidPaymentService(RequestModel _obj)
        {
            MExtBillPayResponse _responsedetails = null;
            try
            {
                string BAVRequestId = "";
                BAVRequestId = BIllAvenueCredientials.BAVRequestId;                
                int reqId = Int32.Parse(BAVRequestId);
                String requestId = GenerateRequestId.RandomString(reqId);
                //get Reponse From APP Side ..
                MPrepaidPayRequestAPP _MBillerPayRequestAPP = DALCommon.CustomDeserializeObjectWithDecryptString<MPrepaidPayRequestAPP>(_obj.body);
                //Now convert the Model into XML for BiilAvenue
                string _finalxml = CreateXmlForBiller(_MBillerPayRequestAPP, out _finalxml);
                //Calling API Here
                _responsedetails = GenericAPI.BAVsendRequestToUtilityAPIPostWithCompIdAndFkMemIDNew<MExtBillPayResponse>(_finalxml, "extBillPayCntrl/billPayRequest/xml", _MBillerPayRequestAPP.BillerId, _MBillerPayRequestAPP.ProjectId, _MBillerPayRequestAPP.Fk_MemID, _MBillerPayRequestAPP.AndroidId, _MBillerPayRequestAPP.IP,
                 _MBillerPayRequestAPP.DeviceId, "BILLPrepaidPayRequest", requestId);

                _MBillerPayRequestAPP.RequestId = requestId;
    

               

                _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MExtBillPayResponse>(_responsedetails); // on success we will be added records on
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                _responsedetails.responseCode = "0";
                _responsedetails.errorInfo.error.errorMessage = DALErrorManament.CommonErrorMsg + ex.ToString();
                return DALErrorManament.ErroMethod<MExtBillPayResponse>(_responsedetails);
            }
        }
        private static inputParams InputParmetrs(string location, string mobileNO)
        {
            List<input> _objlist = new List<input>();
            input _input1 = new input();
            _input1.paramName = "Location";
            _input1.paramValue = location;
            _objlist.Add(_input1);

            input _input2 = new input();
            _input2.paramName = "Mobile Number";
            _input2.paramValue = mobileNO;
            _objlist.Add(_input2);

            inputParams inputParams = new inputParams();
            inputParams.input = _objlist;
            return inputParams;
        }
        private static string CreateXmlForBiller(MPrepaidPayRequestAPP _BillPayModelAPP, out string _finalxml)
        {
            try
            {
                MPrepaidPaymentRequest _mBillerPayRequest = new MPrepaidPaymentRequest();
                _mBillerPayRequest.AgentId = _BillPayModelAPP.AgentId;
                _mBillerPayRequest.BillerId = _BillPayModelAPP.BillerId;
                _mBillerPayRequest.BillerAdhoc = _BillPayModelAPP.BillerAdhoc.ToString();

                AgentDeviceInfo agentDeviceInfoDetails = new AgentDeviceInfo();
                agentDeviceInfoDetails.Ip = _BillPayModelAPP.IP;
                agentDeviceInfoDetails.InitChannel = "MOB";  //will disscuss ltr
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
                inputParams inputParams = InputParmetrs(_BillPayModelAPP.Location, _BillPayModelAPP.MobileNumber);
                _mBillerPayRequest.InputParams = inputParams;


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




                List<Info> lstInfo = new List<Info>();
                PaymentInfo payment = new PaymentInfo();
                Info paymentInfo = new Info();
                paymentInfo.InfoName = "WalletName";
                paymentInfo.InfoValue = "LifeoneWellness WALLET";
                lstInfo.Add(paymentInfo);
                Info paymentInfo1 = new Info();
                paymentInfo1.InfoName = "MobileNo";
                paymentInfo1.InfoValue = _BillPayModelAPP.CustomerMobile;
                lstInfo.Add(paymentInfo1);
                payment.Info = lstInfo;
                _mBillerPayRequest.paymentInfo = payment;

                ////payment infomation
                //MpaymentInforecharge PaymentInfo = new MpaymentInforecharge();

                //PaymentInfo.Info = _BillPayModelAPP._Objlist;
                ////----------------------------------------------
                //_mBillerPayRequest.PaymentInfo = PaymentInfo;
                ////--------------------------------------------------

                string _xmlresult = DALXmlSerializers.SerializeToString(_mBillerPayRequest);
                _finalxml = DALCommon.RemoveCompanyIdAndOtherParams(_xmlresult);
                return _finalxml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}