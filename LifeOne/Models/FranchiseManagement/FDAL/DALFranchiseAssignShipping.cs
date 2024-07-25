using Dapper;
using LifeOne.Models.Common;
using LifeOne.Models.FranchiseManagement.FEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.FranchiseManagement.FDAL
{
    public class DALFranchiseAssignShipping
    {

        public static List<MAssignShippingInfo> GetChildFranchiseList(MAssignShippingInfo obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Bid", 0);
                queryParameters.Add("@FranchiseId", obj.FranchiseId);
                queryParameters.Add("@TransferInvoiceNo", obj.TransferInvoiceNo);
                queryParameters.Add("@TableName", "BindFranOrder");
                queryParameters.Add("@DateSearch", obj.ShippingDate);
                queryParameters.Add("@Page", obj.Page);
                queryParameters.Add("@Size", SessionManager.Size);
                List<MAssignShippingInfo> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAssignShippingInfo>("GetFranchiseAssignOrder", queryParameters);
                List<MAssignShippingInfo> distinctresult = _iresult.GroupBy(p => new { p.TransferInvoiceNo }).Select(g => g.First()).ToList();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<MAssignShippingInfo> ViewAssignOrderProduct(MAssignShippingInfo obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Bid", obj.FK_FbSID);
                queryParameters.Add("@FranchiseId", obj.FranchiseId);
                queryParameters.Add("@TableName", "BindFranOrderProduct");
                List<MAssignShippingInfo> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAssignShippingInfo>("GetFranchiseAssignOrder", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}