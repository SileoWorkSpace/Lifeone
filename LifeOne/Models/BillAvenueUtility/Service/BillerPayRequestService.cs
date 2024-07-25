using LifeOne.Models.BillAvenues.Entity;
using LifeOne.Models.BillAvenueUtility.API;
using LifeOne.Models.BillAvenueUtility.DAL;
using LifeOne.Models.BillAvenueUtility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LifeOne.Models.TravelModel.RechargeAPI;

namespace LifeOne.Models.BillAvenueUtility.Service
{
    public class BillerPayRequestService
    {
        ~BillerPayRequestService() { }
        ResponseModel _objResponseModel = new ResponseModel();
        DALBillPayment dALBillPayment = new DALBillPayment();
        public ResponseModel InvokeBillPayRequestService(RequestModel _obj)
        {
            MBillerPayRequestResponse _responsedetails = null;
            try
            {
                //get Reponse From APP Side ..and decrypt it
                MBillerPayRequestAPP _MBillerPayRequestAPP = DALCommon.CustomDeserializeObjectWithDecryptString<MBillerPayRequestAPP>(_obj.body);
                //API name here
                string _url = "BILLPayRequest";
                string _finalxml = CreateXmlForBiller(_MBillerPayRequestAPP, out _finalxml);
                //Calling API Here
                _responsedetails = GenericAPI.BAVsendRequestToUtilityAPIPostWithCompIdAndFkMemID<MBillerPayRequestResponse>(_finalxml, "extBillPayCntrl/billPayRequest/xml", _MBillerPayRequestAPP.BillerId, _MBillerPayRequestAPP.ProjectId, _MBillerPayRequestAPP.Fk_MemID, _MBillerPayRequestAPP.AndroidId, _MBillerPayRequestAPP.IP,
                _MBillerPayRequestAPP.DeviceId, "BILLPayRequest", _MBillerPayRequestAPP.RequestId);

                if (_responsedetails.responseCode == "000" || _responsedetails.responseCode == "111 ") //000 Recharge successful   / 111 Recharge in process
                    dALBillPayment.SaveBillPayRequest(_responsedetails, _MBillerPayRequestAPP);
                else
                    dALBillPayment.SaveBillPayResponseError(_responsedetails, _MBillerPayRequestAPP);

                _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MBillerPayRequestResponse>(_responsedetails);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                _responsedetails.responseCode = "0";
                _responsedetails.errorInfo.error.errorMessage = DALErrorManament.CommonErrorMsg + ex.ToString();
                return DALErrorManament.ErroMethod<MBillerPayRequestResponse>(_responsedetails);
            }
        }
        private static string CreateXmlForBiller(MBillerFetchRequestAPP _BillFetchModelAPP, out string _finalxml)
        {
            try
            {
                BillPaymentRequest _BillFetchModel = new BillPaymentRequest();

                _BillFetchModel.agentId = _BillFetchModelAPP.AgentId;
                _BillFetchModel.billerAdhoc = true;
                agentDeviceInfo agentDeviceInfoDetails = new agentDeviceInfo();
                agentDeviceInfoDetails.ip = _BillFetchModelAPP.IP;
                agentDeviceInfoDetails.initChannel = "AGT";
                agentDeviceInfoDetails.mac = _BillFetchModelAPP.MAC;
                agentDeviceInfoDetails.imei = _BillFetchModelAPP.imei;
                agentDeviceInfoDetails.app = _BillFetchModelAPP.app;
                agentDeviceInfoDetails.os = _BillFetchModelAPP.os;

                _BillFetchModel.agentDeviceInfo = agentDeviceInfoDetails;

                // Bind Customer information 
                customerInfo _customerInfo = new customerInfo();
                _customerInfo.customerMobile = _BillFetchModelAPP.CustomerMobile;
                _customerInfo.customerEmail = _BillFetchModelAPP.CustomerEmail;
                _customerInfo.customerAdhaar = _BillFetchModelAPP.CustomerAdhaar;
                _customerInfo.customerPan = _BillFetchModelAPP.CustomerPan;
                //---------------------------------------------------



                _BillFetchModel.customerInfo = _customerInfo;
                _BillFetchModel.billerId = _BillFetchModelAPP.BillerId;

                //Input Paramters will have to disscuss from Bill Avenue Team why we take IT

                inputParams inputParams = InputParamters(_BillFetchModelAPP.inputParams);

                _BillFetchModel.inputParams = inputParams;

                MbillerResponse mbillerResponse = new MbillerResponse();
                List<BillAvenues.Entity.option> lstOptions = new List<BillAvenues.Entity.option>();
                List<BillAvenues.Entity.MbilleramountOptions> lstMbiller = new List<BillAvenues.Entity.MbilleramountOptions>();
                List<info> lstInfo = new List<info>();
                mbillerResponse.billAmount = _BillFetchModelAPP.billerResponse.billAmount;
                mbillerResponse.billDate = _BillFetchModelAPP.billerResponse.billDate;
                mbillerResponse.billNumber = _BillFetchModelAPP.billerResponse.billNumber;
                mbillerResponse.billPeriod = _BillFetchModelAPP.billerResponse.billPeriod;
                mbillerResponse.customerName = _BillFetchModelAPP.billerResponse.customerName;
                mbillerResponse.dueDate = _BillFetchModelAPP.billerResponse.dueDate;

                for (int i = 0; i <= _BillFetchModelAPP.billerResponse.amountOptions.Count - 1; i++)
                {
                    BillAvenues.Entity.MbilleramountOptions mbilleramountOptions = new BillAvenues.Entity.MbilleramountOptions();
                    for (int j = 0; j <= _BillFetchModelAPP.billerResponse.amountOptions[i].option.Count - 1; j++)
                    {
                        BillAvenues.Entity.option options = new BillAvenues.Entity.option();
                        options.amountName = _BillFetchModelAPP.billerResponse.amountOptions[i].option[j].amountName.ToString();
                        options.amountValue = _BillFetchModelAPP.billerResponse.amountOptions[i].option[j].amountValue.ToString();
                        lstOptions.Add(options);
                    }
                    mbilleramountOptions.option = lstOptions;
                    lstMbiller.Add(mbilleramountOptions);

                }

                for (int k = 0; k <= _BillFetchModelAPP.additionalInfo._objinfolist.Count - 1; k++)
                {
                    info additional = new info();

                    additional.infoName = _BillFetchModelAPP.additionalInfo._objinfolist[k].infoName;
                    additional.infoValue = _BillFetchModelAPP.additionalInfo._objinfolist[k].infoValue;
                    lstInfo.Add(additional);


                }

                _BillFetchModelAPP.additionalInfo._objinfolist = lstInfo;
                _BillFetchModelAPP.billerResponse.amountOptions = lstMbiller;
                _BillFetchModel.additionalInfo = _BillFetchModelAPP.additionalInfo;
                _BillFetchModel._objbillerResponse = _BillFetchModelAPP.billerResponse;

                amountInfo amtinfo = new amountInfo();
                amtinfo.amount = _BillFetchModelAPP.amount;
                amtinfo.currency = "356";
                amtinfo.custConvFee = "0";
                //  amtinfo.amountTags = _BillFetchModelAPP.amountInfo.amountTags;

                _BillFetchModel.amountInfo = amtinfo;

                MpaymentMethod mpaymentMethod = new MpaymentMethod();
                mpaymentMethod.paymentMode = "Cash";
                mpaymentMethod.quickPay = "N";
                mpaymentMethod.splitPay = "N";
                _BillFetchModel.PaymentMethod = mpaymentMethod;

                MpaymentInfo PaymentInfo = new MpaymentInfo();
                PaymentInfo.Info = _BillFetchModelAPP.info;
                //----------------------------------------------
                _BillFetchModel.PaymentInfo = PaymentInfo;

                //Now convert the Model into XML for BiilAvenue                                             
                string _xmlresult = DALXmlSerializers.SerializeToString(_BillFetchModel);
                _finalxml = DALCommon.RemoveCompanyIdAndOtherParams(_xmlresult);
                Console.WriteLine("XMl Request:" + _finalxml);
                return _finalxml;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //dynamic input paraneters here 
        private static inputParams InputParamters(List<input> APIinputParams)
        {
            inputParams inputParams = new inputParams();

            List<input> _objlist = new List<input>();
            _objlist = APIinputParams;
            inputParams.input = _objlist;
            return inputParams;
        }
        private static additionalInfo AdditionInfo(List<info> APIinputParams)
        {
            additionalInfo inputParams = new additionalInfo();

            List<info> _objlist = new List<info>();
            _objlist = APIinputParams;
            inputParams._objinfolist = _objlist;
            return inputParams;
        }

    }
}