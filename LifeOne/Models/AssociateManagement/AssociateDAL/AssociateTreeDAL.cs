using Dapper;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateDAL
{
    public class AssociateTreeDAL
    {

        public static TreeModel AssociateRootId(string LoginId)
        {
            string Action = "Root";
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@LoginID", LoginId);
            queryParameters.Add("@Action", Action);
            TreeModel _iresult = DBHelperDapper.DAAddAndReturnModel<TreeModel>("GetRootId", queryParameters);
            return _iresult;
        }
        public static TreeModel AssociateGetUPId(string LoginId)
        {
            string Action = "Up";
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@LoginID", LoginId);
            queryParameters.Add("@Action", Action);
            TreeModel _iresult = DBHelperDapper.DAAddAndReturnModel<TreeModel>("GetRootId", queryParameters);
            return _iresult;
        }



    }
}