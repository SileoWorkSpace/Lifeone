using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeOne.Models.AdminManagement.AEntity
{
   
    public class AdminPrivacyPolicy
    {
        public string Pk_Id { get; set; }
        [AllowHtml]
        public string PrivacyPolicy { get; set; }
        public long CreatedBy { get; set; }
        public List<AdminPrivacyPolicy> PrivacyPolicyList { get; set; }        
            
    }
    public class ResponsePrivacyPolicy
    {
        public int Flag { get; set; }
        public string Msg { get; set; }
    }   
}