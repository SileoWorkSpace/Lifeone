using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateEntity
{
    public class MEmail
    {
        public string Subject { get; set; }
        public string EmailContect { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public long CreatedBy { get; set; }
        public List<string> LstMemberId { set; get; }
        public List<string> Email { get; set; }
        public string EmailCC { get; set; }
        public string EmailBCC { get; set; }
    }
    public class EmailResponse
    {
        public int Flag { get; set; }
        public string response { get; set; }
        public string Msg { get; set; }
    }
}