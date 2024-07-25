using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
   
    public class MAdminBank
    {
        public int Pk_BankId { get; set; }
        public string BankName { get; set; }
        public int IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int ProcId { get; set; }
        public string Status { get; set; }
        public bool IsAscDesc { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public List<MAdminBank> objList { get; set; }

    }
    public class ResponseBankModel
    {
        public int Flag { get; set; }
        public string Msg { get; set; }


        public string MemberName { get; set; }
        public string Mobile { get; set; }

        public string DeviceId { get; set; }
        public string Password { get; set; }
        public string LoginID { get; set; }
        public string Pincode { get; set; }
        public string Fk_MemId { get; set; }



    }
}