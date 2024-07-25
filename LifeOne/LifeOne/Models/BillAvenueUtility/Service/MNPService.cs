using LifeOne.Models.BillAvenueUtility.API;
using LifeOne.Models.BillAvenueUtility.DAL;
using LifeOne.Models.BillAvenueUtility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.BillAvenueUtility.Service
{
    public class MNPService
    {
        ~MNPService() { }

        ResponseModel _objResponseModel = new ResponseModel();
        public ResponseModel InvokeMNPRequestService(RequestModel _obj)
        {
            MnpResponse _responsedetails = null;
            try
            {
                //get Reponse From APP Side ..Futher, we will be stored request on the database here..
                MnpRequest _biilDetails = DALCommon.CustomDeserializeObjectWithDecryptString<MnpRequest>(_obj.body);
                string _url = "MNPRequest";
                //Calling API Here
                //_objResponseModel = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
               _responsedetails = DALCommon.CustomDeserializeObjectWithDecryptString<MnpResponse>(_objResponseModel.body);   
                if (_responsedetails.ResponseCode == "000" || _responsedetails.ResponseCode == "111 ") //000 Recharge successful   / 111 Recharge in process                
                    DALMnpRequest.SaveMNPRequest(_responsedetails, _biilDetails);
                else
                    DALMnpRequest.SaveMNPResponse(_responsedetails, _biilDetails);


                _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MnpResponse>(_responsedetails); // on success we will be added records on 
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                _responsedetails.ResponseCode = "0";
                _responsedetails.errorInfo.error.errorMessage = DALErrorManament.CommonErrorMsg + ex.ToString();
                return DALErrorManament.ErroMethod<MnpResponse>(_responsedetails);
            }
        }
    }
}