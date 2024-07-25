using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MAdminProfile
    {
        public long Fk_Memid { get; set; }
        #region ParaForChangePassword
        public string LoginId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public HttpPostedFileBase file { get; set; }
        public string NormalPass { get; set; }
        public string OldPicture { get; set; }
        public string NewPicture { get; set; }
        public string Msg { get; set; }
        public int Flag { get; set; }
        public int OpCode { get; set; }
       
        #endregion
    }

    public class ResponseAdminProfileModel
    {
        public int Flag { get; set; }
        public string Msg { get; set; }
        public string OldPicture { get; set; }
    }
}