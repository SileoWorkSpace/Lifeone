using System;
using LifeOne.Models.HomeManagement.HEntity;
using LifeOne.Models.HomeManagement.HDAL;
using LifeOne.Models.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.HomeManagement.HService
{
    public class ContactService
    {
        public static ContactResponse Contact(Contact model)
        {
            ContactResponse obj = DALContact.Contact(model);
            return obj;
        }
    }
}