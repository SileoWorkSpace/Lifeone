using LifeOne.Models.QUtility.API;
using LifeOne.Models.QUtility.DAL;
using LifeOne.Models.QUtility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.QUtility.Service
{
    public class GasBillPaymentService
    {
        public ResponseModel CallGasBillPaymentService(RequestModel _obj, string _url,string _ActionType, string _RechargeType)
        {
            try
            {
                ResponseModel _rsponsedetails = null;
                DALCommon _objDALCommon = new DALCommon();
                //Insert one row as a debit on the  tblWalletDetails and alltransactionlog tables. 
                //If the transaction may be failed, will make the credit again, As we defined in the line - 36
                WalletTransactionResponse objResTrans = _objDALCommon.DALSaveTransactionDetails(_obj, "Debit", _ActionType, _RechargeType);
                if (objResTrans.Response == "Success")
                {
                    DALGasBillPayment _objDalPrepaid = new DALGasBillPayment();
                    //calling Quaere LifeOnee Perpaid API here
                    _rsponsedetails = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                    //After getting response from api side, we are storeing details on the database..as per response  3 Pending, 7 success
                    List<MGasBillPaymentResponse> _GasBillPaymentResponse = DALCommon.GetGenericRechareAPIResponseWithDecryptList<MGasBillPaymentResponse>(_rsponsedetails);
                    if (_GasBillPaymentResponse[0].ERROR == "0" && _GasBillPaymentResponse[0].RESULT == "0" && (_GasBillPaymentResponse[0].TRNXSTATUS == "7" || _GasBillPaymentResponse[0].TRNXSTATUS == "3"))
                        _objDalPrepaid.BBPSIntegration(_obj, _GasBillPaymentResponse, objResTrans);
                    else //when Transaction failed                    
                        _objDALCommon.DALSaveTransactionDetails(_obj, "Credit", _ActionType, _RechargeType);
                }
                return _rsponsedetails;
            }
            catch (Exception ex )
            {
                return DALCommon.CustomErrorhandleForBroadBandAndOtherServices(ex.ToString());
            }
        }
        public ResponseModel VerifyGasService(RequestModel _obj)
        {
            try
            {
                string _url = "GasPaymentVerification";   //BaseUrl+API URL                       
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