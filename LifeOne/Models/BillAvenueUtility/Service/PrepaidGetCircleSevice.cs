using SGCareWeb.Models.BillAvenueUtility.API;
using SGCareWeb.Models.BillAvenueUtility.DAL;
using SGCareWeb.Models.BillAvenueUtility.Entity;
using SGCareWeb.Models.BillAvenueUtility.Entity.PrepaidRecharge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGCareWeb.Models.BillAvenueUtility.Service
{
    public class PrepaidGetCircleSevice
    {
        ~PrepaidGetCircleSevice() { }
        ResponseModel _objResponseModel = new ResponseModel();
        public ResponseModel InvokeGetPrepaidCircleService(RequestModel _obj)
        {
            try
            {
                string _url = "GetAllPrepaidCircle";   //API URL                              
                ResponseModel _rsponsedetails = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}