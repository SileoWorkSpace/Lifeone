using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class ChangeProfile
    {
        public string Fk_MemId { get; set; }
        public string LoginId { get; set; }
        public string NewMobileNo { get; set; }
        public string NewEmailId { get; set; }
        public string NewName { get; set; }
        public string OldMobileNo { get; set; }
        public string OldEmailId { get; set; }
        public string OldName { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public string Pk_Id { get; set; }
        public string Msg { get; set; }
        public string Remark { get; set; }
        public List<ChangeProfile> List { get; set; }
    }
}