using LifeOne.Models.BillAvenues.Entity;
using LifeOne.Models.BillAvenueUtility.API;
using LifeOne.Models.BillAvenueUtility.DAL;
using LifeOne.Models.BillAvenueUtility.Entity;
using System;
using System.Collections.Generic;

namespace LifeOne.Models.BillAvenueUtility.Service
{
    public class BillerFetchService
    {
        ~BillerFetchService() { }
        ResponseModel _objResponseModel = new ResponseModel();

        public ResponseModel InvokeBillFetchService(RequestModel _obj)
        {
            MBillerFetchResponse _responsedetails = null;
            try
            {
                DALBillFetch dALBillFetch = new DALBillFetch();
                //decrypt app team reponse
                MBillerFetchRequestAPP _BillFetchModel = DALCommon.CustomDeserializeObjectWithDecryptString<MBillerFetchRequestAPP>(_obj.body);
                //Create XMl for Bill Aenue
                string _finalxml = CreateXmlForBiller(_BillFetchModel, out _finalxml);
                //Calling API   
                _responsedetails = GenericAPI.BAVsendRequestToUtilityAPIPostWithCompIdAndFkMemID<MBillerFetchResponse>(_finalxml, "extBillCntrl/billFetchRequest/xml", _BillFetchModel.BillerId, _BillFetchModel.ProjectId, _BillFetchModel.Fk_MemID, _BillFetchModel.AndroidId, _BillFetchModel.IP, _BillFetchModel.DeviceId, "BILLFETCH");


                if (_responsedetails.responseCode == "000" || _responsedetails.responseCode == "111 ") //000 Recharge successful   / 111 Recharge in process
                    dALBillFetch.SaveBillFetchRequest(_responsedetails, _BillFetchModel);
                else
                    dALBillFetch.SaveBillFetchResponseError(_responsedetails, _BillFetchModel);

                _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MBillerFetchResponse>(_responsedetails);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                _responsedetails.responseCode = "0";
                _responsedetails.errorInfo.error.errorMessage = DALErrorManament.CommonErrorMsg + ex.ToString();
                return DALErrorManament.ErroMethod<MBillerFetchResponse>(_responsedetails);
            }
        }
        //Generate XMl Here as per Bill Avenue API
        private static string CreateXmlForBiller(MBillerFetchRequestAPP _BillFetchModelAPP, out string _finalxml)
        {
            try
            {
                MBillerFetchRequest _BillFetchModel = new MBillerFetchRequest();
                _BillFetchModel.agentId = _BillFetchModelAPP.AgentId;
                agentDeviceInfo agentDeviceInfoDetails = new agentDeviceInfo();
                agentDeviceInfoDetails.ip = _BillFetchModelAPP.IP;
                agentDeviceInfoDetails.initChannel = "MOB";
                agentDeviceInfoDetails.app = _BillFetchModelAPP.app;
                agentDeviceInfoDetails.os = _BillFetchModelAPP.os;
                agentDeviceInfoDetails.imei = _BillFetchModelAPP.imei;

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

                //Now convert the Model into XML for BiilAvenue                                             
                string _xmlresult = DALXmlSerializers.SerializeToString(_BillFetchModel);
                _finalxml = DALCommon.RemoveCompanyIdAndOtherParams(_xmlresult);
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

    }
}