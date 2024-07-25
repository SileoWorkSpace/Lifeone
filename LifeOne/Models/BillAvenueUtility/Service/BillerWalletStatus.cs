using LifeOne.Models.BillAvenueUtility.API;
using LifeOne.Models.BillAvenueUtility.DAL;
using LifeOne.Models.BillAvenueUtility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.BillAvenueUtility.Service
{
    public class BillerWalletStatus
    {
        ~BillerWalletStatus() { }
        ResponseModel _objResponseModel = new ResponseModel();

        public ResponseModel InvokeBillerWalletStatus(RequestModel _obj)
        {
            MBillerWalletResponse _responsedetails = null;
            try
            {
                //decrypt app team reponse
                MBillerWalletSatus _BillFetchModel = DALCommon.CustomDeserializeObjectWithDecryptString<MBillerWalletSatus>(_obj.body);
                //Create XMl for Bill Aenue
                string _finalxml = DALXmlSerializers.SerializeToString(_BillFetchModel);
                //Calling API   
               
                _responsedetails= DALWalletStatus.CheckWalletStatus(_BillFetchModel);

                    _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MBillerWalletResponse>(_responsedetails);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                _responsedetails.ResponseCode = "0";
                _responsedetails.errorInfo.error.errorMessage = DALErrorManament.CommonErrorMsg + ex.ToString();
                return DALErrorManament.ErroMethod<MBillerWalletResponse>(_responsedetails);
            }
        }
    }
}