using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
   
    public class AdminTermsCondition
    {
        public string Pk_Id { get; set; }
        public string TermsandCondition { get; set; }
        public long CreatedBy { get; set; }
        public List<AdminTermsCondition> TermsConditionList { get; set; }        
            
    }
    public class ResponseTermsCondition
    {
        public int Flag { get; set; }
        public string Msg { get; set; }
    }   
}