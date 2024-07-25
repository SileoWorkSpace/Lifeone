using LifeOne.Models.QUtility.DAL;
using LifeOne.Models.QUtility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.QUtility.Service
{
    public class VerifyTransactionPasswordService
    {

        public ResponseModel VerifyMemberWalletPassword(RequestModel _obj)
        {
            try
            {
                DALVerifyPassword _objgeneric = new DALVerifyPassword();
                ResponseModel _rsponsedetails = _objgeneric.DALVerifyPasswordHere(_obj);
                return _rsponsedetails;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
} 