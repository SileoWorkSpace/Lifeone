using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALAdminPlanIncome
    {
        public static List<MAdminPlanIncome> GetData(MAdminPlanIncome obj)
        {
            try
            {
                if (obj.Page == 0)
                {
                    obj.Page = 1;
                }
                if (obj.Size == 0)
                {
                    obj.Size = 100;
                }
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@PK_PlanIncomeID", obj.Pk_PlanIncomeId);

                List<MAdminPlanIncome> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminPlanIncome>("GetPlanIncome", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static ResponsePlanIncomeModel SavePlanIncome(MAdminPlanIncome obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@Pk_PlanIncomeId", obj.Pk_PlanIncomeId);
                queryParameters.Add("@Fk_PlanId", obj.Fk_PlanId);
                queryParameters.Add("@FK_ProductId", obj.FK_ProductId);
                queryParameters.Add("@IncomeValue", obj.IncomeValue);
                queryParameters.Add("@IsPercentage", obj.IsPercentage == true ? 1 : 0);
                queryParameters.Add("@Creatatedby", obj.CreatedBy);
                queryParameters.Add("@ProcId", obj.ProcId);

                ResponsePlanIncomeModel _iresult = DBHelperDapper.DAAddAndReturnModel<ResponsePlanIncomeModel>("PlanIncome", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}