using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALConsolidatedPayout
    {
        public static MConsolidatedPayoutModel DALConsolidatedPayoutData(MConsolidatedPayoutModel obj)
        {
            try
            {                
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@PayoutNo", obj.Payout);
                obj .ConsolidatedPayouts= DBHelperDapper.DAAddAndReturnModelList<MConsolidatedPayoutModel>("Proc_ConsolidatedPayoutReport", queryParameters);
                return obj;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public static List<SelectListItem> BindPayoutList()
        {
            try
            {
                               
                return DBHelperDapper.DAGetDetailsInList<SelectListItem>("Proc_PayoutMasterList"); ;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


     
    }

}