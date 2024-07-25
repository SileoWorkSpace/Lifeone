using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateEntity
{
    public class Member
    {
        public int FK_MemId { get; set; }
        public string LoginId { get; set; }
        public string MemberName { get; set; }
        public string EncryptedId { get; set; }
    }
}