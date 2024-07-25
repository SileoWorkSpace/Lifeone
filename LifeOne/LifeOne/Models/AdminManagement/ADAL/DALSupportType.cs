using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALSupportType
    {
        public static List<MSupportType> GetData(MSupportType obj)
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

                queryParameters.Add("@PK_SupportId", obj.PK_SupportId);
                List<MSupportType> _iresult = DBHelperDapper.DAAddAndReturnModelList<MSupportType>("GetSupport", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static ResponseSupportTypeModel SaveSupportType(MSupportType obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@PK_SupportId", obj.PK_SupportId);
                queryParameters.Add("@SupportName", obj.SupportName); 
                queryParameters.Add("@CreatedBy", obj.CreatedBy);
                queryParameters.Add("@ProcId", obj.ProcId);
                ResponseSupportTypeModel _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseSupportTypeModel>("Support", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}