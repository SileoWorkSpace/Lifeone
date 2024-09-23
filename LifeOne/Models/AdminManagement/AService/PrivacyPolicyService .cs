using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
    
    public class PrivacyPolicyService
    {
        ~PrivacyPolicyService() { }
       
        public ResponsePrivacyPolicy SavePrivacyPolicyService(AdminPrivacyPolicy _obj)
        {
            ResponsePrivacyPolicy _objResponseModel = new ResponsePrivacyPolicy();
            try
            {
                _obj.CreatedBy = SessionManager.Fk_MemId;
                if (_obj.Pk_Id != null)
                {
                    _objResponseModel = DALAdminPrivacyPolicy.UpdatePrivacyPolicy(_obj);
                }
                else
                {
                    _objResponseModel = DALAdminPrivacyPolicy.SavePrivacyPolicy(_obj);
                }
                
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        ResponsePrivacyPolicy _objResponseModel = new ResponsePrivacyPolicy();
        public List<AdminPrivacyPolicy> GetPrivacyPolicy(AdminPrivacyPolicy _obj)
        {
            List<AdminPrivacyPolicy> _objResponseModel = new List<AdminPrivacyPolicy>();
            try
            {

                //decrypt app team reponse
                _objResponseModel = DALAdminPrivacyPolicy.GetPrivacyPolicyList(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public AdminPrivacyPolicy PrivacyPolicyEditService(AdminPrivacyPolicy _obj)
        {
            AdminPrivacyPolicy _objResponseModel = new AdminPrivacyPolicy();
            try
            {

                //decrypt app team reponse
                _objResponseModel = DALAdminPrivacyPolicy.GetPrivacyPolicyEdit(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public ResponsePrivacyPolicy PrivacyPolicyDeleteService(string id)
        {
            ResponsePrivacyPolicy _objResponseModel = new ResponsePrivacyPolicy();
            try
            {

                //decrypt app team reponse
                _objResponseModel = DALAdminPrivacyPolicy.PrivacyPolicyDelete(id);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}