using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
    public class SupportTypeService
    {
        ~SupportTypeService() { }

        public List<MSupportType> GetSupportTypeService(MSupportType _obj)
        {
            List<MSupportType> _objResponseModel = new List<MSupportType>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALSupportType.GetData(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public ResponseSupportTypeModel SaveSupportTypeService(MSupportType _obj)
        {
            ResponseSupportTypeModel _objResponseModel = new ResponseSupportTypeModel();
            try
            {
                _objResponseModel = DALSupportType.SaveSupportType(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}