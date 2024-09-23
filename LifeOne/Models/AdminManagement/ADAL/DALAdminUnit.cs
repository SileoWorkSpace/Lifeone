using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
   
    public class DALAdminUnit
    {
        public static List<MAdminUnit> GetData(MAdminUnit obj)
        {
            try
            {
                if(obj.Page==0)
                {
                    obj.Page = 1;
                }
                if(obj.Size==0)
                {
                    obj.Size = 100;
                }
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@Pk_UnitId", obj.Pk_UnitId);
                queryParameters.Add("@IsAscDesc", obj.IsAscDesc);
                queryParameters.Add("@Status", obj.Status);
                queryParameters.Add("@Page", obj.Page);
                queryParameters.Add("@Size", obj.Size);
                //string _qury = "GetUnitMaster @Pk_UnitId=" + obj.Pk_UnitId + ",@IsAscDesc=" + obj.IsAscDesc + ", @Status = " + obj.Status + ", @Page = " + obj.Page + ", @Size = " + obj.Size + "";
                List<MAdminUnit> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminUnit>("GetUnitMaster", queryParameters);               
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static ResponseUnitModel SaveUnit(MAdminUnit obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@Pk_UnitId", obj.Pk_UnitId);
                queryParameters.Add("@UnitName", obj.UnitName);
                queryParameters.Add("@CreatedBy", obj.CreatedBy);
                queryParameters.Add("@ProcId", obj.ProcId);                
                ResponseUnitModel _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseUnitModel>("Unit", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}