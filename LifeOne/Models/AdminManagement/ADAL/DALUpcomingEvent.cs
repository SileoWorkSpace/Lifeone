using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALUpcomingEvent
    {
        public  DataSet UploadUpcomingEvent(MUpcomingEvent obj)
        {
            try
            {
                SqlParameter[] para = { 
                new SqlParameter("@Id",obj.Id),
                new SqlParameter("@ImageUrl",obj.ImageUrl),
                new SqlParameter("@AddedBy",obj.AddedBy),
                new SqlParameter("@Action",obj.OpCode)
                };
                DataSet ds = Connection.ExecuteQuery("ManageUpcomingEvent", para);
                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}