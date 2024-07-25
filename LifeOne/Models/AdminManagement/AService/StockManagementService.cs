using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.QUtility.DAL;

namespace LifeOne.Models.AdminManagement.AService
{
    public class StockManagementService
    {
        public static List<MAdminStockManagement> GetStockManagementService(MAdminStockManagement objmodel)
        {
            List<MAdminStockManagement> _objstock = null;
            try
            {
                _objstock = DALStockManagement.DALGetStockManagement(objmodel);
                return _objstock;
            }
            catch (Exception ex)
            {
                return _objstock;
            }
        }

        public static List<ProductStockReport> GetStockManagementReport(ProductStockReport objmodel)
        {
            List<ProductStockReport> _objstock = null;
            try
            {
                _objstock = DALStockManagement.DALGetStockManagementReport(objmodel);
                return _objstock;
            }
            catch (Exception ex)
            {
                return _objstock;
            }
        }
        


    }
}