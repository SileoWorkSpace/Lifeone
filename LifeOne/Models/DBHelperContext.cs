using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace LifeOne.Models
{
    public class DBHelperContext : DbContext
    {
        public DBHelperContext()
           : base("name=connectionString")
        {
        }
    }
}