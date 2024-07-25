using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DAlPayoutLeadger
    {
        public static List<MPayoutLeadger> DAlAllPayoutLeadger(MPayoutLeadger _model)
        {
            try
            {
                List<MPayoutLeadger> _iresult = new List<MPayoutLeadger>();
                var queryParameters = new DynamicParameters();                
                queryParameters.Add("@LoginId", _model.LoginId ?? string.Empty);
                queryParameters.Add("@FromDate", _model.FromDate);
                queryParameters.Add("@ToDate", _model.ToDate);
                queryParameters.Add("@page", _model.Page);
                queryParameters.Add("@size", _model.Size);
                queryParameters.Add("@Fk_MemId", _model.Fk_MemId);                                
                queryParameters.Add("@ProdId", _model.ProdId);
                _iresult = DBHelperDapper.DAAddAndReturnModelList<MPayoutLeadger>("GetPayoutWalletDetails", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }          
        }      
    }
}