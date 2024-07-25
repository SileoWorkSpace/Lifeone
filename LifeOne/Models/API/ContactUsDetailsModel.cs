using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class ContactUsDetailsModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Mobile { get; set; }
        public List<string> MobileList { get; set; }
        public string Email { get; set; }
        public List<string> EmailList { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public string FaxNo { get; set; }
        public bool Status { get; set; }
        public string Fk_MemId { get; set; }
    }
    public class ContactUsDetailsResponseModel
    {
        public int Flag { get; set; }
        public string response { get; set; }
        public string Message { get; set; }
        public ContactUsDetailsModel Data { get; set; }
    }

    public class AppNotification
    {
        public int NotificationId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int Fk_MemId { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public int Flag { get; set; }
        public string response { get; set; }
        public string Remark { get; set; }
        public string SendingDate { get; set; }
        public string SendingTime { get; set; }
        public bool Isread { get; set; }
        public int Total { get; set; }
        public List<AppNotification> AppNotificationList { get; set; }
    }

}