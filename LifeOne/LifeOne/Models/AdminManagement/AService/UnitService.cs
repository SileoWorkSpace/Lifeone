using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
   
    public class UnitService
    {
        ~UnitService() { }

        public List<MAdminUnit> GetUnitService(MAdminUnit _obj)
        {
            List<MAdminUnit> _objResponseModel = new List<MAdminUnit>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALAdminUnit.GetData(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public ResponseUnitModel SaveUnitService(MAdminUnit _obj)
        {
            ResponseUnitModel _objResponseModel = new ResponseUnitModel();
            try
            {
                _objResponseModel = DALAdminUnit.SaveUnit(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}