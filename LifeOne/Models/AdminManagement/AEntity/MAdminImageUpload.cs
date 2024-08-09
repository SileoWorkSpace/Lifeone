using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{   
    public class MAdminImageUpload
    {       
        public HttpPostedFileBase file { get; set; }                      
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public string Videolink { get; set; }
        public int OpCode { get; set; }
        public List<MAdminImageUpload>ImageList { get; set; }        
         
    }    
    public class ResponseUploadImage 
    {
        public string Flag { get; set; }
        public string Msg { get; set; }
        public string Code { get; set; }
        public string TransId { get; set; }
    }
}