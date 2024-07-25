using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Models.AdminManagement.AService
{
    public class ConsolidatedPayoutService
    {

        public static MConsolidatedPayoutModel GetConsolidatedPayoutService(MConsolidatedPayoutModel _param)
        {
            try
            {
                _param = DALConsolidatedPayout.DALConsolidatedPayoutData(_param);
                return _param;
            }
            catch (Exception)
            {

                throw;
            }
            
       
        }
        public static List<SelectListItem> GetAndBindPayoutDDl()
        {
            try
            {
                List<SelectListItem> _objlist = new List<SelectListItem>();
                _objlist = DALConsolidatedPayout.BindPayoutList();
                return _objlist;
            }
            catch (Exception)
            {

                throw;
            }


        }

    }
}