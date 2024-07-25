using Dapper;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateDAL
{
    public class DALEmail
    {
        public static EmailResponse SaveEmail(string xmldata)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@xmldocumentData", xmldata);
            EmailResponse _iresult = DBHelperDapper.DAAddAndReturnModel<EmailResponse>("dbo.Email_Insert", queryParameters);
            return _iresult;
        }
    }
}