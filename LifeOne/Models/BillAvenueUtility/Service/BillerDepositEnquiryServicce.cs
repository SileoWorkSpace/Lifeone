using SGCareWeb.Models.BillAvenueUtility.API;
using SGCareWeb.Models.BillAvenueUtility.DAL;
using SGCareWeb.Models.BillAvenueUtility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGCareWeb.Models.BillAvenueUtility.Service
{
    public class BillerDepositEnquiryServicce
    {
        ~BillerDepositEnquiryServicce() { }

        ResponseModel _objResponseModel = new ResponseModel();
        public ResponseModel InvokeDepositEnquiryService(SGCareWeb.Models.BillAvenueUtility.Entity.RequestModel _obj)
        {
            try
            {
                string _url = "DepositEnquiry";
                //Calling API Here
                _objResponseModel = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                MBillDepositEnquiryResponse _responsedetails = DALCommon.CustomDeserializeObjectWithDecryptString<MBillDepositEnquiryResponse>(_objResponseModel.body);
                if (_responsedetails.responseCode == "000" || _responsedetails.responseCode == "111 ") //000 Recharge successful or 111 Recharge in process               
                    _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MBillDepositEnquiryResponse>(_responsedetails);
                else
                    _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MBillDepositEnquiryResponse>(_responsedetails);

                return _objResponseModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}