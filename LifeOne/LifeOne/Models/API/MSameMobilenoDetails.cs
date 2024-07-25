using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{

    public class MSameMobilenoDetailsResponse : CommonJsonReponse
    {
        public string CampaignStartDate { get; set; }
        public bool MemberExistOrNot { get; set; }
        public bool IsRegistered { get; set; }
        public int VersionCode { get; set; }
        public List<MsaveMobileDetailsList> result { get; set; }

    }
    public class MsaveMobileDetailsList
    {
        public string LoginId { get; set; }
        public string Fullname { get; set; }
        public long FK_MemId { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }

        public bool IsBlocked { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsBusinessId { get; set; }
        public bool IsPasswordChange { get; set; }

        public string CampaignStartDate { get; set; }
        public bool MemberExistOrNot { get; set; }
        public bool IsRegistered { get; set; }

        public bool isfamilyn { get; set; }
        public int VersionCode { get; set; }


    }


    public class MMobileNo
    {
        public string mobileNo { get; set; }
        public int VersionCode { get; set; }
    }
}