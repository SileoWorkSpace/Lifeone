using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
    public class CPCommissionService
    {
        ~CPCommissionService() { }

        public List<MCommission> GetCommissionService(MCommission _obj)
        {
            List<MCommission> _objResponseModel = new List<MCommission>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALCPCommission.GetData(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public ResponseCommissionModel SaveCommissionService(MCommission _obj)
        {
            ResponseCommissionModel _objResponseModel = new ResponseCommissionModel();
            try
            {
                _objResponseModel = DALCPCommission.SaveCommission(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}