using LifeOne.Models.FranchiseManagement.FDAL;
using LifeOne.Models.FranchiseManagement.FEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.FranchiseManagement.FService
{
    public class AssignShippingService
    {

        public static List<MAssignShippingInfo> AssignOrderShippingService(MAssignShippingInfo _obj)
        {
            List<MAssignShippingInfo> _objResponseModel = new List<MAssignShippingInfo>();
            try
            {

                _obj.FranchiseId = SessionManager.FranchiseFk_MemId;
                _objResponseModel = DALFranchiseAssignShipping.GetChildFranchiseList(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public static List<MAssignShippingInfo> ViewAssignProductDetailsService(MAssignShippingInfo _obj)
        {
            List<MAssignShippingInfo> _objResponseModel = new List<MAssignShippingInfo>();
            try
            {
                _obj.FranchiseId = SessionManager.FranchiseFk_MemId;
                _objResponseModel = DALFranchiseAssignShipping.ViewAssignOrderProduct(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}