using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
   
    public class FAQService
    {
        ~FAQService() { }

        public List<MAdminFAQ> GetFAQService(MAdminFAQ _obj)
        {
            List<MAdminFAQ> _objResponseModel = new List<MAdminFAQ>();
            try
            {               

                _objResponseModel = DALAdminFAQ.GetFAQData(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public ResponseFAQModel UploadFAQService(MAdminFAQ _obj)
        {            
            ResponseFAQModel _objResponseModel = new ResponseFAQModel();
            try
            {
                _objResponseModel = DALAdminFAQ.UploadFAQ(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}