using LifeOne.Models.BillAvenueUtility.API;
using LifeOne.Models.BillAvenueUtility.DAL;
using LifeOne.Models.BillAvenueUtility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.BillAvenueUtility.Service
{
    public class TransactionHistoryService
    {

        ~TransactionHistoryService() { }
        ResponseModel _objResponseModel = new ResponseModel();
        public ResponseModel TransactionHistory(RequestModel _obj)
        {
            try
            {
                string _url = "TransacationHistory";   //API URL                              
                _objResponseModel = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
           
            }
       
        }
    }
}