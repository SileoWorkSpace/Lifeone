using Dapper;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
   

    public class DALAdminProductType
    {
        public static List<MAdminProductType> GetData(MAdminProductType obj)
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

                queryParameters.Add("@Pk_ProductTypeId", obj.Pk_ProductTypeId);
                queryParameters.Add("@IsAscDesc", obj.IsAscDesc);
                queryParameters.Add("@Status", obj.Status);
                queryParameters.Add("@Page", obj.Page == 0 ? 1 : obj.Page);
                queryParameters.Add("@Size", obj.Size == 0 ? 50 : obj.Size);                
                List<MAdminProductType> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminProductType>("GetProductTypeMaster", queryParameters);
                return _iresult;
            
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static ResponseProductTypeModel SaveProductType(MAdminProductType obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@Pk_ProductTypeId", obj.Pk_ProductTypeId);
                queryParameters.Add("@ProductType", obj.ProductType);
                queryParameters.Add("@CreatedBy", obj.CreatedBy);
                queryParameters.Add("@ProcId", obj.ProcId);
                queryParameters.Add("@IsSpaceOrbit", obj.IsSpaceOrbit);
                queryParameters.Add("@ProductTypeImage", obj.ProductTypeImage);
                // string _qury = "ProductType @Pk_ProductTypeId=" + obj.Pk_ProductTypeId + ",@ProductType=" + obj.ProductType + ", @CreatedBy = " + obj.CreatedBy + ", @ProcId = " + obj.ProcId + "";
                // ResponseProductTypeModel _iresult = DBHelperDapper.DASave<ResponseProductTypeModel>(_qury);
                ResponseProductTypeModel _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseProductTypeModel>("ProductType", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
