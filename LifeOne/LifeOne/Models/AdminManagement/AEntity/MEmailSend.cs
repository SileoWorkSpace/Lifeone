using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MEmailSend
    {
        public long FK_MemId { get; set; }
        public string LoginId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string EmailCC { get; set; }
        public string EmailBCC { get; set; }
        public string RequestedBy { get; set; }
        public string Subject { get; set; }
        public string EmailAttachmentPath { get; set; }
        public string Message { get; set; }
        public string DeviceId { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public Pager Pager { get; set; }
        public int TotalRecord { get; set; }
        public bool IsSend { get; set; }
        public List<MEmailSend> LstSendEmail { get; set; }
    }

    public class EmailSendResponse
    {
        public int Flag { get; set; }
        public string Msg { get; set; }
    }
}