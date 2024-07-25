using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MAdminUntility
    {
       
            public int Pk_UtilityId { get; set; }
            public string UtilityName { get; set; } 
            public int CreatedBy { get; set; } 
            public int ProcId { get; set; }
            public List<MAdminUntility> objList { get; set; }
         
    }
    public class ResponseUtilityModel
    {

        public int Flag { get; set; }
        public string Msg { get; set; }
        //public List<MAdminUntility> objList { get; set; }


    }
}