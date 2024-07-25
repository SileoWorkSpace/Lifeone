using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
    public class PlanIncomeService
    {
        public List<MAdminPlanIncome> GetPlanIncomeService(MAdminPlanIncome _obj)
        {
            List<MAdminPlanIncome> _objResponseModel = new List<MAdminPlanIncome>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALAdminPlanIncome.GetData(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public ResponsePlanIncomeModel SavePlanIncomeService(MAdminPlanIncome _obj)
        {
            ResponsePlanIncomeModel _objResponseModel = new ResponsePlanIncomeModel();
            try
            {
                _objResponseModel = DALAdminPlanIncome.SavePlanIncome(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

    }
}