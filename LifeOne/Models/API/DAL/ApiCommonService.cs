using Dapper;
using LifeOne.Models.AdminManagement.ADAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API.DAL
{
    public static class ApiCommonService
    {
        public static AppNotification AppNotification(AppNotification req)
        {
            AppNotification model = new AppNotification();
            List<AppNotification> ObjList = new List<AppNotification>();
            try
            {
                var QuaryParameter = new DynamicParameters();
                QuaryParameter.Add("@Fk_MemId", req.Fk_MemId);
                QuaryParameter.Add("@Page", req.Page);
                QuaryParameter.Add("@PageSize", req.Size);
                ObjList = DBHelperDapper.DAAddAndReturnModelList<AppNotification>("Proc_GetNotification", QuaryParameter);
                model.AppNotificationList = ObjList;
                return model;
            }
            catch (Exception ex)
            {
                return new AppNotification { Flag = 0, response = "failed", Message = ex.Message, AppNotificationList = null };
            }
        }

        public static AppNotification UpdateNotification(AppNotification req)
        {
            AppNotification model = new AppNotification();
            List<AppNotification> ObjList = new List<AppNotification>();
            try
            {
                var QuaryParameter = new DynamicParameters();
                QuaryParameter.Add("@notificationId", req.NotificationId);
                QuaryParameter.Add("@Fk_MemId", req.Fk_MemId);
                QuaryParameter.Add("@procid", 1);
                ObjList = DBHelperDapper.DAAddAndReturnModelList<AppNotification>("Proc_updatenotification", QuaryParameter);
                model.AppNotificationList = ObjList;
                return model;
            }
            catch (Exception ex)
            {
                return new AppNotification { Flag = 0, response = "failed", Message = ex.Message, AppNotificationList = null };
            }
        }
    
    }
}