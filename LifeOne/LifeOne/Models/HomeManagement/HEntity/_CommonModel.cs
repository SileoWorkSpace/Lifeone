using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.HomeManagement.HEntity
{
    public class _CommonModel
    {

    }

    public class CheckCampingModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string CampingDate { get; set; }
    }
}