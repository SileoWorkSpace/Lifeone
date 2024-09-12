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
   

    public class DALAdminPrivacyPolicy
    {
        public static ResponsePrivacyPolicy SavePrivacyPolicy(AdminPrivacyPolicy obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@Pk_Id", obj.Pk_Id);
                queryParameters.Add("@PrivacyPolicy", obj.PrivacyPolicy);
                queryParameters.Add("@CreatedBy", obj.CreatedBy);

                ResponsePrivacyPolicy _iresult = DBHelperDapper.DAAddAndReturnModel<ResponsePrivacyPolicy>("UploadPrivacyPolicy", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static ResponsePrivacyPolicy UpdatePrivacyPolicy(AdminPrivacyPolicy obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@Pk_Id", obj.Pk_Id);
                queryParameters.Add("@PrivacyPolicy", obj.PrivacyPolicy);
                queryParameters.Add("@Updatedby", SessionManager.Fk_MemId);
                ResponsePrivacyPolicy _iresult = DBHelperDapper.DAAddAndReturnModel<ResponsePrivacyPolicy>("PrivacyPolicyUpdate", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static List<AdminPrivacyPolicy> GetPrivacyPolicyList(AdminPrivacyPolicy obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();               
                List<AdminPrivacyPolicy> _iresult = DBHelperDapper.DAAddAndReturnModelList<AdminPrivacyPolicy>("PrivacyPolicyList", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static AdminPrivacyPolicy GetPrivacyPolicyEdit(AdminPrivacyPolicy obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Pk_Id", obj.Pk_Id);
                AdminPrivacyPolicy _iresult = DBHelperDapper.DAAddAndReturnModel<AdminPrivacyPolicy>("PrivacyPolicyList", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static ResponsePrivacyPolicy PrivacyPolicyDelete(string id)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Pk_Id", id);

                ResponsePrivacyPolicy _iresult = DBHelperDapper.DAAddAndReturnModel<ResponsePrivacyPolicy>("PrivacyPolicyDelete", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
