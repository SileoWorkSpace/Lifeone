using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AssociateManagement.AssociateDAL;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateService
{
    public class AssociateTreeService
    {
        public dynamic GetTreeData(TreeRequest _obj)
        {
           
            dynamic result;
            try
            {
                result = DBHelperDapper.GetAssociateTree(_obj.LoginID);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public TreeModel AssociateRootId(string LoginId)
        {

            TreeModel result = new TreeModel();
            try
            {
                result = AssociateTreeDAL.AssociateRootId(LoginId);
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
        public TreeModel AssociateGetUPId(string LoginId)
        {

            TreeModel result = new TreeModel();
            try
            {
                result = AssociateTreeDAL.AssociateGetUPId(LoginId);
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

    }
}