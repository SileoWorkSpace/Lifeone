using LifeOne.Models.QUtility.API;
using LifeOne.Models.QUtility.DAL;
using LifeOne.Models.QUtility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.QUtility.Service
{
    public class BeneficiaryService
    {
        public ResponseModel CustomerVerificationService(RequestModel _obj)
        {
            try
            {
                string _url = "CustomerVerification";   //BaseUrl+API URL                       
                ResponseModel _rsponsedetails = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ResponseModel RemitterRegistrationService(RequestModel _obj)
        {
            try
            {
                string _url = "RemitterRegistration";   //BaseUrl+API URL                       
                ResponseModel _rsponsedetails = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ResponseModel RemitterRegistrationValidationOTPService(RequestModel _obj)
        {
            try
            {
                string _url = "RemitterRegistrationValidationOTP";   //BaseUrl+API URL                       
                ResponseModel _rsponsedetails = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ResponseModel BeneficiaryRegistrationService(RequestModel _obj)
        {
            try
            {
                string _url = "BeneficiaryRegistration";   //BaseUrl+API URL                       
                ResponseModel _rsponsedetails = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ResponseModel AddBeneficiaryDetilsService(RequestModel _obj)
        {
            try
            {
                string _url = "AddBeneficiaryDetils";   //BaseUrl+API URL                       
                ResponseModel _rsponsedetails = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                AddBeneficiaryDetilsResponse _addBeneficiaryDetilsesponse = DALCommon.CustomDeserializeObjectWithDecryptString<AddBeneficiaryDetilsResponse>(_rsponsedetails.body);
                if (_addBeneficiaryDetilsesponse.Msg == "Success")
                {
                    DALBeneficiary _dalBeneficiary = new DALBeneficiary();
                    _dalBeneficiary.DALAddBeneficiaryDetils(_obj);
                }
                return _rsponsedetails;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ResponseModel FundTransfer(RequestModel _obj)
        {
            try
            {
                //calling API here
                string _url = "FundTransfer";   //BaseUrl+API URL                       
                ResponseModel _rsponsedetails = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);

                List<FundTransferResponse> _objResponse = DALCommon.GetGenericRechareAPIResponseWithDecryptList<FundTransferResponse>(_rsponsedetails);
                LifeOne.Models.LifeOnee.WalletModel _objRequest = DALCommon.CustomDeserializeObjectWithDecryptString<LifeOne.Models.LifeOnee.WalletModel>(_obj.body);
                if (_objResponse[0].TRANSID != "")
                {
                    _objRequest.CreditAmount = 0;
                    _objRequest.AddInfo = _objResponse[0].ADDINFO;
                    _objRequest.DebitAmount = Convert.ToDecimal(_objRequest.AMOUNT);
                    _objRequest.TransNo = _objResponse[0].TRANSID;
                    _objRequest.TransDate = _objResponse[0].DATE;
                    _objRequest.TransAmount = Convert.ToDecimal(_objRequest.AMOUNT);
                    _objRequest.TransStatus = _objResponse[0].TRNXSTATUS;
                    _objRequest.AuthCode = _objResponse[0].AUTHCODE;
                    _objRequest.ErrorMsg = _objResponse[0].ERROR;
                    _objRequest.ERROR = _objResponse[0].ERROR;
                    _objRequest.RESULT = _objResponse[0].RESULT;
                    _objRequest.TransDate = _objResponse[0].DATE;
                    DALBeneficiary _dalBeneficiary = new DALBeneficiary();
                    _dalBeneficiary.AddWalletAmount(_objRequest);
                }
                return _rsponsedetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ResponseModel FundTransferLogService(RequestModel _obj)
        {
            try
            {
                DALBeneficiary _dalBeneficiary = new DALBeneficiary();
                ResponseModel _rsponsedetails = _dalBeneficiary.DALFundTransferLogResponse(_obj);


                return _rsponsedetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ResponseModel GetBeneficiaryDetilService(RequestModel _obj)
        {
            try
            {
                DALBeneficiary _dalBeneficiary = new DALBeneficiary();
                ResponseModel _rsponsedetails = _dalBeneficiary.GetBeneficiaryDetils(_obj);               
                return _rsponsedetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ResponseModel BeneficiaryDeleteService(RequestModel _obj)
        {
            try
            {
                DALBeneficiary _dalBeneficiary = new DALBeneficiary();
                string _url = "BeneficiaryDelete";   //BaseUrl+API URL                       
                ResponseModel _rsponsedetails = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                BeneficiaryDeleteResponse _addBeneficiaryDetilsesponse = DALCommon.CustomDeserializeObjectWithDecryptString<BeneficiaryDeleteResponse>(_rsponsedetails.body);
              //  ResponseModel _rsponsedetails = _dalBeneficiary.GetBeneficiaryDetils(_obj);
                return _rsponsedetails;
            }
            catch (Exception ex )
            {
                throw ex;
            }
        }
    }
}