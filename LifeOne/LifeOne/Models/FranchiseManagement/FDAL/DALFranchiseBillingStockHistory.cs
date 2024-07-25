using Dapper;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.FranchiseManagement.FEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.FranchiseManagement.FDAL
{
    public class DALFranchiseBillingStockHistory
    {

        public static List<MFranchiseBillingStockHistory> FranchiseBillingStockHistory(MFranchiseBillingStockHistory data)
        {
            try
            {
                var _qry = new DynamicParameters();
                _qry.Add("@Pk_Id", data.FranchiseId);
                _qry.Add("@Fk_PID", data.PrdId);
                _qry.Add("@FromDate",data.FromDate);
                _qry.Add("@ToDate",data.ToDate);


               // string _qry = "GetFranchiseBillingHistory @Pk_Id=" + data.FranchiseId + ",@Fk_PID=" + data.PrdId + "";
                List<MFranchiseBillingStockHistory> _iresult = DBHelperDapper.DAAddAndReturnModelList<MFranchiseBillingStockHistory>("GetFranchiseBillingHistory", _qry);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}