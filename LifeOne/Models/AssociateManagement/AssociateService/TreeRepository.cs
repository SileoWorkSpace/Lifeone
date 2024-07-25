using LifeOne.Models.API.DAL;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateService
{
    public class TreeRepository
    {
        public List<TreeModel> TreeList(TreeModel model)
        {
            List<TreeModel> Tree = new List<TreeModel>();
            try
            {
                SqlParameter[] Param =
                {
                    new SqlParameter("@LoginId",model.LoginId),
                    new SqlParameter("@SearchLoginId",model.SearchLoginId)
                };
                DataSet ds = DBHelper.ExecuteQuery("TreeViewCommonAPI", Param);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {

                        TreeModel _objTreeModel = new TreeModel();
                        _objTreeModel.Status = dr["Status"].ToString();
                        _objTreeModel.AllLeg1 = Convert.ToInt32(dr["AllLeg1"].ToString());
                        _objTreeModel.AllLeg2 = Convert.ToInt32(dr["AllLeg2"].ToString());
                        _objTreeModel.JoiningDate = dr["JoiningDate"].ToString();
                        _objTreeModel.LeftBP = Convert.ToDecimal(dr["LeftBP"].ToString());
                        _objTreeModel.Leg = dr["Leg"].ToString();
                        _objTreeModel.Level = Convert.ToInt32(dr["MemberLevel"].ToString());
                        _objTreeModel.LeftNoneActive = Convert.ToInt32(dr["LeftNonActive"].ToString());
                        _objTreeModel.RightNoneActive = Convert.ToInt32(dr["RightNonActive"].ToString());
                        _objTreeModel.ActiveInActiveIcon = dr["IDStatus"].ToString();
                        _objTreeModel.LoginId = dr["LoginId"].ToString();
                        _objTreeModel.MemberName = dr["DisplayName"].ToString();
                        _objTreeModel.MemberStatus = dr["TempPermanent"].ToString();
                        _objTreeModel.MemId = Convert.ToInt32(dr["MemId"].ToString());
                        _objTreeModel.PaidLeftBP = Convert.ToDecimal(dr["PaidLeftBP"].ToString());
                        _objTreeModel.ParentId = dr["ParentId"].ToString();
                        _objTreeModel.PaidRightBP = Convert.ToDecimal(dr["PaidRightBP"].ToString());
                        _objTreeModel.ParentLoginId = dr["ParentLoginId"].ToString();
                        _objTreeModel.PermanentLeg1 = Convert.ToInt32(dr["PermanentLeg1"].ToString());
                        _objTreeModel.PermanentLeg2 = Convert.ToInt32(dr["PermanentLeg2"].ToString());
                        _objTreeModel.ProfilePic = dr["profilepic"].ToString();
                        _objTreeModel.RightBP = Convert.ToDecimal(dr["RightBP"].ToString());
                        _objTreeModel.SponsorId = dr["SponsorId"].ToString();
                        _objTreeModel.SponsorLoginId = dr["SponsorLoginId"].ToString();
                        _objTreeModel.TotalBusiness = Convert.ToDecimal(dr["TotalBusiness"].ToString());
                        _objTreeModel.TotalBV = Convert.ToDecimal(dr["TotalBv"].ToString());
                        _objTreeModel.UpGradeDate = dr["UpgradeDate"].ToString();
                        _objTreeModel.TotalNonActive = Convert.ToInt32(dr["LeftNonActive"].ToString()) + Convert.ToInt32(dr["RightNonActive"].ToString());
                        _objTreeModel.TotalActive = Convert.ToInt32(dr["PermanentLeg1"].ToString()) + Convert.ToInt32(dr["PermanentLeg2"].ToString());
                        _objTreeModel.TotalActiveInActiveLeftRight = Convert.ToInt32(dr["AllLeg1"].ToString()) + Convert.ToInt32(dr["AllLeg2"].ToString());
                        _objTreeModel.PackageName = dr["PackageName"].ToString();
                        _objTreeModel.PerformanceLevel = dr["PerformanceLevel"].ToString();
                        Tree.Add(_objTreeModel);
                    }

                }
            }
            catch (Exception ex)
            {


            }
            return Tree;


        }

        public List<TreeModel> TreeMembersChild(TreeModel model)
        {
            List<TreeModel> Tree = new List<TreeModel>();
            try
            {
                SqlParameter[] Param =
                {
                    new SqlParameter("@headID",model.LoginId)
                };
                DataSet ds = DBHelper.ExecuteQuery("GetTreeMembersApi", Param);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Tree.Add(new TreeModel
                        {
                            Status = dr["Status"].ToString(),
                            AllLeg1 = Convert.ToInt32(dr["AllLeg1"].ToString()),
                            AllLeg2 = Convert.ToInt32(dr["AllLeg2"].ToString()),
                            TotalActive = Convert.ToInt32(dr["PermanentLeg1"].ToString()) + Convert.ToInt32(dr["PermanentLeg2"].ToString()),
                            JoiningDate = dr["JoiningDate"].ToString(),
                            LeftBP = Convert.ToDecimal(dr["PCountLeg1"].ToString()),
                            Leg = dr["Leg"].ToString(),
                            //Level = Convert.ToInt32(dr["MemberLevel"].ToString()),
                            LeftNoneActive = Convert.ToInt32(dr["InactiveLeft"].ToString()),
                            RightNoneActive = Convert.ToInt32(dr["InactiveRight"].ToString()),
                            ActiveInActiveIcon = dr["cssStatus"].ToString(),
                            LoginId = dr["LoginId"].ToString(),
                            MemberName = dr["MemberName"].ToString(),
                            MemberStatus = dr["Status"].ToString(),
                            MemId = Convert.ToInt32(dr["MemberId"].ToString()),
                            PaidLeftBP = Convert.ToDecimal(dr["BalanceLeft"].ToString()),
                            ParentId = dr["ParentId"].ToString(),
                            PaidRightBP = Convert.ToDecimal(dr["BalanceRight"].ToString()),
                            ParentLoginId = dr["ParentId"].ToString(),
                            PermanentLeg1 = Convert.ToInt32(dr["PermanentLeg1"].ToString()),
                            PermanentLeg2 = Convert.ToInt32(dr["PermanentLeg2"].ToString()),
                            ProfilePic = dr["profilepic"].ToString(),
                            ActiveTotalLeft = Convert.ToInt32(dr["AllLeg1"].ToString()) + Convert.ToInt32(dr["InactiveLeft"].ToString()),
                            ActiveTotalRight = Convert.ToInt32(dr["AllLeg2"].ToString()) + Convert.ToInt32(dr["InactiveRight"].ToString()),
                            TotalActiveInActiveLeftRight = Convert.ToInt32(dr["AllLeg1"].ToString()) + Convert.ToInt32(dr["AllLeg2"].ToString()),
                            RightBP = Convert.ToDecimal(dr["PCountLeg2"].ToString()),
                            //SponsorId = dr["SponsorId"].ToString(),
                            SponsorLoginId = dr["SponsorLoginId"].ToString(),
                             TotalBusiness = Convert.ToDecimal(dr["TotalBusiness"].ToString()),
                            TotalBV = Convert.ToDecimal(dr["TotalBv"].ToString()),
                            // UpGradeDate = dr["UpgradeDate"].ToString(),
                            TotalNonActive = Convert.ToInt32(dr["InactiveLeft"].ToString()) + Convert.ToInt32(dr["InactiveRight"].ToString()),
                            Url = dr["href"].ToString(),
                            PackageName = dr["PackageName"].ToString(),
                            PerformanceLevel = dr["PerformanceLevel"].ToString()


                        });
                    }

                }
            }
            catch (Exception ex)
            {


            }
            return Tree;


        }
        public TreeModel Upline(TreeModel model)
        {
            TreeModel obj = new TreeModel();
            try
            {
                SqlParameter[] Param =
               {
                    new SqlParameter("@LoginId",model.LoginId)
                };
                DataSet ds = DBHelper.ExecuteQuery("Proc_GetUplineParentId", Param);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {

                    obj.ParentId = ds.Tables[0].Rows[0]["Fk_ParentId"].ToString();
                    obj.MemId = Convert.ToInt32(ds.Tables[0].Rows[0]["FK_MemId"].ToString());
                    obj.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                    obj.ParentLoginId = ds.Tables[0].Rows[0]["ParentLoginId"].ToString();

                }
            }
            catch (Exception ex)
            {


            }
            return obj;
        }
    }
}