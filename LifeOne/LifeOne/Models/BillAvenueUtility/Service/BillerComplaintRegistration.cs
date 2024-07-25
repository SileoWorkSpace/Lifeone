using SGCareWeb.Models.BillAvenueUtility.API;
using SGCareWeb.Models.BillAvenueUtility.DAL;
using SGCareWeb.Models.BillAvenueUtility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGCareWeb.Models.BillAvenueUtility.Service
{
    public class BillerComplaintRegistration
    {
        ~BillerComplaintRegistration() { }

        ResponseModel _objResponseModel = new ResponseModel();
        public ResponseModel InvokeBillComplaintRegistrationService(RequestModel _obj)
        {
            try
            {
                //get Reponse From APP Side ..Futher, we will be stored request on the database here..
                MBillComplaintRegistration _biilId = DALCommon.CustomDeserializeObjectWithDecryptString<MBillComplaintRegistration>(_obj.body);
                //Now convert the Model into XML for BillAvenue
                MBillInfoAppInformation _objMBillInfoAppInformation = new MBillInfoAppInformation();
                string _url = "COMPLAINTREGISTRATION";
                //Calling API Here
                _objResponseModel = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                MBillComplaintRegistrationResponse _responsedetails = DALCommon.CustomDeserializeObjectWithDecryptString<MBillComplaintRegistrationResponse>(_objResponseModel.body);

                if (_responsedetails.responseCode == "000" || _responsedetails.responseCode == "111 ") //000 Recharge successful   / 111 Recharge in process
                    DALComplain.SaveComplainRegRequest(_responsedetails, _biilId);
                else
                    DALComplain.SaveComplainRegResponse(_responsedetails, _biilId);

                _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MBillComplaintRegistrationResponse>(_responsedetails);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return DALErrorManament.ErroMethodstring(ex.Message.ToString());
            }
        }

        public ResponseModel InvokeBillComplaintTrackingService(RequestModel _obj)
        {
            try
            {
                //get Reponse From APP Side ..Futher, we will be stored request on the database here..
                MBillComplaintTracking _biilId = DALCommon.CustomDeserializeObjectWithDecryptString<MBillComplaintTracking>(_obj.body);
                //Now convert the Model into XML for BillAvenue
                MBillInfoAppInformation _objMBillInfoAppInformation = new MBillInfoAppInformation();
                string _xmlresult = DALXmlSerializers.SerializeToString(_biilId);
                //entcrypt XML here as per BillAvenue Req..                    
                string _url = "COMPLAINTTRACKING";
                //Calling API Here
                _objResponseModel = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                MBillComplaintTrackingResponse _responsedetails = DALCommon.CustomDeserializeObjectWithDecryptString<MBillComplaintTrackingResponse>(_objResponseModel.body);
                if (_responsedetails.responseCode == "000" || _responsedetails.responseCode == "111 ") //000 Recharge successful   / 111 Recharge in process
                    DALComplain.SaveComplainTrackingRequest(_responsedetails, _biilId);
                else
                    DALComplain.SaveComplainTrackingResponse(_responsedetails, _biilId);

                _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MBillComplaintTrackingResponse>(_responsedetails);

                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return DALErrorManament.ErroMethodstring(ex.Message.ToString());
            }
        }
    }
}