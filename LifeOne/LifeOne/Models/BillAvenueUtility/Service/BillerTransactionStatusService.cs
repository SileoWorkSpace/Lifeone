
using LifeOne.Models.BillAvenueUtility.API;
using LifeOne.Models.BillAvenueUtility.DAL;
using LifeOne.Models.BillAvenueUtility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.BillAvenueUtility.Service
{
    public class BillerTransactionStatusService
    {
        ~BillerTransactionStatusService() { }
        ResponseModel _objResponseModel = new ResponseModel();
        public ResponseModel InvokeTransactionStatusService(RequestModel _obj)
        {
            try
            {
                //get Reponse From APP Side ..Futher, we will  be stored request on the database here..
                MBillTransactionStatus _BillFetchModel = DALCommon.CustomDeserializeObjectWithDecryptString<MBillTransactionStatus>(_obj.body);
                //Now convert the Model into XML for BiilAvenue
                MBillInfoAppInformation _objMBillInfoAppInformation = new MBillInfoAppInformation();
                string _xmlresult = DALXmlSerializers.SerializeToString(_BillFetchModel);
                //Calling API Here
                MBillTransactionStatusResponse _responsedetails = GenericAPI.sendRequestToUtilityAPIPostWithCompIdAndFkMemID<MBillTransactionStatusResponse>(_xmlresult, "transactionStatus/fetchInfo/xml", _BillFetchModel.BillerId, _BillFetchModel.CompanyId, _BillFetchModel.Fk_MemID, _BillFetchModel.AndroidId, _BillFetchModel.IPAdressAPP, _BillFetchModel.DeviceId, "TRANSACTIONSTATUS", _BillFetchModel.trackValue);
                if (_responsedetails.responseCode == "000" || _responsedetails.responseCode == "111 ")
                {
                    _BillFetchModel.RequestId = _BillFetchModel.trackValue;
                    DABillTransaction.SaveBillTransRequest(_responsedetails, _BillFetchModel);

                }//000 Recharge successful   / 111 Recharge in process    

                else
                {
                    DABillTransaction.SaveBillTransResponseError(_responsedetails, _BillFetchModel);

                }


                _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MBillTransactionStatusResponse>(_responsedetails); // on success we will be added records on 
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}