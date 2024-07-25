using LifeOne.Models.QUtility.DAL;
using LifeOne.Models.QUtility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.QUtility.Service
{
    public class RechargeHistoryService
    {
        public ResponseModel GetRecentRecharegService(RequestModel model)
        {
            try
            {                         
                ResponseModel _objmodelresponse = DALRechargeHistory.DALGetRecentChargeDetails(model);
                return _objmodelresponse;
            }
            catch (Exception ex )
            {
                throw ex;
            }
        }
    }
}