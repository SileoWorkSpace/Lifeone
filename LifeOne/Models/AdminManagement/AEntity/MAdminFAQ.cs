using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MAdminFAQ
    {
        public int Pk_FAQId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public long CreatedBy { get; set; }
        public int ProcId { get; set; }      
        public int Page { get; set; }
        public int Size { get; set; }
        public List<MAdminFAQ> objList { get; set; }

    }
    public class ResponseFAQModel
    {
        public int Flag { get; set; }
        public string Msg { get; set; }
    }   
}