using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
    public class UtilityService
    {
        ~UtilityService() { }        
        public List<MAdminUntility> GetUtilityService(MAdminUntility _obj)
        {
            List<MAdminUntility> _objResponseModel = new List<MAdminUntility>();
            try
            {               
                //decrypt app team reponse                
                _objResponseModel = DALAdminUtility.GetData(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public ResponseUtilityModel SaveUtilityService(MAdminUntility _obj)
        {
            ResponseUtilityModel _objResponseModel = new ResponseUtilityModel();
            try
            {                
                _objResponseModel = DALAdminUtility.SaveUtility(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public static DynamicUtilityResponseModel BindAllUtilityMaster()
        {
            return DALAdminUtility.BindAllUtilityMaster();
        }
        public static DynamicUtilityResponseModel ChangeUtilityMasterStatus(DynamicUtilityModel req)
        {
            return DALAdminUtility.ChangeUtilityMasterStatus(req);
        }
        public static DynamicUtilityResponseModel ChangeCMDStartDateTime(DynamicUtilityModel req)
        {
            return DALAdminUtility.ChangeCMDStartDateTime(req);
        }
        public static List<Member_RefundAmountModel> RefundAmount(Member_RefundAmountModel req)
        {
            return DALAdminUtility.RefundAmount(req);
        }
        public static Member_RefundAmountModel AddRefundAmount(Member_RefundAmountModel req)
        {
            return DALAdminUtility.AddRefundAmount(req);
        }
    }
}