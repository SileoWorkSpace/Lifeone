using LifeOne.Models.BillAvenueUtility.API;
using LifeOne.Models.BillAvenueUtility.DAL;
using LifeOne.Models.BillAvenueUtility.Entity;
using LifeOne.Models.BillAvenueUtility.Entity.PrepaidRecharge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.BillAvenueUtility.Service
{
    public class PrepaidPlanService
    {
        ~PrepaidPlanService() { }

        ResponseModel _objResponseModel = new ResponseModel();
        //public ResponseModel InvokePrepaidPlanDetailsService(RequestModel _obj)
        //{
        //    try
        //    {
        //        string _url = "PREPAIDPLANS";   //API URL                       
        //        ResponseModel _rsponsedetails = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
        //        return _rsponsedetails;
        //    }
        //    catch (Exception ex)
        //    {
        //        _objResponseModel.body = MCommonErrorAndSuccessMsg.ErrorMsg;
        //        return _objResponseModel;
        //    }
        //}
    }
}