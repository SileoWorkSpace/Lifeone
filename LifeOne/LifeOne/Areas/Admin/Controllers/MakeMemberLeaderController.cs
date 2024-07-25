using LifeOne.Models.AdminManagement.AEntity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LifeOne.Models.Manager;
using LifeOne.Models.Common;
using LifeOne.Models.AdminManagement.AService;

namespace LifeOne.Areas.Admin.Controllers
{
    public class MakeMemberLeaderController : Controller
    {
        logic obj = new logic();
        public ActionResult ViewAndMakePayment(MFranchiseLeaders leaders)
        {
            leaders.FranchiseLeaderList = MemberLeaderService.MemberLeaderList(leaders);
            if (leaders.FranchiseLeaderList.Count > 0)
                leaders.Pager = new Pager(leaders.FranchiseLeaderList[0].TotalRecord, leaders.Page == null ? 1 : leaders.Page, SessionManager.Size);
            else
                leaders.FranchiseLeaderList = new List<MFranchiseLeaders>();
            return View(leaders);
        }

        [HttpGet]
        public JsonResult getbyid(string id)
        {
            var res = obj.getrecordbyid(id);

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult add(MFranchiseLeaders sh)
        {
            return Json(obj.addrecord(sh), JsonRequestBehavior.AllowGet);
        }
        public JsonResult delete(long id)
        {
            return Json(obj.deletebyid(id), JsonRequestBehavior.AllowGet);
        }

        /*Add tetsimonial Users*/
        public ActionResult ViewandAddTestimonialUsers(int? page)
        {
            try
            {
                int pagesize = SessionManager.Size, pageindex = 1;
                pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
                IPagedList<MFranchiseLeaders> li = obj.ShowtetsimonialMembers().ToPagedList(pageindex, pagesize);
                return View(li);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult addTetsimonialMembers(MFranchiseLeaders sh)
        {
            return Json(obj.addrecordForTetsimonial(sh), JsonRequestBehavior.AllowGet);
        }
        public JsonResult deletetetsimonial(long id)
        {
            return Json(obj.deletebyidTettimonial(id), JsonRequestBehavior.AllowGet);
        }
    }

    public class logic
    {
        private string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public List<MFranchiseLeaders> ShowtetsimonialMembers()
        {
            List<MFranchiseLeaders> li = new List<MFranchiseLeaders>();
            SqlConnection con = new SqlConnection(cs);
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.GetTestimonialMembers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    MFranchiseLeaders pro = new MFranchiseLeaders();
                    pro.LoginId = rdr["LoginId"].ToString();
                    pro.Fk_MemId = Convert.ToInt32(rdr["Fk_MemId"]);
                    pro.FirstName = rdr["FirstName"].ToString();
                    pro.LastName = rdr["LastName"].ToString();
                    pro.Phone = rdr["Phone"].ToString();
                    pro.Email = rdr["Email"].ToString();
                    li.Add(pro);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                con.Close();
            }

            return li;
        }

        public int deletebyid(long id)
        {
            int i;
            SqlConnection con = new SqlConnection(cs);
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.DeleteFranchiseLeaders", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Fk_MemId", id);
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                con.Close();
            }

            return i;
        }


        public int deletebyidTettimonial(long id)
        {
            int i;
            SqlConnection con = new SqlConnection(cs);
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.DeleteTetsimonial", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Fk_MemId", id);
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                con.Close();
            }

            return i;
        }

        public List<MFranchiseLeaders> getrecordbyid(string id)
        {

            List<MFranchiseLeaders> li = new List<MFranchiseLeaders>();
            SqlConnection con = new SqlConnection(cs);

            try
            {
                SqlCommand cmd = new SqlCommand("dbo.GetLeaderBYId", con);
                cmd.Parameters.AddWithValue("@LoginId", id);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    MFranchiseLeaders pro = new MFranchiseLeaders();
                    if (Convert.ToInt32(rdr["STATUS"]) == 1)
                    {

                        pro.Status = Convert.ToInt32(rdr["STATUS"]);
                        pro.Fk_MemId = Convert.ToInt32(rdr["Fk_MemId"]);
                        pro.FirstName = rdr["FirstName"].ToString();
                        pro.LastName = rdr["LastName"].ToString();
                        pro.Phone = rdr["Phone"].ToString();
                        pro.Email = rdr["Email"].ToString();

                    }
                    else
                    {
                        pro.Status = Convert.ToInt32(rdr["STATUS"]);
                        pro.Message = rdr["remark"].ToString();
                    }
                    li.Add(pro);
                }
            }

            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                con.Close();
            }

            return li;
        }

        public int addrecord(MFranchiseLeaders sw)
        {
            int i;
            SqlConnection con = new SqlConnection(cs);
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.AddFranchiseLeaders", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Fk_MemId", sw.Fk_MemId);
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw;
            }

            finally
            {
                con.Close();
            }

            return i;
        }


        public int addrecordForTetsimonial(MFranchiseLeaders sw)
        {
            int i;
            SqlConnection con = new SqlConnection(cs);
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.AddTestimonialMembers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Fk_MemId", sw.Fk_MemId);
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw;
            }

            finally
            {
                con.Close();
            }

            return i;
        }

    }

}