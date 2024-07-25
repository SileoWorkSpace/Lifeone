using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MCommission
    {
        public long Pk_CommonId { get; set; }
        public long Fk_UtilityId { get; set; }
        public string VendorName { get; set; }
        public double GatewayNumber { get; set; }
        public string Serviceprovidername { get; set; }
        public double Margin { get; set; }
        public double Surcharge { get; set; }
        public double MinSurcharge { get; set; }
        public double RemunerationPerTransaction { get; set; }
        public double Returnedpartofsurcharge { get; set; }
        public int CreatedBy { get; set; }
        public int ProcId { get; set; }
        public string Status { get; set; }
        public bool IsAscDesc { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public List<MCommission> objList { get; set; }
    }
    public class ResponseCommissionModel
    {
        public int Flag { get; set; }
        public string Msg { get; set; }
    }
}