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

    public class CompanyDetails
    {
        public long CompanyId { get; set; }

    }


    public class WalletService: IWallet
    {
        //call the api here to check the company wallet balance
        public ResponseModel ChkCompanyWalletBalanceService(RequestModel _obj)
        {
            try
            {                
                string _url = "CheckBalancesAmount";   //BaseUrl+API URL                       
                ResponseModel _rsponsedetails = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ResponseModel ChkMemberWalletBalanceService(RequestModel _obj)
        {
            try
            {                
                DALWalletTransaction _objgeneric = new DALWalletTransaction();
                ResponseModel _rsponsedetails = _objgeneric.CheckWalletransaction(_obj);
                return _rsponsedetails;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public WalletTransactionResponse WalletTransactionsService(WalletTransaction objWalletTrans)
        {
            WalletTransactionResponse obj = null;
            try
            {
                DALWalletTransaction _objdal = new DALWalletTransaction();
                obj = _objdal.WalletTransactions(objWalletTrans);
            }
            catch (Exception ex)
            {
                obj.Response = "Error";
            }
            return obj;
        }

        public WalletTransactionResponse WalletTransactionsService_V2(WalletTransaction objWalletTrans)
        {
            WalletTransactionResponse obj = null;
            try
            {
                DALWalletTransaction _objdal = new DALWalletTransaction();
                obj = _objdal.WalletTransactions_V2(objWalletTrans);
            }
            catch (Exception ex)
            {
                obj.Response = "Error";
            }
            return obj;
        }
    }
}