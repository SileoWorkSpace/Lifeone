using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
    public class SendNotificationService
    {
        public List<MSendNotification> GetListForSendNotification(MSendNotification _obj)
        {
            List<MSendNotification> _objResponseModel = new List<MSendNotification>();
            try
            {
                _objResponseModel = DALSendNotification.GetData(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public List<MSendNotification> GetDetailsForSendNotification(MSendNotification _obj)
        {
            List<MSendNotification> _objResponseModel = new List<MSendNotification>();
            try
            {
                _objResponseModel = DALSendNotification.GetDatails(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public SendNotificationResponse UpdateNotification(MSendNotification _obj)
        {
            SendNotificationResponse _objResponseModel = new SendNotificationResponse();
            try
            {
                _objResponseModel = DALSendNotification.UpdateNotificationDetails(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}