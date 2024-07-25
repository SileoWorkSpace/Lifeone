using Dapper;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateDAL
{
    public class DALSMS
    {
        public static SMSResponse SaveSMS(string xmldata)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@xmldocumentData", xmldata);
            SMSResponse _iresult = DBHelperDapper.DAAddAndReturnModel<SMSResponse>("dbo.SMS_Insert", queryParameters);
            return _iresult;
        }
    }
}