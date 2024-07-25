using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.AssociateManagement.AssociateDAL;

namespace LifeOne.Models.AssociateManagement.AssociateService
{
    public class NotificationService
    {
        public NotificationResponse SaveNotification(string xmldata)
        {
            NotificationResponse _objResponseModel = new NotificationResponse();
            try
            {
                _objResponseModel = DALNotification.SaveNotification(xmldata);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}