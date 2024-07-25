using SGCareWeb.Models.BillAvenueUtility.API;
using SGCareWeb.Models.BillAvenueUtility.DAL;
using SGCareWeb.Models.BillAvenueUtility.Entity;
using System;
using static SGCareWeb.Models.BillAvenueUtility.Entity.DMT.MDMTRequest;

namespace SGCareWeb.Models.BillAvenueUtility.Service
{
    public class DMTRequestService
    {
        ~DMTRequestService() { }
        ResponseModel _objResponseModel = new ResponseModel();
        public ResponseModel InvokeDMTRequestService(RequestModel _obj)
        {
            MDmtServiceResponse _responsedetails = null;
            try
            {
                string _url = "DMTServiceRequest";
                //Calling API Here
                _objResponseModel = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                _responsedetails = DALCommon.CustomDeserializeObjectWithDecryptString<MDmtServiceResponse>(_objResponseModel.body);
                if (_responsedetails.ResponseCode == "000" || _responsedetails.ResponseCode == "111 ") //000 Recharge successful   / 111 Recharge in process
                   _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MDmtServiceResponse>(_responsedetails);              
                else
                _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MDmtServiceResponse>(_responsedetails);

                return _objResponseModel;
            }
            catch (Exception ex)
            {
                _responsedetails.ResponseCode = "0";
                _responsedetails.errorInfo.error.errorMessage = DALErrorManament.CommonErrorMsg + ex.ToString();
                return DALErrorManament.ErroMethod<MDmtServiceResponse>(_responsedetails);
            }
        }

        public ResponseModel InvokeDMTSenderRegistration(RequestModel _obj)
        {
            MDmtServiceResponse _responsedetails = null;
            try
            {
                //decrypt app team reponse
                MDmtServiceRequest _BillFetchModel = DALCommon.CustomDeserializeObjectWithDecryptString<MDmtServiceRequest>(_obj.body);
                string _url = "DMTRequestSenderRegistration";
                //Calling API Here
                _objResponseModel = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                _responsedetails = DALCommon.CustomDeserializeObjectWithDecryptString<MDmtServiceResponse>(_objResponseModel.body);
                if (_responsedetails.ResponseCode == "000" || _responsedetails.ResponseCode == "111 ") //000 Recharge successful   / 111 Recharge in process
                    _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MDmtServiceResponse>(_responsedetails);
                else
                    _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MDmtServiceResponse>(_responsedetails);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                _responsedetails.ResponseCode = "0";
                _responsedetails.errorInfo.error.errorMessage = DALErrorManament.CommonErrorMsg + ex.ToString();
                return DALErrorManament.ErroMethod<MDmtServiceResponse>(_responsedetails);
            }
        }

        public ResponseModel InvokeDMTVerifySender(RequestModel _obj)
        {
            MDmtServiceResponse _responsedetails = null;
            try
            {
                //decrypt app team reponse
                MDmtServiceRequest _BillFetchModel = DALCommon.CustomDeserializeObjectWithDecryptString<MDmtServiceRequest>(_obj.body);
                string _url = "DMTVerifySender";
                //Calling API Here
                _objResponseModel = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                _responsedetails = DALCommon.CustomDeserializeObjectWithDecryptString<MDmtServiceResponse>(_objResponseModel.body);
                if (_responsedetails.ResponseCode == "000" || _responsedetails.ResponseCode == "111 ") //000 Recharge successful   / 111 Recharge in process
                    _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MDmtServiceResponse>(_responsedetails);
                else
                    _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MDmtServiceResponse>(_responsedetails);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                _responsedetails.ResponseCode = "0";
                _responsedetails.errorInfo.error.errorMessage = DALErrorManament.CommonErrorMsg + ex.ToString();
                return DALErrorManament.ErroMethod<MDmtServiceResponse>(_responsedetails);
            }
        }

        public ResponseModel InvokeDMTResendOTP(RequestModel _obj)
        {
            MDmtServiceResponse _responsedetails = null;
            try
            {
                //decrypt app team reponse
                MDmtServiceRequest _BillFetchModel = DALCommon.CustomDeserializeObjectWithDecryptString<MDmtServiceRequest>(_obj.body);
                string _url = "DMTResendOTP";
                //Calling API Here
                _objResponseModel = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                _responsedetails = DALCommon.CustomDeserializeObjectWithDecryptString<MDmtServiceResponse>(_objResponseModel.body);
                if (_responsedetails.ResponseCode == "000" || _responsedetails.ResponseCode == "111 ") //000 Recharge successful   / 111 Recharge in process

                    _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MDmtServiceResponse>(_responsedetails);
                else
                  _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MDmtServiceResponse>(_responsedetails);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                _responsedetails.ResponseCode = "0";
                _responsedetails.errorInfo.error.errorMessage = DALErrorManament.CommonErrorMsg + ex.ToString();
                return DALErrorManament.ErroMethod<MDmtServiceResponse>(_responsedetails);
            }
        }

        public ResponseModel InvokeDMTAllRecipients(RequestModel _obj)
        {
            MDmtServiceResponse _responsedetails = null;
            try
            {
                //decrypt app team reponse
                MDmtServiceRequest _BillFetchModel = DALCommon.CustomDeserializeObjectWithDecryptString<MDmtServiceRequest>(_obj.body);
                string _url = "DMTAllRecipients";
                //Calling API Here
                _objResponseModel = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                _responsedetails = DALCommon.CustomDeserializeObjectWithDecryptString<MDmtServiceResponse>(_objResponseModel.body); 
                if (_responsedetails.ResponseCode == "000" || _responsedetails.ResponseCode == "111 ") //000 Recharge successful   / 111 Recharge in process
                    _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MDmtServiceResponse>(_responsedetails);
                else
                    _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MDmtServiceResponse>(_responsedetails);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                _responsedetails.ResponseCode = "0";
                _responsedetails.errorInfo.error.errorMessage = DALErrorManament.CommonErrorMsg + ex.ToString();
                return DALErrorManament.ErroMethod<MDmtServiceResponse>(_responsedetails);
            }
        }

        public ResponseModel InvokeDMTGetRecipients(RequestModel _obj)
        {
            MDmtServiceResponse _responsedetails = null;
            try
            {
                //decrypt app team reponse
                MDmtServiceRequest _BillFetchModel = DALCommon.CustomDeserializeObjectWithDecryptString<MDmtServiceRequest>(_obj.body);
                string _url = "DMTGetRecipients";
                //Calling API Here
                _objResponseModel = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                _responsedetails = DALCommon.CustomDeserializeObjectWithDecryptString<MDmtServiceResponse>(_objResponseModel.body);
                if (_responsedetails.ResponseCode == "000" || _responsedetails.ResponseCode == "111 ") //000 Recharge successful   / 111 Recharge in process
                    _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MDmtServiceResponse>(_responsedetails);
                else
                _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MDmtServiceResponse>(_responsedetails);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                _responsedetails.ResponseCode = "0";
                _responsedetails.errorInfo.error.errorMessage = DALErrorManament.CommonErrorMsg + ex.ToString();
                return DALErrorManament.ErroMethod<MDmtServiceResponse>(_responsedetails);
            }
        }

        public ResponseModel InvokeDMTAddRecipients(RequestModel _obj)
        {
            MDmtServiceResponse _responsedetails = null;
            try
            {
                //decrypt app team reponse
                MDmtServiceRequest _BillFetchModel = DALCommon.CustomDeserializeObjectWithDecryptString<MDmtServiceRequest>(_obj.body);
                string _url = "DMTAddRecipients";
                //Calling API Here
                _objResponseModel = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                _responsedetails = DALCommon.CustomDeserializeObjectWithDecryptString<MDmtServiceResponse>(_objResponseModel.body);
                if (_responsedetails.ResponseCode == "000" || _responsedetails.ResponseCode == "111 ") //000 Recharge successful   / 111 Recharge in process
                    _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MDmtServiceResponse>(_responsedetails);
                else
                 _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MDmtServiceResponse>(_responsedetails);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                _responsedetails.ResponseCode = "0";
                _responsedetails.errorInfo.error.errorMessage = DALErrorManament.CommonErrorMsg + ex.ToString();
                return DALErrorManament.ErroMethod<MDmtServiceResponse>(_responsedetails);
            }
        }

        public ResponseModel InvokeDMTDeleteRecipients(RequestModel _obj)
        {
            MDmtServiceResponse _responsedetails = null;
            try
            {
                //decrypt app team reponse
                MDmtServiceRequest _BillFetchModel = DALCommon.CustomDeserializeObjectWithDecryptString<MDmtServiceRequest>(_obj.body);
                string _url = "DMTDeleteRecipients";
                //Calling API Here
                _objResponseModel = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                _responsedetails = DALCommon.CustomDeserializeObjectWithDecryptString<MDmtServiceResponse>(_objResponseModel.body);
                if (_responsedetails.ResponseCode == "000" || _responsedetails.ResponseCode == "111 ") //000 Recharge successful   / 111 Recharge in process
                    _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MDmtServiceResponse>(_responsedetails);
                else

                    _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MDmtServiceResponse>(_responsedetails);
                return _objResponseModel;
            }           
            catch (Exception ex)
            {
                _responsedetails.ResponseCode = "0";
                _responsedetails.errorInfo.error.errorMessage = DALErrorManament.CommonErrorMsg + ex.ToString();
                return DALErrorManament.ErroMethod<MDmtServiceResponse>(_responsedetails);
            }
        }
        public ResponseModel InvokeDMTBankList(RequestModel _obj)
        {
            MDmtServiceResponse _responsedetails = null;
            try
            {
                //decrypt app team reponse
                MDmtServiceRequest _BillFetchModel = DALCommon.CustomDeserializeObjectWithDecryptString<MDmtServiceRequest>(_obj.body);
                //Create XMl for Bill Aenue
                string _url = "BankList";
                //Calling API Here
                _objResponseModel = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                _responsedetails = DALCommon.CustomDeserializeObjectWithDecryptString<MDmtServiceResponse>(_objResponseModel.body);
                if (_responsedetails.ResponseCode == "000" || _responsedetails.ResponseCode == "111 ") //000 Recharge successful   / 111 Recharge in process
                    _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MDmtServiceResponse>(_responsedetails);
                else

                _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MDmtServiceResponse>(_responsedetails);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                _responsedetails.ResponseCode = "0";
                _responsedetails.errorInfo.error.errorMessage = DALErrorManament.CommonErrorMsg + ex.ToString();
                return DALErrorManament.ErroMethod<MDmtServiceResponse>(_responsedetails);
            }
        }


    }
}