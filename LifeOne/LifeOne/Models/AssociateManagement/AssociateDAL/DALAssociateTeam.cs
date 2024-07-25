using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.API;
using LifeOne.Models.API.DAL;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateDAL
{
    public class DALAssociateTeam
    {
        public static List<DirectAPI> GetDirect(int? Fk_MemId,string SearchLoginId, int? Page, int PageSize)
        {
            try
            {

                string _SearchId = string.IsNullOrEmpty(SearchLoginId) ? "" : SearchLoginId;
                string _qury = "Proc_GetDirectAPI @Fk_memId=" + Fk_MemId + ",@SearchLoginId='" + _SearchId + "',@Page=" + Page + ",@PageSize=" + PageSize + "";
                List<DirectAPI> _iresult = DBHelperDapper.DAGetDetailsInList<DirectAPI>(_qury).ToList();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<DirectAPI> GetDirectList(int? Fk_MemId, string SearchLoginId)
        {
            List<DirectAPI> objlist = new List<DirectAPI>();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Fk_memId",Fk_MemId),
                    new SqlParameter("@SearchLoginId",string.IsNullOrEmpty(SearchLoginId)?null:SearchLoginId)

                };
                DataSet ds = DBHelper.ExecuteQuery("AssociateDirectTeam", param);

                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        objlist.Add(new DirectAPI
                        {
                            DateOfActivation = dr["DateOfActivation"].ToString(),
                            DateOfjoining = dr["DateOfjoining"].ToString(),
                            MemberLoginId = dr["MemberLoginId"].ToString(),
                            MemberName = dr["MemberName"].ToString(),
                            ParentLoginId = dr["ParentLoginId"].ToString(),
                            SelfBusiness = Convert.ToDecimal(dr["SelfBusiness"].ToString()),
                            SponsorLoginId = dr["SponsorLoginId"].ToString(),
                            Place = dr["Place"].ToString(),
                            TeamBusiness = Convert.ToDecimal(dr["TeamBusiness"].ToString()),
                            FK_MemId = Convert.ToInt64(dr["FK_MemId"].ToString()),
                            ParentName = dr["ParentName"].ToString(),
                            ActiveInactiveStatus = dr["ActiveInactiveStatus"].ToString(),
                            ActivationDate = dr["ActivationDate"].ToString()


                        });


                    }

                }
                return objlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<DownlineAPI> GetDownline(int? Fk_MemId, string SearchLoginId, int? Page,int PageSize)
        {
            try
            {
                string _SearchId = string.IsNullOrEmpty(SearchLoginId) ? "" : SearchLoginId;
                string _qury = "GetDownlineApI @FK_MemId=" + Fk_MemId + ",@Search='" + _SearchId + "',@Page=" + Page + ",@PageSize=" + PageSize + "";
                List<DownlineAPI> _iresult = DBHelperDapper.DAGetDetailsInList<DownlineAPI>(_qury).ToList();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<DownlineAPI> GetDownlineList(int? Fk_MemId, string SearchLoginId, int? Page, int PageSize,string Leg)
        {
            List<DownlineAPI> objlist = new List<DownlineAPI>();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Fk_memId",Fk_MemId),
                    new SqlParameter("@Search",string.IsNullOrEmpty(SearchLoginId)?null:SearchLoginId),
                    new SqlParameter("@Leg",string.IsNullOrEmpty(Leg)?null:Leg)

                };
                DataSet ds = DBHelper.ExecuteQuery("AssociateDownlineTeam", param);

                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        objlist.Add(new DownlineAPI
                        {
                            DateOfActivation = dr["DateOfActivation"].ToString(),
                            DateOfjoining = dr["DateOfjoining"].ToString(),
                            MemberLoginId = dr["MemberLoginId"].ToString(),
                            MemberName = dr["MemberName"].ToString(),
                            ParentLoginId = dr["ParentLoginId"].ToString(),
                            SelfBusiness = Convert.ToDecimal(dr["SelfBusiness"].ToString()),
                            SponsorLoginId = dr["SponsorLoginId"].ToString(),
                            Place = dr["Place"].ToString(),
                            TeamBusiness = Convert.ToDecimal(dr["TeamBusiness"].ToString()),
                            FK_MemId = Convert.ToInt64(dr["FK_MemId"].ToString()),
                            TotalRecords= Convert.ToInt32(dr["TotalCount"].ToString()),
                            ParentName = dr["ParentName"].ToString(),
                            SponsorName = dr["SponsorName"].ToString(),
                            Status=dr["ActiveStatus"].ToString()
                            

                        });


                    }

                }
                return objlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<MemberKYC> GetMemberKYCListDownline(MemberKYC obj)
        {
            var _qury = @"GetMemberKYCListDownlineAPI @FK_MemId=" + obj.Fk_MemId + ",@LoginId=" + obj.LoginId + "";
            List<MemberKYC> _iresult = DBHelperDapper.DAGetDetailsInList<MemberKYC>(_qury).ToList();
            return _iresult;
        }

        public static List<Member> GetMemberById(string LoginId)
        {
            try
            {
                string _qury = "Proc_GetMemberById @LoginId=" + LoginId + "";
                List<Member> _iresult = DBHelperDapper.DAGetDetailsInList<Member>(_qury).ToList();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<FranchiseList> GetFranchiseDetail(FranchiseList obj)
        {
            try
            {
                string _qury = "Proc_GetTeamFranchiseReaquest @ParentId='" + obj.LoginId + "',@Page=" + obj.Page + ",@Size=" + obj.Size + ",@Type='" + obj.Type + "'";
                List<FranchiseList> _iresult = DBHelperDapper.DAGetDetailsInList<FranchiseList>(_qury).ToList();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<FranchiseList> GetFranchiseDetailV2(FranchiseList obj)
        {
            try
            {
                string _qury = "Proc_GetTeamFranchiseReaquestDetailsV2 @ParentId='" + obj.LoginId + "',@Page=" + obj.Page + ",@Size=" + obj.Size + ",@Type='" + obj.Type + "',@IsApproved='" + obj.IsApproved + "'";
                List<FranchiseList> _iresult = DBHelperDapper.DAGetDetailsInList<FranchiseList>(_qury).ToList();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static List<DistributorDetails> DistributorDetails(DistributorDetails obj)
        {
            List<DistributorDetails> _iresult = new List<DistributorDetails>();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Fk_MemId",obj.Fk_memId),
                    new SqlParameter("@Status",string.IsNullOrEmpty(obj.Status)?null:obj.Status)


                };
                DataSet ds = DBHelper.ExecuteQuery("AssociateDetails", param);
                if(ds !=null && ds.Tables[0] !=null && ds.Tables[0].Rows.Count>0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        _iresult.Add(new API.DistributorDetails
                        {

                            LoginId=dr["LoginId"].ToString(),
                            Address=dr["Address1"].ToString(),
                            Status=dr["Status"].ToString(),
                            UserName=dr["FirstName"].ToString()

                        });

                    }

                }


                //string _qury = "AssociateDetails @Fk_MemId='" + obj.Fk_memId + "',@Status=" + obj.Status + "";
                //List<DistributorDetails> _iresult = DBHelperDapper.DAGetDetailsInList<DistributorDetails>(_qury).ToList();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        
    }
}