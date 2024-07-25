using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
    
    public class BankService
    {
        ~BankService() { }

        public List<MAdminBank> GetBankService(MAdminBank _obj)
        {
            List<MAdminBank> _objResponseModel = new List<MAdminBank>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALAdminBank.GetData(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public ResponseBankModel SaveBankService(MAdminBank _obj)
        {
            ResponseBankModel _objResponseModel = new ResponseBankModel();
            try
            {
                _objResponseModel = DALAdminBank.SaveBank(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}