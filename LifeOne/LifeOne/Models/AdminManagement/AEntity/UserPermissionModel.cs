using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class UserPermissionModel
    {
        public int PK_FormId { get; set; }
        public int FK_FormTypeId { get; set; }
        public string FormName { get; set; }
        public string Url { get; set; }
    }
}