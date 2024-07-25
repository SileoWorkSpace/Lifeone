using SGCareWeb.Models.BillAvenueUtility.API;
using SGCareWeb.Models.BillAvenueUtility.DAL;
using SGCareWeb.Models.BillAvenueUtility.Entity;
using SGCareWeb.Models.BillAvenueUtility.Entity.DMT;
using SGCareWeb.Models.BillAvenueUtility.Entity.DMTServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGCareWeb.Models.BillAvenueUtility.Service
{
    public class DMTTransactionRequestService
    {

        ~DMTTransactionRequestService() { }
        ResponseModel _objResponseModel = new ResponseModel();
        public ResponseModel InvokeDMTTransactionRequestService(RequestModel _obj)
        {
            DmtTransactionResponse _responsedetails = null;
            try
            {
                //decrypt app team reponse
                MDmtTransactionRequest _BillFetchModel = DALCommon.CustomDeserializeObjectWithDecryptString<MDmtTransactionRequest>(_obj.body);
                string _url = "DMTTransactionRequest";
                //Calling API Here
                _objResponseModel = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                _responsedetails = DALCommon.CustomDeserializeObjectWithDecryptString<DmtTransactionResponse>(_objResponseModel.body);
                if (_responsedetails.ResponseCode == "000" || _responsedetails.ResponseCode == "111 ") //000 Recharge successful   / 111 Recharge in process
                   DALDMTServices.SaveDMTTransServiceRequest(_responsedetails, _BillFetchModel);
                else
                    DALDMTServices.SaveDMTServiceResponse(_responsedetails, _BillFetchModel);

                _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<DmtTransactionResponse>(_responsedetails);
                return _objResponseModel;

            }
            catch (Exception ex)
            {
                _responsedetails.ResponseCode = "0";
                _responsedetails.errorInfo.error.errorMessage = DALErrorManament.CommonErrorMsg + ex.ToString();
                return DALErrorManament.ErroMethod<DmtTransactionResponse>(_responsedetails);
            }
        }
        public ResponseModel InvokeDMTTransactionRefundOTPService(RequestModel _obj)
        {
            DmtTransactionResponse _responsedetails = null;
            try
            {
                //decrypt app team reponse
                MDmtTransactionRequest _BillFetchModel = DALCommon.CustomDeserializeObjectWithDecryptString<MDmtTransactionRequest>(_obj.body);
                string _url = "DMTTransactionRefundOTP";
                //Calling API Here
                _objResponseModel = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                _responsedetails = DALCommon.CustomDeserializeObjectWithDecryptString<DmtTransactionResponse>(_objResponseModel.body);
                if (_responsedetails.ResponseCode == "000" || _responsedetails.ResponseCode == "111 ") //000 Recharge successful   / 111 Recharge in process
                    DALDMTServices.SaveDMTTransRefundOTPRequest(_responsedetails, _BillFetchModel);
                else
                    DALDMTServices.SaveDMTTransRefundOTPResponse(_responsedetails, _BillFetchModel);

                _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<DmtTransactionResponse>(_responsedetails);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                _responsedetails.ResponseCode = "0";
                _responsedetails.errorInfo.error.errorMessage = DALErrorManament.CommonErrorMsg + ex.ToString();
                return DALErrorManament.ErroMethod<DmtTransactionResponse>(_responsedetails);
            }
        }

        public ResponseModel InvokeDMTFundTransfer(RequestModel _obj)
        {
            DmtTransactionResponse _responsedetails = null;
            try
            {
                //decrypt app team reponse
                MDmtTransactionRequest _BillFetchModel = DALCommon.CustomDeserializeObjectWithDecryptString<MDmtTransactionRequest>(_obj.body);
                string _url = "DMTFundTransfer";
                //Calling API Here
                _objResponseModel = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                _responsedetails = DALCommon.CustomDeserializeObjectWithDecryptString<DmtTransactionResponse>(_objResponseModel.body);
                if (_responsedetails.ResponseCode == "000" || _responsedetails.ResponseCode == "111 ") //000 Recharge successful   / 111 Recharge in process
                    DALDMTServices.SaveDMTTransFundTransferRequest(_responsedetails, _BillFetchModel);
                else
                    DALDMTServices.SaveDMTTransFundTransferResponse(_responsedetails, _BillFetchModel);

                    _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<DmtTransactionResponse>(_responsedetails);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                _responsedetails.ResponseCode = "0";
                _responsedetails.errorInfo.error.errorMessage = DALErrorManament.CommonErrorMsg + ex.ToString();
                return DALErrorManament.ErroMethod<DmtTransactionResponse>(_responsedetails);
            }
        }

        public ResponseModel InvokeDMTFundTransferMaximumValue(RequestModel _obj)
        {
            DmtTransactionResponse _responsedetails = null;
            try
            {
                //decrypt app team reponse
                MDmtTransactionRequest _BillFetchModel = DALCommon.CustomDeserializeObjectWithDecryptString<MDmtTransactionRequest>(_obj.body);
                string _url = "DMTFundTransferMaximumValue";
                //Calling API Here
                _objResponseModel = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                _responsedetails = DALCommon.CustomDeserializeObjectWithDecryptString<DmtTransactionResponse>(_objResponseModel.body);
                if (_responsedetails.ResponseCode == "000" || _responsedetails.ResponseCode == "111 ") //000 Recharge successful   / 111 Recharge in process
                    DALDMTServices.SaveDMTTransFundTransferRequest(_responsedetails, _BillFetchModel);
                else
                    DALDMTServices.SaveDMTTransFundTransferResponse(_responsedetails, _BillFetchModel);

                _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<DmtTransactionResponse>(_responsedetails);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                _responsedetails.ResponseCode = "0";
                _responsedetails.errorInfo.error.errorMessage = DALErrorManament.CommonErrorMsg + ex.ToString();
                return DALErrorManament.ErroMethod<DmtTransactionResponse>(_responsedetails);
            }
        }

        public ResponseModel InvokeDMTMultiTransactionService(RequestModel _obj)
        {
            DmtTransactionResponse _responsedetails = null;
            try
            {
                //decrypt app team reponse
                MDmtTransactionRequest _BillFetchModel = DALCommon.CustomDeserializeObjectWithDecryptString<MDmtTransactionRequest>(_obj.body);
                string _url = "DMTMultiTransaction";
                //Calling API Here
                _objResponseModel = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                _responsedetails = DALCommon.CustomDeserializeObjectWithDecryptString<DmtTransactionResponse>(_objResponseModel.body);
                if (_responsedetails.ResponseCode == "000" || _responsedetails.ResponseCode == "111 ") //000 Recharge successful   / 111 Recharge in process
                    _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<DmtTransactionResponse>(_responsedetails);
                else
                    // DALBillFetch.SaveBillFetchResponseError(_responsedetails, _BillFetchModel);

                    _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<DmtTransactionResponse>(_responsedetails);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                _responsedetails.ResponseCode = "0";
                _responsedetails.errorInfo.error.errorMessage = DALErrorManament.CommonErrorMsg + ex.ToString();
                return DALErrorManament.ErroMethod<DmtTransactionResponse>(_responsedetails);
            }
        }

        public ResponseModel InvokeDMTGetCustomerConvFeeService(RequestModel _obj)
        {
            DmtTransactionResponse _responsedetails = null;
            try
            {
                //decrypt app team reponse
                MDmtTransactionRequest _BillFetchModel = DALCommon.CustomDeserializeObjectWithDecryptString<MDmtTransactionRequest>(_obj.body);
                string _url = "DMTGetCustomerConvFee";
                //Calling API Here
                _objResponseModel = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                _responsedetails = DALCommon.CustomDeserializeObjectWithDecryptString<DmtTransactionResponse>(_objResponseModel.body);
                if (_responsedetails.ResponseCode == "000" || _responsedetails.ResponseCode == "111 ") //000 Recharge successful   / 111 Recharge in process
                    _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<DmtTransactionResponse>(_responsedetails);
                else
                    // DALBillFetch.SaveBillFetchResponseError(_responsedetails, _BillFetchModel);

                    _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<DmtTransactionResponse>(_responsedetails);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                _responsedetails.ResponseCode = "0";
                _responsedetails.errorInfo.error.errorMessage = DALErrorManament.CommonErrorMsg + ex.ToString();
                return DALErrorManament.ErroMethod<DmtTransactionResponse>(_responsedetails);
            }
        }

    }
}