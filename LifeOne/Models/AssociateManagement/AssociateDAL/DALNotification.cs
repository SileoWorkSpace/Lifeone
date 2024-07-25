using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AssociateManagement.AssociateEntity;

namespace LifeOne.Models.AssociateManagement.AssociateDAL
{
    public class DALNotification
    {

        public static NotificationResponse SaveNotification(string xmldata)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@xmldocumentData", xmldata);
            NotificationResponse _iresult = DBHelperDapper.DAAddAndReturnModel<NotificationResponse>("dbo.Notification_Insert", queryParameters);
            return _iresult;
        }
    }
}