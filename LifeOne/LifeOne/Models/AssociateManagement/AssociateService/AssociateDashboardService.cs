using System;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.AdminManagement.ADAL;

namespace LifeOne.Models.AssociateManagement.AssociateService
{
    public class AssociateDashboardService
    {

        public dynamic AssociateDashboard(AssociateDashboardReq _obj)
        {

            dynamic result;
            try
            {
                result = DBHelperDapper.GetAsscoaiteDashboard(_obj.Fk_MemId);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public dynamic AssociateDashboardStatistics(AssociateDashboardReq _obj)
        {

            dynamic result;
            try
            {
                result = DBHelperDapper.GetAsscoaiteDashboardStatistics(_obj.Fk_MemId);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        public dynamic UIDAssociateDashboard(AssociateDashboardReq _obj)
        {

            dynamic result;
            try
            {
                result = DBHelperDapper.GetUIDAsscoaiteDashboard(_obj.Fk_MemId);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

    }
}