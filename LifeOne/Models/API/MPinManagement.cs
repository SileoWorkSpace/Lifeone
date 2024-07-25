using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class MPinManagement
    {
        public string UserPin { get; set; }
        public long Fk_MemId { get; set; }
        public int Prod { get; set; }
    }
    public class MPinManagementResponse: RegistrationResponse
    {
        
    }    
}