using Dapper;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
   

    public class DALAdminTermsCondition
    {
        public static ResponseTermsCondition SaveTermsandCondition(AdminTermsCondition obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@Pk_Id", obj.Pk_Id);
                queryParameters.Add("@TermsandCondition", obj.TermsandCondition);
                queryParameters.Add("@CreatedBy", obj.CreatedBy);

                ResponseTermsCondition _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseTermsCondition>("UploadTermsCondition", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static ResponseTermsCondition UpdateTermsandCondition(AdminTermsCondition obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@Pk_Id", obj.Pk_Id);
                queryParameters.Add("@TermsandCondition", obj.TermsandCondition);
                queryParameters.Add("@Updatedby", SessionManager.Fk_MemId);                
                ResponseTermsCondition _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseTermsCondition>("TermsandConditionUpdate", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static List<AdminTermsCondition> GetTermsConditionList(AdminTermsCondition obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();               
                List<AdminTermsCondition> _iresult = DBHelperDapper.DAAddAndReturnModelList<AdminTermsCondition>("TermsandConditionList", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static AdminTermsCondition GetTermsConditionEdit(AdminTermsCondition obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Pk_Id", obj.Pk_Id);
                AdminTermsCondition _iresult = DBHelperDapper.DAAddAndReturnModel<AdminTermsCondition>("TermsandConditionList", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static ResponseTermsCondition TermsConditionDelete(string id)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Pk_Id", id);

                ResponseTermsCondition _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseTermsCondition>("TermsandConditionDelete", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
