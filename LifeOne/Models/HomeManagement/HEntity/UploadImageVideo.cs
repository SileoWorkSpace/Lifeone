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
    public class UploadImageVideo
    {
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public string Videolink { get; set; }
        public DataTable dtDetails { get; set; }
        public string ConvertToEmbedLink { get; set; }
        public int OpCode { get; set; }

        public List<UploadImageVideo>ImageList { get; set; }

        public DataSet getImagegallery()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para = {

                };
                ds = DBHelper.ExecuteQuery("WebsiteImageList", para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet getVideogallery()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para = {

                };
                ds = DBHelper.ExecuteQuery("WebsiteVideoList", para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }       
    }
    public class UploadImageVideoResponse
    {
        public string Flag { get; set; }
        public string Msg { get; set; }
    }  
}