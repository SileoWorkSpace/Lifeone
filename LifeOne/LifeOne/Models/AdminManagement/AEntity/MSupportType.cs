using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MSupportType
    {
        public int PK_SupportId { get; set; }
        public string SupportName { get; set; }
        public int IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int ProcId { get; set; }
        public string Status { get; set; }
        public bool IsAscDesc { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public List<MSupportType> objList { get; set; }
    }
    public class ResponseSupportTypeModel
    {
        public int Flag { get; set; }
        public string Msg { get; set; }
    }
}