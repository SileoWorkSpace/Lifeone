using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace LifeOne.Models.AdminManagement.AService
{
    public class IncomeTypeService
    {
        ~IncomeTypeService() { }

        public List<MAdminIncomeTypeMaster> GetIncomeTypeService(MAdminIncomeTypeMaster _obj)
        {
            List<MAdminIncomeTypeMaster> _objResponseModel = new List<MAdminIncomeTypeMaster>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALAdminIncomeType.GetData(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public ResponseIncomeTypeMasterModel SaveIncomeTypeService(MAdminIncomeTypeMaster _obj)
        {
            ResponseIncomeTypeMasterModel _objResponseModel = new ResponseIncomeTypeMasterModel();
            try
            {
                _objResponseModel = DALAdminIncomeType.SaveIncomeType(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}