using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALAdminManageStock
    {
        public static List<MAdminManageStock> GetData(MAdminManageStock obj)
        {
            try
            {
                if (obj.Page == 0)
                {
                    obj.Page = 1;
                }
                if (obj.Size == 0)
                {
                    obj.Size = 100;
                }
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@Page", obj.Page);
                queryParameters.Add("@Size", obj.Size);
                List<MAdminManageStock> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminManageStock>("GetStockForAdmin", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static ResponseManageStock CrDrStock(MAdminManageStock obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@Fk_ProductID", obj.Fk_ProductID);
                queryParameters.Add("@ProductQty", obj.ProductQty);
                queryParameters.Add("@Fk_Memid", obj.Fk_Memid);
                queryParameters.Add("@Type", obj.Type);
                queryParameters.Add("@Mode", obj.Mode);
                ResponseManageStock _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseManageStock>("ManageProductQty", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<MAdminManageStock> GetFranchiseStock(int Fk_memid)
        {
            try
            {
              
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@Fk_memid", Fk_memid);
                queryParameters.Add("@Page", 1);
                queryParameters.Add("@Size",1000);
                List<MAdminManageStock> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminManageStock>("GetFranchiseStock", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}