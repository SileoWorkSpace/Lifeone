using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
    
    public class ProductTypeService
    {
        ~ProductTypeService() { }

        public List<MAdminProductType> GetProductTypeService(MAdminProductType _obj)
        {
            List<MAdminProductType> _objResponseModel = new List<MAdminProductType>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALAdminProductType.GetData(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public ResponseProductTypeModel SaveProductTypeService(MAdminProductType _obj)
        {
            ResponseProductTypeModel _objResponseModel = new ResponseProductTypeModel();
            try
            {
                _obj.CreatedBy = SessionManager.Fk_MemId;
                _objResponseModel = DALAdminProductType.SaveProductType(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}