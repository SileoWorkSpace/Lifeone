using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALAdminDashboard
    {
        private static string connectionString = string.Empty;
        public static string connection()
        {
            try
            {
                return connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            }
            catch (Exception)
            {
                //todo error handling  mechanism
                throw;
            }
        }
        public static List<MAdminDashbord> GetDashboardList(MAdminDashbord obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                List<MAdminDashbord> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminDashbord>("AdminDashboard", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<MAdminDashbord> GetRecentList(MAdminDashbord obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                List<MAdminDashbord> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminDashbord>("GetRecentJoiners", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<AdminDashboardChart> GetMemberList(MAdminDashbord obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                List<AdminDashboardChart> _iresult = DBHelperDapper.DAAddAndReturnModelList<AdminDashboardChart>("GetRecentJoiners", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<MemberChart> GetDateWiseMemberList(MAdminDashbord obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                List<MemberChart> _iresult = DBHelperDapper.DAAddAndReturnModelList<MemberChart>("AdminMemberChart", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static FranchiseDashboardViewModel FranchiseDahsboard()
        {
            FranchiseDashboardViewModel model = new FranchiseDashboardViewModel();
            try
            {
                using (SqlConnection objConnection = new SqlConnection(connection()))
                {
                    try
                    {
                        AssociateDashboardReq objcls = new AssociateDashboardReq();
                        objcls.Fk_MemId = SessionManager.FranchiseFk_MemId;
                        var reader = objConnection.QueryMultiple("dbo.Proc_FranchiseDahsboard", objcls, commandType: CommandType.StoredProcedure);
                        model.FranchiseAmounts = reader.Read<FranchiseAmountViewModel>().FirstOrDefault();
                        model.FranchisePaidList = reader.Read<FranchisePaidViewModel>().ToList();
                        model.FranchiseOrders = reader.Read<FranchiseOrderViewModel>().FirstOrDefault();
                        return model;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}