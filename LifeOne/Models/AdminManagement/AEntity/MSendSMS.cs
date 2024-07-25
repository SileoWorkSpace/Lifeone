using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MSendSMS
    {
        public long FK_MemId { get; set; }
        public string LoginId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string SMSContent { get; set; }
        public string RequestedBy { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public Pager Pager { get; set; }
        public int TotalRecord { get; set; }
        public bool IsSend { get; set; }
        public List<MSendSMS> LstSendSms { get; set; }
    }

    public class SMSResponse
    {
        public int Flag { get; set; }
        public string Msg { get; set; }
    }
}