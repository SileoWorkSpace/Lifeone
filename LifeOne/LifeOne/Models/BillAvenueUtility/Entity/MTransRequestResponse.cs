using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.BillAvenueUtility.Entity
{

    public class MTransCommon
    {
        public string TransReqXML { get; set; }
        public string TransReqEncType { get; set; }
        public string Url { get; set; }
        public string AccessCode { get; set; }
        public string RequestId { get; set; }
        public string BillerId { get; set; }
        public string EnvId { get; set; }
        public string InstituteId { get; set; }
        public string WorkingKey { get; set; }
        public string IPAdress { get; set; }
        public string AndroidId { get; set; }
        public string DeviceId { get; set; }
        public long CompanyId { get; set; }
        public long FK_memId { get; set; }
        public long CreatedBy { get; set; }
        public int ProcId { get; set; }
        public string APIName { get; set; }
    }


    public class MTransRequest : MTransCommon
    {

    }
    public class MTransResponse : MCommon
    {
        public long Fk_TransId { get; set; }

    }
}