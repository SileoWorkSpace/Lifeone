using DocumentFormat.OpenXml.Office2010.Excel;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.API.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.HomeManagement.HEntity
{
    public class NewsandAnnouncement
    {
        public int Pk_NewsId { get; set; }
        public string NewsHeading { get; set; }
        public string News { get; set; }
        public string NewsPreference { get; set; }
        public string InfoImgUrl { get; set; }
        public string InfoLink { get; set; }
        public int IsActive { get; set; }
        public DataTable dtDetails { get; set; }       

        public List<NewsandAnnouncement> ImageList { get; set; }

        public DataSet getNewandAnnouncement()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para = {
                new SqlParameter("@Pk_NewsId",Pk_NewsId),
            };
                ds = DBHelper.ExecuteQuery("GetNewsandAnnouncement", para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }      
    }
    public class NewsandAnnouncementResponse
    {
        public string Flag { get; set; }
        public string Msg { get; set; }
    }
}