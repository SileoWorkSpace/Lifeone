using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MAdminUnit
    {
        public int Pk_UnitId { get; set; }
        public string UnitName { get; set; }
        public int CreatedBy { get; set; }
        public int ProcId { get; set; }
        public string Status { get; set; }
        public bool IsAscDesc { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public List<MAdminUnit> objList { get; set; }

    }
    public class ResponseUnitModel
    {
        public int Flag { get; set; }
        public string Msg { get; set; }
    }

    public class WebSitePopup
    {
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public HttpPostedFileBase ImageUrl_Doc { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public string ActiveType { get; set; }
        public string CreationDate { get; set; }
        public string UpdateDate { get; set; }
        public int Code { get; set; }

    }
}