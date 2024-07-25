using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    

    public class DALAdminBank
    {
        public static List<MAdminBank> GetData(MAdminBank obj)
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

                queryParameters.Add("@Pk_BankId", obj.Pk_BankId);               
                List<MAdminBank> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminBank>("GetBank", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static ResponseBankModel SaveBank(MAdminBank obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@Pk_BankId", obj.Pk_BankId);
                queryParameters.Add("@BankName", obj.BankName);
                queryParameters.Add("@IsActive", obj.IsActive);
                queryParameters.Add("@CreatedBy", obj.CreatedBy);
                queryParameters.Add("@ProcId", obj.ProcId);                
                ResponseBankModel _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseBankModel>("Bank", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}