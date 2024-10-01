using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{  
    public class MUpcomingEvent
    {
        public string Id { get; set; }
        public HttpPostedFileBase ImageUrl_Doc { get; set; }
        public string ImageUrl { get; set; }
        public string AddedBy { get; set; }
        public string OpCode { get; set; }
        public DataTable dtDetails { get; set; }
        public List<MUpcomingEvent> lstData { get; set; }
    }
}