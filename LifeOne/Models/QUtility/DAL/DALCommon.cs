using LifeOne.Models.Common;
using LifeOne.Models.QUtility.Entity;
using LifeOne.Models.QUtility.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.QUtility.DAL
{

    public class DALCommon
    {
        public WalletTransactionResponse DALSaveTransactionDetails(RequestModel _obj, string _Action, string _ActionType, string _RechargeType)
        {
            try
            {
                string _dataRequestModel = ApiEncrypt_Decrypt.DecryptString(DBHelper.Aeskey, _obj.body);
                PrepaidAPI para = JsonConvert.DeserializeObject<PrepaidAPI>(_dataRequestModel);
                WalletTransaction _objWalletTransaction = new WalletTransaction();
                _objWalletTransaction.Fk_MemId = para.FK_MemId;
                _objWalletTransaction.Amount = para.AMOUNT;
                _objWalletTransaction.Operator = para.Provider;
                _objWalletTransaction.Number = para.NUMBER;
                _objWalletTransaction.ActionType = _ActionType;//"Prepaid Recharge";
                _objWalletTransaction.Action = _Action;
                _objWalletTransaction.RechargeType = _RechargeType;// "PrepaidRecharge";
                WalletService _objwalletservice = new WalletService();
                WalletTransactionResponse objResTrans = _objwalletservice.WalletTransactionsService(_objWalletTransaction);
                return objResTrans;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public WalletTransactionResponse DALSaveTransactionDetails_V2(RequestModel _obj, string _Action, string _ActionType, string _RechargeType)
        {
            try
            {
                string _dataRequestModel = ApiEncrypt_Decrypt.DecryptString(DBHelper.Aeskey, _obj.body);
                PrepaidAPI para = JsonConvert.DeserializeObject<PrepaidAPI>(_dataRequestModel);
                WalletTransaction _objWalletTransaction = new WalletTransaction();
                _objWalletTransaction.Fk_MemId = para.FK_MemId;
                _objWalletTransaction.Amount = para.AMOUNT;
                _objWalletTransaction.Operator = para.Provider;
                _objWalletTransaction.Number = para.NUMBER;
                _objWalletTransaction.ActionType = _ActionType;//"Prepaid Recharge";
                _objWalletTransaction.Action = _Action;
                _objWalletTransaction.RechargeType = _RechargeType;// "PrepaidRecharge";
                WalletService _objwalletservice = new WalletService();
                WalletTransactionResponse objResTrans = _objwalletservice.WalletTransactionsService_V2(_objWalletTransaction);
                return objResTrans;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<T> GetGenericRechareAPIResponseWithDecryptList<T>(ResponseModel _obj)
        {
            try
            {
                string _date = ApiEncrypt_Decrypt.DecryptString(DBHelper.Aeskey, _obj.body);
                List<T> _objPrepaidResponse = JsonConvert.DeserializeObject<List<T>>(_date);
                return _objPrepaidResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static T CustomDeserializeObjectWithDecryptString<T>(string _body)
        {
            try
            {
                string _dateresponse = ApiEncrypt_Decrypt.DecryptString(DBHelper.Aeskey, _body);
                T _finalResponse = JsonConvert.DeserializeObject<T>(_dateresponse);
                return _finalResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string CustomSerializeObjectWithEncryptString<T>(T _obj)
        {
            try
            {
                string _sResult = JsonConvert.SerializeObject(_obj);
                string _encryptedText = ApiEncrypt_Decrypt.EncryptString(DBHelper.Aeskey, _sResult);
                return _encryptedText;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string CustomSerializeObjectWithEncryptList<T>(List<T> _obj)
        {
            try
            {
                string _sResult = JsonConvert.SerializeObject(_obj);
                string _encryptedText = ApiEncrypt_Decrypt.EncryptString(DBHelper.Aeskey, _sResult);
                return _encryptedText;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static ResponseModel CustomErrorhandleForPreaidAndOtherServices(string _errormessage)
        {
            try
            {
                List<PrepaidResponse> _objlist = new List<PrepaidResponse>();
                PrepaidResponse _objmodel = new PrepaidResponse();
                ResponseModel _objResponseModel = new ResponseModel();
                _objmodel.ERROR = "Custom Error: " + _errormessage + " Please Contact To Admin.";
                _objmodel.Status = "0";
                _objlist.Add(_objmodel);
                string _sResult = JsonConvert.SerializeObject(_objlist);
                string _encryptedText = ApiEncrypt_Decrypt.EncryptString(DBHelper.Aeskey, _sResult);
                _objResponseModel.body = _encryptedText;
                return _objResponseModel;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public static ResponseModel CustomErrorhandleForPreaidAndOtherServices(string _errormessage, string _funtionName)
        {
            try
            {
                List<PrepaidResponse> _objlist = new List<PrepaidResponse>();
                PrepaidResponse _objmodel = new PrepaidResponse();
                ResponseModel _objResponseModel = new ResponseModel();
                _objmodel.ERROR = "Custom Error: " + _errormessage + " Please Contact To Admin. " + _funtionName;
                _objmodel.Status = "0";
                _objlist.Add(_objmodel);
                string _sResult = JsonConvert.SerializeObject(_objlist);
                string _encryptedText = ApiEncrypt_Decrypt.EncryptString(DBHelper.Aeskey, _sResult);
                _objResponseModel.body = _encryptedText;
                return _objResponseModel;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public static ResponseModel CustomErrorhandleForBroadBandAndOtherServices(string _errormessage)
        {
            try
            {
                List<MBroadBandProviderResponse> _objlist = new List<MBroadBandProviderResponse>();
                MBroadBandProviderResponse _objmodel = new MBroadBandProviderResponse();
                ResponseModel _objResponseModel = new ResponseModel();
                _objmodel.ERROR = "Custom Error: " + _errormessage + " Please Contact To Admin.";
                _objmodel.Status = "0";
                _objlist.Add(_objmodel);
                string _sResult = JsonConvert.SerializeObject(_objlist);
                string _encryptedText = ApiEncrypt_Decrypt.EncryptString(DBHelper.Aeskey, _sResult);
                _objResponseModel.body = _encryptedText;
                return _objResponseModel;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}