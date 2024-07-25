using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALCPCommission
    {
        public static List<MCommission> GetData(MCommission obj)
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

                queryParameters.Add("@Pk_CommissionId", obj.Pk_CommonId);
                List<MCommission> _iresult = DBHelperDapper.DAAddAndReturnModelList<MCommission>("GetCommission", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static ResponseCommissionModel SaveCommission(MCommission obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@Pk_CommonId", obj.Pk_CommonId);
                queryParameters.Add("@VendorName", obj.VendorName);
                queryParameters.Add("@GatewayNumber", obj.GatewayNumber);
                queryParameters.Add("@Serviceprovidername", obj.Serviceprovidername);
                queryParameters.Add("@Margin", obj.Margin);
                queryParameters.Add("@Surcharge", obj.Surcharge);
                queryParameters.Add("@MinSurcharge", obj.MinSurcharge);
                queryParameters.Add("@RemunerationPerTransaction", obj.RemunerationPerTransaction);
                queryParameters.Add("@Returnedpartofsurcharge", obj.Returnedpartofsurcharge); 
                queryParameters.Add("@CreatedBy", obj.CreatedBy);
                queryParameters.Add("@ProcId", obj.ProcId);
                ResponseCommissionModel _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseCommissionModel>("Proc_Commissions", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }

}