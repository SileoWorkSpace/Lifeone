using LifeOne.Models.QUtility.API;
using LifeOne.Models.QUtility.DAL;
using LifeOne.Models.QUtility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.QUtility.Service
{
    public class ElectricityBillPaymentService
    {
        public ResponseModel CallElectricityBillPaymentService(RequestModel _obj, string _url,string _ActionType, string _RechargeType)
        { 
            try
            {
                ResponseModel _rsponsedetails = null;
                DALCommon _objDALCommon = new DALCommon();
                //Insert one row as a debit on the  tblWalletDetails and alltransactionlog tables. 
                //If the transaction may be failed, will make the credit again, As we defined in the line - 32
                WalletTransactionResponse objResTrans = _objDALCommon.DALSaveTransactionDetails(_obj, "Debit", _ActionType, _RechargeType);
                if (objResTrans.Response == "Success")
                {
                    DALElectricityBillPayment _objDalElectricityBillPayment = new DALElectricityBillPayment();
                    //calling Quaere LifeOnee Perpaid API here
                    _rsponsedetails = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                    //After getting response from api side, we are storeing details on the database..as per response  3 Pending, 7 success
                    List<MElectricityBillPaymentResponse> _ElectroinicResponse = DALCommon.GetGenericRechareAPIResponseWithDecryptList<MElectricityBillPaymentResponse>(_rsponsedetails);
                    if (_ElectroinicResponse[0].ERROR == "0" && _ElectroinicResponse[0].RESULT == "0" && (_ElectroinicResponse[0].TRNXSTATUS == "7" || _ElectroinicResponse[0].TRNXSTATUS == "3"))
                        _objDalElectricityBillPayment.BBPSIntegration(_obj, _ElectroinicResponse, objResTrans);
                    else //when Transaction failed                    
                        _objDALCommon.DALSaveTransactionDetails(_obj, "Credit", _ActionType, _RechargeType);
                }
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                return DALCommon.CustomErrorhandleForBroadBandAndOtherServices(ex.ToString());
            }
        }
        public ResponseModel VerifyElectricityVerificationService(RequestModel _obj)
        {
            try
            {
                string _url = "ElectricityVerification";   //BaseUrl+API URL                       
                ResponseModel _rsponsedetails = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}