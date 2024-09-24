using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
    
    public class TermsConditionService
    {
        ~TermsConditionService() { }
       
        public ResponseTermsCondition SaveTermsConditionService(AdminTermsCondition _obj)
        {
            ResponseTermsCondition _objResponseModel = new ResponseTermsCondition();
            try
            {
                _obj.CreatedBy = SessionManager.Fk_MemId;
                if (_obj.Pk_Id != null)
                {
                    _objResponseModel = DALAdminTermsCondition.UpdateTermsandCondition(_obj);
                }
                else
                {
                    _objResponseModel = DALAdminTermsCondition.SaveTermsandCondition(_obj);
                }
                
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        ResponseTermsCondition _objResponseModel = new ResponseTermsCondition();
        public List<AdminTermsCondition> GetTermsandCondition(AdminTermsCondition _obj)
        {
            List<AdminTermsCondition> _objResponseModel = new List<AdminTermsCondition>();
            try
            {

                //decrypt app team reponse
                _objResponseModel = DALAdminTermsCondition.GetTermsConditionList(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public AdminTermsCondition TermsConditionEditService(AdminTermsCondition _obj)
        {
            AdminTermsCondition _objResponseModel = new AdminTermsCondition();
            try
            {

                //decrypt app team reponse
                _objResponseModel = DALAdminTermsCondition.GetTermsConditionEdit(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public ResponseTermsCondition TermsConditionDeleteService(string id)
        {
            ResponseTermsCondition _objResponseModel = new ResponseTermsCondition();
            try
            {

                //decrypt app team reponse
                _objResponseModel = DALAdminTermsCondition.TermsConditionDelete(id);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}