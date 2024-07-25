using LifeOne.Models.API.DAL;
using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class UserDownlineReport:MPaging
    {
        public string LoginId { get; set; }
        public string Name { get; set; }
        public string TemPermanent { get; set; }
        public string Place { get; set; }
        public string JoiningDate { get; set; }
        public string PermanentDate { get; set; }
        public string SponsorId { get; set; }
        public string SponsorName { get; set; }
        public string ParentLoginId { get; set; }
        public string ParentName { get; set; }
        public string Fk_MemId { get; set; }
        public string MobileNo { get; set; }
        public string EncryptedLoginId { get; set; }

        public int IsExport { get; set; }
        
        public List<UserDownlineReport> ObjList { get; set; }
    }
    public class UserDownlineList
    {

       
        public List<UserDownlineReport> DownlineList(UserDownlineReport model)
        {
            List<UserDownlineReport> lst = new List<UserDownlineReport>();
            try
            {
                SqlParameter[] param =
                {
            new SqlParameter("@LoginId",model.LoginId),
            new SqlParameter("@Place",string.IsNullOrEmpty(model.Place)?null:model.Place),
            new SqlParameter("@Status",string.IsNullOrEmpty(model.TemPermanent)?null:model.TemPermanent),
            new SqlParameter("@Page",model.Page),
            new SqlParameter("@Size",model.Size),
            new SqlParameter("@IsExport",model.IsExport),

                };
                DataSet ds = Common.DBHelper.ExecuteQuery("UserDownlineReport", param);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        lst.Add(new UserDownlineReport
                        {
                            LoginId = dr["LoginId"].ToString(),
                            JoiningDate = dr["JoiningDate"].ToString(),
                            Name = dr["Name"].ToString(),
                            ParentLoginId = dr["ParentLoginId"].ToString(),
                            ParentName = dr["ParentName"].ToString(),
                            PermanentDate = dr["PermanentDate"].ToString(),
                            Place = dr["Place"].ToString(),
                            SponsorId = dr["SponsorId"].ToString(),
                            SponsorName = dr["SponsorName"].ToString(),
                            TemPermanent = dr["TemPermanent"].ToString(),
                            Fk_MemId = dr["Fk_MemId"].ToString(),
                            TotalRecords = int.Parse(dr["TotalRecords"].ToString()),
                            MobileNo = dr["MobileNo"].ToString(),


                        }); 

                    }

                }
            }
            catch (Exception ex)
            {


            }
            return lst;

        }
        public List<UserDownlineReport> AdminDirectList(UserDownlineReport model)
        {
            List<UserDownlineReport> lst = new List<UserDownlineReport>();
            try
            {
                SqlParameter[] param =
                {
            new SqlParameter("@LoginId",model.LoginId),
            new SqlParameter("@Place",string.IsNullOrEmpty(model.Place)?null:model.Place),
            new SqlParameter("@Status",string.IsNullOrEmpty(model.TemPermanent)?null:model.TemPermanent),
            new SqlParameter("@Page",model.Page),
            new SqlParameter("@Size",model.Size),
            new SqlParameter("@IsExport",model.IsExport),

                };
                DataSet ds = Common.DBHelper.ExecuteQuery("UserDirectReport", param);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        lst.Add(new UserDownlineReport
                        {
                            LoginId = dr["LoginId"].ToString(),
                            JoiningDate = dr["JoiningDate"].ToString(),
                            Name = dr["Name"].ToString(),
                            ParentLoginId = dr["ParentLoginId"].ToString(),
                            ParentName = dr["ParentName"].ToString(),
                            PermanentDate = dr["PermanentDate"].ToString(),
                            Place = dr["Place"].ToString(),
                            SponsorId = dr["SponsorId"].ToString(),
                            SponsorName = dr["SponsorName"].ToString(),
                            TemPermanent = dr["TemPermanent"].ToString(),
                            Fk_MemId = dr["Fk_MemId"].ToString(),
                            TotalRecords = int.Parse(dr["TotalRecords"].ToString())


                        }); ;

                    }

                }
            }
            catch (Exception ex)
            {


            }
            return lst;

        }


    }
}