using LifeOne.Models.QUtility.API;
using LifeOne.Models.QUtility.DAL;
using LifeOne.Models.QUtility.Entity;
using LifeOne.Models.QUtility.InterFace;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.QUtility.Service
{
    public class PrepaidService: IPrepaid
    {   //Mobile Recharge, DTH, Brodband, 
        public ResponseModel CallPrepaidAndOtherRechargeService(RequestModel _obj,string _url, string _ActionType,string _RechargeType)
        {
            DALCommon _objDALCommon = new DALCommon();
            try
            {
                ResponseModel _rsponsedetails = null;                                
                //Insert one row as a debit on the tblPrepaidMobileRecharge, tblWalletDetails and alltransactionlog tables. 
                //If the transaction may be failed, will make the credit again, As we defined in the line - 36
                WalletTransactionResponse objResTrans = _objDALCommon.DALSaveTransactionDetails(_obj,"Debit",_ActionType, _RechargeType);
                if (objResTrans.Response == "Success")
                {
                    DALPrepaid _objDalPrepaid = new DALPrepaid();                                       
                    //calling Quaere LifeOnee Perpaid API here
                    _rsponsedetails = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                    //After getting response from api side, we are storeing details on the database..as per response  3 Pending, 7 success
                    List<PrepaidResponse> PrepaidResponse = DALCommon.GetGenericRechareAPIResponseWithDecryptList<PrepaidResponse>(_rsponsedetails);
                    if (PrepaidResponse.Count > 0)
                    {
                        if (PrepaidResponse[0].ERROR == "0" && PrepaidResponse[0].RESULT == "0" && (PrepaidResponse[0].TRNXSTATUS == "7" || PrepaidResponse[0].TRNXSTATUS == "3"))
                            _objDalPrepaid.PrepaidRecharge(_obj, PrepaidResponse, objResTrans);
                        else                             
                            //when Transaction failed                      
                            _objDALCommon.DALSaveTransactionDetails(_obj, "Credit", _ActionType, _RechargeType);
                    }
                    else
                    {
                        _objDALCommon.DALSaveTransactionDetails(_obj, "Credit", _ActionType, _RechargeType);
                    }                                      
                }
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
               return DALCommon.CustomErrorhandleForPreaidAndOtherServices(ex.ToString(), "DALSaveTransactionDetails");                
            }
        }

        /*Online*/

        public ResponseModel CallPrepaidAndOtherRechargeGatewayService(RequestModel _obj, string _url, string _ActionType, string _RechargeType)
        {
            DALCommon _objDALCommon = new DALCommon();
            try
            {
                ResponseModel _rsponsedetails = null;          
                WalletTransactionResponse objResTrans = _objDALCommon.DALSaveTransactionDetails(_obj, "Debit", _ActionType, _RechargeType);
                if (objResTrans.Response == "Success")
                {
                    DALPrepaid _objDalPrepaid = new DALPrepaid();
                    //calling Quaere LifeOnee Perpaid API here
                    _rsponsedetails = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                    //After getting response from api side, we are storeing details on the database..as per response  3 Pending, 7 success
                    List<PrepaidResponse> PrepaidResponse = DALCommon.GetGenericRechareAPIResponseWithDecryptList<PrepaidResponse>(_rsponsedetails);
                    if (PrepaidResponse.Count > 0)
                    {
                        if (PrepaidResponse[0].ERROR == "0" && PrepaidResponse[0].RESULT == "0" && (PrepaidResponse[0].TRNXSTATUS == "7" || PrepaidResponse[0].TRNXSTATUS == "3"))

                            _objDalPrepaid.PrepaidRecharge(_obj, PrepaidResponse, objResTrans);
                        else //when Transaction failed                      
                            _objDALCommon.DALSaveTransactionDetails(_obj, "Credit", _ActionType, _RechargeType);
                    }
                    else
                    {
                        _objDALCommon.DALSaveTransactionDetails(_obj, "Credit", _ActionType, _RechargeType);
                    }
                }
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                return DALCommon.CustomErrorhandleForPreaidAndOtherServices(ex.ToString(), "DALSaveTransactionDetails");
            }
        }


        public ResponseModel CallPrepaidAndOtherRechargeService_V2(RequestModel _obj, string _url, string _ActionType, string _RechargeType)
        {
            DALCommon _objDALCommon = new DALCommon();
            try
            {
                ResponseModel _rsponsedetails = null;
                //Insert one row as a debit on the tblPrepaidMobileRecharge, tblWalletDetails and alltransactionlog tables. 
                //If the transaction may be failed, will make the credit again, As we defined in the line - 36
                WalletTransactionResponse objResTrans = _objDALCommon.DALSaveTransactionDetails_V2(_obj, "Debit", _ActionType, _RechargeType);
                if (objResTrans.Response == "Success")
                {
                    DALPrepaid _objDalPrepaid = new DALPrepaid();
                    //calling Quaere LifeOnee Perpaid API here
                    _rsponsedetails = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                    //After getting response from api side, we are storeing details on the database..as per response  3 Pending, 7 success
                    List<PrepaidResponse> PrepaidResponse = DALCommon.GetGenericRechareAPIResponseWithDecryptList<PrepaidResponse>(_rsponsedetails);
                    if (PrepaidResponse.Count > 0)
                    {
                        if (PrepaidResponse[0].ERROR == "0" && PrepaidResponse[0].RESULT == "0" && (PrepaidResponse[0].TRNXSTATUS == "7" || PrepaidResponse[0].TRNXSTATUS == "3"))

                            _objDalPrepaid.PrepaidRecharge(_obj, PrepaidResponse, objResTrans);
                        else //when Transaction failed                      
                            _objDALCommon.DALSaveTransactionDetails_V2(_obj, "Credit", _ActionType, _RechargeType);
                    }
                    else
                    {
                        _objDALCommon.DALSaveTransactionDetails_V2(_obj, "Credit", _ActionType, _RechargeType);
                    }
                }
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                return DALCommon.CustomErrorhandleForPreaidAndOtherServices(ex.ToString(), "DALSaveTransactionDetails");
            }
        }
    }
}