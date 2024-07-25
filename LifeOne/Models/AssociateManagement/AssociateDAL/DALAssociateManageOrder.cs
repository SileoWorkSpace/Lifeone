using Dapper;
using LifeOne.Models.Common;
using LifeOne.Models.FranchiseManagement.FEntity;
using LifeOne.Models.Manager;
using LifeOne.Models.QUtility.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateDAL
{
    public class DALAssociateManageOrder
    {
        public static List<MFranchiseorderRequest> GetOrderTemp(MSimpleString _param)
        {
            try
            {
                string _qry = "Proc_GetOrderTemp " + _param.RequestId + "";
                List<MFranchiseorderRequest> _iresult = DBHelperDapper.DAGetDetailsInList<MFranchiseorderRequest>(_qry);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}