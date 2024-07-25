using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
    public class ManageStockService
    {
        ~ManageStockService() { }

        public List<MAdminManageStock> GetStockService(MAdminManageStock _obj)
        {
            List<MAdminManageStock> _objResponseModel = new List<MAdminManageStock>();
            try
            {
                //decrypt app team reponse
                _objResponseModel = DALAdminManageStock.GetData(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public ResponseManageStock CrDrStockService(MAdminManageStock _obj)
        {
            ResponseManageStock _objResponseModel = new ResponseManageStock();
            try
            {
                _objResponseModel = DALAdminManageStock.CrDrStock(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }


        public List<MAdminManageStock> GetFranchiseStockService(int Fk_Memid)
        {
            List<MAdminManageStock> _objResponseModel = new List<MAdminManageStock>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALAdminManageStock.GetFranchiseStock(Fk_Memid);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}