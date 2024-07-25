using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALSendNotification
    {
        public static List<MSendNotification> GetData(MSendNotification obj)
        {
            try
            {
                if (obj.Page == 0)
                {
                    obj.Page = 1;
                }
                if (obj.Size == 0)
                {
                    obj.Size = 100;
                }
                var queryParameters = new DynamicParameters();
                List<MSendNotification> _iresult = DBHelperDapper.DAAddAndReturnModelList<MSendNotification>("dbo.SendNotificationUserList", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<MSendNotification> GetDatails(MSendNotification obj)
        {
            try
            {
                if (obj.Page == 0)
                {
                    obj.Page = 1;
                }
                if (obj.Size == 0)
                {
                    obj.Size = 100;
                }
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Fk_MemId", obj.FK_MemId);
                List<MSendNotification> _iresult = DBHelperDapper.DAAddAndReturnModelList<MSendNotification>("dbo.SendNotificationUserDetails", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static SendNotificationResponse UpdateNotificationDetails(MSendNotification obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Fk_MemId", obj.FK_MemId);
                SendNotificationResponse _iresult = DBHelperDapper.DAAddAndReturnModel<SendNotificationResponse>("dbo.UpdateNotificationStatus", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}