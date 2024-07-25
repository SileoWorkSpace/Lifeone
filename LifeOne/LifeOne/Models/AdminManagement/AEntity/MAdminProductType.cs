using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
   
    public class MAdminProductType
    {
        public int Pk_ProductTypeId { get; set; }
        public string ProductType { get; set; }
        public string ProductTypeImage { get; set; }
        //public string file { get; set; }
        public bool IsSpaceOrbit { get; set; }
        public long CreatedBy { get; set; }
        public int ProcId { get; set; }
        public string Status { get; set; }
        public bool IsAscDesc { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public HttpPostedFileBase file { get; set; }
        public List<MAdminProductType> objList { get; set; }

    }
    public class ResponseProductTypeModel
    {
        public int Flag { get; set; }
        public string Msg { get; set; }
    }
}