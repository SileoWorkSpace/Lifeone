using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.QUtility.Entity
{
    public class MVerifyWalletPassword
    {
        public string Fk_MemId { get; set; }
        public string Password { get; set; }
    }

    public class MVerifyWalletPasswordResponse
    {        
        public string Response { get; set; }
        public string Status { get; set; }

    }
}