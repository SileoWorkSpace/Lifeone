using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MAdminUploadKyc
    {
        public int Fk_MemId { get; set; }
        public string PanCardNo { get; set; }
        public string AddressProofNo { get; set; }
        public string BankACNo { get; set; }
        public string PanUrl { get; set; }
        public string BankUrl { get; set; }
        public string AddressFrontUrl { get; set; }
        public string AddressBackUrl { get; set; }
        public int Code { get; set; }
        public string Remark { get; set; }
        public string type { get; set; }
        public string IfscCode { get; set; }
        public string BankName { get; set; }
        public string AccountHolder { get; set; }
    }
}