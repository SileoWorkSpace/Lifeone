using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MPayoutLeadger
    {

        public int  Fk_MemId { get; set; } 
        public string TransactionDate { get; set; }
        public string Narration { get; set; }
        public string CrAmount { get; set; }
        public string DrAmount { get; set; }
        public string Balance { get; set; }
        public string LoginId { get; set; }
        public string Name { get; set; }

        public int? Page { get; set; }
        public int Size { get; set; }
        public int TotalRecord { get; set; }

        public int ProdId { get; set; }
       
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public Pager Pager { get; set; }
        public List<MPayoutLeadger> _objPayoutList { get; set; }

    }
}