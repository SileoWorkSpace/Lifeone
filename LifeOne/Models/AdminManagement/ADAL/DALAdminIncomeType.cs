using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALAdminIncomeType
    {

        public static List<MAdminIncomeTypeMaster> GetData(MAdminIncomeTypeMaster obj)
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

                queryParameters.Add("@PlanId", obj.Pk_IncomeTypeId);
                
                List<MAdminIncomeTypeMaster> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminIncomeTypeMaster>("GetPlan", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static ResponseIncomeTypeMasterModel SaveIncomeType(MAdminIncomeTypeMaster obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@PK_PlanId", obj.Pk_IncomeTypeId);
                queryParameters.Add("@FK_Utility", obj.Fk_UtilityId);
                queryParameters.Add("@PlanName", obj.IncomeTypeName);
                queryParameters.Add("@UserPlanName", obj.IncomeTypeName);
                queryParameters.Add("@Active", obj.IsActive==true?1:0);
                queryParameters.Add("@CreatedBy", obj.CreatedBy);
                queryParameters.Add("@ProcId", obj.ProcId);
                
                ResponseIncomeTypeMasterModel _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseIncomeTypeMasterModel>("planmaster", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}