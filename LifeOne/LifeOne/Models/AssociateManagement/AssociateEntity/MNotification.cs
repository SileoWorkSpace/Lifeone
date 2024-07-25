using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateEntity
{
    public class MNotification
    {
        public string Notificationmessage { get; set; }
        public string Title { get; set; }
        public long CreatedBy { get; set; }
        public List<string> LstMemberId { set; get; }
        public List<string> LstDeviceId { set; get; }

    }

    public class NotificationResponse
    {
        public int Flag { get; set; }
        public string response { get; set; }
        public string Msg { get; set; }
    }
}